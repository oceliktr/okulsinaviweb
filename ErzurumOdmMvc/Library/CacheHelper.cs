using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Entities;
using System.Web.Helpers;

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


        private static void Remove(string key)
        {
            WebCache.Remove(key);
        }



    }
}