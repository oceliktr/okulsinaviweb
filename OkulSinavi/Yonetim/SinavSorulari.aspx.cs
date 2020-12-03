using System;
using System.Web.Services;
using Newtonsoft.Json;

public partial class OkulSinavi_CevrimiciSinavYonetim_SinavSorulari : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }
            if (Request.QueryString["OturumId"] != null)
            {
                if (Request.QueryString["OturumId"].IsInteger())
                {
                    int oturumId = Request.QueryString["OturumId"].ToInt32();

                    TestOturumlarDb veriDb = new TestOturumlarDb();
                    var info = veriDb.KayitBilgiGetir(oturumId);
                    hlOturum.NavigateUrl = "OturumYonetim.aspx?SinavId=" + info.SinavId;
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

    [WebMethod]
    public static string SoruSil(string SoruId)
    {
        int id = SoruId.ToInt32();
        JsonMesaj soList;
        if (YetkiKontrol())
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Bunun için yetkiniz yoktur.",
            };
            return JsonConvert.SerializeObject(soList);
        }
        TestSorularDb veriDb = new TestSorularDb();

        TestSorularInfo sonuc = veriDb.KayitBilgiGetir(id);
        if (sonuc.Id == 0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Kayıt bulunamadı.",
            };
            return JsonConvert.SerializeObject(soList);
        }

        int res = veriDb.KayitSil(id);
        if (res > 0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "ok",
                Mesaj = "Kayıt silindi.",
            };
            return JsonConvert.SerializeObject(soList);
        }

        soList = new JsonMesaj
        {
            Sonuc = "no",
            Mesaj = "Kayıt silinemedi.",
        };
        return JsonConvert.SerializeObject(soList);
    }
}