using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ErzurumOdmMvc.Areas.ODM.Model;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Business.ViewManager;
using ErzurumOdmMvc.Common.Enums;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;
using ErzurumOdmMvc.Filter;
using ErzurumOdmMvc.Library;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    public class KullaniciController : Controller
    {
        public ActionResult Index()
        {
            IlceManager ilceManager = new IlceManager();
            BransManager bransManager= new BransManager();
            KullaniciBilgileriViewManager kullaniciManager = new KullaniciBilgileriViewManager();
            
            KullanicilarViewModel model = new KullanicilarViewModel()
            {
                Ilceler = ilceManager.List(),
                Branslar = bransManager.List(),
                KullaniciYetkileri = Yetki.KullaniciYetkileri()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection frm)
        {
            int ilceId = frm["ilceler"].ToInt32();
            int kurumKodu = frm["okullar"].ToInt32();
            int bransId = frm["branslar"].ToInt32();
            string yetki = frm["yetki"];

            IlceManager ilceManager = new IlceManager();
            BransManager bransManager = new BransManager();
            KullaniciManager kullaniciManager = new KullaniciManager();
            Task<IEnumerable<Kullanici>> kullanicilar = kullaniciManager.Kullanicilar(ilceId,kurumKodu,bransId,yetki);

            KullanicilarViewModel model = new KullanicilarViewModel()
            {
                Ilceler = ilceManager.List(),
                Branslar = bransManager.List(),
                KullaniciYetkileri = Yetki.KullaniciYetkileri(),
                Kullanicilar =kullanicilar.Result
            };
            return View(model);
        }
        public ActionResult Bilgilerim()
        {
            if (CurrentSession.Kullanici == null)
            {
                return RedirectToAction("Index", "Default");
            }
            KullaniciBilgileriViewManager bilgilerimViewManager = new KullaniciBilgileriViewManager();
            KullaniciBilgileriViewModel model = bilgilerimViewManager.Bilgilerim(CurrentSession.Kullanici.TcKimlik).Result;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bilgilerim(KullaniciBilgileriViewModel model)
        {
            
            ModelState.Remove("Sifre");
            ModelState.Remove("TcKimlik");
            ModelState.Remove("Yetki");
            if (ModelState.IsValid)
            {
                KullaniciManager kullaniciManager = new KullaniciManager();
                Kullanici kullanici = CurrentSession.Kullanici;
                kullanici.AdiSoyadi = model.AdiSoyadi.ToTemizMetin();
                kullanici.Email = model.Email.ToTemizMetin();
                kullanici.CepTlf = model.CepTlf.ToTemizMetin();

                if (!string.IsNullOrEmpty(model.Sifre))
                    kullanici.Sifre = model.Sifre.ToTemizMetin().MD5Sifrele();

                kullaniciManager.Update(kullanici);
                CurrentSession.Set(kullanici);

                ModelState.AddModelError("", "Bilgileriniz güncellendi.");
                ViewBag.Uyari = "islemTamam";
            }
            else
            {
                ViewBag.Uyari = "uyari";
            }
            KullaniciBilgileriViewManager bilgilerimViewManager = new KullaniciBilgileriViewManager(); 
            model = bilgilerimViewManager.Bilgilerim(CurrentSession.Kullanici.TcKimlik).Result;
            return View(model);
        }

        public ActionResult KullaniciBilgileri(string tcKimlik)
        {
            KullaniciBilgileriViewManager kullaniciManager = new KullaniciBilgileriViewManager();
            KullaniciBilgileriViewModel model = kullaniciManager.Bilgilerim(tcKimlik).Result;

            return View("_KullaniciBilgileri", model);
        }
        public ActionResult OkulListesi(int kurumKodu)
        {
            KullaniciBilgileriViewManager kullaniciManager = new KullaniciBilgileriViewManager();
             Task<IEnumerable<KullaniciBilgileriViewModel>> model = kullaniciManager.KurumunKullanicilari(kurumKodu);

            return View("_KullaniciListesi", model);
        }
    }
}