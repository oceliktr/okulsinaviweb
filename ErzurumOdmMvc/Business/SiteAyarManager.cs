using ErzurumOdmMvc.Business.Abstract;
using ErzurumOdmMvcDAL.Dapper;
using ErzurumOdmMvc.Entities;
using System.Linq;

namespace ErzurumOdmMvc.Business
{
    public class SiteAyarManager : ManagerBase<SiteAyar>
    {
        private readonly Repository<SiteAyar> repo = new Repository<SiteAyar>();
        public SiteAyar SiteBilgi()
        {
            return repo.List().FirstOrDefault(x => x.Id == 1);
        }

    }
}
