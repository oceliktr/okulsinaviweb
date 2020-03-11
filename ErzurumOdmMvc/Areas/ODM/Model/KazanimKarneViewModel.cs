using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErzurumOdmMvc.Common.Enums;
using ErzurumOdmMvc.Entities;
using ErzurumOdmMvc.Entities.CKKarne;

namespace ErzurumOdmMvc.Areas.ODM.Model
{
    public class KazanimKarneViewModel
    {
        public IEnumerable<CkSinavAdi> CkSinavlar { get; set; }
        public IEnumerable<CkKarneKutuk> Ilceler { get; set; }
        public IEnumerable<CkKarneKutuk> CkKurumlar { get; set; }
    }
}