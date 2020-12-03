using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim__Rapor_OgrenciAra : System.Web.UI.Page
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

        TestKutukDb veriDb = new TestKutukDb();
        rptOgrenciler.DataSource = veriDb.OgrenciAra(aranan);
        rptOgrenciler.DataBind();
        ltrKayit.Text = rptOgrenciler.Items.Count + " kayıt bulundu";
    }

    protected void rptOgrenciler_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
        }
    }
}