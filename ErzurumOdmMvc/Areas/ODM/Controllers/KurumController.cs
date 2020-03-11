using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ErzurumOdmMvc.Areas.ODM.Model;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Business.ViewManager;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;
using ErzurumOdmMvc.Filter;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    [YetkiKontrol(Roles = "Root")]
    public class KurumController : Controller
    {
        // GET: ODM/Kurum
        public ActionResult Index(int ilce=21, string tur="")
        {
            IlceManager ilceManager = new IlceManager();
            IlceKurumViewManager kurumManager = new IlceKurumViewManager();
            Task<IEnumerable<IlceKurumViewModel>> kurumlar = tur == "" ? kurumManager.IlceKurumlari(ilce, 0) : kurumManager.IlceKurumlari(ilce, tur, 0);
            KurumlarViewModel model = new KurumlarViewModel()
            {
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                Kurumlar = kurumlar
            };
            return View(model);
        }

        public ActionResult Yeni()
        {
            KurumTurleri kurumTurleri = new KurumTurleri();
            ViewBag.Title = "Yeni Kurum Kayıt Formu";

            IlceManager ilceManager = new IlceManager();
            KurumKayitModel model = new KurumKayitModel()
            {
                Kurum = new Kurum(),
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                KurumTurleri = kurumTurleri.Listele()
            };

            return View("KayitFormu",model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(KurumKayitModel data)
        {

            KurumTurleri kurumTurleri = new KurumTurleri();
            ViewBag.Title = "Yeni Kurum Kayıt Formu";

            IlceManager ilceManager = new IlceManager();
            KurumKayitModel model = new KurumKayitModel()
            {
                Kurum = new Kurum(),
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                KurumTurleri = kurumTurleri.Listele()
            };

            ModelState.Remove("Kurum.Tur");

            if (ModelState.IsValid)
            {
                KurumManager kurumManager= new KurumManager();
                if (kurumManager.KurumKontrol(0,data.Kurum.KurumKodu))
                {
                    ModelState.AddModelError("","Bu kurum kodunda bir kurum kayıtlı.");
                    ViewBag.Uyari = "uyari";

                    return View("KayitFormu", model);
                }
                var tur = kurumTurleri.Listele().FirstOrDefault(x => x.KurumTuru == data.Kurum.KurumTuru).Tur;
                data.Kurum.Tur = tur;
                if (kurumManager.Insert(data.Kurum) ==0)
                {
                    ModelState.AddModelError("", "Hata oldu. Yeni kurum kaydedilemedi.");
                    ViewBag.Uyari = "uyari";

                    return View("KayitFormu", model);
                }

                return RedirectToAction("Index");
            }

            ViewBag.Uyari = "uyari";

            return View("KayitFormu", model);
        }
        public ActionResult Guncelle(int id)
        {
            KurumTurleri kurumTurleri = new KurumTurleri();
            KurumManager kurumManager= new KurumManager();
            Kurum kurum = kurumManager.Find(id);
            if (kurum==null)
            {
                return HttpNotFound("Kurum bulunamadı.");
            }

            ViewBag.Title = $"{kurum.KurumAdi} Bilgi Güncelleme Formu";

            IlceManager ilceManager = new IlceManager();
            KurumKayitModel model = new KurumKayitModel()
            {
                Kurum = kurum,
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                KurumTurleri = kurumTurleri.Listele()
            };

            return View("KayitFormu", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guncelle(KurumKayitModel data)
        {
            KurumTurleri kurumTurleri = new KurumTurleri();
            KurumManager kurumManager = new KurumManager();
            Kurum kurum = kurumManager.Find(data.Kurum.Id);
            if (kurum == null)
            {
                return HttpNotFound("Kurum bulunamadı.");
            }

            ViewBag.Title = $"{kurum.KurumAdi} Bilgi Güncelleme Formu";

            IlceManager ilceManager = new IlceManager();
            KurumKayitModel model = new KurumKayitModel()
            {
                Kurum = kurum,
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                KurumTurleri = kurumTurleri.Listele()
            };
            ModelState.Remove("Kurum.Tur");
            if (ModelState.IsValid)
            {
                if (kurumManager.KurumKontrol(data.Kurum.Id, data.Kurum.KurumKodu))
                {
                    ModelState.AddModelError("", "Bu kurum kodunda başka bir kurum kayıtlı.");
                    ViewBag.Uyari = "uyari";

                    return View("KayitFormu", model);
                }

                var tur = kurumTurleri.Listele().FirstOrDefault(x => x.KurumTuru == data.Kurum.KurumTuru).Tur;

                kurum.IlceId = data.Kurum.IlceId;
                kurum.KurumKodu = data.Kurum.KurumKodu;
                kurum.KurumAdi = data.Kurum.KurumAdi;
                kurum.Email = data.Kurum.Email;
                kurum.KurumTuru = data.Kurum.KurumTuru;
                kurum.Tur = tur;
                kurum.Kapali = data.Kurum.Kapali;
              
                if (!kurumManager.Update(kurum))
                {
                    ModelState.AddModelError("", "Hata oldu. Değişiklikler kaydedilemedi.");
                    ViewBag.Uyari = "uyari";

                    return View("KayitFormu", model);
                }

                return RedirectToAction("Index");
            }

            ViewBag.Uyari = "uyari";//ModelState validation kontrollerini göstermek için

            return View("KayitFormu", model);
        }

        [HttpPost]
        public JsonResult Sil(int id)
        {
            KurumManager kurumManager = new KurumManager();
            var kurum = kurumManager.FindAsync(id).Result;
            if (kurum == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Kurum bilgisi bulunamadı." });
            }
            KullaniciManager kullaniciManager = new KullaniciManager();
            var kullanıci = kullaniciManager.KullaniciKontrol(kurum.KurumKodu);
            if (kullanıci) //kullanıcı varsa
            {
                return Json(new { Sonuc = false, Mesaj = "Bu kurumda kullanıcı bulunduğundan silinemedi." });
            }
           
            if (kurumManager.DeleteAsync(kurum).Result)
            {
                return Json(new { Sonuc = true, Mesaj = "Kurum silindi" });
            }
            return Json(new { Sonuc = false, Mesaj = "Kurum silinemedi." });
        }


        [AllowAnonymous]
        [HttpPost]
        public JsonResult IlceKurumlari(int ilce)
        {
            KurumManager kurumManager = new KurumManager();

            IEnumerable<Kurum> ilceler = kurumManager.IlceKurumIdleri(ilce).Result;
            return Json(ilceler, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult Kurumlar(string tur)
        {
            KurumManager kurumManager = new KurumManager();

            IEnumerable<Kurum> ilceler = kurumManager.Kurumlar(tur).Result;
            return Json(ilceler, JsonRequestBehavior.AllowGet);
        }
    }
}