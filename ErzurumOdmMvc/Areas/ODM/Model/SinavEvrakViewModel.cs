using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc.Areas.ODM.Model
{
    public class SinavEvrakViewModel
    {

        public SinavEvrak SinavEvrak { get; set; }
        public List<KurumTurleri> KurumTurleri { get; set; }
        public IEnumerable<Kurum> SeciliKurumlar { get; set; }
    }
}