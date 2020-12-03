using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TestIlcePuanInfo
/// </summary>
public class TestIlcePuanInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public string IlceAdi { get; set; }
    public int KurumKodu { get; set; }
    public int OgrSayisi { get; set; }
    public int Dogru { get; set; }
    public int Yanlis { get; set; }
    public int Bos { get; set; }
    public decimal Puan { get; set; }

    public TestIlcePuanInfo()
    {
        
    }

    public TestIlcePuanInfo(int id, int sinavId, string ilceAdi, int kurumKodu, int dogru, int yanlis, int bos, decimal puan)
    {
        Id = id;
        SinavId = sinavId;
        IlceAdi = ilceAdi;
        KurumKodu = kurumKodu;
        Dogru = dogru;
        Yanlis = yanlis;
        Bos = bos;
        Puan = puan;
    }
}