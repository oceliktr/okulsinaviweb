using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LGSSoruBank_Sorularim : System.Web.UI.Page
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
        LgsSorularDB veriDb = new LgsSorularDB();
        rptKayitlar.DataSource = veriDb.KayitlariGetir(Master.UyeId());
        rptKayitlar.DataBind();
    }
    protected void rptKayitlar_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int soruId = e.CommandArgument.ToInt32();

        if (e.CommandName.Equals("Sil"))
        {
            LgsSorularDB mkDb = new LgsSorularDB();
            LgsSorularInfo info = mkDb.KayitBilgiGetir(soruId, Master.UyeId());

            DizinIslemleri.DosyaSil(Server.MapPath(info.SoruUrl));

            mkDb.KayitSil(soruId);

            Master.UyariIslemTamam("Soru başarıyla silindi.", phUyari);
            KayitlariListele();
        }
    }

    protected void rptKayitlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int durum = DataBinder.Eval(e.Item.DataItem, "Onay").ToInt32();

            LinkButton lnkDuzenle = (LinkButton)e.Item.FindControl("lnkDuzenle");
            LinkButton lnkSil = (LinkButton)e.Item.FindControl("lnkSil");

            if (durum == (int)SoruDurumlari.LgsDurum.Incelendi)
            {
                lnkSil.Visible = lnkDuzenle.Visible = false;
            }
        }
    }

}