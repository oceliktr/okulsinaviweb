using System.Collections.Generic;
using System.Threading.Tasks;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities.CKKarne;

namespace ErzurumOdmMvc.Business.CkKarne
{
    public class CkKarneLogManager:ManagerBase<CkKarneLog>
    {
        public Task<CkKarneLog> KullaniciLog(int kullaniciId,int sinavId,int kurumKodu)
        {
            string sql = "select * from ckkarnelog where KullaniciId=@KullaniciId and SinavId=@SinavId and KurumKodu=@KurumKodu";
            Task<CkKarneLog> result = FindAsync(sql, new { KullaniciId=kullaniciId, SinavId = sinavId, KurumKodu=kurumKodu });

            return result;
        }
        public Task<CkKarneLog> KullaniciLog(int kullaniciId, int sinavId, int kurumKodu, int sinif, int brans)
        {
            string sql = "select * from ckkarnelog where KullaniciId=@KullaniciId and SinavId=@SinavId and KurumKodu=@KurumKodu and Sinif=@Sinif and Brans=@Brans";
            Task<CkKarneLog> result = FindAsync(sql, new { KullaniciId = kullaniciId, SinavId = sinavId, KurumKodu = kurumKodu, Sinif = sinif, Brans = brans });

            return result;
        }
    }
}