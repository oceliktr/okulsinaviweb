using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ODM_LgsRapor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Master.Yetki().Contains("Root") && !Master.Yetki().Contains("Admin") && !Master.Yetki().Contains("IlceMEMYetkilisi") && !Master.Yetki().Contains("OkulYetkilisi") && !Master.Yetki().Contains("LgsIlKomisyonu"))
                Response.Redirect("Giris.aspx");

        }
    }

    protected void btnGetir_OnClick(object sender, EventArgs e)
    {
        OkullariGetir();
    }

    private void OkullariGetir()
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        string ilce = ddlIlce.SelectedValue;

        CkKarneKutukDB ckKutuk = new CkKarneKutukDB();
        rptKurumlar.DataSource = ckKutuk.IlceninOkullari(sinif, ilce);
        rptKurumlar.DataBind();
    }

    protected void rptKurumlar_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        throw new NotImplementedException();
    }

    private int a;
    protected void rptKurumlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        a++;
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Literal ltr = (Literal)e.Item.FindControl("ltrBilgi");
            ltr.Text = a == 1 ? "<p class=\"text-danger\">Kayıt bulunamadı</p>" : "<p class=\"text-info\"><strong>" + (a - 1) + " kurum</strong> listelendi</p>";
        }
    }
}