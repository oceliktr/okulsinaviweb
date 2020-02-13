using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Entities;
using ErzurumOdmMvc.Library;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErzurumOdmMvc.Filter
{
    public class YetkiKontrol : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var context = System.Web.HttpContext.Current;
           //Kullanıcı giriş yapmamışsa login sayfasına at
            if (!HttpContext.Current.Request.IsAuthenticated)
            {

                httpContext.Response.Redirect("~/ODM/Giris?returnurl="+ context.Request.Url.AbsolutePath);
            }

            //cookie'deki kullanıcı idsini alıyorum
            // string rolid = httpContext.User.Identity.Name;
            //idsini aldığım kullanıcıyı db'den çekiyorum
            // KullaniciManager kullaniciManager = new KullaniciManager();
            //  var user = kullaniciManager.FindAsync(rolid);
            Kullanici user = CurrentSession.Kullanici;
            if (user==null)
            {
                string girisKodu = httpContext.User.Identity.Name;

                KullaniciManager kullaniciManager = new KullaniciManager();
                user = kullaniciManager.KullaniciBilgisi(girisKodu);
                if (user == null)
                {
                    return false;
                }

                CurrentSession.Set(user);
            }

            //Controller veya Actiondan gelen yetkiler. 
            var roles = Roles.Split(',');
            //Root,Admin,Editor,Musteri olabilir

            foreach (var role in roles)
            {
                //Kullanıcının yetkileerinde actiondan istenen yetki varsa
                if (user.Yetki.Contains(role))
                {
                        return true;
                }
               
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}