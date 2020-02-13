using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.DAL;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb.Business
{
   public class CkSablonManager: ManagerBase<CkSablon>
    {
        private readonly Repository<CkSablon> repo = new Repository<CkSablon>();
        public void TumunuSil()
        {
            repo.DeleteAll("Sablonlar");
        }

    }
}
