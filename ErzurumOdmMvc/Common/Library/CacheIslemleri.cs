using System.Web;

namespace ErzurumOdmMvc.Common.Library
{
    public class CacheIslemleri
    {
        /// <summary>
        /// Actionların Cachelerini silmek için kullanılan metod
        /// </summary>
        /// <param name="urls">string tipinde dizi içerir</param>
        public void CacheReset(string[] urls)
        {
            foreach (var s in urls)
            {
                HttpResponse.RemoveOutputCacheItem(s);
            }
        }
    }
}
