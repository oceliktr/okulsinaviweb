using ErzurumOdmMvc.Entities;
using ErzurumOdmMvc.Business;
using System.Web;

namespace ErzurumOdmMvc.Library
{
    public static class CurrentSession
    {
        public static Kullanici Kullanici => Get<Kullanici>();

        public static void Set<T>(T obj)
        {
            string key = "Kullanici";
            HttpContext.Current.Session[key] = obj;
            HttpContext.Current.Session.Timeout = 1440;//bir gün
        }

        public static T Get<T>()
        {
            string key = "Kullanici";

            if (HttpContext.Current.Session[key] != null)
            {
                return (T)HttpContext.Current.Session[key];
            }
            else if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                string girisKodu = HttpContext.Current.User.Identity.Name;

                KullaniciManager kullaniciManager = new KullaniciManager();
                Kullanici kontrol = kullaniciManager.KullaniciBilgisi(girisKodu);

                if (kontrol == null)
                    HttpContext.Current.Response.Redirect("~/ODM/Giris/Cikis");
                Set(kontrol);

                return (T)HttpContext.Current.Session[key];
            }

            return default(T);
        }

        public static void Remove()
        {
            string key = "Kullanici";
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}