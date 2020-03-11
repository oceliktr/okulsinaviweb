using ErzurumOdmMvc.Areas.ODM.Model;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Business.ViewManager;
using ErzurumOdmMvc.Common.Enums;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;
using ErzurumOdmMvc.Library;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ErzurumOdmMvc.Entities.SoruBank;
using ErzurumOdmMvc.Filter;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    [YetkiKontrol(Roles = "Root,Admin")]
    public class KullaniciController : Controller
    {
        public ActionResult Index()
        {
            IlceManager ilceManager = new IlceManager();
            BransManager bransManager = new BransManager();

            KullanicilarViewModel model = new KullanicilarViewModel()
            {
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                Branslar = bransManager.List().OrderBy(x => x.BransAdi),
                KullaniciYetkileri = Yetki.KullaniciYetkileri(),
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
            Task<IEnumerable<Kullanici>> kullanicilar = kullaniciManager.Kullanicilar(ilceId, kurumKodu, bransId, yetki);

            KullanicilarViewModel model = new KullanicilarViewModel()
            {
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                Branslar = bransManager.List().OrderBy(x => x.BransAdi),
                KullaniciYetkileri = Yetki.KullaniciYetkileri(),
                Kullanicilar = kullanicilar.Result
            };
            return View(model);
        }

      

        public ActionResult Yeni()
        {
            ViewBag.Title = "Yeni Kullanıcı Kayıt Formu";

            IlceManager ilceManager = new IlceManager();
            BransManager bransManager = new BransManager();

            KullanicilarViewModel model = new KullanicilarViewModel()
            {
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                Branslar = bransManager.List().OrderBy(x => x.BransAdi),
                KullaniciYetkileri = Yetki.KullaniciYetkileri(),
                Kullanici = new Kullanici()
            };
            return View("_KayitFormu", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(KullanicilarViewModel model, FormCollection frm)
        {
            ViewBag.Title = "Yeni Kullanıcı Kayıt Formu";

            KullaniciManager kullaniciManager = new KullaniciManager();

            string kurumKodu = frm["okullar"];
            if (!kurumKodu.IsInteger())
            {
                ModelState.AddModelError("", "Kurumunu seçmediniz.");
                ViewBag.Uyari = "uyari";
            }
            if (string.IsNullOrEmpty(model.Kullanici.Sifre))
            {
                ModelState.AddModelError("", "Şifre girmediniz.");
                ViewBag.Uyari = "uyari";
            }
            string yetki = "";
            foreach (var y in Yetki.KullaniciYetkileri())
            {
                if (frm[y.ToString()] == "on")
                    yetki += y + "|";
            }


            Kullanici kullanici = model.Kullanici;
            kullanici.OncekiGiris = TarihIslemleri.YerelTarih();
            kullanici.SonGiris = TarihIslemleri.YerelTarih();
            kullanici.GirisSayisi = 0;
            kullanici.KurumKodu = kurumKodu.ToInt32();
            kullanici.Yetki = yetki;
            kullanici.Sifre =model.Kullanici.Sifre.MD5Sifrele();


            ModelState.Remove("Kullanici.Sifre");
            ModelState.Remove("Kullanici.Yetki");
            ModelState.Remove("Kullanici.Bransi");
            if (ModelState.IsValid)
            {
                if (kullaniciManager.KullaniciKontrol(0, model.Kullanici.TcKimlik))
                {
                    ModelState.AddModelError("", "Bu giriş bilgisi başka kullanı tarafndan kullanılıyor.");
                    ViewBag.Uyari = "uyari";
                }
                else
                {
                    kullaniciManager.Insert(kullanici);
                    ModelState.AddModelError("", "Kullanıcı kaydedildi.");

                    ViewBag.Uyari = "islemTamam";
                }
            }
            else
            {
                ViewBag.Uyari = "uyari";
            }


            IlceManager ilceManager = new IlceManager();
            BransManager bransManager = new BransManager();

            model = new KullanicilarViewModel()
            {
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                Branslar = bransManager.List().OrderBy(x => x.BransAdi),
                KullaniciYetkileri = Yetki.KullaniciYetkileri(),
                Kullanici = new Kullanici()
            };


            return View("_KayitFormu", model);
        }

        public ActionResult Guncelle(int id)
        {
            ViewBag.Title = "Kullanıcı Bilgi Düzenleme Formu";
            KullaniciManager kullaniciManager = new KullaniciManager();
            Kullanici kullanici = kullaniciManager.Find(id);
            if (kullanici == null)
            {
                return HttpNotFound("Kullanıcı bulunamadı");
            }

            IlceManager ilceManager = new IlceManager();
            BransManager bransManager = new BransManager();

            KullanicilarViewModel model = new KullanicilarViewModel()
            {
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                Branslar = bransManager.List().OrderBy(x => x.BransAdi),
                KullaniciYetkileri = Yetki.KullaniciYetkileri(),
                Kullanici = kullanici
            };
            return View("_KayitFormu", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guncelle(KullanicilarViewModel model, FormCollection frm)
        {
            ViewBag.Title = "Kullanıcı Bilgi Düzenleme Formu";
            KullaniciManager kullaniciManager = new KullaniciManager();
            Kullanici kullanici = kullaniciManager.Find(model.Kullanici.Id);
            if (kullanici == null)
            {
                return HttpNotFound("Kullanıcı bulunamadı");
            }

            string kurumKodu = frm["okullar"];
            if (!kurumKodu.IsInteger())
            {
                ModelState.AddModelError("", "Kurumunu seçmediniz.");
                ViewBag.Uyari = "uyari";
            }

            string yetki = "";
            foreach (var y in Yetki.KullaniciYetkileri())
            {
                if (frm[y.ToString()] == "on")
                    yetki += y + "|";
            }

            model.Kullanici.OncekiGiris = kullanici.OncekiGiris;
            model.Kullanici.SonGiris = kullanici.SonGiris;
            model.Kullanici.GirisSayisi = kullanici.GirisSayisi;
            model.Kullanici.GirisKodu = kullanici.GirisKodu;
            model.Kullanici.KurumKodu = kurumKodu.ToInt32();
            model.Kullanici.Yetki = yetki;

            model.Kullanici.Sifre = string.IsNullOrEmpty(model.Kullanici.Sifre)
                                    ? kullanici.Sifre : model.Kullanici.Sifre.MD5Sifrele();


            ModelState.Remove("Kullanici.Sifre");
            ModelState.Remove("Kullanici.Yetki");
            ModelState.Remove("Kullanici.Bransi");
            if (ModelState.IsValid)
            {
                if (kullaniciManager.KullaniciKontrol(model.Kullanici.Id, model.Kullanici.TcKimlik))
                {
                    ModelState.AddModelError("", "Bu giriş bilgisi başka kullanı tarafndan kullanılıyor.");
                    ViewBag.Uyari = "uyari";
                }
                else
                {
                    kullaniciManager.Update(model.Kullanici);
                    ModelState.AddModelError("", "Değişiklikler kaydedildi.");

                    ViewBag.Uyari = "islemTamam";
                }
            }
            else
            {
                ViewBag.Uyari = "uyari";
            }


            IlceManager ilceManager = new IlceManager();
            BransManager bransManager = new BransManager();

            model = new KullanicilarViewModel()
            {
                Ilceler = ilceManager.List().OrderBy(x => x.IlceAdi),
                Branslar = bransManager.List().OrderBy(x => x.BransAdi),
                KullaniciYetkileri = Yetki.KullaniciYetkileri(),
                Kullanici = model.Kullanici
            };


            return View("_KayitFormu", model);
        }

        [HttpPost]
        public JsonResult Sil(int id)
        {
            KullaniciManager kullaniciManager = new KullaniciManager();
            var kullanici = kullaniciManager.FindAsync(id).Result;
            if (kullanici == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Kullanıcı bilgisi bulunamadı." });
            }
            
            LgsSoruManager lgsSoruManager= new LgsSoruManager();
            if (lgsSoruManager.SoruKontrol(id))
            {
                return Json(new { Sonuc = false, Mesaj = "Kullanıcıya ait soru olduğu için silinemedi." });
            }
            if (kullaniciManager.DeleteAsync(kullanici).Result)
            {
                return Json(new { Sonuc = true, Mesaj = "Kullanıcı silindi" });
            }
            return Json(new { Sonuc = false, Mesaj = "Kullanıcı silinemedi." });
        }


        [AllowAnonymous]
        public ActionResult Bilgilerim()
        {
            if (CurrentSession.Kullanici == null)
            {
                return RedirectToAction("Index", "Yonetim");
            }
            KullaniciBilgileriViewManager bilgilerimViewManager = new KullaniciBilgileriViewManager();
            KullaniciBilgileriViewModel model = bilgilerimViewManager.Bilgilerim(CurrentSession.Kullanici.TcKimlik).Result;
            return View(model);
        }
        [AllowAnonymous]
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
                Kullanici kullanici = kullaniciManager.Find(CurrentSession.Kullanici.Id);
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