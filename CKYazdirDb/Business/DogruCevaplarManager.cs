using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.DAL;


namespace ODM.CKYazdirDb
{
   public class DogruCevaplarManager: ManagerBase<DogruCevap>
   {
        private readonly Repository<DogruCevap> repo = new Repository<DogruCevap>();
       public void TumunuSil()
       {
           repo.DeleteAll("Cevaplar");
       }
    }
}
