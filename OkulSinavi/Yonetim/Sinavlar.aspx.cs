using System;
using System.Web.UI.WebControls;

public partial class Okul_SinaviYonetim_Sinavlar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();

            if (!kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin") &&!kInfo.Yetki.Contains("Ogretmen"))
            {
               Response.Redirect("Default.aspx");
            }
            
            TestSinavlarDb sinavAdi = new TestSinavlarDb();
            rptTestler.DataSource = kInfo.Yetki.Contains("Root")? sinavAdi.KayitlariGetir(): sinavAdi.KayitlariGetir(kInfo.KurumKodu);
            rptTestler.DataBind();

        }
    }
    protected void rptTestler_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
        }
    }
}