using System;
using System.Globalization;

namespace ErzurumOdmMvc.Common.Library
{
    public static class TarihIslemleri
    {

        public static string AyIsmi(this int ay)
        {
            string sonuc;
            switch (ay)
            {
                case 1:
                    sonuc = "Ocak";
                    break;
                case 2:
                    sonuc = "Şubat";
                    break;
                case 3:
                    sonuc = "Mart";
                    break;
                case 4:
                    sonuc = "Nisan";
                    break;
                case 5:
                    sonuc = "Mayıs";
                    break;
                case 6:
                    sonuc = "Haziran";
                    break;
                case 7:
                    sonuc = "Temmuz";
                    break;
                case 8:
                    sonuc = "Ağustos";
                    break;
                case 9:
                    sonuc = "Eylül";
                    break;
                case 10:
                    sonuc = "Ekim";
                    break;
                case 11:
                    sonuc = "Kasım";
                    break;
                case 12:
                    sonuc = "Aralık";
                    break;
                default:
                    sonuc = "Hata Mesajı";
                    break;
            }

            return sonuc;
        }

        public static string GunIsmi(int gun)
        {
            string sonuc;
            switch (gun)
            {
                case 1:
                    sonuc = "Pazartesi";
                    break;
                case 2:
                    sonuc = "Salı";
                    break;
                case 3:
                    sonuc = "Çarşamba";
                    break;
                case 4:
                    sonuc = "Perşembe";
                    break;
                case 5:
                    sonuc = "Cuma";
                    break;
                case 6:
                    sonuc = "Cumartesi";
                    break;
                case 7:
                    sonuc = "Pazar";
                    break;
                default:
                    sonuc = "Hata mesajı";
                    break;
            }

            return sonuc;
        }

        public static string GecenSure(this DateTime tarih)
        {
            DateTime sonDeger = YerelTarih();
            TimeSpan zaman = sonDeger - tarih;

            int gun = zaman.TotalDays.ToInt32();
            int saat = zaman.TotalHours.ToInt32() - (gun * 24);
            int dakika = zaman.TotalMinutes.ToInt32() - ((gun * 24 + saat) * 60);

            string toplamDakika = "";
            if (zaman.TotalMinutes.ToInt32() <= 5)
                toplamDakika = "Şimdi";
            else
            {
                if (gun > 0)
                    toplamDakika = string.Format(" {0} gün ", gun.ToInt32());
                if (saat > 0)
                    toplamDakika += string.Format(" {0} saat ", saat.ToInt32());

                if (dakika > 0)
                    toplamDakika += string.Format(" {0} dakika ", dakika.ToInt32());

                toplamDakika += " önce";
            }

            return toplamDakika;
        }

        public static string GecenGun(this DateTime tarih)
        {
            TimeSpan ts = YerelTarih() - Convert.ToDateTime(tarih);
            return ts.Days.ToString();
        }

        public static string KalanGun(this DateTime tarih)
        {
            TimeSpan ts = Convert.ToDateTime(tarih.AddDays(1)) - YerelTarih();

            string kalanGun = string.Format("{0}", ts.Days); //5
            return kalanGun;
        }

        public static string KalanSure(this DateTime tarih)
        {
            DateTime sonDeger = YerelTarih();
            TimeSpan zaman = tarih - sonDeger;
            string kalanSaat = zaman.TotalHours.ToInt32() == 0
                ? string.Format("{0} dakika", zaman.TotalMinutes.ToInt32())
                : string.Format("{0} saat", zaman.TotalHours.ToInt32());
            return kalanSaat;
        }

        public static DateTime YerelTarih()
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset istambulTime = TimeZoneInfo.ConvertTime(localServerTime, info);
            return istambulTime.DateTime;
        }

