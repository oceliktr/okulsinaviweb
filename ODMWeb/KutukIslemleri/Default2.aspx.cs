using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KutukIslemleriDefault2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            KutukIslemleriDB veriDb = new KutukIslemleriDB();
            rptOkullar.DataSource = veriDb.OkullariGetir();
            rptOkullar.DataBind();
        }
    }

    protected void rptOkullar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
            DataRowView dr = (DataRowView)e.Item.DataItem;
            string kurumKodu = dr.Row["KurumKodu"].ToString();

            KutukIslemleriDB veriDb = new KutukIslemleriDB();
            Literal ltrToplamSubeSayisi = (Literal)e.Item.FindControl("ltrToplamSubeSayisi");
            ltrToplamSubeSayisi.Text = veriDb.SubeSayisi(kurumKodu).ToString();
            Literal ltrToplamOgrenciSayisi = (Literal)e.Item.FindControl("ltrToplamOgrenciSayisi");
            ltrToplamOgrenciSayisi.Text = veriDb.OkulOgrenciSayisi(kurumKodu).ToString();
        }
    }
}