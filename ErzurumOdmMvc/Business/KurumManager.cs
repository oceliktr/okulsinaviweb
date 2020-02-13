using System.Collections.Generic;
using System.Threading.Tasks;
using ErzurumOdmMvc.Business;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc
{
   public class KurumManager:ManagerBase<Kurum>
    {
       

        public Task<IEnumerable<Kurum>> IlceKurumIdleri(int ilceId)
        {
            string sql = "select Id,KurumAdi,KurumKodu from kurumlar where IlceId=@Ilce order by KurumAdi asc"; //kapalı okullardan da kullanıcı olabilir
            Task<IEnumerable<Kurum>> result = QueryAsync(sql,new{Ilce=ilceId});

            return result;
        }

        public bool KurumKontrol(int id,int kurumKodu)
        {
            var resultList = Scalar("SELECT * FROM kurumlar WHERE Id<>@Id and KurumKodu=@KurumKodu", new { Id=id, KurumKodu = kurumKodu });

            return resultList.ToInt32() != 0;

        }
    }
}