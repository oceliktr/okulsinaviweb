using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Okul_SinaviYonetim_SinavDetay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();

            if (!kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin") &&!kInfo.Yetki.Contains("Ogretmen"))
            {
                Response.Redirect("Default.aspx");
            }

            if (Request.QueryString["SinavId"] != null)
            {
                if (Request.QueryString["SinavId"].IsInteger())
                {
                    int sinavId = Request.QueryString["SinavId"].ToInt32();

                    TestIlcePuanDb veriDb = new TestIlcePuanDb();
                    if (kInfo.Yetki.Contains("Ogretmen")|| kInfo.Yetki.Contains("Admin"))
                    {
                        rptKayitlar.DataSource = veriDb.KayitlariGetir(sinavId, kInfo.KurumKodu.ToInt32());
                        rptKayitlar.DataBind();
                    }
                    else if (kInfo.Yetki.Contains("Root"))
                    {
                        IlcelerDb ilcelerDb = new IlcelerDb();
                        IlcelerInfo ilce = ilcelerDb.KayitBilgiGetir(kInfo.IlceId);
                        rptKayitlar.DataSource = veriDb.KayitlariGetir(sinavId, ilce.IlceAdi);
                        rptKayitlar.DataBind();
                    
                        ddlIlce.Visible = true; 
                    }
                }
            }
        }
    }

    protected void rptKayitlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
        }
    }

    protected void ddlIlce_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (Request.QueryString["SinavId"] != null)
        {
            int sinavId = Request.QueryString["SinavId"].ToInt32();

            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();
            if (kInfo.Yetki.Contains("Root") || kInfo.Yetki.Contains("Admin") || kInfo.Yetki.Contains("LgsIlKomisyonu"))
            {
                string ilceAdi = ddlIlce.SelectedValue;

                TestIlcePuanDb veriDb = new TestIlcePuanDb();
                rptKayitlar.DataSource = veriDb.KayitlariGetir(sinavId, ilceAdi);
                rptKayitlar.DataBind();
            }
        }
    }
}