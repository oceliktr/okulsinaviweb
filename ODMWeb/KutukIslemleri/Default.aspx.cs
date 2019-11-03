using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KutukIslemleri_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            KutukIslemleriDB veriDb = new KutukIslemleriDB();
            rptSubeler.DataSource = veriDb.SubeleriGetir();
            rptSubeler.DataBind();
        }
    }

    protected void rptSubeler_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
            DataRowView dr = (DataRowView)e.Item.DataItem;
            string kurumKodu = dr.Row["KurumKodu"].ToString();
            string sube = dr.Row["Sube"].ToString();

            KutukIslemleriDB veriDb = new KutukIslemleriDB();
            Literal ltr = (Literal) e.Item.FindControl("ltrToplamOgrenciSayisi");
            ltr.Text = veriDb.SubeOgrenciSayisi(kurumKodu, sube).ToString();
        }
    }
}