using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using SoruBankasi;

public partial class SoruBank_Sorular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MaddeTurleri maddeTurleri = new MaddeTurleri();
            List<MaddeTurleriInfo> lst = maddeTurleri.MaddeTurleriniGetir();
            ddlMaddeTuru.DataSource = lst;
            ddlMaddeTuru.DataValueField = "Id";
            ddlMaddeTuru.DataTextField = "MaddeTuru";
            ddlMaddeTuru.DataBind();
            ddlMaddeTuru.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));

            BranslarDb brnsDb = new BranslarDb();
            ddlBrans.DataSource = brnsDb.KayitlariGetir();
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataValueField = "Id";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));

            ddlOgrenmeAlani.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));
            ddlAltOgrenmeAlani.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));

        }
    }

    private void KayitlariListele()
    {
        string maddeTuru = ddlMaddeTuru.SelectedValue != "" ? "MaddeTuru=" + ddlMaddeTuru.SelectedValue + " AND" : "";
        string brans = ddlBrans.SelectedValue != "" ? "Brans=" + ddlBrans.SelectedValue + " AND" : "";
        string sinif = ddlSinif.SelectedValue != "" ? "Sinif=" + ddlSinif.SelectedValue + " AND" : "";

        string kazanimlar = "";
        for (var i = 0; i < lbKazanimlar.Items.Count; i++)
        {
            if (lbKazanimlar.Items[i].Selected)
            {
                kazanimlar += "Kazanim LIKE '%|" + lbKazanimlar.Items[i].Value + "|%' OR ";
            }
        }

        if (kazanimlar.Length > 0)
        {
            kazanimlar = "(" + kazanimlar.Substring(0, kazanimlar.Length - 3) + ") AND";//sondaki or deyimini kaldırmak için son 3 karakteri sil
        }

        string sql = string.Format("select * from SbMaddeKokleri WHERE {0} {1} {2} {3} Id<>0 order by Sinif asc", maddeTuru, brans, sinif, kazanimlar);

        SbMaddeKokleriDB veriDb = new SbMaddeKokleriDB();
        rptKayitlar.DataSource = veriDb.KayitlariGetir(sql);
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

        }
    }
    private void UstOgrenmeAlanlari()
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();

        KOgrenmeAlanlariDB uoaDb = new KOgrenmeAlanlariDB();
        ddlOgrenmeAlani.DataSource = uoaDb.KayitlariGetir(0, brans, sinif);
        ddlOgrenmeAlani.DataValueField = "AlanNo";
        ddlOgrenmeAlani.DataTextField = "NoOgrenmeAlani";
        ddlOgrenmeAlani.DataBind();

        ddlOgrenmeAlani.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));
        ddlAltOgrenmeAlani.Items.Clear();
        ddlAltOgrenmeAlani.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));
    }
    private void AltOgrenmealanlariniGetir(int ogrenmeAlani)
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();
        KOgrenmeAlanlariDB uoaDb = new KOgrenmeAlanlariDB();
        ddlAltOgrenmeAlani.DataSource = uoaDb.KayitlariGetir(ogrenmeAlani, brans, sinif);
        ddlAltOgrenmeAlani.DataValueField = "AlanNo";
        ddlAltOgrenmeAlani.DataTextField = "NoOgrenmeAlani";
        ddlAltOgrenmeAlani.DataBind();
        if (ogrenmeAlani == 0)
        {
            ddlAltOgrenmeAlani.Items.Clear();
            lbKazanimlar.Items.Clear();
        }
        ddlAltOgrenmeAlani.Items.Insert(0, new ListItem("--- Seçiniz ---", ""));

    }

    protected void ddlOgrenmeAlani_OnSelectedIndexChangedlectedIndexChanged(object sender, EventArgs e)
    {
        int ogrenmeAlani = ddlOgrenmeAlani.SelectedValue.ToInt32();
        AltOgrenmealanlariniGetir(ogrenmeAlani);

        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();


        ListboxWithTooltip kazanimlar = new ListboxWithTooltip();
        kazanimlar.Kazanimlar(lbKazanimlar, sinif, brans, ogrenmeAlani, 0);
    }



    protected void ddlAltOgrenmeAlani_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();
        int ogrenmeAlani = ddlOgrenmeAlani.SelectedValue.ToInt32();
        int altOgrenmeAlani = ddlAltOgrenmeAlani.SelectedValue.ToInt32();

        ListboxWithTooltip kazanimlar = new ListboxWithTooltip();
        kazanimlar.Kazanimlar(lbKazanimlar, sinif, brans, ogrenmeAlani, altOgrenmeAlani);
    }

    protected void ddlSinif_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        UstOgrenmeAlanlari();
    }

    protected void ddlBrans_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        UstOgrenmeAlanlari();
    }

    protected void btnListele_OnClick(object sender, EventArgs e)
    {
        KayitlariListele();
    }
}