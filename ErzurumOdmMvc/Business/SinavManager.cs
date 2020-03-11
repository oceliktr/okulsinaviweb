using System.Collections.Generic;
using System.Threading.Tasks;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc.Business
{
    public class SinavManager:ManagerBase<Sinav>
    {
        public Task<IEnumerable<Sinav>> Sinavlar()
        {
            string sql = "select * from sinavlar order by SinavAdi";
            Task<IEnumerable<Sinav>> result = QueryAsync(sql);

            return result;
        }
    }
}