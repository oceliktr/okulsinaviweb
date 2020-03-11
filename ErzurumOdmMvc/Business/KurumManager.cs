using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErzurumOdmMvc.Business
{
    public class KurumManager:ManagerBase<Kurum>
    {
       

        public Task<IEnumerable<Kurum>> IlceKurumIdleri(int ilceId)
        {
            string sql = "select Id,KurumAdi,KurumKodu from kurumlar where IlceId=@Ilce order by KurumAdi asc"; //kapalı okullardan da kullanıcı olabilir
            Task<IEnumerable<Kurum>> result = QueryAsync(sql,new{Ilce=ilceId});

            return result;
        }
        public Task<IEnumerable<Kurum>> Kurumlar(string tur)
        {
            string sql = "select Id,KurumAdi,KurumKodu from kurumlar where Tur=@Tur and Kapali=0 order by KurumAdi asc"; //kapalı okullar seçilmemeli
            Task<IEnumerable<Kurum>> result = QueryAsync(sql, new { Tur = tur });

            return result;
        }

        public Task<IEnumerable<Kurum>> Kurumlar(string kurumkodlari,int x)
        {
            string sql = $"select Id,KurumAdi,KurumKodu from kurumlar where KurumKodu in ("+ kurumkodlari+") order by KurumAdi asc"; //kapalı okullar seçilmemeli
            Task<IEnumerable<Kurum>> result = QueryAsync(sql);
            return result;
        }

        public bool KurumKontrol(int id,int kurumKodu)
        {
            var resultList = Scalar("SELECT * FROM kurumlar WHERE Id<>@Id and KurumKodu=@KurumKodu", new { Id=id, KurumKodu = kurumKodu });

            return resultList.ToInt32() != 0;

        }
    }
}