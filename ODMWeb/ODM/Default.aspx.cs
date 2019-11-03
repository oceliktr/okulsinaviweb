﻿using System;
using System.Web;
using System.Web.UI.WebControls;
using DAL;

namespace ODM
{
    public partial class SpVeriDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            try
            {
                if (Request.Cookies["uyeCookie"] == null) return;
                string uyeAdiCookies = Request.Cookies["uyeCookie"]["Oturum"].ToString();

                if (uyeAdiCookies == "Acik")
                {
                    Response.Redirect("Giris.aspx");
                }
                else
                {
                    ltrHata.Text = "İlk defa giriş yapacaksanız şifre alanına da kurum kodunuzu giriniz.";
                    divHata.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ltrHata.Text = string.Format("Hata oluştu : {0}", ex.Message);
                divHata.Visible = true;
            }
        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            string tckimlik = txtKullaniciAdi.Text.ToTemizMetin().Md5Sifrele();
            string sifre = txtSifre.Text.ToTemizMetin().Md5Sifrele();
           
            KullanicilarDb veriDb = new KullanicilarDb();
            KullanicilarInfo info = veriDb.KayitBilgiGetir(tckimlik);
            if (string.IsNullOrEmpty(info.Sifre)&&info.Id!=0)
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
                    bool giris = veriDb.KayitKontrol(tckimlik, sifre);

                    if (giris)
                    {
                        KullanicilarInfo infoGiris = veriDb.KayitBilgiGetir(tckimlik, sifre);
                        int girisSayisi = infoGiris.GirisSayisi + 1;
                        int uyeId = infoGiris.Id;
                        string yetki = infoGiris.Yetki;
                        DateTime sonGiris = infoGiris.SonGiris; //önceki giriş tarihi olarak kaydedelim.
                        veriDb.KayitGuncelle(girisSayisi, uyeId, sonGiris);

                        HttpCookie uyeCookie = new HttpCookie("uyeCookie");
                        uyeCookie["Oturum"] = "Acik";
                        uyeCookie["UyeId"] = uyeId.ToString();
                        uyeCookie["Yetki"] = yetki;
                        uyeCookie.Expires = cbBeniHatirla.Checked ? DateTime.Now.AddDays(1) : DateTime.Now.AddMinutes(20);
                        Response.Cookies.Add(uyeCookie);

                        Response.Redirect("Giris.aspx");
                    }
                    else
                    {
                        ltrHata.Text = "Hatalı kullanıcı adı veya şifre";
                        divHata.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    ltrHata.Text = string.Format("Üzgünüm bir hata oluştu. Hata (2): {0}", ex.Message);
                    divHata.Visible = true;
                }
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

            KullanicilarDb veriDb = new KullanicilarDb();
            KullanicilarInfo info = veriDb.KayitBilgiGetir(ilce, kurumId, kurumKodu);
            if (info.Id != 0)
            {
                string yeniSifre = GenelIslemler.RastgeleMetinUret(12);
                veriDb.YeniSifreOlustur(info.Id, yeniSifre);

                divSifreDegis.Visible = true;
                hfId.Value = info.Id.ToString();
                divsifreUnuttum.Visible = false;
            }
            else
            {
                divHata.Visible = true;
                ltrHata.Text = "Kurum eşleşmesi bulunamadı. <br>Bilgilerin doğruluğundan eminseniz İl Milli Eğitim Müdürlüğü Strateji Geliştirme Şubesi ile irtibata geçiniz.<br> Tlf : 0442 234 48 00 - Dahili 175";
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