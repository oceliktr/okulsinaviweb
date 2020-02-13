using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ErzurumOdmMvc.Business.ViewManager;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    public class BransController : Controller
    {
        // GET: ODM/Brans
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Branslar()
        {
            BransManager bransManager = new BransManager();
            Task<IEnumerable<Brans>> model = bransManager.Branslar();
            return PartialView("_Branslar", model);
        }

        [HttpPost]
        public JsonResult Yeni(Brans model)
        {
            BransManager bransManager = new BransManager();
            Brans brans = new Brans
            {
                BransAdi = model.BransAdi
            };
            if (bransManager.InsertAsync(brans).Result > 0)
            {
                return Json(new { Sonuc = true, Mesaj = "Yeni branş başarıyla eklendi." });
            }
            return Json(new { Sonuc = false, Mesaj = "Yeni branş eklenemedi." });
        }
        [HttpPost]
        public JsonResult Duzenle(Brans model)
        {
            BransManager bransManager = new BransManager();
            var brans = bransManager.FindAsync(model.Id).Result;
            if (brans == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Branş bilgisi bulunamadı." });
            }

            brans.BransAdi = model.BransAdi;

            if (bransManager.UpdateAsync(brans).Result)
            {
                return Json(new { Sonuc = true, Mesaj = "Branş bilgisi güncellendi." });
            }
            return Json(new { Sonuc = false, Mesaj = "Branş bilgisi güncellenemedi." });
        }
        [HttpPost]
        public JsonResult Getir(int id)
        {
            BransManager bransManager = new BransManager();
            var brans = bransManager.FindAsync(id).Result;
            if (brans == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Branş bilgisi bulunamadı." });
            }

            return Json(new { Sonuc = true, Mesaj = "", BransInfo = brans });
        }
        [HttpPost]
        public JsonResult Sil(int id)
        {
            BransManager bransManager = new BransManager();
            var brans = bransManager.FindAsync(id).Result;
            if (brans == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Brans bilgisi bulunamadı." });
            }
            BransIstatistikManager bransIstatistikManager = new BransIstatistikManager();
            var bransIstatistik = bransIstatistikManager.BransIstatistik(id);
            if (bransIstatistik .KullaniciSayisi> 0)
            {
                return Json(new { Sonuc = false, Mesaj = $"Bu branşta {bransIstatistik.KullaniciSayisi} kullanıcı bulunduğundan silinemedi." });
            }
            if (bransIstatistik.LgsKazanimSayisi > 0)
            {
                return Json(new { Sonuc = false, Mesaj = $"Bu branşta LGS Soru Bankasında {bransIstatistik.LgsKazanimSayisi} adet kazanım bilgisi bulunduğundan silinemedi." });
            }
            if (bransIstatistik.LgsSoruSayisi > 0)
            {
                return Json(new { Sonuc = false, Mesaj = $"Bu branşta LGS Soru Bankasında {bransIstatistik.LgsSoruSayisi} adet soru bulunduğundan silinemedi." });
            }
            if (bransManager.DeleteAsync(brans).Result)
            {
                return Json(new { Sonuc = true, Mesaj = "Brans silindi" });
            }
            return Json(new { Sonuc = false, Mesaj = "Brans silinemedi." });
        }
    }
}