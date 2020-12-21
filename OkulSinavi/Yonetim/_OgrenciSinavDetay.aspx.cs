using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim_OgrenciSinavDetay : System.Web.UI.Page
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

            int sinavId = 0;
            if (Request.QueryString["SinavId"] != null)
            {
                if (Request.QueryString["SinavId"].IsInteger())
                {
                    sinavId = Request.QueryString["SinavId"].ToInt32();
                }
            }

            if (sinavId == 0)
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
                TestOgrCevapDb veriDb = new TestOgrCevapDb();
                rptOturumlar.DataSource = veriDb.KayitlariGetir(sinavId, opaqId);
                rptOturumlar.DataBind();
            }
        }
    }

    protected void rptOturumlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Button btnBitir = (Button)e.Item.FindControl("btnBitir");
            int oturumId = DataBinder.Eval(e.Item.DataItem, "OturumId").ToInt32();
            int bitti = DataBinder.Eval(e.Item.DataItem, "Bitti").ToInt32();
            if (bitti == 0)
            {
                btnBitir.Visible = true;
                btnBitir.Attributes.Add("onclick", "SinavTamamla(" + oturumId + ");return false;");
            }
            else
            {
                btnBitir.Visible = false;
            }
        }
        if (rptOturumlar.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                PlaceHolder phEmpty = (PlaceHolder)e.Item.FindControl("phEmpty");
                phEmpty.Visible = true;
            }

        }
    }
}