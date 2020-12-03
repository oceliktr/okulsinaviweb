using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TestOgrPuan
/// </summary>
public class TestOkulPuanInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int KurumKodu { get; set; }
    public int BransId { get; set; }
    public int Dogru { get; set; }
    public int Yanlis { get; set; }
    public int Bos { get; set; }

    public TestOkulPuanInfo()
    {
        
    }

    public TestOkulPuanInfo(int id, int sinavId, int kurumKodu, int bransId, int dogru, int yanlis, int bos)
    {
        Id = id;
        SinavId = sinavId;
        KurumKodu = kurumKodu;
        BransId = bransId;
        Dogru = dogru;
        Yanlis = yanlis;
        Bos = bos;
    }
}