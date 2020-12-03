using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class OkulSinavi_CevrimiciSinavYonetim_OgrenciveSubeSayilari : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }

            IlcelerDb ilcelerDb = new IlcelerDb();
            ddlIlce.DataSource = ilcelerDb.KayitlariGetir();
            ddlIlce.DataValueField = "Id";
            ddlIlce.DataTextField = "IlceAdi";
            ddlIlce.DataBind();
            ddlIlce.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
            ddlIlce.Items.Insert(1, new ListItem("Tümü", "0"));

        }
    }
    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin");
        return yetkili;
    }

    [WebMethod]
    public static string Sonuc(string ilce,string sinif)
    {
        int donem = TestSeciliDonem.SeciliDonem().Id;

        OgrenciveSubeSayilariDb veriDb = new OgrenciveSubeSayilariDb();
        List<OgrenciveSubeSayilariModel> list = veriDb.OgrenciSayilari(donem,sinif.ToInt32(),ilce.ToInt32());

        return JsonConvert.SerializeObject(new{list});
    }
}