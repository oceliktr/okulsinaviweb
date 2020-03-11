using System.Collections.Generic;
using System.Threading.Tasks;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Entities.CKKarne;

namespace ErzurumOdmMvc.Business.CkKarne
{
   public class CkKarneKazanimlarManager:ManagerBase<CkKarneKazanimlar>
    {
        public Task<IEnumerable<CkKarneKazanimlar>> SinavKazanimListesi(int sinavId)
        {
            string sql = "SELECT * FROM ckkarnekazanimlar WHERE SinavId=@SinavId";
            Task<IEnumerable<CkKarneKazanimlar>> result = QueryAsync(sql, new { SinavId = sinavId});

            return result;
        }
    }
}