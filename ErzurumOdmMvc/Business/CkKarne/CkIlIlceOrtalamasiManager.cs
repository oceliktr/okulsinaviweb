using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Entities.CKKarne;

namespace ErzurumOdmMvc.Business.CkKarne
{
    public class CkIlIlceOrtalamasiManager:ManagerBase<CkIlIlceOrtalamasi>
    {
        public Task<IEnumerable<CkIlIlceOrtalamasi>> IlIlceOrtalamalari(int sinavId)
        {
            string sql = "SELECT * FROM ckililceortalamasi WHERE SinavId=@SinavId";
            Task<IEnumerable<CkIlIlceOrtalamasi>> result = QueryAsync(sql, new { SinavId = sinavId});

            return result;
        }
    }
}