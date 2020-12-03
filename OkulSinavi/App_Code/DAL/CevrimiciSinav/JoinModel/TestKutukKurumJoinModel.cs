using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class TestKutukKurumJoinModel:TestKutukInfo
{
    public string KurumAdi { get; set; }

    public TestKutukKurumJoinModel(int id, string ilceAdi,int kurumKodu, string kurumAdi, string adi, string soyadi, int sinifi, string sube)
    {
        Id = id;
        IlceAdi = ilceAdi;
        KurumKodu = kurumKodu;
        KurumAdi = kurumAdi;
        Adi = adi;
        Soyadi = soyadi;
        Sinifi = sinifi;
        Sube = sube;
    }
}