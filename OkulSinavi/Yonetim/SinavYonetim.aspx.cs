using Newtonsoft.Json;
using System;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Okul_SinaviYonetim_SinavYonetim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
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
    public static string SinavSil(string SinavId)
    {
        int id = SinavId.ToInt32();
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
        TestSinavlarDb sinavDb = new TestSinavlarDb();
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        TestSinavlarInfo sonuc = sinavDb.KayitBilgiGetir(id, kInfo.KurumKodu.ToString());
        if (sonuc.Id == 0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Kayıt bulunamadı.",
            };
            return JsonConvert.SerializeObject(soList);
        }

        int res = sinavDb.KayitSil(id);
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