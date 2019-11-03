using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace ODM
{
    public partial class OdmPuanlayiciIslemleri : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Master.Yetki().Contains("Admin"))
                    Response.Redirect("Giris.aspx");

                BranslarDb brnsDb = new BranslarDb();
                ddlBranslar.DataSource = brnsDb.KayitlariGetir();
                ddlBranslar.DataValueField = "Id";
                ddlBranslar.DataTextField = "BransAdi";
                ddlBranslar.DataBind();
                ddlBranslar.Items.Insert(0, new ListItem("Branş Seçiniz", ""));


                SinavlarDb snvDb = new SinavlarDb();
                ddlSinavlar.DataSource = snvDb.KayitlariGetir();
                ddlSinavlar.DataValueField = "Id";
                ddlSinavlar.DataTextField = "SinavAdi";
                ddlSinavlar.DataBind();
                ddlSinavlar.Items.Insert(0, new ListItem("Sınavı Seçiniz", ""));

            }
        }

        protected void btnListele_OnClick(object sender, EventArgs e)
        {
            int brans = ddlBranslar.SelectedValue.ToInt32();

            KullanicilarDb veriDb = new KullanicilarDb();
            rptKullanicilar.DataSource = veriDb.OgretmenleriGetir(brans);
            rptKullanicilar.DataBind();

        }

        protected void rptKullanicilar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                GenelIslemler.SiraNumarasiForRepeater(e, "lblSira");

                int sinavId = ddlSinavlar.SelectedValue.ToInt32();
                if (sinavId == 0 && e.Item.ItemIndex == 0)
                    Master.UyariKirmizi("Sınav seçmediniz.", phUyari);

                int ogretmenId = DataBinder.Eval(e.Item.DataItem, "Id").ToInt32();
                string grup = DataBinder.Eval(e.Item.DataItem, "Grup").ToString();
                string kurumKodu = DataBinder.Eval(e.Item.DataItem, "KurumKodu").ToString();
                int ilceId = DataBinder.Eval(e.Item.DataItem, "IlceId").ToInt32();
                int bransi = DataBinder.Eval(e.Item.DataItem, "Bransi").ToInt32();
                Literal ltrKurumAdi = (Literal)e.Item.FindControl("ltrKurumAdi");

                KurumlarDb kDb = new KurumlarDb();
                ltrKurumAdi.Text = kDb.KayitBilgiGetir(kurumKodu).KurumAdi;

                Literal ltrIlce = (Literal)e.Item.FindControl("ltrIlce");

                IlcelerDb iDb = new IlcelerDb();
                ltrIlce.Text = iDb.KayitBilgiGetir(ilceId).IlceAdi;

                CevaplarDb cvpDb = new CevaplarDb();
                Literal ltrOkunacakCevapSayisi = (Literal)e.Item.FindControl("ltrOkunacakCevapSayisi");
                ltrOkunacakCevapSayisi.Text = grup=="A" ? cvpDb.CevaplanacakCkSayisiA(sinavId, ogretmenId).ToString() : cvpDb.CevaplanacakCkSayisiB(sinavId, ogretmenId).ToString();


                BranslarDb brnsDb = new BranslarDb();
                Literal ltrBrans = (Literal)e.Item.FindControl("ltrBrans");
                ltrBrans.Text = brnsDb.KayitBilgiGetir(bransi).BransAdi;
            }
        }

        protected void btnGrupla_OnClick(object sender, EventArgs e)
        {
            // string rapor;
            int sinavId = ddlSinavlar.SelectedValue.ToInt32();
            SinavlarDb sDb = new SinavlarDb();

            if (sinavId == 0)
            {
                Master.UyariTuruncu("Öncelikle atama yapılacak sınavı seçiniz.", phUyari);
            }
            else if (sDb.AktifSinavAdi().SinavId!=sinavId)
            {
                Master.UyariTuruncu("Aktif olmayan bir sınava atama yapılamaz.",phUyari);
            }
            else
            {
                KullanicilarDb veriDb = new KullanicilarDb();
                CevaplarDb cDb = new CevaplarDb();

                List<CevaplarInfo> dersler = cDb.DersleriDiziyeGetir(sinavId); //sınavda sorulacak dersler.
                if (dersler.Count > 0)
                {
                    foreach (var drs in dersler)
                    {
                        //Öğretmenler A ve B grubuna ayır. 
                        List<KullanicilarInfo> ogretmenler = veriDb.KayitlariDiziyeGetir(drs.BransId, "Ogretmen|");
                        // rapor += string.Format("{0} nolu ders için {1} öğretmen var.<br>", drs.BransId, ogretmenler.Count);
                        int x = 0;
                        foreach (var grp in ogretmenler)
                        {
                            string hrf = x % 2 == 0 ? "A" : "B";
                            x++;
                            veriDb.KayitGrupGuncelle(grp.Id, hrf);
                        }

                        List<CevaplarInfo> cevaplar = cDb.KayitlariDiziyeGetir(sinavId, drs.BransId);

                        if (cevaplar.Count > 0)
                        {
                            int z = x = 0;
                            List<KullanicilarInfo> grupA = veriDb.KayitlariDiziyeGetir(drs.BransId, "Ogretmen|", "A");
                            List<KullanicilarInfo> grupB = veriDb.KayitlariDiziyeGetir(drs.BransId, "Ogretmen|", "B");

                            // rapor += string.Format("1. değerlendirme grubuna {0} öğretmen, 2. değerlendirme grubuna {1} öğretmen eklendi.<br>", grupA.Count, grupB.Count);
                            //A grubuNA (değerlendirici1) okunacak cevapları ata
                            if (grupA.Count > 0)
                            {
                                foreach (var info in cevaplar)
                                {
                                    if (z >= grupA.Count)
                                        z = 0;
                                    cDb.KayitGuncelle(info.Id, grupA[z].Id);
                                    z++;
                                }
                            }
                            else
                            {
                                Master.UyariKirmizi("A grubunda öğretmen bulunamadı", phUyari);
                            }

                            //B grubuNA (değerlendirici2) okunacak cevapları ata
                            if (grupB.Count > 0)
                            {
                                foreach (var info in cevaplar)
                                {
                                    if (x >= grupB.Count)
                                        x = 0;
                                    cDb.KayitGuncelle(info.Id, grupB[x].Id, 2); //ikinci grup
                                    x++;
                                }
                            }
                            else
                            {
                                Master.UyariKirmizi("B grubunda öğretmen bulunamadı", phUyari);
                            }
                        }
                        else
                        {
                            Master.UyariKirmizi("Okunacak cevap kağıdı bulunamadı.", phUyari);
                        }
                    }
                    Master.UyariIslemTamam("Okunacak cevap kağıtları, ilgili branş öğretmenlerine başarıyla atandı.", phUyari);
                }
                else
                {
                    Master.UyariKirmizi("Bu sınav için okunacak cevap kağıdı bulunamadı.", phUyari);
                }
            }
        }
    }
}