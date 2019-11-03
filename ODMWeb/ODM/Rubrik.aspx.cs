using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace ODM
{
    public partial class OdmRubrik : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Master.Yetki().Contains("Admin"))
                    Response.Redirect("Giris.aspx");

                for (int i = 1; i <= 30; i++)
                {
                    ddlSoruNo.Items.Add(new ListItem(i + ". Soru", i.ToString()));
                }
                ddlSoruNo.Items.Insert(0, new ListItem("Soru No Seçiniz", ""));

                SinavlarDb veriDb = new SinavlarDb();
                ddlSinavId.DataSource = ddlSinavId2.DataSource = veriDb.KayitlariGetir();
                ddlSinavId.DataValueField = ddlSinavId2.DataValueField = "Id";
                ddlSinavId.DataTextField = ddlSinavId2.DataTextField = "SinavAdi";
                ddlSinavId.DataBind();
                ddlSinavId2.DataBind();
                ddlSinavId.Items.Insert(0, new ListItem("Sınav Seçiniz", ""));
                ddlSinavId2.Items.Insert(0, new ListItem("Sınav Seçiniz", ""));



                BranslarDb brnsDb = new BranslarDb();
                ddlBrans.DataSource = brnsDb.KayitlariGetir();
                ddlBrans.DataValueField = "Id";
                ddlBrans.DataTextField = "BransAdi";
                ddlBrans.DataBind();
                ddlBrans.Items.Insert(0, new ListItem("Ders Seçiniz", ""));
            }
        }

        protected void rptKayitlar_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = e.CommandArgument.ToInt32();
            hfId.Value = id.ToString();
            RubrikDb veriDb = new RubrikDb();
            if (e.CommandName.Equals("Sil"))
            {
                veriDb.KayitSil(id);
                Master.UyariIslemTamam("Sayfa başarıyla silindi.", phUyari);
                KayitlariListele();
            }
            if (e.CommandName.Equals("Kazanim"))
            {
                hfSoruId.Value = id.ToString();

                RubrikInfo info = veriDb.KayitBilgiGetir(id);

                KOgrenmeAlanlariDB uoaDb = new KOgrenmeAlanlariDB();
                ddlOgrenmeAlani.DataSource = uoaDb.KayitlariGetir(0, info.BransId, info.Sinif);
                ddlOgrenmeAlani.DataValueField = "AlanNo";
                ddlOgrenmeAlani.DataTextField = "OgrenmeAlani";
                ddlOgrenmeAlani.DataBind();
                ddlOgrenmeAlani.Items.Insert(0, new ListItem("Seçiniz", ""));

                lbSecilenler.Items.Clear();
                string[] kategoriler = info.Kazanimlar.Split('|');

                KazanimlarDB kznmDb = new KazanimlarDB();

                for (int i = 0; i < kategoriler.Length; i++)
                {
                    if (kategoriler[i].IsInteger())
                    {
                        KazanimlarInfo kznInfo = kznmDb.KayitBilgiGetir(kategoriler[i].ToInt32());
                        lbSecilenler.Items.Add(new ListItem(kznInfo.Kazanim, kznInfo.Id.ToString()));
                    }
                }

                TablariKapat();
                Kazanim.Visible = tabliKazanim.Visible = true;
                tabliKazanim.Attributes.Add("class", "active");
                Kazanim.Attributes.Add("class", "tab-pane active");
            }
            if (e.CommandName.Equals("Duzenle"))
            {
                RubrikInfo info = veriDb.KayitBilgiGetir(id);
                ddlSinavId2.SelectedValue = info.SinavId.ToString();
                ddlSoruNo.SelectedValue = info.SoruNo.ToString();
                txtKismiPuan.Text = info.KismiPuan.ToString();
                txtTamPuan.Text = info.Tampuan.ToString();
                txtDogruCevap.Text = info.DogruCevap;
                txtYanlisCevap.Text = info.YanlisCevap;
                txtKismiCevap.Text = info.KismiCevap;
                txtSoru.Text = info.Soru;

                TablariKapat();
                tabliKayit.Attributes.Add("class", "active");
                Kayit.Attributes.Add("class", "tab-pane active");

                btnKaydet.Text = "Değişiklikleri Kaydet";
            }
        }

        protected void btnVazgec_OnClick(object sender, EventArgs e)
        {
            FormuTemizle();
            TablariKapat();
            tabliSayfalar.Attributes.Add("class", "active");
            Sayfalar.Attributes.Add("class", "tab-pane active");
        }

        private void FormuTemizle()
        {
            txtDogruCevap.Text = "";
            txtYanlisCevap.Text = "";
            txtKismiCevap.Text = "";
            txtKismiPuan.Text = "";
            txtTamPuan.Text = "";
            ddlSoruNo.SelectedValue = "";
            txtSoru.Text = "";
            hfId.Value = "0";
            btnKaydet.Text = "Kaydet";
        }

        protected void btnKaydet_OnClick(object sender, EventArgs e)
        {
            int id = hfId.Value.ToInt32();
            int tamPuan;
            int kismiPuan;
            int sinavId = ddlSinavId2.SelectedValue.ToInt32();
            int soruNo = ddlSoruNo.SelectedValue.ToInt32();
            int bransId = ddlBrans.SelectedValue.ToInt32();
            int sinif = ddlSinif.SelectedValue.ToInt32();

            if (txtKismiPuan.Text.IsInteger())
                kismiPuan = txtKismiPuan.Text.ToInt32();
            else
            {
                Master.UyariKirmizi("Kısmi Puan alanına sayısal değer giriniz. Puanlamada kısmi puan yoksa 0 yazınız.", phUyari);

                TablariKapat();
                tabliKayit.Attributes.Add("class", "active");
                Kayit.Attributes.Add("class", "tab-pane active");
                return;
            }
            if (txtTamPuan.Text.IsInteger())
                tamPuan = txtTamPuan.Text.ToInt32();
            else
            {
                Master.UyariKirmizi("Tam Puan alanına sayısal değer giriniz.", phUyari);

                TablariKapat();
                tabliKayit.Attributes.Add("class", "active");
                Kayit.Attributes.Add("class", "tab-pane active");
                return;
            }
            string dogruCevap = txtDogruCevap.Text;
            string yanlisCevap = txtYanlisCevap.Text;
            string kismiCevap = txtKismiCevap.Text;
            string soru = txtSoru.Text;



            RubrikDb rDb = new RubrikDb();
            if (rDb.KayitKontrol(sinavId, sinif, bransId, soruNo) && id == 0)
            {
                Master.UyariKirmizi("Bu soru için daha önce rubrik yazılmış.", phUyari);

                TablariKapat();
                tabliKayit.Attributes.Add("class", "active");
                Kayit.Attributes.Add("class", "tab-pane active");
                return;
            }
            RubrikInfo info = new RubrikInfo
            {
                SinavId = sinavId,
                SoruNo = soruNo,
                DogruCevap = dogruCevap,
                YanlisCevap = yanlisCevap,
                KismiCevap = kismiCevap,
                KismiPuan = kismiPuan,
                Tampuan = tamPuan,
                BransId = bransId,
                Soru = soru,
                Sinif = sinif
            };
            if (id == 0)
            {
                rDb.KayitEkle(info);
                Master.UyariIslemTamam("Rubrik başarıyla kaydedildi.", phUyari);
            }
            else
            {
                info.Id = id;
                rDb.KayitGuncelle(info);
                Master.UyariIslemTamam("Değişiklikler kaydedildi.", phUyari);
            }
            KayitlariListele();
            FormuTemizle();


            TablariKapat();
            tabliSayfalar.Attributes.Add("class", "active");
            Sayfalar.Attributes.Add("class", "tab-pane active");
        }

        protected void btnDosyaYukle_OnClick(object sender, EventArgs e)
        {
            if (fuDosya.HasFile)
            {
                string dosyaAdi = Server.HtmlEncode(fuDosya.FileName);
                string uzanti = Path.GetExtension(dosyaAdi);
                if (uzanti != null)
                {
                    uzanti = uzanti.ToLower();
                    string rastgeleMetin = GenelIslemler.RastgeleMetinUret(8);
                    if (GenelIslemler.YuklenecekResimler.Contains(uzanti))
                    {
                        dosyaAdi = string.Format("{0}{1}", rastgeleMetin, uzanti);
                        string dosyaYolu = string.Format(@"{0}upload\{1}", HttpContext.Current.Server.MapPath("/"), dosyaAdi);
                        File.WriteAllBytes(dosyaYolu, fuDosya.FileBytes);

                        string dosyaYolu2 = string.Format(@"/upload/{0}", dosyaAdi);


                        imgDosyaResim.Visible = true;
                        imgDosyaResim.ImageUrl = dosyaYolu2;
                        Master.UyariBilgilendirme("Fotoğraf başarıyla yüklendi. Fotoğrafı editöre sürükle-bırak yöntemiyle kullanabilirsiniz.", phUyari);


                        TablariKapat();
                        tabliKayit.Attributes.Add("class", "active");
                        Kayit.Attributes.Add("class", "tab-pane active");
                    }
                    else
                    {
                        Master.UyariKirmizi("Yalnızca " + GenelIslemler.YuklenecekDosyalar + " uzantılı dosyalar yüklenir.", phUyari);
                    }
                }
            }
        }
        private void TablariKapat()
        {
            tabliSayfalar.Attributes.Add("class", "");
            Sayfalar.Attributes.Add("class", "tab-pane");
            tabliKayit.Attributes.Add("class", "");
            Kayit.Attributes.Add("class", "tab-pane");
        }

        protected void ddlSinavId_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            KayitlariListele();
        }

        private void KayitlariListele()
        {
            int sinavId = ddlSinavId.SelectedValue.ToInt32();
            string kitapcikTuru = ddlKitapcikTuru.SelectedValue;
            RubrikDb rDb = new RubrikDb();
            rptKayitlar.DataSource = rDb.KayitlariGetir(sinavId,kitapcikTuru);
            rptKayitlar.DataBind();
        }
        protected void ddlOgrenmeAlani_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            int id = hfSoruId.Value.ToInt32();

            RubrikDb veriDb = new RubrikDb();
            RubrikInfo info = veriDb.KayitBilgiGetir(id);
            int ogrenmeAlani = ddlOgrenmeAlani.SelectedValue.ToInt32();

            AltOgrenmealanlariniGetir(ogrenmeAlani, info.BransId, info.Sinif);
            lbKategoriler.Items.Clear();
        }
        private void AltOgrenmealanlariniGetir(int ogrenmeAlani, int brans, int sinif)
        {
            KOgrenmeAlanlariDB uoaDb = new KOgrenmeAlanlariDB();
            ddlAltOgrenmeAlani.DataSource = uoaDb.KayitlariGetir(ogrenmeAlani, brans, sinif);
            ddlAltOgrenmeAlani.DataValueField = "AlanNo";
            ddlAltOgrenmeAlani.DataTextField = "OgrenmeAlani";
            ddlAltOgrenmeAlani.DataBind();
            ddlAltOgrenmeAlani.Items.Insert(0, new ListItem("Seçiniz", ""));
            ddlAltOgrenmeAlani.Items.Insert(1, new ListItem("Genel", "0"));
        }

        protected void blnSecileniEkle_OnClick(object sender, EventArgs e)
        {
            List<ListItem> secililer = lbSecilenler.Items.Cast<ListItem>().ToList();

            List<ListItem> itemEkle = lbKategoriler.Items.Cast<ListItem>().Where(listItem => listItem.Selected).ToList();

            foreach (ListItem listItem in itemEkle)
            {
                if (!secililer.Contains(new ListItem(listItem.Text, listItem.Value)))
                    lbSecilenler.Items.Add(new ListItem(listItem.Text, listItem.Value));
            }

            TablariKapat();
            tabliKazanim.Attributes.Add("class", "active");
            Kazanim.Attributes.Add("class", "tab-pane active");
        }

        protected void blnSecileniCikar_OnClick(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = lbSecilenler.Items.Cast<ListItem>().Where(listItem => listItem.Selected).ToList();

            foreach (ListItem listItem in itemsToRemove)
            {
                lbSecilenler.Items.Remove(listItem);
            }

            TablariKapat();
            tabliKazanim.Attributes.Add("class", "active");
            Kazanim.Attributes.Add("class", "tab-pane active");
        }

        protected void ddlAltOgrenmeAlani_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            KazanimlariGetir();
        }
        private void KazanimlariGetir()
        {

            int id = hfSoruId.Value.ToInt32();

            RubrikDb veriDb = new RubrikDb();
            RubrikInfo info = veriDb.KayitBilgiGetir(id);

            int sinif = info.Sinif;
            int brans = info.BransId;
            int ogrenmeAlani = ddlOgrenmeAlani.SelectedValue.ToInt32();
            int altOgrenmeAlani = ddlAltOgrenmeAlani.SelectedValue.ToInt32();
            KazanimlarDB kznDb = new KazanimlarDB();

            lbKategoriler.DataSource = kznDb.KayitlariGetir(brans, sinif, ogrenmeAlani, altOgrenmeAlani);
            lbKategoriler.DataValueField = "Id";
            lbKategoriler.DataTextField = "Kazanim";
            lbKategoriler.DataBind();
        }

        protected void btnKazanimKaydet_OnClick(object sender, EventArgs e)
        {
            int id = hfSoruId.Value.ToInt32();
            string kazanimlar = "|";
            for (int a = 0; a < lbSecilenler.Items.Count; a++)
                kazanimlar += lbSecilenler.Items[a].Value + "|";
            RubrikDb veriDb = new RubrikDb();
            veriDb.KayitGuncelle(id, kazanimlar);

            TablariKapat();
            tabliSayfalar.Attributes.Add("class", "active");
            Sayfalar.Attributes.Add("class", "tab-pane active");

            Kazanim.Visible = tabliKazanim.Visible = false;
            lbSecilenler.Items.Clear();

            ddlAltOgrenmeAlani.Items.Clear();
            lbKategoriler.Items.Clear();
            ddlAltOgrenmeAlani.Items.Insert(0, new ListItem("Seçiniz", ""));

            KayitlariListele();
        }

        protected void rptKayitlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                Literal ltrBrans = (Literal)e.Item.FindControl("ltrBrans");
                Literal ltrKazanim = (Literal)e.Item.FindControl("ltrKazanim");

                int bransId = DataBinder.Eval(e.Item.DataItem, "BransId").ToInt32();
                string kazanim = DataBinder.Eval(e.Item.DataItem, "Kazanimlar").ToString();

                BranslarDb brnsDb = new BranslarDb();
                BranslarInfo info = brnsDb.KayitBilgiGetir(bransId);
                ltrBrans.Text = info.BransAdi;

                KazanimlarDB kznDb = new KazanimlarDB();
                string[] konum = kazanim.Split('|');
                foreach (KazanimlarInfo kinfo in from t in konum where t.IsInteger() select kznDb.KayitBilgiGetir(t.ToInt32()))
                {
                    ltrKazanim.Text += string.Concat(info.BransAdi.Substring(0, 1), ".", kinfo.Sinif, ".", kinfo.OgrenmeAlani, ".", kinfo.AltOgrenmeAlani, ".", kinfo.KazanimNo, " - ");
                }
            }
        }

        protected void btnKazanimVazgec_OnClick(object sender, EventArgs e)
        {
            hfSoruId.Value = "0";
            Kazanim.Visible= tabliKazanim.Visible = false;
            TablariKapat();
            tabliSayfalar.Attributes.Add("class", "active");
            Sayfalar.Attributes.Add("class", "tab-pane active");
        }

        protected void ddlSoruTipi_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string soruTipi = ddlSoruTipi.SelectedValue;
            phAcikUclu.Visible = soruTipi=="au";


            TablariKapat();
            tabliKayit.Attributes.Add("class", "active");
            Kayit.Attributes.Add("class", "tab-pane active");
        }

        protected void ddlKitapcikTuru_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            KayitlariListele();
        }
    }
}
