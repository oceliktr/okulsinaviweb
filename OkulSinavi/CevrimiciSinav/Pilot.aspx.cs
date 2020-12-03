using System;
using System.Web;
using System.Web.Services;

public partial class CevrimiciSinav_Pilot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string OgrenciGiris(string OpaqId)
    {
        if (!OpaqId.IsInteger64())
        {
            return "no1";
        }
        if (OpaqId.Length<=7)
        {
            return "no2";
        }
        TestKutukDb okullarDb = new TestKutukDb();
        var sonuc = okullarDb.OgrenciBilgiGetir(11111, OpaqId);
        if (sonuc.Id == 0)
        {
            TestKutukInfo info = new TestKutukInfo
            {
                OpaqId = OpaqId,
                KurumKodu = 111111,
                Adi = OpaqId,
                Soyadi = "Öğretmen",
                Sinifi = 8,
                Sube = "A",
                IlceAdi = "BÜYÜKŞEHİR"
            };
            okullarDb.KayitEkle(info);

            TestOgrenci ogrenci = new TestOgrenci
            {
                OpaqId = OpaqId,
                KurumKodu = 111111,
                Adi = OpaqId,
                Soyadi = "Öğretmen",
                Sinifi = 8,
                IlceAdi = "BÜYÜKŞEHİR"
            };
            HttpContext.Current.Session["Ogrenci"] = ogrenci;
            HttpContext.Current.Session.Timeout = 300;
            return "ok";
        }
        else
        {
            okullarDb.GirisGuncelle(sonuc.OpaqId);//son giriş saatini kontrol et

            TestOgrenci ogrenci = new TestOgrenci
            {
                OpaqId = sonuc.OpaqId,
                KurumKodu = sonuc.KurumKodu,
                Adi = sonuc.Adi,
                Soyadi = sonuc.Soyadi,
                Sinifi = sonuc.Sinifi,
                IlceAdi = sonuc.IlceAdi
            };
            HttpContext.Current.Session["Ogrenci"] = ogrenci;
            HttpContext.Current.Session.Timeout = 300;
            return "ok";
        }
    }

}