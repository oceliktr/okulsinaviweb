using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Areas.ODM.Model
{
    public class BransIstatistik
    {
        public int Id { get; set; }
        public int KullaniciSayisi { get; set; }
        public int LgsKazanimSayisi { get; set; }
        public int LgsSoruSayisi { get; set; }
    }
}