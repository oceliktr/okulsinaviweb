using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Common.Library;
using ErzurumOdmMvc.Entities.SoruBank;

namespace ErzurumOdmMvc.Business
{
    public class LgsSoruManager:ManagerBase<LgsSoru>
    {
        //Kullanıcı LGS soru bankası çin soru yazmış mı?
        public bool SoruKontrol(int kullaniciId)
        {
            var resultList = Scalar("SELECT Id FROM lgssorular WHERE KullaniciId=@Id", new { Id = kullaniciId });

            return resultList.ToInt32() != 0;
        }
        public bool SoruKontrol(int sinavId,bool x)
        {
            var resultList = Scalar("SELECT Id FROM lgssorular WHERE SinavId=@Id", new { SinavId = sinavId });

            return resultList.ToInt32() != 0;
        }
    }
}