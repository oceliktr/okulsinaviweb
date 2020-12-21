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

            

        }

    }
}