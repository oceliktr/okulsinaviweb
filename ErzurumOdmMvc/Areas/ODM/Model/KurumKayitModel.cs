using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc.Areas.ODM.Model
{
    public class KurumKayitModel
    {
        public Kurum Kurum { get; set; }
        public IEnumerable<Ilce> Ilceler { get; set; }
        public List<KurumTurleri> KurumTurleri { get; set; }
    }
}