using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Anket : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cikis"] != null)
            {
                if (Request.QueryString["cikis"].ToString()=="ok")
                {
                    Session["KurumKodu"] = null;
                }
            }

            string kurumKodu = "";
            if (Session["KurumKodu"] != null)
            {
                kurumKodu = Session["KurumKodu"].ToString();
                phLogin.Visible = false;
                anketFormu.Visible = true;
            }
            else
            {
                phLogin.Visible = true;
                anketFormu.Visible = false;
            }

            AnketDB anket = new AnketDB();
            rptList.DataSource = anket.KayitlariGetir(kurumKodu);
            rptList.DataBind();

            var id = KayitNo();

            if (id != 0)
            {
                AnketInfo info = anket.KayitBilgiGetir(id);
                ltrAdiSoyadi.Text = info.AD_SOYAD;
                try
                {
                    ddlBabaEgitim.SelectedValue = info.BABAEGITIMDUZEY;
                    ddlAnneEgitimDuzeyi.SelectedValue = info.ANNEEGITIMDUZEY;
                    ddlAnneHayatta.SelectedValue = info.ANNEHAYATTA;
                    ddlBabaHayatta.SelectedValue = info.BABAHAYATTS;
                }
                catch (Exception)
                {
                  //
                }
                txtBabaMeslek.Text = info.BABAMESLEK;
                txtAnneMeslek.Text = info.ANNEMESLEK;
                txtAileGelir.Text = info.AILEGELIR;
                btnKaydet.Text = "KAYDET";
                btnKaydet.Enabled = true;
            }
            else
            {
                btnKaydet.Text = "ÖĞRENCİ SEÇMEDİNİZ";
                btnKaydet.Enabled = false;
            }
        }
    }

    private int KayitNo()
    {
        int id = 0;
        if (Request.QueryString["Id"] != null)
        {
            if (Request.QueryString["Id"].IsInteger())
                id = Request.QueryString["Id"].ToInt32();
        }

        return id;
    }

    protected void btnKaydet_OnClick(object sender, EventArgs e)
    {
        var id = KayitNo();
        AnketDB anket = new AnketDB();
        if (id != 0)
        {
            AnketInfo info = anket.KayitBilgiGetir(id);
            info.ANKETDURUMU = "Tamam";
            info.AILEGELIR = txtAileGelir.Text.ToTemizMetin();
            info.BABAEGITIMDUZEY = ddlBabaEgitim.SelectedValue.ToTemizMetin();
            info.BABAHAYATTS = ddlBabaHayatta.SelectedValue.ToTemizMetin();
            info.BABAMESLEK = txtBabaMeslek.Text.ToTemizMetin();
            info.ANNEEGITIMDUZEY = ddlAnneEgitimDuzeyi.SelectedValue.ToTemizMetin();
            info.ANNEHAYATTA = ddlAnneHayatta.SelectedValue.ToTemizMetin();
            info.ANNEMESLEK = txtAnneMeslek.Text.ToTemizMetin();
            info.Id = id;
            anket.KayitGuncelle(info);

            txtAileGelir.Text = "";
            ddlBabaEgitim.SelectedValue = "";
            ddlBabaHayatta.SelectedValue = "";
            txtBabaMeslek.Text = "";
            ddlAnneEgitimDuzeyi.SelectedValue = "";
            ddlAnneHayatta.SelectedValue = "";
            txtAnneMeslek.Text = "";

            string kurumKodu = "";
            if (Session["KurumKodu"] != null)
            {
                kurumKodu = Session["KurumKodu"].ToString();
                phLogin.Visible = false;
                anketFormu.Visible = true;
            }
            rptList.DataSource = anket.KayitlariGetir(kurumKodu);
            rptList.DataBind();

            phOkey.Visible = true;
        }
    }

    protected void btnGiris_OnClick(object sender, EventArgs e)
    {
        string kurumKodu = txtKurumKodu.Text.ToTemizMetin();
        if (!string.IsNullOrEmpty(kurumKodu))
        {
            AnketDB anket = new AnketDB();

            AnketInfo info = anket.KayitBilgiGetir(kurumKodu);
            if (info.Id != 0)
            {
                phLogin.Visible = false;
                anketFormu.Visible = true;
                Session["KurumKodu"] = info.YENIKURUMKODU;
                
                rptList.DataSource = anket.KayitlariGetir(info.YENIKURUMKODU);
                rptList.DataBind();
            }
            else
            {
                phGirisError.Visible = true;
            }
        }
    }

    protected void rptList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
    }
}