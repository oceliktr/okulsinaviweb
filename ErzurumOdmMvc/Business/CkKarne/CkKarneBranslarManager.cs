using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Entities.CKKarne;

namespace ErzurumOdmMvc.Business.CkKarne
{
    public class CkKarneBranslarManager:ManagerBase<CkKarneBranslar>
    {
        public CkKarneBranslar BransBilgisi(int sinavId, int bransId)
        {
            string sql = "SELECT * FROM ckkarnebranslar WHERE SinavId=@sinavId and BransId=@bransId";
            CkKarneBranslar res = Find(sql, new {SinavId = sinavId, BransId = bransId});

            return res;
        }
        public IEnumerable<CkKarneBranslar> Branslar(int sinavId)
        {
            string sql = "SELECT * FROM ckkarnebranslar WHERE SinavId=@sinavId ORDER BY sira";
            IEnumerable<CkKarneBranslar> res = Query(sql, new { SinavId = sinavId });

            return res;
        }
    }
}