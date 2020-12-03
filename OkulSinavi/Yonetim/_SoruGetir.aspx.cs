using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim_SoruGetir : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }
            if (Request.QueryString["Id"] != null)
            {
                if (Request.QueryString["Id"].IsInteger())
                {
                    int id = Request.QueryString["Id"].ToInt32();

                    TestSorularDb veriDb = new TestSorularDb();
                    TestSorularInfo info = veriDb.KayitBilgiGetir(id);
                    if (info.Id==0)
                    {
                        ltrSoru.Text = "Soru bulunamadı";
                        imgSoru.Visible = false;
                    }
                    else
                    {
                        if (info.Soru.Contains("<"))
                        {
                            ltrSoru.Text = info.Soru;
                            imgSoru.Visible = false;
                        }
                        else
                        {
                            imgSoru.ImageUrl = info.Soru;
                        }
                    }
                }
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
}