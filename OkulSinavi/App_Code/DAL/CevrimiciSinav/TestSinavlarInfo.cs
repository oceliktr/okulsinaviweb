using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TestSinavlarInfo
/// </summary>
public class TestSinavlarInfo
{
    public int Id { get; set; }
    public int DonemId { get; set; }
    public int Sinif { get; set; }
    public int Puanlama { get; set; }
    public string SinavAdi { get; set; }
    public string Aciklama { get; set; }
    public string Kurumlar { get; set; }
    public int Aktif { get; set; }
    public int OturumTercihi { get; set; }
    public int BeklemeSuresi { get; set; }
    public TestSinavlarInfo()
    {
        
    }

    public TestSinavlarInfo(int id,int donemId, int sinif, int puanlama, string sinavAdi, string aciklama, int aktif, int oturumTercihi, int beklemeSuresi)
    {
        Id = id;
        DonemId = donemId;
        Sinif = sinif;
        Puanlama = puanlama;
        SinavAdi = sinavAdi;
        Aciklama = aciklama;
        Aktif = aktif;
        OturumTercihi = oturumTercihi;
        BeklemeSuresi = beklemeSuresi;
    }
}