using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DAL;

namespace ODM
{
    public partial class OdmSinavEvrak : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Master.Yetki().Contains("Root") && !Master.Yetki().Contains("Admin") && !Master.Yetki().Contains("OkulYetkilisi"))
                    Response.Redirect("Giris.aspx");

                if (!Master.Yetki().Contains("Root") && !Master.Yetki().Contains("Admin"))
                {
                    tabliKayit.Visible = false;
                    tabliEvrakGelenler.Visible = false;
                }

                KurumlarDb veriDb = new KurumlarDb();
                ddlOkulTuru.DataSource = veriDb.KurumTurleri();
                ddlOkulTuru.DataValueField = "Tur";
                ddlOkulTuru.DataTextField = "Tur";
                ddlOkulTuru.DataBind();

                CkSinavAdiDB snvDb = new CkSinavAdiDB();
                ddlSinavId2.DataSource =  snvDb.KayitlariGetir();
                ddlSinavId2.DataValueField = "SinavId";
                ddlSinavId2.DataTextField =  "SinavAdi";
                ddlSinavId2.DataBind();
                ddlSinavId2.Items.Insert(0, new ListItem("Sınavı Seçiniz", ""));

                KayitlariListele();
            }
        }

        private void KayitlariListele()
        {
            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo info = kDb.KayitBilgiGetir(Master.UyeId());

            CkSinavEvrakDb veriDb = new CkSinavEvrakDb();
            rptSinavEvraklari.DataSource = Master.Yetki().Contains("OkulYetkilisi") ? veriDb.KayitlariGetir(info.KurumKodu) : veriDb.KayitlariGetir();
            rptSinavEvraklari.DataBind();
        }

        protected void blnSecileniEkle_OnClick(object sender, EventArgs e)
        {
            List<ListItem> secililer = lbSecilenler.Items.Cast<ListItem>().ToList();

            List<ListItem> itemEkle = lbKurumlar.Items.Cast<ListItem>().Where(listItem => listItem.Selected).ToList();

            foreach (ListItem listItem in itemEkle)
            {
                if (!secililer.Contains(new ListItem(listItem.Text, listItem.Value)))
                    lbSecilenler.Items.Add(new ListItem(listItem.Text, listItem.Value));
            }

            TablariKapat();
            tabliKayit.Attributes.Add("class", "active");
            Kayit.Attributes.Add("class", "tab-pane active");
        }
        protected void blnSecileniCikar_OnClick(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = lbSecilenler.Items.Cast<ListItem>().Where(listItem => listItem.Selected).ToList();

            foreach (ListItem listItem in itemsToRemove)
            {
                lbSecilenler.Items.Remove(listItem);
            }

            TablariKapat();
            tabliKayit.Attributes.Add("class", "active");
            Kayit.Attributes.Add("class", "tab-pane active");
        }
        protected void btnVazgec_OnClick(object sender, EventArgs e)
        {
            FormuTemizle();

            TablariKapat();
            tabliSayfalar.Attributes.Add("class", "active");
            Sayfalar.Attributes.Add("class", "tab-pane active");
        }
        protected void btnDosyaEkle_OnClick(object sender, EventArgs e)
        {
            int id = hfId.Value.ToInt32();
            string dosya = "";
            if (fuFoto.HasFile)
            {
                string dosyaAdi = Server.HtmlEncode(fuFoto.FileName);
                string uzanti = Path.GetExtension(dosyaAdi);
                if (uzanti != null)
                {
                    //Dizin yoksa
                    if (!DizinIslemleri.DizinKontrol(Server.MapPath("/upload/SinavEvrak/")))
                        Directory.CreateDirectory(Server.MapPath("/upload/SinavEvrak/"));

                    uzanti = uzanti.ToLower();
                    string rastgeleMetin = GenelIslemler.RastgeleMetinUret(8);
                    if (GenelIslemler.YuklenecekDosyalar.Contains(uzanti))
                    {
                        dosyaAdi = string.Format("{0}{1}", rastgeleMetin, uzanti);
                        string dosyaYolu = string.Format(@"{0}upload\SinavEvrak\{1}", HttpContext.Current.Server.MapPath("/"), dosyaAdi);
                        File.WriteAllBytes(dosyaYolu, fuFoto.FileBytes);

                        dosya = string.Format(@"/upload/SinavEvrak/{0}", dosyaAdi);
                    }
                    else
                    {
                        Master.UyariKirmizi("Yalnızca " + GenelIslemler.YuklenecekDosyalar + " uzantılı dosyalar yüklenir.", phUyari);
                    }
                }
            }

            string sinavAdi = txtSinavAdi.Text;
            string url = string.IsNullOrEmpty(txtUrl.Text) ? dosya : txtUrl.Text;
            string kurumlar = "|";
            for (int a = 0; a < lbSecilenler.Items.Count; a++)
                kurumlar += lbSecilenler.Items[a].Value + "|";


            DateTime baslangicTarihi = txtBaslangicTarihi.Text.ToDateTime();
            DateTime bitisTarihi = txtBitisTarihi.Text.ToDateTime();

            CkSinavEvrakDb veriDb = new CkSinavEvrakDb();
            CkSinavEvrakInfo info = new CkSinavEvrakInfo
            {
                SinavAdi = sinavAdi,
                BitisTarihi = bitisTarihi,
                BaslangicTarihi = baslangicTarihi,
                Kurumlar = kurumlar,
                Url = url
            };
            if (id == 0)
            {
                veriDb.KayitEkle(info);
                Master.UyariIslemTamam("Dosya sisteme başarıyla eklendi.", phUyari);
            }
            else
            {
                info.Id = id;
                veriDb.KayitGuncelle(info);
                Master.UyariIslemTamam("Değişiklikler kaydedildi.", phUyari);
            }
            TablariKapat();
            tabliSayfalar.Attributes.Add("class", "active");
            Sayfalar.Attributes.Add("class", "tab-pane active");

            KayitlariListele();

            FormuTemizle();
        }
        private void TablariKapat()
        {
            tabliSayfalar.Attributes.Add("class", "");
            Sayfalar.Attributes.Add("class", "tab-pane");
            tabliKayit.Attributes.Add("class", "");
            Kayit.Attributes.Add("class", "tab-pane");
        }
        private void FormuTemizle()
        {
            txtBitisTarihi.Text = "";
            txtBaslangicTarihi.Text = "";
            txtUrl.Text = "";
            lbSecilenler.Items.Clear();
            hfId.Value = "0";
            btnDosyaEkle.Text = "Ekle";
        }
        protected void rptSinavEvraklari_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);

                DataRowView dr = (DataRowView)e.Item.DataItem;
                DateTime baslangicTarihi = dr.Row["BaslangicTarihi"].ToDateTime();
                DateTime bitisTarihi = dr.Row["BitisTarihi"].ToDateTime();

                LinkButton lnkDuzenle = (LinkButton)e.Item.FindControl("lnkDuzenle");
                LinkButton lnkSil = (LinkButton)e.Item.FindControl("lnkSil");
                if (!Master.Yetki().Contains("Root|")&&!Master.Yetki().Contains("Admin|"))
                {
                    lnkDuzenle.Visible = lnkSil.Visible = false;

                }
                LinkButton lnkUrl = (LinkButton)e.Item.FindControl("lnkUrl");
                if (GenelIslemler.YerelTarih(true) >= baslangicTarihi && GenelIslemler.YerelTarih(true) <= bitisTarihi)
                {
                    lnkUrl.Text = "İndir";
                }
                else
                {
                    lnkUrl.Visible = false;
                }
            }
        }
        protected void rptSinavEvraklari_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lbSecilenler.Items.Clear();

            int id = e.CommandArgument.ToInt32();
            CkSinavEvrakDb veriDb = new CkSinavEvrakDb();
            CkSinavEvrakInfo info = veriDb.KayitBilgiGetir(id);
            if (e.CommandName.Equals("Sil"))
            {
                try
                {
                    string dosya = Server.MapPath(info.Url);
                    if (DizinIslemleri.DosyaKontrol(dosya))
                    {
                        DizinIslemleri.DosyaSil(dosya);
                        veriDb.KayitSil(id);
                        Master.UyariIslemTamam("Kayıt silindi.", phUyari);
                    }
                    else
                    {
                        veriDb.KayitSil(id);
                        Master.UyariKirmizi("Kayıt silindi ancak sınav evrak dosyası bulunamadı. Dosya Yolu : " + dosya, phUyari);
                    }
                    KayitlariListele();
                }
                catch (Exception ex)
                {
                    Master.UyariTuruncu("Hata :" + ex.Message, phUyari);
                }
            }
            if (e.CommandName.Equals("Duzenle"))
            {
                hfId.Value = id.ToString();
                txtBitisTarihi.Text = info.BitisTarihi.ToString(CultureInfo.CurrentCulture);
                txtBaslangicTarihi.Text = info.BaslangicTarihi.ToString(CultureInfo.CurrentCulture);
                txtUrl.Text = info.Url;
                txtSinavAdi.Text = info.SinavAdi;

                KurumlarDb kurumlarDb = new KurumlarDb();
                var kurumList = kurumlarDb.IlceveOkuluDiziyeGetir();

                string[] kategoriler = info.Kurumlar.Split('|');

                
                foreach (var t in kategoriler)
                {
                    var kontrol = kurumList.Find(x => x.KurumKodu == t.ToString());
                    if (kontrol!=null)
                    {
                        lbSecilenler.Items.Add(new ListItem(kontrol.KurumAdi,kontrol.KurumAdi));
                    }
                }
                btnDosyaEkle.Text = "Değiştir";
                TablariKapat();
                tabliKayit.Attributes.Add("class", "active");
                Kayit.Attributes.Add("class", "tab-pane active");
            }
            if (e.CommandName.Equals("Indir"))
            {
                int hit = info.Hit + 1;
                veriDb.KayitGuncelle(hit,GenelIslemler.YerelTarih(true),id);
                string url = info.Url;
                if (url != null)
                    Response.Redirect(url);
            }
        }
        protected void btnEvrakGonder_OnClick(object sender, EventArgs e)
        {
            SinavlarDb ayr = new SinavlarDb();
            int sinavId = ayr.AktifSinavAdi().SinavId;

            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo info = kDb.KayitBilgiGetir(Master.UyeId());

            if (fuEvrakGnder.HasFile)
            {
                string dosyaAdi = Server.HtmlEncode(fuEvrakGnder.FileName);
                string uzanti = Path.GetExtension(dosyaAdi);
                if (uzanti != null)
                {
                    //Dizin yoksa
                    if (!DizinIslemleri.DizinKontrol(Server.MapPath("/upload/gelenevrak/")))
                        Directory.CreateDirectory(Server.MapPath("/upload/gelenevrak/"));

                    uzanti = uzanti.ToLower();
                    string rastgeleMetin = GenelIslemler.RastgeleMetinUret(8);
                    if (GenelIslemler.YuklenecekDosyalar.Contains(uzanti))
                    {
                        dosyaAdi = string.Format("{0}_{1}{2}", info.KurumKodu, rastgeleMetin, uzanti);
                        string dosyaYolu = string.Format(@"{0}upload\gelenevrak\{1}\{2}", HttpContext.Current.Server.MapPath("/"), sinavId, dosyaAdi);
                        File.WriteAllBytes(dosyaYolu, fuEvrakGnder.FileBytes);

                        string dosya = string.Format(@"/upload/gelenevrak/{0}/{1}", sinavId, dosyaAdi);

                        SinavEvrakGelenDb sgDb = new SinavEvrakGelenDb();
                        SinavEvrakGelenInfo sgInfo = new SinavEvrakGelenInfo
                        {
                            SinavId = sinavId,
                            Dosya = dosya,
                            KurumKodu = info.KurumKodu
                        };
                        sgDb.KayitEkle(sgInfo);
                        Master.UyariIslemTamam("Dosya başarıya yüklendi. Teşekkür ederiz.", phUyari);
                        TablariKapat();
                        tabliEvrakGonder.Attributes.Add("class", "active");
                        EvrakGonder.Attributes.Add("class", "tab-pane active");
                    }
                    else
                    {
                        Master.UyariKirmizi("Yalnızca " + GenelIslemler.YuklenecekDosyalar + " uzantılı dosyalar yüklenir.", phUyari);
                    }
                }
            }
        }
        protected void rptGelenEvraklar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
            }
        }
        protected void rptGelenEvraklar_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = e.CommandArgument.ToInt32();
            if (e.CommandName.Equals("Sil"))
            {
                try
                {
                    SinavEvrakGelenDb veriDb = new SinavEvrakGelenDb();
                    SinavEvrakGelenInfo info = veriDb.KayitBilgiGetir(id);
                    string dosya = Server.MapPath(info.Dosya);
                    if (DizinIslemleri.DosyaKontrol(dosya))
                    {
                        DizinIslemleri.DosyaSil(dosya);
                        veriDb.KayitSil(id);
                        Master.UyariIslemTamam("Kayıt silindi.", phUyari);
                    }
                    else
                    {
                        veriDb.KayitSil(id);
                        Master.UyariKirmizi("Kayıt silindi ancak sınav evrak dosyası bulunamadı. Dosya Yolu : " + dosya, phUyari);
                    }
                    GelenSinavEvraklari();
                    TablariKapat();
                    tabliEvrakGelenler.Attributes.Add("class", "active");
                    EvrakGelenler.Attributes.Add("class", "tab-pane active");
                }
                catch (Exception ex)
                {
                    Master.UyariTuruncu("Hata :" + ex.Message, phUyari);
                }
            }
        }
        protected void ddlSinavId2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GelenSinavEvraklari();
            TablariKapat();
            tabliEvrakGelenler.Attributes.Add("class", "active");
            EvrakGelenler.Attributes.Add("class", "tab-pane active");
        }

        private void GelenSinavEvraklari()
        {
            int sinavId = ddlSinavId2.SelectedValue.ToInt32();
            SinavEvrakGelenDb veriDb = new SinavEvrakGelenDb();
            rptGelenEvraklar.DataSource = veriDb.KayitlariGetir(sinavId);
            rptGelenEvraklar.DataBind();
        }

        protected void ddlOkulTuru_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string okulTuru = ddlOkulTuru.SelectedValue;
            KurumlarDb veriDb = new KurumlarDb();
            lbKurumlar.DataSource = veriDb.IlceveOkuluBirlestirGetir(okulTuru);
            lbKurumlar.DataValueField = "KurumKodu";
            lbKurumlar.DataTextField = "IlceveKurumAdi";
            lbKurumlar.DataBind();
        }
    }
}