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
            string sPath = HttpContext.Current.Request.Url.AbsolutePath;
            if (sPath == "/SPVeri/Kurumlar.aspx")
                liKurumlar.Attributes.Add("class", "treeview active");

            if (Request.Cookies["uyeCookie"] == null) return;
            int uyeId = Request.Cookies["uyeCookie"]["UyeId"].ToInt32();

            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo kInfo = kDb.KayitBilgiGetir(uyeId);

            if (kInfo.Yetki.Contains("Admin"))
            {
                liKurumlar.Visible = true;
                liKullanicilar.Visible = true;
                liPuanlayciIslemleri.Visible = true;
                liSinavlar.Visible = true;
                liDegerlendirme.Visible = true;
                liRubrik.Visible = true;
                liDegerlendirmeUst.Visible = true;
                liSinavEvraklari.Visible = true;
                liSinavSonuclari.Visible = true;
                liKazanim.Visible = true;
                liOgrenciVeri.Visible = true;
            }
            if (kInfo.Yetki.Contains("Ogretmen|"))
            {
                liDegerlendirme.Visible = true;
            }
            if (kInfo.Yetki.Contains("UstDegerlendirici|"))
            {
                liDegerlendirmeUst.Visible = true;
            }
            if (kInfo.Yetki.Contains("OkulYetkilisi"))
            {
                liSinavEvraklari.Visible = true;
            }
        }
    }
}