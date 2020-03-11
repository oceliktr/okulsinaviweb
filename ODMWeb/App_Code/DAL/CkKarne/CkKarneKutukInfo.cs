
    public class CkKarneKutukInfo
    {
        public int Id { get; set; }
        public int OpaqId { get; set; }
        public string IlAdi { get; set; }
        public string IlceAdi { get; set; }
        public int KurumKodu { get; set; }
        public string KurumAdi { get; set; }
        public int OgrenciNo { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int Sinifi { get; set; }
        public string Sube { get; set; }
        public int SinavId { get; set; }
        public int DersKodu { get; set; }
        public string KatilimDurumu { get; set; }
        public string KitapcikTuru { get; set; }
        public string Cevaplar { get; set; }


        public CkKarneKutukInfo()
        {
        
        }

        public CkKarneKutukInfo(int kurumKodu, string kurumAdi)
        {
            KurumKodu = kurumKodu;
            KurumAdi = kurumAdi;
        }
        public CkKarneKutukInfo(int id, int opaqId, string ilceAdi, int kurumKodu, string kurumAdi, int ogrenciNo, string adi, string soyadi, int sinifi, string sube, int sinavId, string katilimDurumu, string kitapcikTuru, string cevaplar)
        {
            Id = id;
            OpaqId = opaqId;
            IlceAdi = ilceAdi;
            KurumKodu = kurumKodu;
            KurumAdi = kurumAdi;
            OgrenciNo = ogrenciNo;
            Adi = adi;
            Soyadi = soyadi;
            Sinifi = sinifi;
            Sube = sube;
            SinavId = sinavId;
            KatilimDurumu = katilimDurumu;
            KitapcikTuru = kitapcikTuru;
            Cevaplar = cevaplar;
        }
    }
