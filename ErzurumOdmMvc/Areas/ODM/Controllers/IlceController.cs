using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc.Areas.ODM.Controllers
{
    public class IlceController : Controller
    {
        // GET: ODM/Ilceler
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public JsonResult Tumu()
        {
            IlceManager ilceManager = new IlceManager();

            IEnumerable<Ilce> ilceler = ilceManager.List().OrderBy(x=>x.IlceAdi);
            return Json(ilceler, JsonRequestBehavior.AllowGet);
        }
    }
}