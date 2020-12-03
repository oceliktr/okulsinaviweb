using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class OkulSinavi_CevrimiciSinavYonetim_SinavRapor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }

            int donem = TestSeciliDonem.SeciliDonem().Id;
            TestSinavlarDb veriDb = new TestSinavlarDb();
            ddlSinavlar.DataSource = veriDb.KayitlariGetir(donem);
            ddlSinavlar.DataTextField = "SinavAdi";
            ddlSinavlar.DataValueField = "Id";
            ddlSinavlar.DataBind();
            ddlSinavlar.Items.Insert(0, new ListItem("Sınav Seçiniz", ""));

            ddlSinavlar.Attributes.Add("onchange", "Sinav(this)");
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
    public static string KutukGirmeyenler(string tur,string sinif)
    {
        int donem = TestSeciliDonem.SeciliDonem().Id;
        KurumlarDb veriDb = new KurumlarDb();
        var result = veriDb.KutukGirmeyenKurumlar(donem,tur,sinif.ToInt32());

        return JsonConvert.SerializeObject(result);
    }
}