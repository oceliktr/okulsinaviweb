using System;

namespace OkulSinavi
{
    public partial class AdminGiris : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Master.UyeId() == 0) Response.Redirect("Cikis.aspx");
            int uyeId = Master.UyeId();

            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo kInfo = kDb.KayitBilgiGetir(uyeId);
            ltrKullaniciAdi.Text = string.IsNullOrEmpty(kInfo.AdiSoyadi) ? "DİKKAT:Bilgilerim bölümünden isminizi giriniz" : kInfo.AdiSoyadi;
           
            int girisSayisi = kInfo.GirisSayisi;//login olmadığı için sayı artmamalı
           
            DateTime sonGiris = kInfo.SonGiris; //önceki giriş tarihi olarak kaydedelim.
            kDb.KayitGuncelle(girisSayisi, uyeId, sonGiris);

            string[] yetkiler = Master.Yetki().Split(Convert.ToChar("|"));
            foreach (var yt in yetkiler)
            {
                if (yt == "Ogretmen")
                    ltrYetkiler.Text += "<li>Puanlayıcı Öğretmen</li>";
                if (yt == "UstDegerlendirici")
                    ltrYetkiler.Text += "<li>Üst Puanlayıcı Öğretmen</li>";
                if (yt == "OkulYetkilisi")
                    ltrYetkiler.Text += "<li>Okul Modül yöneticisi</li>";
                if (yt == "IlceMEMYetkilisi")
                    ltrYetkiler.Text += "<li>İlçe MEM Modül yöneticisi</li>";
                if (yt == "Admin")
                    ltrYetkiler.Text += "<li>Modül yöneticisi</li>";
                if (yt == "Root")
                    ltrYetkiler.Text += "<li>Modül yöneticisi</li>";
            }

        }

    }
}