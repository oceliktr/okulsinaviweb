using System;

/// <summary>
/// Summary description for TestCevapInfo
/// </summary>
public class TestOgrCevapInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int OturumId { get; set; }
    public string OpaqId { get; set; }
    public string Cevap { get; set; }
    public int Dogru { get; set; }
    public int Yanlis { get; set; }
    public int Bitti { get; set; }
    public DateTime Baslangic { get; set; }
    public DateTime? Bitis { get; set; }
    public DateTime SonIslem { get; set; }

    public TestOgrCevapInfo()
    {
        
    }
    public TestOgrCevapInfo(int id,int sinavId, int oturumId, string opaqId, string cevap, int dogru, int yanlis, DateTime baslangic,int bitti)
    {
        Id = id;
        SinavId = sinavId;
        OturumId = oturumId;
        OpaqId = opaqId;
        Cevap = cevap;
        Dogru = dogru;
        Yanlis = yanlis;
        Baslangic = baslangic;
        Bitti = bitti;
    }
    public TestOgrCevapInfo(string opaqId)
    {
        OpaqId = opaqId;
    }
}