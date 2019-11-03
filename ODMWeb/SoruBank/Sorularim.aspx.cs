using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SoruBank_Sorularim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            KayitlariListele();
        }
    }
    private void KayitlariListele()
    {
        SbMaddeKokleriDB veriDb = new SbMaddeKokleriDB();
        rptKayitlar.DataSource = veriDb.KayitlariGetir(Master.UyeId());
        rptKayitlar.DataBind();
    }
    protected void rptKayitlar_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int soruId = e.CommandArgument.ToInt32();

        if (e.CommandName.Equals("Sil"))
        {
            SbMaddeKokleriDB mkDb = new SbMaddeKokleriDB();
            SbSeceneklerDB scDb = new SbSeceneklerDB();

            mkDb.KayitSil(soruId);
            scDb.KayitSil(soruId);
            Master.UyariIslemTamam("Soru başarıyla silindi.", phUyari);
            KayitlariListele();
        }
    }
    protected void rptKayitlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int durum = DataBinder.Eval(e.Item.DataItem, "Durum").ToInt32();

            LinkButton lnkDuzenle = (LinkButton)e.Item.FindControl("lnkDuzenle");
            LinkButton lnkSil = (LinkButton)e.Item.FindControl("lnkSil");

            if (durum == (int)SoruDurumlari.Durum.RedakteEdildi || durum == (int)SoruDurumlari.Durum.RedakteEdiliyor)
            {
                lnkSil.Visible = lnkDuzenle.Visible = false;
            }
        }
    }
}