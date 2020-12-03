using System;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Sinav_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    [WebMethod]
    public static string OgrenciGiris(string OpaqId)
    {
        string opaqIdStr = OpaqId.Md5Sifrele();
        TestKutukDb okullarDb = new TestKutukDb();
        var sonuc = okullarDb.KayitBilgiGetir(opaqIdStr);
        if (sonuc.Id == 0)
        {
            //##### LOG İŞLEMLERİ
            BrowserBilgisi browserBilgisi = new BrowserBilgisi();
            TestLogInfo logInfo = new TestLogInfo
            {
                OpaqId = opaqIdStr,
                Grup = "Başarısız Login",
                Rapor = browserBilgisi.Getir()
            };
            TestLogDb logDb = new TestLogDb();
            logDb.KayitEkle(logInfo);
            //##### LOG İŞLEMLERİ

            return "no"; //
        }
        else
        {
            string girisKey = Guid.NewGuid().ToString().Replace("-", "");

            okullarDb.GirisGuncelle(sonuc.OpaqId);//son giriş saatini kontrol et

            TestOgrenci ogrenci = new TestOgrenci
            {
                OpaqId = sonuc.OpaqId,
                KurumKodu = sonuc.KurumKodu,
                Adi = sonuc.Adi,
                Soyadi = sonuc.Soyadi,
                Sinifi = sonuc.Sinifi,
                IlceAdi = sonuc.IlceAdi,
                GirisKey = girisKey
            };
            HttpContext.Current.Session["Ogrenci"] = ogrenci;
            HttpContext.Current.Session.Timeout = 300;

            //##### LOG İŞLEMLERİ
            BrowserBilgisi browserBilgisi = new BrowserBilgisi();
            TestLogInfo logInfo = new TestLogInfo
            {
                OpaqId = sonuc.OpaqId,
                Grup = "Başarılı Login",
                Rapor = browserBilgisi.Getir()
            };
            TestLogDb logDb = new TestLogDb();
            logDb.KayitEkle(logInfo);
            //##### LOG İŞLEMLERİ

            CacheHelper.KullaniciGirisKeyYaz(sonuc.OpaqId, girisKey);

            return "ok";
        }
    }


}