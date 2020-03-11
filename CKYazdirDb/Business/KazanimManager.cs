using ODM.CKYazdirDb.DAL;
using ODM.CKYazdirDb.Entities;
using ODM.CKYazdirDb.Library;
using System.Collections.Generic;
using System.Linq;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb.Business
{
    public class KazanimManager : ManagerBase<Kazanim>
    {
        private readonly Repository<Kazanim> repo = new Repository<Kazanim>();
        /// <summary>
        /// Kazanımları ve branş adını birlikte gösterir
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KazanimveBrans> KazanimList()
        {
            BransManager brans = new BransManager();
            IEnumerable<KazanimveBrans> q1 = from kznm in List()
                                             join brns in brans.List() on kznm.BransId equals brns.Id
                                             select new KazanimveBrans
                                             {
                                                 Id = kznm.Id,
                                                 BransId = kznm.BransId,
                                                 Sinif = kznm.Sinif,
                                                 BransAdi = brns.BransAdi,
                                                 KazanimNo = kznm.KazanimNo,
                                                 KazanimAdi = kznm.KazanimAdi,
                                                 KazanimAdiOgrenci = kznm.KazanimAdiOgrenci,
                                                 Sorulari = kznm.Sorulari
                                             };
            return q1;
        }

        public void TumunuSil()
        {
            repo.DeleteAll("Kazanimlar");

        }
    }
}
