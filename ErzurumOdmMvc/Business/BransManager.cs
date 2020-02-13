using System.Collections.Generic;
using System.Threading.Tasks;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc
{
    public class BransManager:ManagerBase<Brans>
    {
        public Task<IEnumerable<Brans>> Branslar()
        {
            string sql = "select * from branslar order by BransAdi";
            Task<IEnumerable<Brans>> result = QueryAsync(sql);

            return result;
        }
    }
}