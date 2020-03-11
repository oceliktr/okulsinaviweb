using ErzurumOdmMvc.Areas.ODM.Model;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Common.Enums;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;
using ErzurumOdmMvc.Filter;
using ErzurumOdmMvc.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    public class SinavEvrakController :Controller
    {

        [YetkiKontrol(Roles = "Root,OkulYetkilisi")]
        public ActionResult Index()
        {
            bool root = CurrentSession.Kullanici.Yetki.Contains(KullaniciSeviye.Root.ToString());

            string kurumKodu = CurrentSession.Kullanici.KurumKodu.ToString();

            SinavEvrakManager sinavEvrakManager = new SinavEvrakManager();
            //Root ise tümünü değil ise 
            Task<IEnumerable<SinavEvrak>> model = root
                ? sinavEvrakManager.SinavEvraklari()
                : sinavEvrakManager.SinavEvraklari(kurumKodu);

            return View(model);
        }

        [YetkiKontrol(Roles = "Root")]
        public ActionResult Yeni()
        {
            ViewBag.Title = "Yeni Evrak Kayıt Formu";

            KurumTurleri kurumTurleri = new KurumTurleri();
            SinavEvrakViewModel model = new SinavEvrakViewModel
            {
                SinavEvrak = new SinavEvrak(),
                KurumTurleri = kurumTurleri.TurleriListele(),
                SeciliKurumlar = new List<Kurum>()
            };

            return View("KayitFormu", model);
        }

        [YetkiKontrol(Roles = "Root")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(SinavEvrakViewModel data, FormCollection frm, HttpPostedFileBase updateFile)
        {
            ViewBag.Title = "Yeni Evrak Kayıt Formu";

            KurumTurleri kurumTurleri = new KurumTurleri();
            SinavEvrakViewModel model = new SinavEvrakViewModel
            {
                SinavEvrak = new SinavEvrak(),
                KurumTurleri = kurumTurleri.TurleriListele(),
                SeciliKurumlar = new List<Kurum>()
            };
            string seciliKurumlar = frm["secilikurumlar"];
            string tarih = frm["tarih"].Replace(" - ", "-");
            DateTime baslangicTarihi = tarih.Split('-')[0].ToDateTime();
            DateTime bitisTarihi = tarih.Split('-')[1].ToDateTime();

            string yuklemeAdresi = "/Upload/SinavEvrak/";
            DosyaResult dosya = DosyaYukle.Yukle(updateFile, yuklemeAdresi, false);
            if (dosya.Alert == "var") //Fotoğraf yüklemede hata varsa
            {
                ModelState.AddModelError("", dosya.Mesaj);
            }

            string url = dosya.Dosya ?? data.SinavEvrak.Url;

            if (string.IsNullOrEmpty(url))
            {
                ModelState.AddModelError("", "Dosya seçin veya url adresini giriniz.");
            }
            if (string.IsNullOrEmpty(seciliKurumlar))
            {
                ModelState.AddModelError("", "Kurum seçiniz.");
            }
            data.SinavEvrak.Kurumlar = seciliKurumlar;
            data.SinavEvrak.BaslangicTarihi = baslangicTarihi;
            data.SinavEvrak.BitisTarihi = bitisTarihi;
            data.SinavEvrak.Url = url;

            ModelState.Remove("SeciliKurumlar"); //bunu hata olarak görüyor ne alaka anlamadım 
            ModelState.Remove("SinavEvrak.Url");
            ModelState.Remove("SinavEvrak.Kurumlar");
            if (ModelState.IsValid)
            {

                SinavEvrakManager sinavEvrakManager = new SinavEvrakManager();
                data.SinavEvrak.Hit = 0;
                if (sinavEvrakManager.Insert(data.SinavEvrak) == 0)
                {
                    ModelState.AddModelError("", "Hata oldu. Yeni evrak kaydedilemedi.");
                    ViewBag.Uyari = "uyari";

                    return View("KayitFormu", model);
                }

                return RedirectToAction("Index");
            }

            ViewBag.Uyari = "uyari";

            return View("KayitFormu", model);
        }

        [YetkiKontrol(Roles = "Root")]
        public ActionResult Guncelle(int id)
        {
            SinavEvrakManager sinavEvrakManager = new SinavEvrakManager();
            SinavEvrak sinavEvrak = sinavEvrakManager.Find(id);
            if (sinavEvrak == null)
            {
                return HttpNotFound("Evrak bulunamadı.");
            }
            KurumManager kurumManager = new KurumManager();
            IEnumerable<Kurum> seciliKurumList = kurumManager.Kurumlar(sinavEvrak.Kurumlar, 0).Result;

            KurumTurleri kurumTurleri = new KurumTurleri();
            SinavEvrakViewModel model = new SinavEvrakViewModel
            {
                SinavEvrak = sinavEvrak,
                KurumTurleri = kurumTurleri.TurleriListele(),
                SeciliKurumlar = seciliKurumList
            };
            ViewBag.Title = "Evrak Güncelleme Formu";

            return View("KayitFormu", model);
        }

        [YetkiKontrol(Roles = "Root")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guncelle(SinavEvrakViewModel data, FormCollection frm, HttpPostedFileBase updateFile)
        {
            SinavEvrakManager sinavEvrakManager = new SinavEvrakManager();
            SinavEvrak sinavEvrak = sinavEvrakManager.Find(data.SinavEvrak.Id);
            if (sinavEvrak == null)
            {
                return HttpNotFound("Kayıt bulunamadı.");
            }

            ViewBag.Title = "Evrak Güncelleme Formu";
            KurumManager kurumManager = new KurumManager();
            IEnumerable<Kurum> seciliKurumList = kurumManager.Kurumlar(sinavEvrak.Kurumlar, 0).Result;

            KurumTurleri kurumTurleri = new KurumTurleri();
            SinavEvrakViewModel model = new SinavEvrakViewModel
            {
                SinavEvrak = sinavEvrak,
                KurumTurleri = kurumTurleri.TurleriListele(),
                SeciliKurumlar = seciliKurumList
            };

            string seciliKurumlar = frm["secilikurumlar"];
            string tarih = frm["tarih"].Replace(" - ", "-");
            DateTime baslangicTarihi = tarih.Split('-')[0].ToDateTime();
            DateTime bitisTarihi = tarih.Split('-')[1].ToDateTime();

            string yuklemeAdresi = "/Upload/SinavEvrak/";
            DosyaResult dosya = DosyaYukle.Yukle(updateFile, yuklemeAdresi, false);
            if (dosya.Alert == "var") //Fotoğraf yüklemede hata varsa
            {
                ModelState.AddModelError("", dosya.Mesaj);
            }

            string url = dosya.Dosya ?? data.SinavEvrak.Url;

            if (string.IsNullOrEmpty(url))
            {
                ModelState.AddModelError("", "Dosya seçin veya url adresini giriniz.");
            }
            if (string.IsNullOrEmpty(seciliKurumlar))
            {
                ModelState.AddModelError("", "Kurum seçiniz.");
            }
            data.SinavEvrak.Kurumlar = seciliKurumlar;
            data.SinavEvrak.BaslangicTarihi = baslangicTarihi;
            data.SinavEvrak.BitisTarihi = bitisTarihi;
            data.SinavEvrak.Url = url;

            ModelState.Remove("SinavEvrak.Url");
            ModelState.Remove("SinavEvrak.Kurumlar");
            ModelState.Remove("SeciliKurumlar"); //bunu hata olarak görüyor ne alaka anlamadım 

            if (ModelState.IsValid)
            {

                sinavEvrak.Aciklama = data.SinavEvrak.Aciklama;
                sinavEvrak.BaslangicTarihi = data.SinavEvrak.BaslangicTarihi;
                sinavEvrak.BitisTarihi = data.SinavEvrak.BitisTarihi;
                sinavEvrak.Url = data.SinavEvrak.Url;
                sinavEvrak.Hit = data.SinavEvrak.Hit;
                sinavEvrak.Kurumlar = data.SinavEvrak.Kurumlar;

                if (!sinavEvrakManager.Update(sinavEvrak))
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

        [YetkiKontrol(Roles = "Root")]
        [HttpPost]
        public JsonResult Sil(int id)
        {
            SinavEvrakManager sinavEvrakManager = new SinavEvrakManager();
            var sinavEvrak = sinavEvrakManager.FindAsync(id).Result;
            if (sinavEvrak == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Sınav evrak bilgisi bulunamadı." });
            }

            if (sinavEvrakManager.DeleteAsync(sinavEvrak).Result)
            {
                return Json(new { Sonuc = true, Mesaj = "Kayıt silindi" });
            }
            return Json(new { Sonuc = false, Mesaj = "Kayıt silinemedi." });
        }

        [YetkiKontrol(Roles = "Root,OkulYetkilisi")]
        //[HttpPost]
        public ActionResult Indir(int id)
        {
            SinavEvrakManager sinavEvrakManager = new SinavEvrakManager();
            var sinavEvrak = sinavEvrakManager.FindAsync(id).Result;
            if (sinavEvrak == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Sınav evrak bilgisi bulunamadı." });
            }

            if (!CurrentSession.Kullanici.Yetki.Contains("Root"))
            {
                string kurumKodu = CurrentSession.Kullanici.KurumKodu.ToString();

                if (!sinavEvrak.Kurumlar.Contains(kurumKodu))
                {
                    return Json(new { Sonuc = false, Mesaj = "Bu evrak için indirme yetkiniz bulunmamaktadır." });
                }
            }

            
            return Json(new { Sonuc = true, Mesaj = "Dosya indiriliyor." },JsonRequestBehavior.AllowGet);
        }

        [YetkiKontrol(Roles = "Root,OkulYetkilisi")]
        public ActionResult EvrakDown(int id)
        {
            SinavEvrakManager sinavEvrakManager = new SinavEvrakManager();
            var sinavEvrak = sinavEvrakManager.FindAsync(id).Result;
            sinavEvrak.Hit++;
            sinavEvrakManager.UpdateAsync(sinavEvrak);
            string filePath = Server.MapPath(sinavEvrak.Url);
            string uzanti = Path.GetExtension(filePath);

            //Set the New File name.
            string newFileName = StringIslemleri.RastgeleMetinUret(8) + uzanti;

            if (uzanti != null && uzanti.Contains("pdf"))
                Response.ContentType = "application/pdf";
            if (uzanti != null && uzanti.Contains("doc"))
                Response.ContentType = "application/msword";
            if (uzanti != null && uzanti.Contains("xls"))
                Response.ContentType = "application/vnd.ms-excel";
            if (uzanti != null && (uzanti.Contains("jpg") || uzanti.Contains("jpeg")))
                Response.ContentType = "image/jpeg";
            if (uzanti != null && (uzanti.Contains("rar") || uzanti.Contains("zip")))
                Response.ContentType = "application/x-compressed";
            if (uzanti != null && uzanti.Contains("psd"))
                Response.ContentType = "application/octet-stream";

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + newFileName);

            //    //Writing the File to Response Stream.
            Response.WriteFile(filePath);
            Response.Flush();
            Response.End();

            return View("Indir");
        }
    }
}