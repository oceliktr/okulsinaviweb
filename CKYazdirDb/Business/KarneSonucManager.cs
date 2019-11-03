using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.DAL;
using ODM.CKYazdirDb.Model;

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
