using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Model
{
    public class OgrenciSonucModel
    {
        public long OpaqId { get; set; }
        public string Ilce { get; set; }
        public int KurumKodu { get; set; }
        public string KurumAdi { get; set; }
        public int BransId { get; set; }
        public int Sinif { get; set; }
        public string Sube { get; set; }
        public string KitapcikTuru { get; set; }
        public int OgrenciNo { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string KatilimDurumu { get; set; }
        public int Dogru { get; set; }
        public int Yanlis { get; set; }
        public int Bos { get; set; }

        public OgrenciSonucModel()
        {
            
        }

        public OgrenciSonucModel(long opaqId, string ilce, int kurumKodu, string kurumAdi, int bransId, int sinif, string sube, string kitapcikTuru, int ogrenciNo, string adi, string soyadi, string katilimDurumu, int dogru, int yanlis, int bos)
        {
            OpaqId = opaqId;
            Ilce = ilce;
            KurumKodu = kurumKodu;
            KurumAdi = kurumAdi;
            BransId = bransId;
            Sinif = sinif;
            Sube = sube;
            KitapcikTuru = kitapcikTuru;
            OgrenciNo = ogrenciNo;
            Adi = adi;
            Soyadi = soyadi;
            KatilimDurumu = katilimDurumu;
            Dogru = dogru;
            Yanlis = yanlis;
            Bos = bos;
        }
    }
}
