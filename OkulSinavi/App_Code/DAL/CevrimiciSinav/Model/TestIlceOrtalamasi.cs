using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IlceOrtalamasi
/// </summary>
public class TestIlceOrtalamasi
{
    public string IlceAdi { get; set; }
    public int OgrenciSayisi { get; set; }
    public double DogruOrtalamasi { get; set; }
    public double YanlisOrtalamasi { get; set; }
    public decimal PuanOrtalamasi { get; set; }

    public TestIlceOrtalamasi(string ilceAdi, int ogrenciSayisi, double dogruOrtalamasi, double yanlisOrtalamasi, decimal puanOrtalamasi)
    {
        IlceAdi = ilceAdi;
        OgrenciSayisi = ogrenciSayisi;
        DogruOrtalamasi = dogruOrtalamasi;
        YanlisOrtalamasi = yanlisOrtalamasi;
        PuanOrtalamasi = puanOrtalamasi;
    }
}