using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Model
{
  partial  class OpaqKontrol
    {
        public long OpaqId { get; set; }
        public int Oturum { get; set; }

        public OpaqKontrol()
        {
            
        }

        public OpaqKontrol(long opaqId, int oturum)
        {
            OpaqId = opaqId;
            Oturum = oturum;
        }
    }
}
