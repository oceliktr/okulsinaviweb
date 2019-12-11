using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class LGSSoruBank_KazanimEkle : System.Web.UI.Page
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

            if (Request.QueryString["Id"] != "")
            {
                LgsKazanimlarDB kDb = new LgsKazanimlarDB();
                LgsKazanimlarInfo kiInfo = kDb.KayitBilgiGetir(Request.QueryString["Id"].ToInt32());
                ddlSinif.SelectedValue = kiInfo.Sinif.ToString();
                ddlBrans.SelectedValue = kiInfo.BransId.ToString();
                txtKazanimNo.Text = kiInfo.KazanimNo;
                txtKazanim.Text = kiInfo.Kazanim;
                hfId.Value = kiInfo.Id.ToString();
            }
        }

    }

    protected void btnKaydet_OnClick(object sender, EventArgs e)
    {
        int id = hfId.Value.ToInt32();
        int brans = ddlBrans.SelectedValue.ToInt32();
        int sinif = ddlSinif.SelectedValue.ToInt32();
        string kazanimNo = txtKazanimNo.Text;

        LgsKazanimlarDB kDb = new LgsKazanimlarDB();
        LgsKazanimlarInfo kazanimInfo = new LgsKazanimlarInfo();
        kazanimInfo.BransId = brans;
        kazanimInfo.Sinif = sinif;
        kazanimInfo.KazanimNo = kazanimNo;
        kazanimInfo.Kazanim = txtKazanim.Text;

        if (id == 0)
        {
            if (kDb.KayitKontrol(brans, sinif, kazanimNo))
            {
                Master.UyariTuruncu("Bu kazanım daha önce eklemiş.", phUyari);
            }
            else
            {
                kDb.KayitEkle(kazanimInfo);
                Master.UyariIslemTamam("Kazanım eklendi.", phUyari);

            }
        }
        else
        {
            if (kDb.KayitKontrol(brans, sinif, kazanimNo, id))
            {
                Master.UyariTuruncu("Bu kazanım daha önce eklemiş...", phUyari);
            }
            else
            {
                kazanimInfo.Id = id;
                kDb.KayitGuncelle(kazanimInfo);
                Master.UyariIslemTamam("Değişiklikler kaydedildi.", phUyari);
            }
        }

        txtKazanim.Text = "";
        txtKazanimNo.Text = "";
    }
}