using System;
using System.Linq;
using System.Xml.Linq;

namespace ErzurumOdmMvc.Common.Library
{
    public class HavaDurumuInfo
    {
        public string Sicaklik { get; set; }
        public string Ruzgar { get; set; }
        public string Hava { get; set; }
        public string Image { get; set; }
        public HavaDurumuInfo()
        {

        }

        public HavaDurumuInfo(string sicaklik, string ruzgar, string hava, string image)
        {
            Sicaklik = sicaklik;
            Ruzgar = ruzgar;
            Hava = hava;
            Image = image;
        }
    }
    public class HavaDurumu
    {
        public HavaDurumuInfo Rapor()
        {
            string sicaklik = "";
            string image = "icon-weather";
            string havaDurumu = "";
            string ruzgarDurumu = "";
            try
            {
                string api = "febc6d5382e8af33d366d63cbaff1dac";
                string baglanti = "http://api.openweathermap.org/data/2.5/weather?q=Erzurum&mode=xml&units=metric&APPID=" + api;

                XDocument hava = XDocument.Load(baglanti);
                sicaklik = hava.Descendants("temperature").ElementAt(0).Attribute("value").Value;

                var durum = hava.Descendants("clouds").ElementAt(0).Attribute("name").Value;
                var ruzgar = hava.Descendants("speed").ElementAt(0).Attribute("name").Value;

                switch (ruzgar)
                {
                    case "Light breeze":
                        ruzgarDurumu = "Hafif Esinti";
                        break;
                    case "Moderate breeze":
                        ruzgarDurumu = "Ilıman Esinti";
                        break;
                    case "Gentle Breeze":
                        ruzgarDurumu = "Yumuşak Hava";
                        break;
                    case "Fresh Breeze":
                        ruzgarDurumu = "Serin Esinti";
                        break;
                    case "Calm":
                        ruzgarDurumu = "Sakin Hava"; //sakin
                        break;
                    case "Overcast Clouds":
                        ruzgarDurumu = "Bulutlu"; //sakin
                        break;
                    default:
                        ruzgarDurumu = ruzgar;
                        break;
                }


                if (durum == "broken clouds")
                {
                    image = "fas fa-cloud-sun";
                    havaDurumu = "Parçalı Bulutlu";
                }
                else if (durum == "sun")
                {
                    image = "fas fa-sun";
                    havaDurumu = "Güneşli";
                }
                else if (durum == "clear sky")
                {
                    image = "fas fa-sun";
                    havaDurumu = "Açık";
                }
                else if (durum == "scattered clouds")
                {
                    image = "fas fa-cloud-sun";
                    havaDurumu = "Parçalı Bulutlu";
                }
                else if (durum == "few clouds")
                {
                    image = "fas fa-cloud-sun";
                    havaDurumu = "Az Bulutlu";
                }
                else if (durum == "overcast clouds")
                {
                    image = "fas fa-cloud-meatball";
                    havaDurumu = "Yoğun Bulutlu";
                }
                else
                    havaDurumu = durum;
            }
            catch (Exception)
            {
                //
            }

            return new HavaDurumuInfo(sicaklik, ruzgarDurumu, havaDurumu, image);
        }
    }
}
