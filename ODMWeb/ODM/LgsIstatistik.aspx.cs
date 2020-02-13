using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ODM_LgsIstatistik : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Master.Yetki().Contains("Root") && !Master.Yetki().Contains("Admin"))
            Response.Redirect("Giris.aspx");

        if (!IsPostBack)
        {
            CkSinavAdiDB sinav = new CkSinavAdiDB();
            ddlSinavlar.DataSource = sinav.KayitlariGetir();
            ddlSinavlar.DataValueField = "SinavId";
            ddlSinavlar.DataTextField = "SinavAdi";
            ddlSinavlar.DataBind();
            ddlSinavlar.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Sınav Seçiniz", ""));
        }
    }

    protected void btnSinavaGirenGirmeyenler_OnClick(object sender, EventArgs e)
    {
        int sinavId = ddlSinavlar.SelectedValue.ToInt32();
        int sinif = ddlSinif.SelectedValue.ToInt32();

        CkKarneKutukDB kutukDb = new CkKarneKutukDB();
        rptGirenGirmeyenSayisi.DataSource = kutukDb.SinifSubeSayilari(sinavId, sinif);
        rptGirenGirmeyenSayisi.DataBind();
        pnlGirenGirmeyenler.Visible = true;
    }

    protected void rptGirenGirmeyenSayisi_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 1000);
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Literal ltrGirmeyenSayisi = (Literal)e.Item.FindControl("ltrGirmeyenSayisi");
            Literal ltrKatilimOrani = (Literal)e.Item.FindControl("ltrKatilimOrani");

            
            int ogrSayisi  = DataBinder.Eval(e.Item.DataItem, "OgrenciSayisi").ToInt32();
            int sinavId = ddlSinavlar.SelectedValue.ToInt32();
            int kurumKodu = DataBinder.Eval(e.Item.DataItem, "KurumKodu").ToInt32();
            int sinif = DataBinder.Eval(e.Item.DataItem, "Sinifi").ToInt32();

            CkKarneKutukDB kutukDb = new CkKarneKutukDB();
            int girmeyenSayisi = kutukDb.SinavaGirmeyenSayisi(sinavId, kurumKodu, sinif);
            
            ltrGirmeyenSayisi.Text = girmeyenSayisi.ToString();

            //öğrsay-girmeyen*100/öğrsay
            float katilimOrani = (((ogrSayisi- girmeyenSayisi) * 100) / (float)ogrSayisi);

            ltrKatilimOrani.Text = katilimOrani.ToString("N");
        }
    }
}