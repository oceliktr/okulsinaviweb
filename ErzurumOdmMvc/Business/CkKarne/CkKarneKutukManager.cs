using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Entities.CKKarne;

namespace ErzurumOdmMvc.Business.CkKarne
{
    public class CkKarneKutukManager:ManagerBase<CkKarneKutuk>
    {
        public Task<IEnumerable<CkKarneKutuk>> SinavIlceleri(int sinavId)
        {
            string sql = "SELECT DISTINCT IlceAdi FROM ckkarnekutuk WHERE SinavId=@SinavId ORDER BY IlceAdi;"; 
            Task<IEnumerable<CkKarneKutuk>> result = QueryAsync(sql, new { SinavId = sinavId });

            return result;
        }

        public Task<IEnumerable<CkKarneKutuk>> SinavSiniflari(int sinavId)
        {
            string sql = "SELECT DISTINCT Sinifi FROM ckkarnekutuk WHERE SinavId=@SinavId ORDER BY Sinifi;"; 
            Task<IEnumerable<CkKarneKutuk>> result = QueryAsync(sql, new { SinavId = sinavId });

            return result;
        }

        public Task<IEnumerable<CkKarneKutuk>> SinavKurumlari(int sinavId,string ilceAdi,int sinif)
        {
            string sql = "SELECT DISTINCT KurumKodu,KurumAdi FROM ckkarnekutuk WHERE SinavId=@SinavId and IlceAdi=@IlceAdi and Sinifi=@Sinifi ORDER BY KurumAdi;";
            Task<IEnumerable<CkKarneKutuk>> result = QueryAsync(sql, new { SinavId = sinavId, IlceAdi=ilceAdi, Sinifi=sinif });

            return result;
        }

        public Task<IEnumerable<CkKarneKutuk>> SinavOkulListesi(int sinavId, int kurumkodu, int sinif)
        {
            string sql = "SELECT * FROM ckkarnekutuk WHERE SinavId=@SinavId and KurumKodu=@KurumKodu and Sinifi=@Sinifi ORDER BY Sube,Adi,Soyadi";
            Task<IEnumerable<CkKarneKutuk>> result = QueryAsync(sql, new { SinavId = sinavId, KurumKodu = kurumkodu, Sinifi = sinif });

            return result;
        }
    }
}