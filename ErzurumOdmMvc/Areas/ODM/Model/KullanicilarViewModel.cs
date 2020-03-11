using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErzurumOdmMvc.Common.Enums;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc.Areas.ODM.Model
{
    public class KullanicilarViewModel
    {
        public Kullanici Kullanici { get; set; }
        public IEnumerable<Kullanici> Kullanicilar { get; set; }
        public IEnumerable<Ilce> Ilceler { get; set; }
        public IEnumerable<Brans> Branslar { get; set; }
        public IEnumerable<KullaniciSeviye> KullaniciYetkileri { get; set; }
    }
}