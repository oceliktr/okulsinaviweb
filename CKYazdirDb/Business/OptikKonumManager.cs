using ODM.CKYazdirDb.Entities;

namespace ODM.CKYazdirDb.Business
{
    public class OptikKonumManager:ManagerBase<OptikKonum>
    {
        public OptikKonum OptikKonumlariGetir()
        {
            return this.Find(x => x.Id == 1);
        }
    }
}
