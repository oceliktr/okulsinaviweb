using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.DAL;
using ODM.CKYazdirDb.Model;

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
