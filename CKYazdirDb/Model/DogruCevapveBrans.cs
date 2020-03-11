using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Model
{
  public  class DogruCevapveBrans
    {
        public int Id { get; set; }
        public int BransId { get; set; }
        public string BransAdi { get; set; }
        public int Sinif { get; set; }
        public string KitapcikTuru { get; set; }
        public string Cevaplar { get; set; }
    }
}
