using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Services;

public partial class Sinav_DemoSinav : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Ogrenci"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            TestOgrenci ogrenci = (TestOgrenci)Session["Ogrenci"];
            if (ogrenci.OpaqId != "252525")
            {
                Response.Redirect("Default.aspx");
            }
            if (Request.QueryString["t"] != "")
            {
                if (Request.QueryString["t"].IsInteger())
                {
                    int id = Request.QueryString["t"].ToInt32();
                    TestOturumlarDb oturumlar = new TestOturumlarDb();
                    var oturum = oturumlar.KayitBilgiGetir(id);
                    if (ogrenci.OpaqId == "252525")
                    {
                        int soruSayisi = TestSoruSayisi(id);
                        HttpContext.Current.Session["SoruSayisi"] = soruSayisi;
                        ltrTestAdi.Text = oturum.SinavAdi;
                        ltrSoruSayisi.Text = soruSayisi.ToString();
                        ltrSure.Text = oturum.Sure.ToString();
                    }
                }
                else
                {
                    Response.Redirect("Sinavlar.aspx");
                }
            }
            else
            {
                Response.Redirect("Sinavlar.aspx");
            }
        }
    }

    [WebMethod]
    public static string SinavaBasla(int OturumId)
    {
        JsonSonucDemo soList;
        if (HttpContext.Current.Session["Ogrenci"] == null)
        {
            soList = new JsonSonucDemo
            {
                Sonuc = "no",
                Mesaj = "Oturum süreniz dolmuş. Tekrar deneyiniz.",
                KalanDakika = 0,
                KalanSaniye = 0,
                KalanSaat = 0
            };
            return JsonConvert.SerializeObject(soList);
        }

        int oturumSuresi = TestSuresi(OturumId, true);
        
        //ilk başlangıç süresi
        TimeSpan kalanSure = GenelIslemler.YerelTarih().AddMinutes(oturumSuresi).KalanSure();

        soList = new JsonSonucDemo
        {
            Mesaj = "Başarılı",
            Sonuc = "ok",
            KalanDakika = kalanSure.Minutes,
            KalanSaniye = kalanSure.Seconds,
            KalanSaat = kalanSure.Hours
        };

        return JsonConvert.SerializeObject(soList);

    }



    [WebMethod]
    public static string Bitir(int OturumId)
    {

        JsonSonucDemo soList = new JsonSonucDemo
        {
            Mesaj = "Demoda bu özellik kullanılmamaktadır.",
            Sonuc = "no",
            KalanDakika = 0,
            KalanSaniye = 0,
            KalanSaat = 0
        };
        return JsonConvert.SerializeObject(soList);

    }


    [WebMethod]
    public static string BransIlkSorusu(int OturumId, int BransId)
    {
        if (HttpContext.Current.Session["Ogrenci"] == null)
        {
            JsonSonucDemo soList = new JsonSonucDemo
            {
                Mesaj = "Oturum süreniz dolmuş.Tekrar deneyiniz.",
                Sonuc = "no",
                KalanDakika = 0,
                KalanSaniye = 0,
                KalanSaat = 0
            };
            return JsonConvert.SerializeObject(soList);
        }

        TestSorularDb sorularDb = new TestSorularDb();
        var sonuc = sorularDb.IlkSoruyuGetir(OturumId, BransId);
        if (sonuc == 0) //daha önce başlamamış ise oturume başlaması için uyar
        {
            JsonSonucDemo soList = new JsonSonucDemo
            {
                Mesaj = "Dersin sorularına ulaşılamadı.",
                Sonuc = "no",
            };
            return JsonConvert.SerializeObject(soList);
        }
        else
        {
            JsonSonucDemo soList = new JsonSonucDemo
            {
                Mesaj = sonuc.ToString(),
                Sonuc = "ok",
            };
            return JsonConvert.SerializeObject(soList);
        }
    }

    private static int TestSuresi(int oturumId, bool noSession)
    {
        if (noSession)
        {
            HttpContext.Current.Session["Sure"] = null;
        }
        int oturumSuresi;
        if (HttpContext.Current.Session["Sure"] == null)
        {
            TestOturumlarDb oturumDb = new TestOturumlarDb();
            var oturumInfo = oturumDb.KayitBilgiGetir(oturumId);
            oturumSuresi = oturumInfo.Sure;
        }
        else
        {
            oturumSuresi = HttpContext.Current.Session["Sure"].ToInt32();
        }

        return oturumSuresi;
    }

    private static int TestSoruSayisi(int oturumId)
    {
        TestSorularDb sorularDb = new TestSorularDb();
        int soruSayisi = sorularDb.SoruSayisi(oturumId);
        HttpContext.Current.Session["SoruSayisi"] = soruSayisi;

        return soruSayisi;
    }
}

public class JsonSonucDemo
{
    public string Sonuc { get; set; }
    public string Mesaj { get; set; }
    public int KalanSaat { get; set; }
    public int KalanDakika { get; set; }
    public int KalanSaniye { get; set; }
}