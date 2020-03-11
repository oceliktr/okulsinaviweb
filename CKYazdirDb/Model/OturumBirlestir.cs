using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Model
{
  public  class OturumBirlestir
    {
        public string OpaqId { get; set; }
        public int Oturum { get; set; }
        public string KitTur { get; set; }
        public string Cevaplar { get; set; }

        public OturumBirlestir(string opaqId, int oturum, string kitTur, string cevaplar)
        {
            OpaqId = opaqId;
            Oturum = oturum;
            KitTur = kitTur;
            Cevaplar = cevaplar;
        }
    }
}
