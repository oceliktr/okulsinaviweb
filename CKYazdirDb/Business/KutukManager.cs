using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.DAL;
using ODM.CKYazdirDb.Entities;


namespace ODM.CKYazdirDb.Business
{
   public class KutukManager:ManagerBase<Kutuk>
    {
        private readonly Repository<Kutuk> repo = new Repository<Kutuk>();

        public void TumunuSil()
        {
            repo.DeleteAll("Kutuk");
            
        }
    }
}
