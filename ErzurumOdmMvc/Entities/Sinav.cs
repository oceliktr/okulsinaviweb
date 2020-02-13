using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities
{
    [Dapper.Contrib.Extensions.Table("sinavlar")]
    public class Sinav
    {
        public int Id { get; set; }
        public string SinavAdi { get; set; }
        public string DonemAdi { get; set; }
        public string KitapcikTurleri { get; set; }
        public int SinavId { get; set; }
        public int VeriGirisi { get; set; }
    }
}