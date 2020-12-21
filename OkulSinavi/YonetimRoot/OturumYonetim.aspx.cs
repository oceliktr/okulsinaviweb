using Newtonsoft.Json;
using System;
using System.Web.Services;

public partial class Okul_SinaviYonetim_OturumYonetimRoot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }

            int sinavId = 0;
            if (Request.QueryString["SinavId"] != null)
            {
                sinavId = Request.QueryString["SinavId"].ToInt32();
            }

            TestSinavlarDb sinavDb = new TestSinavlarDb();
            var sinav = sinavDb.KayitBilgiGetir(sinavId);
            ltrSinavAdi.Text = sinav.SinavAdi;

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
    public static string OturumSil(string OturumId)
    {
        int id = OturumId.ToInt32();
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
        TestOturumlarDb veriDb = new TestOturumlarDb();

        TestOturumlarInfo sonuc = veriDb.KayitBilgiGetir(id);
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