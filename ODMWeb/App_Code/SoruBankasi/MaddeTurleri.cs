using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoruBankasi
{
    public class MaddeTurleriInfo
    {

        public string MaddeTuru { get; set; }
        public int Id { get; set; }

        public MaddeTurleriInfo(string maddeTuru, int id)
        {
            MaddeTuru = maddeTuru;
            Id = id;
        }

    }

    public enum MaddeTurleriEnum
    {
        CoktanSecmeli=1,
        DogruYanlis=2,
        AcikUclu=3,
        BoslukDoldurma=4,
        SecenekEslestirme=5
    }
    public class MaddeTurleri
    {
        public List<MaddeTurleriInfo> MaddeTurleriniGetir()
        {
            List<MaddeTurleriInfo> lst = new List<MaddeTurleriInfo>
            {
                new MaddeTurleriInfo("Çoktan Seçmeli Soru", MaddeTurleriEnum.CoktanSecmeli.ToInt32()),
                new MaddeTurleriInfo("Doğru Yanlış Cevaplı Soru", MaddeTurleriEnum.DogruYanlis.ToInt32()),
                new MaddeTurleriInfo("Açık Uçlu Soru", MaddeTurleriEnum.AcikUclu.ToInt32()),
                new MaddeTurleriInfo("Boşluk Doldurmalı Soru", MaddeTurleriEnum.BoslukDoldurma.ToInt32()),
                new MaddeTurleriInfo("Seçenek Eşleştirmeli Soru", MaddeTurleriEnum.SecenekEslestirme.ToInt32())
            };

            return lst;
        }
    }

}