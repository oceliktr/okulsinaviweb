using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OkulGrafikViewModel
/// </summary>
public class OkulGrafikViewModel
{
    public int SinavId { get; set; }
    public int BransId { get; set; }
    public string Ilce { get; set; }
    public int KurumKodu { get; set; }
    public int Sinif { get; set; }
    public string Sube { get; set; }
    public string KitapcikTuru { get; set; }
    public int SoruNo { get; set; }
    public int Dogru { get; set; }
    public int Yanlis { get; set; }
    public int Bos { get; set; }
    public string SinavAdi { get; set; }
    public string BransAdi { get; set; }
    public decimal Net { get; set; }
    public decimal Ortalama { get; set; }
    public decimal Puan { get; set; }
    public int OgrenciSayisi { get; set; }

    public OkulGrafikViewModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public OkulGrafikViewModel(int sinavId, int bransId, int sinif, string sube, int dogru, int yanlis, int bos, string sinavAdi, string bransAdi, decimal net, decimal ortalama, decimal puan, int ogrenciSayisi)
    {
        SinavId = sinavId;
        BransId = bransId;
        Sinif = sinif;
        Sube = sube;
        Dogru = dogru;
        Yanlis = yanlis;
        Bos = bos;
        SinavAdi = sinavAdi;
        BransAdi = bransAdi;
        Net = net;
        Ortalama = ortalama;
        Puan = puan;
        OgrenciSayisi = ogrenciSayisi;
    }
}