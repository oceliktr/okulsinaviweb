using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Entities.CKKarne;

namespace ErzurumOdmMvc.Business.CkKarne
{
    public class CkKarneSonuclariManager:ManagerBase<CkKarneSonuclari>
    {
        public Task<IEnumerable<CkKarneSonuclari>> KarneSonuclari(int sinavId, int kurumKodu)
        {
            string sql = "SELECT * FROM ckkarnesonuclari WHERE SinavId=@SinavId and KurumKodu=@KurumKodu";
            Task<IEnumerable<CkKarneSonuclari>> result = QueryAsync(sql, new { SinavId = sinavId, KurumKodu = kurumKodu });

            return result;
        }

    }
}