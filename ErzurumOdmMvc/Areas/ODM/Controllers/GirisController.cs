using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ErzurumOdmMvc.Areas.ODM.Model;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Common.Enums;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;
using ErzurumOdmMvc.Library;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    public class GirisController : Controller
    {
        // GET: ODM/Giris
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return RedirectToAction("Index", "Yonetim");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(GirisViewModel model, string returnurl)
        {
            //Response.Write(model.Password.MD5Sifrele());
            if (ModelState.IsValid)
            {
                var captchaResponse = Request.Form["g-recaptcha-response"];

                //localde CaptchaControl işlemini geç
                bool result = ReCaptcha.Validate(captchaResponse) || Request.IsLocal;

                if (result)
                {
                    string guid = StringIslemleri.YeniGuid();
                    KullaniciManager kullaniciManager = new KullaniciManager();
                    BusinessResult<Kullanici> kullanici = await kullaniciManager.Giris(model.KurumKodu, model.Password,guid);

                    //Aşağıdaki if komutu gönderilen mail ve şifre doğrultusunda kullanıcı kontrolu yapar. Eğer kullanıcı var ise login olur.
                    if (kullanici.Result != null)
                    {

                        FormsAuthentication.SetAuthCookie(guid, model.RememberMe);

                        CurrentSession.Set(kullanici.Result);


                        //Ana  sayfa önbelleği sil
                        HttpResponse.RemoveOutputCacheItem("/ODM");

                        if (string.IsNullOrEmpty(returnurl))
                            return RedirectToAction("Index", "Yonetim");
                        else
                            return Redirect(returnurl);
                    }
                    else
                    {
                        kullanici.Uyarilar.ForEach(x => ModelState.AddModelError("", x.Mesaj));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Güvenlik doğrulaması yapılamadı.");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult KullaniciKontrol(int kurumId, int kurumKodu, string giris)
        {
            KullaniciManager kullaniciManager = new KullaniciManager();

            int kontrol = kullaniciManager.KullaniciKontrol(kurumId, kurumKodu, giris);
            if (kontrol != 0)
            {
                return Json(new { Id = kontrol, Mesaj = "", Sonuc = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Id = 0, Mesaj = "Kurum ve kullanıcı eşleşmesi bulunamadı. Bilgilerin doğruluğundan eminseniz Ölçme Değerlendirme Merkezi ile irtibata geçiniz.", Sonuc = false }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult SifreDegistir(int id, string sifre)
        {
            KullaniciManager kullaniciManager = new KullaniciManager();
            Kullanici kullanici = kullaniciManager.Find(id);
            if (kullanici == null)
            {
                return Json(new { Id = 0, Mesaj = "Kullanıcı bilgisi bulunamadı.", Sonuc = false }, JsonRequestBehavior.AllowGet);
            }

            var kontrol = kullaniciManager.SifreDegistir(kullanici, sifre);
            if (kontrol)
            {
                return Json(new { Id = 0, Mesaj = "Şifreniz başarıyla değiştirildi. Yeni şifrenizle giriş yapınız.", Sonuc = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Id = 0, Mesaj = "Şifre değiştirilemedi. Lütfen daha sonra tekrar deneyiniz.", Sonuc = false }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Index", "Giris");
        }
    }
}