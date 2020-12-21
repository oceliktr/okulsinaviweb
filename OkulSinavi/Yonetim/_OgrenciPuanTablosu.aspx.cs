using System;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim__OgrenciPuanTablosu_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();

            if (!kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin") && !kInfo.Yetki.Contains("Ogretmen"))
            {
                Response.Redirect("Default.aspx");
            }

            if (Request.QueryString["OpaqId"] != null)
            {

                string opaqId = Request.QueryString["OpaqId"];

                TestKutukDb testKutukDb = new TestKutukDb();
                TestKutukInfo kutukInfo = testKutukDb.KayitBilgiGetir(opaqId);

                if (kInfo.KurumKodu.ToInt32() != kutukInfo.KurumKodu && !kInfo.Yetki.Contains("Root"))
                {
                    Response.Redirect("Default.aspx");
                }
                TestOgrPuanDb veriDb = new TestOgrPuanDb();
                rptPuanTablosu.DataSource = veriDb.KayitlariGetir(opaqId);
                rptPuanTablosu.DataBind();
            }
        }
    }

    protected void rptPuanTablosu_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (rptPuanTablosu.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                PlaceHolder phEmpty = (PlaceHolder)e.Item.FindControl("phEmpty");
                phEmpty.Visible = true;
            }

        }
    }
}