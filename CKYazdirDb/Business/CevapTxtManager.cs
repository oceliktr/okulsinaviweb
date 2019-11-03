using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.DAL;
using ODM.CKYazdirDb.Library;


namespace ODM.CKYazdirDb
{
    public class CevapTxtManager : ManagerBase<CevapTxt>
    {
        private readonly Repository<CevapTxt> repo = new Repository<CevapTxt>();
        public void TumunuSil()
        {
            repo.DeleteAll("CevapTxt");
        }
       
    }
}
