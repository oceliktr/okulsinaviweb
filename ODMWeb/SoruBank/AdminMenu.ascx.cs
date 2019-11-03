using System;
using System.Web;
using DAL;

namespace SoruBank
{
    public partial class AdminAdminMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            string sPath = HttpContext.Current.Request.Url.AbsolutePath;
           
            if (Request.Cookies["uyeCookie"] == null) return;
            int uyeId = Request.Cookies["uyeCookie"]["UyeId"].ToInt32();

            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo kInfo = kDb.KayitBilgiGetir(uyeId);

            if (kInfo.Yetki.Contains("Admin"))
            {
                liSorular.Visible = true;
                liSinavModulu.Visible = true;
            }
            else if (kInfo.Yetki.Contains("Ogretmen|"))
            {
                liSorular.Visible = false;
            }
            
        }
    }
}