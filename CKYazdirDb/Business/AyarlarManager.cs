using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ODM.CKYazdirDb
{
   public class AyarlarManager:ManagerBase<Ayarlar>
    {
        public Ayarlar AyarlariGetir()
        {
            return this.Find(x => x.Id == 1);
        }
    }
}
