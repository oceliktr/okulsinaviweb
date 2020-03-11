using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Entities.CKKarne;

namespace ErzurumOdmMvc.Business.CkKarne
{
    public class CkKarneDogruCevaplarManager:ManagerBase<CkKarneDogruCevaplar>
    {
        public Task<IEnumerable<CkKarneDogruCevaplar>> DogruCevaplar(int sinavId)
        {
            string sql = "SELECT * FROM ckkarnedogrucevaplar WHERE SinavId=@SinavId";
            Task<IEnumerable<CkKarneDogruCevaplar>> result = QueryAsync(sql, new { SinavId = sinavId });

            return result;
        }
    }
}