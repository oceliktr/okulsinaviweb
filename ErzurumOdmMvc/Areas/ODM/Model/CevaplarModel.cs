namespace ErzurumOdmMvc
{
    public class CevaplarModel
    {
        public int BransId { get; set; }
        public string KitapcikTuru { get; set; }
        public string Cevaplar { get; set; }

        public CevaplarModel(int bransId, string kitapcikTuru, string cevaplar)
        {
            BransId = bransId;
            KitapcikTuru = kitapcikTuru;
            Cevaplar = cevaplar;
        }
    }
}