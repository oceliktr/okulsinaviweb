using ErzurumOdmMvc.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErzurumOdmMvc.Common.Library;

namespace ErzurumOdmMvc.Areas.ODM.Model
{
    public class KurumlarViewModel
    {
        public Task<IEnumerable<IlceKurumViewModel>> Kurumlar { get; set; }
        public IEnumerable<Ilce> Ilceler { get; set; }
    }
}