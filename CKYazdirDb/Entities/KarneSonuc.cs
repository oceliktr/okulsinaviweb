using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Entities
{
    [Table("KarneSonuclari")]
    public class KarneSonuc
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int BransId { get; set; }
        public string Ilce { get; set; }
        public int KurumKodu { get; set; }
        public int Sinif { get; set; }
        public string Sube { get; set; }
        public string KitapcikTuru { get; set; }
        public int SoruNo { get; set; }
        public int Dogru { get; set; }
        public int Yanlis { get; set; }
        public int Bos { get; set; }

        public KarneSonuc()
        {
            //
        }
        //ilce, bransId, kurumKodu, sinif, sube, kitapcikTuru, soruNo, dogru, yanlis, bos
        public KarneSonuc( string ilce,int bransId, int kurumKodu,int sinif, string sube, string kitapcikTuru, int soruNo, int dogru, int yanlis, int bos)
        {
            BransId = bransId;
            Ilce = ilce;
            KurumKodu = kurumKodu;
            Sinif = sinif;
            Sube = sube;
            KitapcikTuru = kitapcikTuru;
            SoruNo = soruNo;
            Dogru = dogru;
            Yanlis = yanlis;
            Bos = bos;
        }
        /// <summary>
        /// il ve ilçe ortalamalarını hesaplmak için kullanılan constructor 
        /// </summary>
        /// <param name="ilce"></param>
        /// <param name="bransId"></param>
        /// <param name="sinif"></param>
        /// <param name="kitapcikTuru"></param>
        /// <param name="soruNo"></param>
        /// <param name="dogru"></param>
        /// <param name="yanlis"></param>
        /// <param name="bos"></param>
        public KarneSonuc(string ilce, int bransId, int sinif, string kitapcikTuru, int soruNo, int dogru, int yanlis, int bos)
        {
            BransId = bransId;
            Ilce = ilce;
            Sinif = sinif;
            KitapcikTuru = kitapcikTuru;
            SoruNo = soruNo;
            Dogru = dogru;
            Yanlis = yanlis;
            Bos = bos;
        }
    }
}
