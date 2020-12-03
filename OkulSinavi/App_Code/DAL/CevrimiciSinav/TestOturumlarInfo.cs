using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TestSinavAdiInfo
/// </summary>
public class TestOturumlarInfo
{
    public int Id { get; set; }
    public int SinavId { get; set; }
    public int SiraNo { get; set; }
    public int Sure { get; set; }
    public string OturumAdi { get; set; }
    public string Aciklama { get; set; }
    public DateTime BaslamaTarihi { get; set; }
    public DateTime BitisTarihi { get; set; }

    public TestOturumlarInfo(int id, int sinavId, int siraNo, int sure, string oturumAdi, string aciklama, DateTime baslamaTarihi, DateTime bitisTarihi)
    {
        Id = id;
        SinavId = sinavId;
        SiraNo = siraNo;
        Sure = sure;
        OturumAdi = oturumAdi;
        Aciklama = aciklama;
        BaslamaTarihi = baslamaTarihi;
        BitisTarihi = bitisTarihi;
    }

    public TestOturumlarInfo()
    {
    }
}