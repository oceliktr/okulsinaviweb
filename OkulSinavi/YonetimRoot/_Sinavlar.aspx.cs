using System;
using System.Web.UI.WebControls;

public partial class Okul_SinaviYonetim__Sinavlar_Root : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            int kurumKodu = 0;
            int sinif = 0;
            int donem = TestSeciliDonem.SeciliDonem().Id;

            if (Request.QueryString["Sinif"] != null)
            {
                sinif = Request.QueryString["Sinif"].ToInt32();
            }
            if (Request.QueryString["KurumKodu"] != null)
            {
                kurumKodu = Request.QueryString["KurumKodu"].ToInt32();
            }


            TestSinavlarDb sinavDb = new TestSinavlarDb();
            rptSinavlar.DataSource = sinavDb.KayitlariGetir(sinif,kurumKodu.ToString());
            rptSinavlar.DataBind();
        }
    }

    protected void rptSinavlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
        }
    }
}