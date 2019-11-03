using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Library
{
   public class TableModel
    {
        public string No { get; set; }
        public string KazanimNo { get; set; }
        public string Kazanim { get; set; }
        public string IlPuani { get; set; }
        public string IlcePuani { get; set; }
        public string OkulPuani { get; set; }
        public string SubePuani { get; set; }
        public string Aciklama { get; set; }

        public TableModel(string no, string kazanimNo, string kazanim, string ilPuani, string ilcePuani, string okulPuani, string subePuani, string aciklama)
        {
            No = no;
            KazanimNo = kazanimNo;
            Kazanim = kazanim;
            IlPuani = ilPuani;
            IlcePuani = ilcePuani;
            OkulPuani = okulPuani;
            SubePuani = subePuani;
            Aciklama = aciklama;
        }

        public TableModel()
        {
        }
    }
}
