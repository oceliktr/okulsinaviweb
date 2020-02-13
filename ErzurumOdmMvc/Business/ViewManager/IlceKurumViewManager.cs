using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ErzurumOdmMvc.Areas.ODM.Model;
using ErzurumOdmMvc.Business.Abstract;

namespace ErzurumOdmMvc.Business.ViewManager
{
    public class IlceKurumViewManager:ManagerBase<IlceKurumViewModel>
    {
        public Task<IEnumerable<IlceKurumViewModel>> IlceKurumlari(int ilceId, int kapali)
        {
            string sql = @"SELECT i.IlceAdi,k.* from kurumlar AS k 
                            INNER JOIN ilceler AS i ON i.Id = k.IlceId 
                            where k.IlceId=@Ilce and k.Kapali=@Kapali order by k.KurumAdi asc";
            Task<IEnumerable<IlceKurumViewModel>> result = QueryAsync(sql, new { Ilce = ilceId, Kapali = kapali });

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ilceId"></param>
        /// <param name="kapali">Açık olan kurumlar:0, Kapalı için: 1</param>
        /// <returns></returns>
        public Task<IEnumerable<IlceKurumViewModel>> IlceKurumlari(int ilceId,string tur, int kapali)
        {
            string sql = @"SELECT i.IlceAdi,k.* from kurumlar AS k 
                            INNER JOIN ilceler AS i ON i.Id = k.IlceId 
                            where k.IlceId=@Ilce and Tur=@Tur and k.Kapali=@Kapali order by k.KurumAdi asc";
            Task<IEnumerable<IlceKurumViewModel>> result = QueryAsync(sql, new { Ilce = ilceId,Tur=tur, Kapali = kapali });

            return result;
        }
    }
}