using System.Collections.Generic;
using System.Linq;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Entities;
using System.Web.Helpers;
using ErzurumOdmMvc.Business.CkKarne;
using ErzurumOdmMvc.Entities.CKKarne;

namespace ErzurumOdmMvc.Library
{
    public static class CacheHelper
    {
        public static SiteAyar SiteAyarFromChache()
        {
            SiteAyar result = WebCache.Get("site-ayar");
            if (result == null)
            {
                SiteAyarKaldirFromCache();

                SiteAyarManager siteAyarManager = new SiteAyarManager();
                result = siteAyarManager.SiteBilgi();

                WebCache.Set("site-ayar", result, 20, false);//false süre sıfırlanmasın
            }
            return result;
        }

        public static void SiteAyarKaldirFromCache()
        {
            Remove("site-ayar");
        }
        public static List<CkSinavAdi> CkSinavlarFromChache()
        {
            string key = "site-cksinavlar";
            List<CkSinavAdi> result = WebCache.Get(key);
            if (result == null)
            {
                Remove(key);

                CkSinavAdiManager ckSinavAdiManager = new CkSinavAdiManager();
                List<CkSinavAdi> list = ckSinavAdiManager.List().ToList();

                result = list;

                WebCache.Set(key, result, 360, true);//false süre sıfırlanmasın 360=6 saat
            }
            return result;
        }
        public static List<CkKarneBranslar> CkBranslarFromChache(int sinavId)
        {
            string key = "site-ckbranslar"+sinavId;
            List<CkKarneBranslar> result = WebCache.Get(key);
            if (result == null)
            {
                Remove(key);

                CkKarneBranslarManager ckBranslarManager = new CkKarneBranslarManager();
                List<CkKarneBranslar> list = ckBranslarManager.Branslar(sinavId).ToList();

                result = list;

                WebCache.Set(key, result, 360, true);//false süre sıfırlanmasın 360=6 saat
            }
            return result;
        }
        public static List<CkIlIlceOrtalamasi> CkIlIlceOrtalamalariFromChache(int sinavId)
        {
            string key = "site-ililceortalamalari" + sinavId;
            List<CkIlIlceOrtalamasi> result = WebCache.Get(key);
            if (result == null)
            {
                Remove(key);

                CkIlIlceOrtalamasiManager ckIlIlceOrtalamasiManager = new CkIlIlceOrtalamasiManager();
                List<CkIlIlceOrtalamasi> list = ckIlIlceOrtalamasiManager.IlIlceOrtalamalari(sinavId).Result.ToList();

                result = list;

                WebCache.Set(key, result, 360, true);//false süre sıfırlanmasın 360=6 saat
            }
            return result;
        }
        public static List<CkKarneDogruCevaplar> DogruCevaplarFromChache(int sinavId)
        {
            string key = "site-dogrucevaplar" + sinavId;
            List<CkKarneDogruCevaplar> result = WebCache.Get(key);
            if (result == null)
            {
                Remove(key);

                CkKarneDogruCevaplarManager ckKarneDogruCevaplarManager = new CkKarneDogruCevaplarManager();
                List<CkKarneDogruCevaplar> list = ckKarneDogruCevaplarManager.DogruCevaplar(sinavId).Result.ToList();

                result = list;

                WebCache.Set(key, result, 360, true);//false süre sıfırlanmasın 360=6 saat
            }
            return result;
        }
        public static List<CkKarneKazanimlar> CkKazanimlarFromChache(int sinavId)
        {
            List<CkKarneKazanimlar> result = WebCache.Get("site-ckkazanim-"+sinavId);
            if (result == null)
            {

                Remove("site-ckkazanim-" + sinavId);

                CkKarneKazanimlarManager kazanimDb = new CkKarneKazanimlarManager();
                List<CkKarneKazanimlar> kazanimList = kazanimDb.SinavKazanimListesi(sinavId).Result.ToList();
              
                result = kazanimList;

                WebCache.Set("site-ckkazanim-" + sinavId, result, 360, true);//false süre sıfırlanmasın 360=6 saat
            }
            return result;
        }
       
        private static void Remove(string key)
        {
            WebCache.Remove(key);
        }



    }
}