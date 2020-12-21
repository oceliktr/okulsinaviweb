using System.Collections.Generic;
using System.Web.Helpers;

/// <summary>
/// Summary description for CacheHelper
/// </summary>
public class CacheHelper
{
    public static int AktifDonem()
    {
        string key = "aktif-donem";
        int result = WebCache.Get(key)==null?0:WebCache.Get(key);
        if (result == 0)
        {
            Remove(key);

            TestDonemDb veriDb = new TestDonemDb();
            result = veriDb.AktifDonem().Id;

            // öğeye her erişildiğinde önbellek öğesinin süresinin sona erdiğini belirtmek için true, öğenin önbelleğe eklendiğinden bu yana sürenin mutlak süreye uzatıldığını belirtmek için false olur.
            WebCache.Set(key, result, 20, false);//false süre sıfırlanmasın
        }
        return result;
    }
    public static List<BranslarInfo> SinavdakiBranslar(int oturumId)
    {
        string key = "sinav-branslar-" + oturumId;
        List<BranslarInfo> result = WebCache.Get(key);
        if (result == null)
        {
            Remove(key);

            BranslarDb veriDb = new BranslarDb();
            result = veriDb.SinavdakiBranslar(oturumId);

            // öğeye her erişildiğinde önbellek öğesinin süresinin sona erdiğini belirtmek için true, öğenin önbelleğe eklendiğinden bu yana sürenin mutlak süreye uzatıldığını belirtmek için false olur.
            WebCache.Set(key, result, 20, false);//false süre sıfırlanmasın
        }
        return result;
    }
    public static void SinavdakiBranslarKaldir(int oturumId)
    {
        string key = "sinav-branslar-" + oturumId;
        Remove(key);
    }

    public static void KullaniciGirisKeyYaz(string opaqId, string girisKey)
    {
        string key = "oturum-" + opaqId;
        Remove(key);

        WebCache.Set(key, girisKey, 300, false);//false süre sıfırlanmasın
    }
    public static string KullaniciGirisKontrol(string opaqId)
    {
        string key = "oturum-" + opaqId;

        return WebCache.Get(key);
    }

    public static List<BranslarInfo> Branslar()
    {
        string key = "branslar";
        List<BranslarInfo> result = WebCache.Get(key);
        if (result == null)
        {
            Remove(key);

            BranslarDb veriDb = new BranslarDb();
            result = veriDb.KayitlariDiziyeGetir();

            WebCache.Set(key, result, 20, false);//false süre sıfırlanmasın
        }
        return result;
    }
    public static void BranslarKaldir()
    {
        string key = "branslar";
        Remove(key);
    }

    public static List<TestSinavlarInfo> Sinavlar(string kurumKodu)
    {
        string key = "sinavlar-all-"+kurumKodu;
        List<TestSinavlarInfo> result = WebCache.Get(key);
        if (result == null)
        {
            Remove(key);

            TestSinavlarDb veriDb = new TestSinavlarDb();
            
            result = veriDb.TumSinavlar(kurumKodu);

            WebCache.Set(key, result, 20, false);//false süre sıfırlanmasın
        }
        return result;
    }
    public static void SinavlarKaldir(string kurumKodu)
    {
        string key = "sinavlar-all-" + kurumKodu;
        Remove(key);
    }

    public static List<TestOturumlarInfo> Oturumlar(int sinavId)
    {
        string key = "sinav-oturumlar-"+ sinavId;
        List<TestOturumlarInfo> result = WebCache.Get(key);
        if (result == null)
        {
            Remove(key);

            TestOturumlarDb veriDb = new TestOturumlarDb();
            result = veriDb.Oturumlar(sinavId);

            WebCache.Set(key, result, 20, false);//false süre sıfırlanmasın
        }
        return result;
    }
    public static void OturumlarKaldir(int sinavId)
    {
        string key = "sinav-oturumlar-" + sinavId;
        Remove(key);
    }


    public static List<TestSinavlarInfo> AktifSinavlar(string kurumKodu,int sinif)
    {
        string key = "aktif-sinavlar-" + sinif+"-"+kurumKodu;
        List<TestSinavlarInfo> result = WebCache.Get(key);
        if (result == null)
        {
            Remove(key);

            
            TestSinavlarDb veriDb = new TestSinavlarDb();
            result = veriDb.AktifSinavlar(sinif, kurumKodu);

            WebCache.Set(key, result, 20, false);//false süre sıfırlanmasın
        }
        return result;
    }
    public static void AktifSinavlarKaldir(string kurumKodu, int sinif)
    {
        string key = "aktif-sinavlar-" + sinif + "-" + kurumKodu;
        Remove(key);
    }

    public static List<TestSorularInfo> SorulariGetir(int oturumId)
    {
        string key = "oturum-" + oturumId;
        List<TestSorularInfo> result = WebCache.Get(key);
        if (result == null)
        {
            Remove(key);

            TestSorularDb veriDb = new TestSorularDb();
            result = veriDb.KayitlariDizeGetir(oturumId);

            WebCache.Set(key, result, 20, false);//false süre sıfırlanmasın
        }
        return result;
    }
    public static void SorulariGetirKaldir(int oturumId)
    {
        string key = "oturum-" + oturumId;
        Remove(key);
    }

    public static void Remove(string key)
    {
        WebCache.Remove(key);
    }
}