using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Entities;
using ErzurumOdmMvc.Filter;
using ErzurumOdmMvc.Library;
using System.Web;
using System.Web.Mvc;
using ErzurumOdmMvc.Common.Mail;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    //Tüm yetkiler : Root,Admin,IlceMEMYetkilisi,OkulYetkilisi,Ogretmen,SoruYazari,LgsYazari,LgsIlKomisyonu
    public class DefaultController : Controller
    {
        // GET: ODM/Default
        [YetkiKontrol(Roles = "Root,Admin,IlceMEMYetkilisi,OkulYetkilisi,Ogretmen,SoruYazari,LgsYazari,LgsIlKomisyonu")]
        public ActionResult Index()
        {
            return View();
        }

        [YetkiKontrol(Roles = "Root,Admin")]
        public ActionResult Ayarlar()
        {
            SiteAyarManager ayar = new SiteAyarManager();
            return View(ayar.SiteBilgi());
        }

        [YetkiKontrol(Roles = "Root,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ayarlar(SiteAyar model)
        {
            SiteAyarManager ayrYonetim = new SiteAyarManager();
            SiteAyar info = ayrYonetim.SiteBilgi();

            if (ModelState.IsValid)
            {
                info.Id = 1;
                info.SiteAdi = model.SiteAdi;
                info.SiteMail = model.SiteMail;
                info.MailServer = model.MailServer;
                info.MailPort = model.MailPort;
                info.MailAdresi = model.MailAdresi;
                info.MailSifresi = model.MailSifresi;
                info.MailGonderenIsim = model.MailGonderenIsim;
                info.AliciMailAdresi = model.AliciMailAdresi;

                ayrYonetim.Update(model);

                ModelState.AddModelError("", "Değişiklikler kaydedildi.");
                ViewBag.Uyari = "islemTamam";
                CacheHelper.SiteAyarKaldirFromCache();
            }
            else
            {
                ViewBag.Uyari = "uyari";
            }

            return View(info);
        }
        [YetkiKontrol(Roles = "Root,Admin")]
        public JsonResult TestMailiGonder(string eposta)
        {
            SiteAyarManager ayar = new SiteAyarManager();

            MailManager mail = new MailManager();
            MailResult res = mail.TestMaili(eposta, ayar.SiteBilgi());

            return Json(new { Sonuc = res.Sonuc, Mesaj = res.Mesaj }, JsonRequestBehavior.AllowGet);
        }

    }
}