using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities.CKKarne
{
    [Dapper.Contrib.Extensions.Table("ckililceortalamasi")]
    public class CkIlIlceOrtalamasi
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int SinavId { get; set; }
        public string Ilce { get; set; }
        public int BransId { get; set; }
        public int Sinif { get; set; }
        public int KazanimId { get; set; }
        public int IlBasariYuzdesi { get; set; }
        public int IlceBasariYuzdesi { get; set; }
    }
}