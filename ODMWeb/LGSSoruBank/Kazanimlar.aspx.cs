using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LGSSoruBank_Kazanimlar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Master.Yetki().Contains("Root") && !Master.Yetki().Contains("Admin"))
                Response.Redirect("/ODM/Cikis.aspx");

            BranslarDb brnsDb = new BranslarDb();
            ddlBrans.DataSource = brnsDb.KayitlariGetir();
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataValueField = "Id";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));


        }
    }

    protected void btnListele_OnClick(object sender, EventArgs e)
    {
        KayitlariListele();
    }

    private void KayitlariListele()
    {
        int bransId = ddlBrans.SelectedValue.ToInt32();
        int sinif = ddlSinif.SelectedValue.ToInt32();
        LgsKazanimlarDb veriDb = new LgsKazanimlarDb();
        rptKayitlar.DataSource = veriDb.KayitlariGetir(bransId, sinif);
        rptKayitlar.DataBind();
    }

    protected void rptKayitlar_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = e.CommandArgument.ToInt32();


        if (e.CommandName.Equals("Sil"))
        {
            LgsSorularDB mkDb = new LgsSorularDB();
            bool kontrol = mkDb.KayitKontrol(id);


            if (kontrol == false)
            {
                LgsKazanimlarDb kDb = new LgsKazanimlarDb();
                kDb.KayitSil(id);

                Master.UyariIslemTamam("Kazanım başarıyla silindi.", phUyari);
                KayitlariListele();
            }
            else
            {
                Master.UyariTuruncu("Kazanım ait sorular olduğu için silinmedi. Önce soruların silinmesi gerekmektedir.", phUyari);
            }
        }

    }

    protected void rptKayitlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira");

        }
    }
}