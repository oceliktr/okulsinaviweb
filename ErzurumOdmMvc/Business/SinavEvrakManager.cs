using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc.Business
{
    public class SinavEvrakManager:ManagerBase<SinavEvrak>
    {
        public Task<IEnumerable<SinavEvrak>> SinavEvraklari()
        {
            string sql = "select * from sinavevrak order by BitisTarihi asc";
            Task<IEnumerable<SinavEvrak>> lst = QueryAsync(sql);
            return lst;
        }
        public Task<IEnumerable<SinavEvrak>> SinavEvraklari(string kurumKodu)
        {
            string sql = "select * from sinavevrak where Kurumlar like @KurumKodu order by BitisTarihi asc";
            Task<IEnumerable<SinavEvrak>> lst = QueryAsync(sql, new { KurumKodu = "%" + kurumKodu + "%" });
            return lst;
        }
    }
}