using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb.Library
{

    public static class DersKoduAdi
    {
        public static string DersAdi(this int bransId)
        {
            BransManager bransManager = new BransManager();
            Brans brans = bransManager.Find(x => x.Id == bransId);
            return brans.BransAdi;
        }
    }
}
