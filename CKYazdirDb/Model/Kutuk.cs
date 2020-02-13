using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb
{
    [Table("Kutuk")]
    public class Kutuk
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long OpaqId { get; set; }
        [StringLength(50)]
        public string IlAdi { get; set; }
        [StringLength(50)]
        public string IlceAdi { get; set; }
        public int KurumKodu { get; set; }
        [StringLength(80)]
        public string KurumAdi { get; set; }
        public int OgrenciNo { get; set; }
        [StringLength(50)]
        public string Adi { get; set; }
        [StringLength(50)]
        public string Soyadi { get; set; }
        public int Sinifi { get; set; }
        [StringLength(50)]
        public string Sube { get; set; }
        public int SinavId { get; set; }
        public int DersKodu { get; set; }
        [StringLength(50)]
        public string Barkod { get; set; }
        [StringLength(3)]
        public string KatilimDurumu { get; set; }
        [StringLength(3)]
        public string KitapcikTuru { get; set; }
        [StringLength(350)]
        public string Cevaplar { get; set; }

        public Kutuk(long opaqId, string ilceAdi, int kurumKodu, string kurumAdi, int ogrenciNo, string adi, string soyadi, int sinifi, string sube)
        {
            OpaqId = opaqId;
            IlceAdi = ilceAdi;
            KurumKodu = kurumKodu;
            KurumAdi = kurumAdi;
            OgrenciNo = ogrenciNo;
            Adi = adi;
            Soyadi = soyadi;
            Sinifi = sinifi;
            Sube = sube;
        }

        public Kutuk()
        {
        }
    }
}
