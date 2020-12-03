using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim_SinavIstatistik : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("~/Yonetim/Default.aspx");
            }

            if (Request.QueryString["SinavId"] != null)
            {
                if (Request.QueryString["SinavId"].IsInteger())
                {
                    int sinavId = Request.QueryString["SinavId"].ToInt32();

                    TestSinavlarDb sinavlarDb = new TestSinavlarDb();
                    TestSinavlarInfo sinavlarInfo = sinavlarDb.KayitBilgiGetir(sinavId);
                    ltrSinavAdi.Text = sinavlarInfo.SinavAdi;



                    TestIstatistikDb veriDb = new TestIstatistikDb();
                    TestIstatistik iInfo = veriDb.SinavIstatistik(sinavId, sinavlarInfo.Sinif);
                    ltrKatilanOgrenciSayisi.Text = iInfo.SinavaGirenSayisi.ToString();
                    ltrOgrenciSayisi.Text = iInfo.ToplamOgrenciSayisi.ToString();
                    ltrKatilanOkulSayisi.Text = iInfo.SinavaKatilanKurumSayisi.ToString();
                    ltrOkulSayisi.Text = iInfo.KurumSayisi.ToString();

                    decimal ogrenciOran = (100 / iInfo.ToplamOgrenciSayisi.ToDecimal()) * iInfo.SinavaGirenSayisi;
                    Session["OgrenciOran"] = ogrenciOran.ToString("##.#");
                    decimal okulOran = (100 / iInfo.KurumSayisi.ToDecimal()) * iInfo.SinavaKatilanKurumSayisi;
                    Session["OkulOran"] = okulOran.ToString("##.#");


                    TestIlcePuanDb ilcelerDb = new TestIlcePuanDb();
                    rptIlceler.DataSource = ilcelerDb.IlceAnlikKatilimOranlari(sinavId);
                    rptIlceler.DataBind();
                }
            }
        }
    }
    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin");
        return yetkili;
    }

    protected void rptIlceler_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
        }
    }
}