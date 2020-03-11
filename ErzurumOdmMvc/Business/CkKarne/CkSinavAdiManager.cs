using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvc.Entities.CKKarne;

namespace ErzurumOdmMvc.Business.CkKarne
{
    public class CkSinavAdiManager : ManagerBase<CkSinavAdi>
    {
        public void AktifSinavlariPasifYap()
        {
            const string sql = @"update cksinavadi set Aktif=0";

            Execute(sql);
        }
        public CkSinavAdi SinavBilgisi(int sinavId)
        {
            const string sql = @"SELECT * FROM cksinavadi WHERE SinavId=@SinavId";

         return   Find(sql, new { SinavId = sinavId });
        }

        public int TumunuSil(int sinavId)
        {
            const string sql = @"DELETE FROM ckililceortalamasi WHERE SinavId=@SinavId;
                                DELETE FROM ckkarnebranslar WHERE SinavId=@SinavId;
                                DELETE FROM ckkarnedogrucevaplar WHERE SinavId=@SinavId;
                                DELETE FROM ckkarnekazanimlar WHERE SinavId=@SinavId;
                                DELETE FROM ckkarnekutuk WHERE SinavId=@SinavId;
                                DELETE FROM ckkarnesonuclari WHERE SinavId=@SinavId;
                                DELETE FROM cksinavadi WHERE SinavId=@SinavId";

          return  Execute(sql,new{SinavId=sinavId});
        }
    }
}