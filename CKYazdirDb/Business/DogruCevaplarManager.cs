using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.DAL;
using ODM.CKYazdirDb.Library;


namespace ODM.CKYazdirDb
{
   public class DogruCevaplarManager: ManagerBase<DogruCevap>
   {
        private readonly Repository<DogruCevap> repo = new Repository<DogruCevap>();
       public void TumunuSil()
       {
           repo.DeleteAll("DogruCevaplar");
       }

       public IEnumerable<DogruCevapveBrans> DogruCevaplarList()
       {
           BransManager brans = new BransManager();
           IEnumerable<DogruCevapveBrans> q1 = from kznm in List()
               join brns in brans.List() on kznm.BransId equals brns.Id
               select new DogruCevapveBrans
               {
                   Id = kznm.Id,
                   BransId = kznm.BransId,
                   Sinif = kznm.Sinif,
                   BransAdi = brns.BransAdi,
                   KitapcikTuru = kznm.KitapcikTuru,
                   Cevaplar = kznm.Cevaplar
               };
           return q1;
       }
    }
}
