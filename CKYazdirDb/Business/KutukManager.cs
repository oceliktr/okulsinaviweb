using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.DAL;


namespace ODM.CKYazdirDb
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
