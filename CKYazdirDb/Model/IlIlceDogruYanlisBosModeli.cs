using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Model
{
   public class IlIlceDogruYanlisBosModeli
    {
        public int BransId { get; set; }
        public string Ilce { get; set; }
        public int Sinif { get; set; }
        public int KazanimId { get; set; }
        public int Dogru { get; set; }
        public int Yanlis { get; set; }
        public int Bos { get; set; }
        public int IlBasariYuzdesi { get; set; }
        public int IlceBasariYuzdesi { get; set; }

        public IlIlceDogruYanlisBosModeli()
        {
            //
        }

        public IlIlceDogruYanlisBosModeli(int bransId, string ilce, int sinif, int kazanimId, int dogru, int yanlis, int bos, int ilBasariYuzdesi, int ilceBasariYuzdesi)
        {
            BransId = bransId;
            Ilce = ilce;
            Sinif = sinif;
            KazanimId = kazanimId;
            Dogru = dogru;
            Yanlis = yanlis;
            Bos = bos;
            IlBasariYuzdesi = ilBasariYuzdesi;
            IlceBasariYuzdesi = ilceBasariYuzdesi;
        }
    }
}
