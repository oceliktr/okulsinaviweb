namespace ODM.CKYazdirDb.Library
{
    public abstract class CevapveKutuk
    {
        public int SinavKodu { get; set; }
        public int OpaqId { get; set; }
        public int DersKodu { get; set; }
        public string KitapcikTuru { get; set; }
        public int Girmedi { get; set; }
        public string Cevaplar { get; set; }
        public int AsilYedek { get; set; }

        public CevapveKutuk()
        {

        }

        public CevapveKutuk(int sinavKodu, int opaqId, int dersKodu, string kitapcikTuru, int girmedi, string cevaplar, int asilYedek)
        {
            SinavKodu = sinavKodu;
            OpaqId = opaqId;
            DersKodu = dersKodu;
            KitapcikTuru = kitapcikTuru;
            Girmedi = girmedi;
            Cevaplar = cevaplar;
            AsilYedek = asilYedek;
        }

        public CevapveKutuk(int opaqId, int dersKodu, string kitapcikTuru, int girmedi, string cevaplar)
        {
            OpaqId = opaqId;
            DersKodu = dersKodu;
            KitapcikTuru = kitapcikTuru;
            Girmedi = girmedi;
            Cevaplar = cevaplar;
        }
    }
    public class Cevap
    {
        public string Cevaplar { get; set; }

        public Cevap()
        {

        }
        public Cevap(string cevaplar)
        {
            Cevaplar = cevaplar;
        }
    }
}
