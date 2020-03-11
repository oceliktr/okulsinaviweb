using ODM.CKYazdirDb.DAL;
using ODM.CKYazdirDb.Entities;

namespace ODM.CKYazdirDb.Business
{
    public class OgrenciCevapManager : ManagerBase<OgrenciCevabi>
    {
        private readonly Repository<OgrenciCevabi> repo = new Repository<OgrenciCevabi>();
        public void TumunuSil()
        {
            repo.DeleteAll("OgrenciCevaplari");
        }
    }
}
