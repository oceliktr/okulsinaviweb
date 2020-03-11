using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.Entities;

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
