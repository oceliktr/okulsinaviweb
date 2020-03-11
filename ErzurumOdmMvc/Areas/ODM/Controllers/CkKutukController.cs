using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Business.CkKarne;
using ErzurumOdmMvc.Common.Enums;
using ErzurumOdmMvc.Entities.CKKarne;
using ErzurumOdmMvc.Filter;
using ErzurumOdmMvc.Library;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    public class CkKutukController : Controller
    {
        [HttpPost]
        public JsonResult IlceKurumlari(int sinavId)
        {
            CkKarneKutukManager kutukManager = new CkKarneKutukManager();

            //Admin root ve lgs il komisyonu için tüm ilçeler
            if (CurrentSession.Kullanici.Yetki.Contains(KullaniciSeviye.Root.ToString()) ||
                CurrentSession.Kullanici.Yetki.Contains(KullaniciSeviye.Admin.ToString()) ||
                CurrentSession.Kullanici.Yetki.Contains(KullaniciSeviye.LgsIlKomisyonu.ToString()))
            {
                IEnumerable<CkKarneKutuk> ilceler = kutukManager.SinavIlceleri(sinavId).Result;
                return Json(ilceler, JsonRequestBehavior.AllowGet);
            }
            //ilçe ve okul  için tüm kendi ilçeleri

            if (CurrentSession.Kullanici.Yetki.Contains(KullaniciSeviye.IlceMEMYetkilisi.ToString())|| CurrentSession.Kullanici.Yetki.Contains(KullaniciSeviye.OkulYetkilisi.ToString()))
            {
                IlceManager ilceManager= new IlceManager();
                var ilce = ilceManager.Find(CurrentSession.Kullanici.IlceId);
                List<CkKarneKutuk> ilceler = new List<CkKarneKutuk>();
                ilceler.Add(new CkKarneKutuk(){IlceAdi = ilce.IlceAdi});
                return Json(ilceler, JsonRequestBehavior.AllowGet);
            }
            return Json("İlçe listeleme yetkiniz yoktur.", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult SinifSinavlari(int sinavId)
        {
            CkKarneKutukManager kutukManager = new CkKarneKutukManager();

                IEnumerable<CkKarneKutuk> siniflar = kutukManager.SinavSiniflari(sinavId).Result;
                return Json(siniflar, JsonRequestBehavior.AllowGet);
            }
    }
}