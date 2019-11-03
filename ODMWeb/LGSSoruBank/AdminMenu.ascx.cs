using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class LGSSoruBank_AdminMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        string sPath = HttpContext.Current.Request.Url.AbsolutePath;

        if (Request.Cookies["uyeCookie"] == null) return;
        int uyeId = Request.Cookies["uyeCookie"]["UyeId"].ToInt32();

        KullanicilarDb kDb = new KullanicilarDb();
        KullanicilarInfo kInfo = kDb.KayitBilgiGetir(uyeId);

        if (kInfo.Yetki.Contains("Root")|| kInfo.Yetki.Contains("Admin"))
        {
            liSorular.Visible = true;
            liSinavModulu.Visible = true;
        }
        

    }
}