using System;

public class TestKutukInfo
{
    public int Id { get; set; }
    public int DonemId { get; set; }
    public string OpaqId { get; set; }
    public string IlceAdi { get; set; }
    public int KurumKodu { get; set; }
    public string Adi { get; set; }
    public string Soyadi { get; set; }
    public int Sinifi { get; set; }
    public string Sube { get; set; }
    public string GirisKey { get; set; }
    public DateTime? SonGiris { get; set; }
    public TestKutukInfo()
    {
    }

    public TestKutukInfo(int kurumKodu)
    {
        KurumKodu = kurumKodu;
    }

    public TestKutukInfo(string opaqId, string adi, string soyadi, int sinifi, string sube)
    {
        OpaqId = opaqId;
        Adi = adi;
        Soyadi = soyadi;
        Sinifi = sinifi;
        Sube = sube;
    }

    public TestKutukInfo(int id, string opaqId, string ilceAdi, int kurumKodu,  string adi, string soyadi, int sinifi, string sube)
    {
        Id = id;
        OpaqId = opaqId;
        IlceAdi = ilceAdi;
        KurumKodu = kurumKodu;
        Adi = adi;
        Soyadi = soyadi;
        Sinifi = sinifi;
        Sube = sube;
    }
}