using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc.Areas.ODM.Model
{
    public class KullaniciBilgileriViewModel:Kullanici
    {
        public string IlceAdi { get; set; }
        public string KurumAdi { get; set; }
        public string BransAdi { get; set; }
    }
}