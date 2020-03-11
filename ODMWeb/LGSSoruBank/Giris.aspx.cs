using System;

public partial class LGSSoruBank_Giris : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       if (Master.UyeId() == 0) Response.Redirect("~/ODM/Cikis.aspx");
        int uyeId = Master.UyeId();

        KullanicilarDb kDb = new KullanicilarDb();
        KullanicilarInfo kInfo = kDb.KayitBilgiGetir(uyeId);
        if (string.IsNullOrEmpty(kInfo.AdiSoyadi))
            ltrKullaniciAdi.Text = "DİKKAT:Bilgilerim bölümünden isminizi giriniz";
        else
            ltrKullaniciAdi.Text = kInfo.AdiSoyadi;

        SinavlarDb sDb = new SinavlarDb();
        SinavlarInfo sinf = sDb.AktifSinavAdi();
        ltrDonemAdi.Text = sinf.SinavAdi;
         if (sinf.VeriGirisi == 0)
        {
            Master.UyariTuruncu(string.Format("<b>{0}</b> için veri girişleri kapatıldı.", sinf.SinavAdi), phUyari);
        }

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
        }

        if (Master.Yetki().Contains("Admin") && Master.Yetki().Contains("UstDegerlendirici") && Master.Yetki().Contains("Ogretmen"))
        {
            divPuanlamaGrafik.Visible = true;
        }
       
    }

}