        public static DateTime YerelTarih(this bool mySql)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset istanbulTime = TimeZoneInfo.ConvertTime(localServerTime, info);
            DateTime tarih = istanbulTime.DateTime;
            return string.Format("{0}-{1:00}-{2:00} {3:00}:{4:00}", tarih.Year, tarih.Month, tarih.Day, tarih.Hour, tarih.Minute).ToDateTime();
        }

        /// <summary>
        /// Gün ve ay ismi 10 Mart gibi
        /// </summary>
        /// <param name="tarih"></param>
        /// <returns></returns>
        public static string KisaTarihYaz(this DateTime tarih)
        {
            string s = string.Format("{0} {1}", tarih.Day, tarih.Month.AyIsmi());
            return s;
        }

        public static string TarihYaz(this DateTime tarih, bool gunlu)
        {
            string s = string.Format("{0} {1} {2} {3:HH:mm}", tarih.Day, tarih.Month.AyIsmi(), tarih.Year, tarih);
            return s;
        }

        /// <summary>
        /// 15 EKIM 2017 PAZAR
        /// </summary>
        /// <param name="tarih"></param>
        /// <param name="gunlu"></param>
        /// <returns></returns>
        public static string TarihYaz(this DateTime tarih, int gunlu)
        {
            string gunAdi = CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)tarih.DayOfWeek];
            string s = string.Format("{0:D} {1} {2} {3} {4:HH:mm}", tarih.Day, tarih.Month.AyIsmi(), tarih.Year, gunAdi,
                tarih);
            return s;
        }

        public static string TarihYaz(this DateTime tarih)
        {
            DateTime simdi = YerelTarih();
            string s;
            if (tarih.Hour == 0 && tarih.Minute == 0 && tarih.Second == 0)
            {
                if (simdi.Date == tarih.Date)
                    s = "Bugün " + tarih.Date;
                else if (tarih.Date.AddDays(1.0).Day == simdi.Date.Day && tarih.Month == simdi.Month &&
                         tarih.Year == simdi.Year)
                    s = "Dün";
                else
                    s = string.Format("{0} {1} {2} {3:HH:mm}", tarih.Day, tarih.Month.AyIsmi(), tarih.Year, tarih);
            }
            else
            {
                if (simdi.Date == tarih.Date)
                    s = string.Format("Bugün {0:00}:{1:00}", tarih.Hour, tarih.Minute);
                else if (tarih.Date.AddDays(1) == simdi.Date && tarih.Month == simdi.Month && tarih.Year == simdi.Year)
                    s = string.Format("Dün {0:00}:{1:00}", tarih.Hour, tarih.Minute);
                else
                    s = string.Format("{0} {1} {2} {3:HH:mm}", tarih.Day, tarih.Month.AyIsmi(), tarih.Year, tarih);
            }

            return s;
        }

        /// <summary>
        /// Tarih bilgisi null gelebilecek alanlar için kullanılacak metot
        /// </summary>
        /// <param name="tarihNull"></param>
        /// <returns></returns>
        public static string TarihYazNull(this string tarihNull)
        {
            if (string.IsNullOrEmpty(tarihNull))
                return "";

            DateTime tarih = Convert.ToDateTime(tarihNull);
            DateTime simdi = YerelTarih();
            string s;
            if (tarih.Hour == 0 && tarih.Minute == 0 && tarih.Second == 0)
            {
                if (simdi.Date == tarih.Date)
                    s = "Bugün " + tarih.Date;
                else if (tarih.Date.AddDays(1.0).Day == simdi.Date.Day && tarih.Month == simdi.Month &&
                         tarih.Year == simdi.Year)
                    s = "Dün";
                else
                    s = string.Format("{0} {1} {2} {3:HH:mm}", tarih.Day, tarih.Month.AyIsmi(), tarih.Year, tarih);
            }
            else
            {
                if (simdi.Date == tarih.Date)
                    s = string.Format("Bugün {0:00}:{1:00}", tarih.Hour, tarih.Minute);
                else if (tarih.Date.AddDays(1) == simdi.Date && tarih.Month == simdi.Month && tarih.Year == simdi.Year)
                    s = string.Format("Dün {0:00}:{1:00}", tarih.Hour, tarih.Minute);
                else
                    s = string.Format("{0} {1} {2} {3:HH:mm}", tarih.Day, tarih.Month.AyIsmi(), tarih.Year, tarih);
            }

            return s;
        }
        public static string TarihYaz(this DateTime tarih, bool gunlu, bool kisa)
        {
            string s = string.Format("{0:00}.{1:00}.{2}", tarih.Day, tarih.Month, tarih.Year);

            return s;
        }

        public static string TarihYaz(this DateTime tarih, string kisa)
        {
            string s = string.Format("{0:00} {1} {2}", tarih.Day, tarih.Month.AyIsmi(), tarih.Year);

            return s;
        }
    }
}
