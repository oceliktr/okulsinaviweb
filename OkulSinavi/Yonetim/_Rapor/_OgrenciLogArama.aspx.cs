using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim__Rapor_OgrenciLogArama : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("~/Yonetim/Default.aspx");
            }

            if (Request.QueryString["aranan"] != null)
            {
                string aranan = Request.QueryString["aranan"];

                KayitlariListele(aranan);

            }
        }
    }
    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin");
        return yetkili;
    }
    private void KayitlariListele(string aranan)
    {

        TestLogDb veriDb = new TestLogDb();
        rptLog.DataSource = veriDb.LogAra(aranan);
        rptLog.DataBind();
    }
}