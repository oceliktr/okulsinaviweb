using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Library
{
    /// <summary>
    /// Sınava girmeyen, kitapçık türü işaretlenmemiş, optik formu gelmemiş öğrencilere ait bilgilerin tutulduğu sınıf
    /// </summary>
    public class KutukRapor
    {
        public long OpaqId { get; set; }

        public string IlceAdi { get; set; }
        public int KurumKodu { get; set; }
        public string KurumAdi { get; set; }
        public int OgrenciNo { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int Sinifi { get; set; }
        public string Sube { get; set; }
        public string KatilimDurumu { get; set; }
        public string KitapcikTuru { get; set; }
        public string Sonuc { get; set; }

        public KutukRapor(long opaqId, string ilceAdi, int kurumKodu, string kurumAdi, int ogrenciNo, string adi, string soyadi, int sinifi, string sube, string katilimDurumu, string kitapcikTuru, string sonuc)
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
            KatilimDurumu = katilimDurumu;
            KitapcikTuru = kitapcikTuru;
            Sonuc = sonuc;
        }

    }
}
