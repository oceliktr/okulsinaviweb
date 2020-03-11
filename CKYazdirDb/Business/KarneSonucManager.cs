using ODM.CKYazdirDb.DAL;
using ODM.CKYazdirDb.Entities;

namespace ODM.CKYazdirDb.Business
{
    public class KarneSonucManager:ManagerBase<KarneSonuc>
    {
        private readonly Repository<KarneSonuc> repo = new Repository<KarneSonuc>();
        public void TumunuSil()
        {
            repo.DeleteAll("KarneSonuclari");
        }
    }
}
