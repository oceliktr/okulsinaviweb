
public class OgrenciPuanIlceJoinModel
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int KurumKodu { get; set; }
    public string OpaqId { get; set; }
    public int Dogru { get; set; }
    public int Yanlis { get; set; }
    public int Bos { get; set; }
    public decimal Puan { get; set; }
    public string IlceAdi { get; set; }


    public OgrenciPuanIlceJoinModel(int id, int sinavId, int kurumKodu, string opaqId, int dogru, int yanlis, int bos, decimal puan, string ilceAdi)
    {
        Id = id;
        SinavId = sinavId;
        KurumKodu = kurumKodu;
        OpaqId = opaqId;
        Dogru = dogru;
        Yanlis = yanlis;
        Bos = bos;
        Puan = puan;
        IlceAdi = ilceAdi;
    }
}