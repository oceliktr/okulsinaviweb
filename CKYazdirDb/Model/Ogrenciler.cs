using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Model
{
public class OgrencilerInfo
{
    public long OpaqId { get; set; }
    public string IlAdi { get; set; }
    public string IlceAdi { get; set; }
    public int KurumKodu { get; set; }
    public string KurumAdi { get; set; }
    public int OgrenciNo { get; set; }
    public string Adi { get; set; }
    public string Soyadi { get; set; }
    public int Sinifi { get; set; }
    public string Sube { get; set; }
    public int SinavId { get; set; }
    public int DersKodu { get; set; }
    public string Barkod { get; }
    public string Text { get; }
    public string Value { get; }

    public OgrencilerInfo(long opaqId, string ilAdi, string ilceAdi, int kurumKodu, string kurumAdi, int ogrenciNo, string adi, string soyadi, int sinifi, string sube, int sinavId, int dersKodu,string barkod)
    {
        OpaqId = opaqId;
        IlAdi = ilAdi;
        IlceAdi = ilceAdi;
        KurumKodu = kurumKodu;
        KurumAdi = kurumAdi;
        OgrenciNo = ogrenciNo;
        Adi = adi;
        Soyadi = soyadi;
        Sinifi = sinifi;
        Sube = sube;
        SinavId = sinavId;
        DersKodu = dersKodu;
        Barkod = barkod;
    }

    public OgrencilerInfo(int opaqId, string ilceAdi, int kurumKodu, string kurumAdi, int ogrenciNo, string adi, string soyadi, int sinifi, string sube)
    {
        OpaqId = opaqId;
        IlceAdi = ilceAdi;
        KurumKodu = kurumKodu;
        KurumAdi = kurumAdi;
        OgrenciNo = ogrenciNo;
        Adi = adi;
        Soyadi = soyadi;
        Sinifi = sinifi;
        Sube = sube;
    }

    public OgrencilerInfo(string text, string value)
    {
        Text = text;
        Value = value;
    }

    public OgrencilerInfo()
    {
    }
}
}

