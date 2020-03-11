using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Entities;
using ErzurumOdmMvc.Filter;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    [YetkiKontrol(Roles = "Root")]
    public class SinavController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sinavlar()
        {
            SinavManager sinavManager = new SinavManager();
            Task<IEnumerable<Sinav>> model = sinavManager.Sinavlar();
            return PartialView("_Sinavlar", model);
        }
       

        [HttpPost]
        public JsonResult Yeni(Sinav model)
        {
            SinavManager sinavManager = new SinavManager();
            Sinav sinav = new Sinav
            {
                SinavAdi = model.SinavAdi
            };
            if (sinavManager.InsertAsync(sinav).Result > 0)
            {
                return Json(new { Sonuc = true, Mesaj = "Yeni sınav başarıyla eklendi." });
            }
            return Json(new { Sonuc = false, Mesaj = "Yeni sınav eklenemedi." });
        }
        [HttpPost]
        public JsonResult Duzenle(Sinav model)
        {
            SinavManager sinavManager = new SinavManager();
            var sinav = sinavManager.FindAsync(model.Id).Result;
            if (sinav == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Sınav bilgisi bulunamadı." });
            }

            sinav.SinavAdi = model.SinavAdi;

            if (sinavManager.UpdateAsync(sinav).Result)
            {
                return Json(new { Sonuc = true, Mesaj = "Sınav bilgisi güncellendi." });
            }
            return Json(new { Sonuc = false, Mesaj = "Sınav bilgisi güncellenemedi." });
        }
        [HttpPost]
        public JsonResult Getir(int id)
        {
            SinavManager sinavManager = new SinavManager();
            var sinav = sinavManager.Find(id);
            if (sinav == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Sınav bilgisi bulunamadı." });
            }

            return Json(new { Sonuc = true, Mesaj = "", SinavInfo = sinav });
        }
        [HttpPost]
        public JsonResult Sil(int id)
        {
            SinavManager sinavManager = new SinavManager();
            var sinav =sinavManager.FindAsync(id).Result;
            if (sinav == null)
            {
                return Json(new { Sonuc = false, Mesaj = "Sınav bilgisi bulunamadı." });
            }
            LgsSoruManager lgsSoruManager = new LgsSoruManager();
            var soruKontrol = lgsSoruManager.SoruKontrol(id,true);
            if (soruKontrol)
            {
                return Json(new { Sonuc = false, Mesaj = $"Bu sınava ait sorular bulunduğundan silinemedi." });
            }
            
            return Json(new { Sonuc = false, Mesaj = "Sınav silinemedi." });
        }
    }
}