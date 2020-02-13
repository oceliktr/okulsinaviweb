using System;
using System.Web;
using DAL;

namespace ODM
{
    public partial class AdminAdminMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (Request.Cookies["uyeCookie"] == null) return;
            int uyeId = Request.Cookies["uyeCookie"]["UyeId"].ToInt32();

            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo kInfo = kDb.KayitBilgiGetir(uyeId);


            if (kInfo.Yetki.Contains("Root"))
            {
                liBaranslar.Visible = true;
                liKurumlar.Visible = true;
                liKullanicilar.Visible = true;
                liCkDataYukle.Visible = true;
                liSinavlar.Visible = true;
                liSinavEvraklari.Visible = true;
                liLgsSoruBankasi.Visible = true;
                liLgsKazanimKarne.Visible = true;
                liCkDataYukle.Visible = true;
                liLgsRapor.Visible = true;
            }

            if (kInfo.Yetki.Contains("Admin"))
            {
                liBaranslar.Visible = true;
                liKurumlar.Visible = true;
                liKullanicilar.Visible = true;
                liSinavEvraklari.Visible = true;
                liLgsSoruBankasi.Visible = true;
                liLgsKazanimKarne.Visible = true;
            }
            else if (kInfo.Yetki.Contains("Ogretmen|"))
            {
                liSoruBankasi.Visible = true;
                liLgsSoruBankasi.Visible = false;
                liDegerlendirme.Visible = true;
            }
            else if (kInfo.Yetki.Contains("UstDegerlendirici|"))
            {
                liSoruBankasi.Visible = true;
                liLgsSoruBankasi.Visible = false;
                liDegerlendirmeUst.Visible = true;
            }
            else if (kInfo.Yetki.Contains("OkulYetkilisi"))
            {
                liSinavEvraklari.Visible = true;
                liSoruBankasi.Visible = false;
                liLgsSoruBankasi.Visible = false;
              //  liKazanimKarne.Visible = true;
                liLgsKazanimKarne.Visible = true;
            }
            else if (kInfo.Yetki.Contains("IlceMEMYetkilisi"))
            {
                liSoruBankasi.Visible = false;
                liLgsSoruBankasi.Visible = false;
              //  liKazanimKarne.Visible = true;
                liLgsKazanimKarne.Visible = true;
            }
            else if(kInfo.Yetki.Contains("LgsIlKomisyonu"))
            {
                liLgsKazanimKarne.Visible = true;

            }
        }
    }
}