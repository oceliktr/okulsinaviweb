using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Model
{
   public class KazanimveBrans
    {
        public int Id { get; set; }
        public int Sinif { get; set; }

        public int BransId { get; set; }
        public string BransAdi { get; set; }
        public string KazanimNo { get; set; }
        public string KazanimAdi { get; set; }
        public string KazanimAdiOgrenci { get; set; }
        public string Sorulari { get; set; }
    }
}
