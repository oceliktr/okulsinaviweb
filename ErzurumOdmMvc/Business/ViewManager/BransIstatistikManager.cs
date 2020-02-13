using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErzurumOdmMvc.Areas.ODM.Model;
using ErzurumOdmMvc.Business.Abstract;

namespace ErzurumOdmMvc.Business.ViewManager
{
    public class BransIstatistikManager:ManagerBase<BransIstatistik>
    {
        public BransIstatistik BransIstatistik(int bransId)
        {
            string sql = @"SELECT b.Id,(SELECT count(k.Id) from kullanicilar AS k WHERE k.Bransi=b.Id) AS KullaniciSayisi, 
                        (SELECT count(lk.Id) from lgskazanimlar AS lk WHERE lk.BransId=b.Id) AS LgsKazanimSayisi, 
                        (SELECT count(ls.Id) from lgssorular AS ls WHERE ls.BransId=b.Id) AS LgsSoruSayisi
                        FROM branslar AS b WHERE b.Id=@bransId;";

            var res = Find(sql, new {BransId = bransId});

            return res;
        }
    }
}