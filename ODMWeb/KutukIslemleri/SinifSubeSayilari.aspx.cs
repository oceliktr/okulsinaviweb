using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class KutukIslemleri_SinifSubeSayilari : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OgrencilerDb veriDb = new OgrencilerDb();
            rptSubeler.DataSource = veriDb.SinavaGirenSubeler(5);
            rptSubeler.DataBind();
        }
    }

    protected void rptSubeler_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
            string kurumKodu =DataBinder.Eval(e.Item.DataItem,"KurumKodu").ToString();
            string sube = DataBinder.Eval(e.Item.DataItem,"Sube").ToString();

            OgrencilerDb veriDb = new OgrencilerDb();
            Literal ltr = (Literal)e.Item.FindControl("ltrToplamOgrenciSayisi");
            ltr.Text = veriDb.SubeOgrenciSayisi(5,kurumKodu, sube).ToString();
        }
    }
}