using System;
using System.Web.UI.WebControls;

public partial class Okul_SinaviYonetim__Sinavlar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }

            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();
      
            TestSinavlarDb sinavDb = new TestSinavlarDb();
            rptSinavlar.DataSource = kInfo.Yetki.Contains("Root")? sinavDb.KayitlariGetir(): sinavDb.KayitlariGetir(kInfo.KurumKodu);
            rptSinavlar.DataBind();
        }
    }
    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin");
        return yetkili;
    }
    protected void rptSinavlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();
            HyperLink hlSinavAktar = (HyperLink)e.Item.FindControl("hlSinavAktar");
            if (kInfo.Yetki.Contains("Root"))
            {
                hlSinavAktar.Visible = true;
            }
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
        }
    }
}