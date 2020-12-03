using System;
using System.Web;
using System.Web.UI.WebControls;

namespace OkulSinavi
{
    public partial class SpVeriDefault : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
          //  Response.Redirect("/CevrimiciSinav");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
           
        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtKullaniciAdi.Text.ToTemizMetin()) || string.IsNullOrEmpty(txtSifre.Text.ToTemizMetin()))
            //{
            //    ltrHata.Text = "Kullanıcı adı ve şifrenizi giriniz.";
            //    divHata.Visible = true;
            //}
            //else
            //{
            string tckimlik = txtKullaniciAdi.Text.ToTemizMetin();
            string sifre = txtSifre.Text.ToTemizMetin();

            KullanicilarDb veriDb = new KullanicilarDb();
            KullanicilarInfo info = veriDb.KayitBilgiGetir(tckimlik);
            if (string.IsNullOrEmpty(info.Sifre) && info.Id != 0)
            {
                divGiris.Visible = false;
                divSifreDegis.Visible = true;
                hfId.Value = info.Id.ToString();
                ltrHata.Text = "İlk defa şifre oluşturacaksınız. Size özel bir şifre giriniz.";
                divHata.Visible = true;
            }
            else
            {
                try
                {
                    bool giris = veriDb.KayitKontrol(tckimlik, sifre.Md5Sifrele());

                    if (giris)
                    {
                        KullanicilarInfo infoGiris = veriDb.KayitBilgiGetir(tckimlik, sifre.Md5Sifrele());
                        int girisSayisi = infoGiris.GirisSayisi + 1;
                        int uyeId = infoGiris.Id;
                        string yetki = infoGiris.Yetki;
                        DateTime sonGiris = infoGiris.SonGiris; //önceki giriş tarihi olarak kaydedelim.
                        veriDb.KayitGuncelle(girisSayisi, uyeId, sonGiris);

                        HttpCookie uyeCookie = new HttpCookie("uyeCookie");
                        uyeCookie["Oturum"] = "Acik";
                        uyeCookie["UyeId"] = uyeId.ToString();
                        uyeCookie["Yetki"] = yetki;
                        uyeCookie.Expires = cbBeniHatirla.Checked ? GenelIslemler.YerelTarih().AddDays(1) : GenelIslemler.YerelTarih().AddMinutes(20);
                        Response.Cookies.Add(uyeCookie);

                        Session["Kullanici"] = infoGiris;

                           Response.Redirect("/Yonetim/Giris.aspx");
                        
                    }
                    else
                    {
                        ltrHata.Text = "Hatalı kullanıcı adı veya şifre";
                        divHata.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    ltrHata.Text = string.Format("Üzgünüm bir hata oluştu. Hata : {0}", ex.Message);
                    divHata.Visible = true;
                }
                //}
            }
        }

        protected void btnSifreDegis_OnClick(object sender, EventArgs e)
        {
            string yeniSifre = txtYeniSifre.Text.ToTemizMetin();
            string yeniSifre2 = txtYeniSifre2.Text.ToTemizMetin();
            if (yeniSifre == yeniSifre2)
            {
                int id = hfId.Value.ToInt32();
                string sifre = yeniSifre.Md5Sifrele();
                string tcKimlik = txtKurumKodu.Text;
                KullanicilarDb veriDb = new KullanicilarDb();
                veriDb.KayitGuncelle(id, sifre);
                ltrHata.Text = "Şifreniz oluşturuldu. Yeni şifre ile giriş yapabilirsiniz.";
                divHata.Visible = true;
                divGiris.Visible = true;
                divSifreDegis.Visible = false;
            }
            else
            {
                ltrHata.Text = "Şifreler birbirinden farklı olmamalı.";
                divHata.Visible = true;
            }
        }

        protected void btnSifreUnuttum_OnClick(object sender, EventArgs e)
        {
            string kurumKodu = txtKurumKodu.Text.ToTemizMetin();
            int ilce = ddlIlce.SelectedValue.ToInt32();
            int kurumId = ddlKurumAdi.SelectedValue.ToInt32();
            string tcKimlik = txtKullaniciAdi2.Text.ToTemizMetin();

            KullanicilarDb veriDb = new KullanicilarDb();
            KullanicilarInfo info = veriDb.KayitBilgiGetir(ilce, kurumId, kurumKodu, tcKimlik);
            if (info.Id != 0)
            {
                //string yeniSifre = GenelIslemler.RastgeleMetinUret(12);
                //veriDb.YeniSifreOlustur(info.Id, yeniSifre);

                divSifreDegis.Visible = true;
                hfId.Value = info.Id.ToString();
                divsifreUnuttum.Visible = false;
                divHata.Visible = false;
            }
            else
            {
                divHata.Visible = true;
                ltrHata.Text = "Kurum ve kullanıcı eşleşmesi bulunamadı. <br><br>Bilgilerin doğruluğundan eminseniz Ölçme Değerlendirme Merkezi ile irtibata geçiniz.<br>";
            }
        }

        protected void lnkSifremiUnuttum_OnClick(object sender, EventArgs e)
        {
            divGiris.Visible = false;
            divsifreUnuttum.Visible = true;
            ddlIlce.Items.Clear(); ddlKurumAdi.Items.Clear();
            IlcelerDb ilcelerDb = new IlcelerDb();
            ddlIlce.DataSource = ilcelerDb.KayitlariGetir();
            ddlIlce.DataValueField = "Id";
            ddlIlce.DataTextField = "IlceAdi";
            ddlIlce.DataBind();
            ddlIlce.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
            ddlKurumAdi.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
        }

        protected void ddlIlce_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int ilce = ddlIlce.SelectedValue.ToInt32();

            ddlKurumAdi.Items.Clear();
            KurumlarDb kurumDb = new KurumlarDb();
            ddlKurumAdi.DataSource = kurumDb.OkullariGetir(ilce);
            ddlKurumAdi.DataValueField = "Id";
            ddlKurumAdi.DataTextField = "KurumAdi";
            ddlKurumAdi.DataBind();
            ddlKurumAdi.Items.Insert(0, new ListItem("Okulunuzu / Kurumunuzu Seçiniz", ""));
        }
    }
}