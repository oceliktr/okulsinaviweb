using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Common.Library
{
    public class KurumTurleri
    {
        public string KurumTuru { get; set; }
        public string Tur { get; set; }

        public KurumTurleri(string tur, string kurumTuru)
        {
            KurumTuru = kurumTuru;
            Tur = tur;
        }
        public KurumTurleri(string tur)
        {
            Tur = tur;
        }
        public KurumTurleri()
        {
        }
        public List<KurumTurleri> TurleriListele() //silinebilir
        {
            List<KurumTurleri> list = new List<KurumTurleri>
            {
                new KurumTurleri("Anaokulu"),
                new KurumTurleri("HEM"),
                new KurumTurleri("İlkokul"),
                new KurumTurleri("Kurum"),
                new KurumTurleri("Lise"),
                new KurumTurleri("MEM"),
                new KurumTurleri("MeslekiLise"),
                new KurumTurleri("Ortaokul")
            };

            return list;
        }

        public List<KurumTurleri> Listele()
        {
            List<KurumTurleri> list = new List<KurumTurleri>
            {
                new KurumTurleri("Anaokulu","Anaokulu"),
                new KurumTurleri("HEM","Halk Eğitim Merkezi"),
                new KurumTurleri("HEM","Mesleki Eğitim Merkezi"),
                new KurumTurleri("İlkokul","İlkokul"),
                new KurumTurleri("İlkokul","İlkokul (Görme Engelliler)"),
                new KurumTurleri("İlkokul","İlkokul (İşitme Engelliler)"),
                new KurumTurleri("Kurum","Mesleki ve Teknik Eğitim Merkezi (ETÖGM)"),
                new KurumTurleri("Kurum","Özel Eğitim İş Uygulama Merkezi ( III. Kademe)"),
                new KurumTurleri("Kurum","Özel Eğitim Uygulama Merkezi (I. Kademe)"),
                new KurumTurleri("Kurum","Özel Eğitim Uygulama Merkezi (II. Kademe)"),
                new KurumTurleri("Kurum","Rehberlik ve Araştırma Merkezi"),
                new KurumTurleri("Kurum","Üstün veya Özel Yetenekliler"),
                new KurumTurleri("Kurum","Ölçme Değerlendirme Merkezi"),
                new KurumTurleri("Lise","Anadolu İmam Hatip Lisesi"),
                new KurumTurleri("Lise","Anadolu Lisesi"),
                new KurumTurleri("Lise","Çok Programlı Anadolu Lisesi"),
                new KurumTurleri("Lise","Endüstri Meslek Lisesi"),
                new KurumTurleri("Lise","Fen Lisesi"),
                new KurumTurleri("Lise","Güzel Sanatlar Lisesi"),
                new KurumTurleri("Lise","İmam Hatip Lisesi"),
                new KurumTurleri("Lise","Meslek Lisesi(İşitme Engelliler)"),
                new KurumTurleri("Lise","Sosyal Bilimler Lisesi"),
                new KurumTurleri("Lise","Spor Lisesi"),
                new KurumTurleri("MEM","İl - İlçe Milli Eğitim Müdürlüğü"),
                new KurumTurleri("MeslekiLise","Anadolu Otelcilik ve Turizm Mes. Lisesi"),
                new KurumTurleri("MeslekiLise","Anadolu Sağlık Meslek Lisesi"),
                new KurumTurleri("MeslekiLise","Anadolu Ticaret Meslek Lisesi"),
                new KurumTurleri("MeslekiLise","Çok Programlı Anadolu Lisesi"),
                new KurumTurleri("MeslekiLise","Endüstri Meslek Lisesi"),
                new KurumTurleri("MeslekiLise","Kız Meslek Lisesi"),
                new KurumTurleri("MeslekiLise","Sağlık Meslek Lisesi"),
                new KurumTurleri("MeslekiLise","Ticaret Meslek Lisesi"),
                new KurumTurleri("Ortaokul","İmam Hatip Ortaokulu"),
                new KurumTurleri("Ortaokul","Ortaokul"),
                new KurumTurleri("Ortaokul","Ortaokul (Görme Engelliler)"),
                new KurumTurleri("Ortaokul","Ortaokul (İşitme Engelliler)"),
                new KurumTurleri("Ortaokul","Yatılı Bölge Ortaokulu")
            };

            return list;
        }
    }
}