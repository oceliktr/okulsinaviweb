using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ODM.Kutuphanem
{
    /// <summary>
    /// Genel iþlemlerin yapýldýðý sýnýf.
    /// </summary>
    public static class GenelIslemler
    {
        public static void YalnizcaRakam(KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        public static string YuklenecekResimler = ".jpeg,.jpg,.png,.gif";
        public static string YuklenecekDosyalar = ".jpeg,.jpg,.png,.gif,.rar,.zip,.doc,.docx,.xls,.xlsx,.psd,.psdx,.pdf";

        public const string SatirSonu = "<br />";
        public static string ToSayiveHarfler(this string s)
        {
            s = s.Trim();
            if (string.IsNullOrEmpty(s)) return "";
            Regex r = new Regex("[^a-zA-Z0-9]");
            s = r.Replace(s, "");
            return s;
        }
        public static string ToUrl(this string s)
        {
            //s = oncul + id + "-" + s;
            if (string.IsNullOrEmpty(s)) return "";
            s = s.ToLower().Trim();
            if (s.Length > 80)
                s = s.Substring(0, 80);
            s = s.Replace("þ", "s");
            s = s.Replace("Þ", "S");
            s = s.Replace("ð", "g");
            s = s.Replace("Ð", "G");
            s = s.Replace("Ý", "I");
            s = s.Replace("ý", "i");
            s = s.Replace("ç", "c");
            s = s.Replace("Ç", "C");
            s = s.Replace("ö", "o");
            s = s.Replace("Ö", "O");
            s = s.Replace("ü", "u");
            s = s.Replace("Ü", "U");
            s = s.Replace("'", "");
            s = s.Replace("\"", "");
            s = s.Replace("-", "");
            s = s.Replace("'", "");
            Regex r = new Regex("[^a-zA-Z0-9_-]");
            //if (r.IsMatch(s))
            s = r.Replace(s, "_");
            if (!string.IsNullOrEmpty(s))
                while (s.IndexOf("--", StringComparison.Ordinal) > -1)
                    s = s.Replace("--", "");
            if (s.StartsWith("-")) s = s.Substring(1);
            if (s.EndsWith("-")) s = s.Substring(0, s.Length - 1);
            return s;
        }
        public static string ToCokSatirli(this string str)
        {
            string yeni = "";
            const int adet = 50;
            for (int i = 0; i < str.Length; i += adet)
            {
                if (i > 0)
                    yeni += "<br/>";
                if (i + adet < str.Length)
                    yeni += str.Substring(i, adet);
                else
                    yeni += str.Substring(i);
            }
            return yeni;
        }
        public static bool IsInteger(this object sayi)
        {
            try
            {
                if (sayi == null) throw new Exception();
                Convert.ToInt32(sayi);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static int ToInt32(this object sayi)
        {
            try
            {
                if (sayi == null) throw new Exception();
                int x = Convert.ToInt32(sayi);
                return x;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static long ToInt64(this object sayi)
        {
            try
            {
                if (sayi == null) throw new Exception();
                long x = Convert.ToInt64(sayi);
                return x;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static double ToDouble(this object sayi)
        {
            try
            {
                if (sayi == null) throw new Exception();
                double x = Convert.ToDouble(sayi);
                return x;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public static decimal ToDecimal(this object sayi)
        {
            try
            {
                if (sayi == null) throw new Exception();
                decimal x = Convert.ToDecimal(sayi);
                return x;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public static DateTime YerelTarih()
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

            DateTimeOffset localServerTime = DateTimeOffset.Now;

            DateTimeOffset istambulTime = TimeZoneInfo.ConvertTime(localServerTime, info);
            return istambulTime.DateTime;
        }
        public static String GecenSure(this DateTime tarih)
        {
            DateTime sonDeger = YerelTarih();
            TimeSpan zaman = sonDeger.Subtract(tarih);
            string toplamDakika = string.Format("{0} dakika, {1} saniye {2} salise", zaman.TotalMinutes.ToInt32(), zaman.TotalSeconds.ToInt32(), zaman.Milliseconds.ToInt32());
            return toplamDakika;
        }
        public static String GecenGun(this DateTime tarih)
        {
            TimeSpan ts = YerelTarih() - Convert.ToDateTime(tarih);
            return ts.Days.ToString();
        }
        public static String KalanGun(this DateTime tarih)
        {
            TimeSpan ts = Convert.ToDateTime(tarih) - YerelTarih();

            string kalanGun = string.Format("{0}", ts.Days); //5
            return kalanGun;
        }
        /// <summary>
        /// Gün ve ay ismi 10 Mart gibi
        /// </summary>
        /// <param name="tarih"></param>
        /// <returns></returns>
        public static String KisaTarihYaz(this DateTime tarih)
        {
            string s = String.Format("{0} {1}", tarih.Day, tarih.Month.AyIsmi());

            return s;
        }
        public static String SaatYaz(this DateTime tarih)
        {
            string s = String.Format("{0:00}:{1:00}:{2:00}", tarih.Hour, tarih.Minute, tarih.Millisecond);

            return s;
        }
        public static String TarihYaz(this DateTime tarih, bool gunlu)
        {
            string s = String.Format("{0} {1} {2} {3:dddd}", tarih.Day, tarih.Month.AyIsmi(), tarih.Year, tarih);

            return s;
        }
        public static String TarihYaz(this DateTime tarih)
        {
            //DateTime tarih = Convert.ToDateTime(obj);
            DateTime simdi = YerelTarih();
            String s;
            if (tarih.Hour == 0 && tarih.Minute == 0 && tarih.Second == 0)
            {
                if (simdi.Date == tarih.Date)
                    s = "Bugün " + tarih.Date;
                else if (tarih.Date.AddDays(1.0).Day == simdi.Date.Day && tarih.Month == simdi.Month && tarih.Year == simdi.Year)
                    s = "Dün";
                else
                    s = String.Format("{0:00}.{1:00}.{2}", tarih.Day, tarih.Month, tarih.Year);
            }
            else
            {
                if (simdi.Date == tarih.Date)
                    s = String.Format("Bugün {0:00}:{1:00}", tarih.Hour, tarih.Minute);
                else if (tarih.Date.AddDays(1) == simdi.Date && tarih.Month == simdi.Month && tarih.Year == simdi.Year)
                    s = String.Format("Dün {0:00}:{1:00}", tarih.Hour, tarih.Minute);
                else
                    s = String.Format("{0:00}.{1:00}.{2} {3:00}:{4:00}", tarih.Day, tarih.Month, tarih.Year, tarih.Hour, tarih.Minute);
            }
            return s;
        }
        public static String TarihYaz(this DateTime tarih, bool gunlu, bool kisa)
        {
       
            string s = String.Format("{0:00}.{1:00}.{2}", tarih.Day, tarih.Month, tarih.Year);

            return s;
        }
        public static String TarihFormala(this DateTime tarih)
        {
            //DateTime tarih = Convert.ToDateTime(obj);

            string s = String.Format("{0}-{1:00}-{2:00} {3:00}:{4:00}", tarih.Year, tarih.Month, tarih.Day, tarih.Hour, tarih.Minute);

            return s;
        }
        public static int TarihGecenSure(this DateTime tarih)
        {
            DateTime simdi = YerelTarih();
            TimeSpan ts = simdi.Subtract(tarih);
            double gunSayisi = ts.TotalDays;
            return gunSayisi.ToInt32();
        }
        public static int TarihGecenSure(this DateTime ilkTarih, DateTime sonTarih)
        {
            TimeSpan zaman = ilkTarih - sonTarih;
            return Math.Abs(zaman.Days); // 2 tarih arasýndaki farkýn kaç gün olduðu döndürülüyor.
        }

        public static DateTime AyinSonGunu(this DateTime tarih)
        {
            DateTime ayinSonGunu = new DateTime(tarih.Year, tarih.Month + 1, 1).AddDays(-1);
            return ayinSonGunu;
        }
        public static String BuyukHarfYap(this String s)
        {
            if(!string.IsNullOrEmpty(s))
                s = s.ToUpper();
            return s;
        }
        public static string IlkHarfleriBuyut(this string metin)
        {
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(metin);
        }
        public static String MetniAzTemizle(String s)
        {
            s = s.Replace("script", "scr_ipt");
            s = s.Replace("'", "`");
            return s;
        }
        public static String ToTemizMetin(this String s)
        {
            s = ToTemizMetin(s, false);
            return s;
        }
        public static String ToTemizMetin(this String s, bool satirSonu)
        {
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&gt;");
            s = s.Replace("script", "scr_ipt");
            s = s.Replace("'", "");
            s = s.Replace("\"", "'");
            s = s.Replace("&", "-");

            if (satirSonu)
                s = s.Replace(Environment.NewLine, SatirSonu);
            s = s.Trim();
            return s;
        }
        public static String HtmlTagIsle(this String s)
        {
            s = s.Replace("[JUSTIFY]", "<p align=JUSTIFY>");
            s = s.Replace("[/JUSTIFY]", "</p>");
            s = s.Replace("[/COLOR]", "</font>");
            s = s.Replace("[COLOR", "<font color");
            s = s.Replace("[B]", "<b>");
            s = s.Replace("[/B]", "</b>");
            s = s.Replace("[I]", "<I>");
            s = s.Replace("[/I]", "</I>");
            s = s.Replace("[IMG]", "<img src=");
            s = s.Replace("[/IMG]", ">");
            s = s.Trim();

            string birinciAsama = s.Replace("[URL=", "<a href=");
            string ikinciAsama = birinciAsama.Replace("[/URL]", "</a>");
            string ucuncuAsama = ikinciAsama.Replace("]", ">");
            s = ucuncuAsama;
            return s;
        }
        public static String ToMetinEditorDuzelt(this String s)
        {
            s = s.Replace("-ouml;", "ö");
            s = s.Replace("-Ouml;", "Ö");
            s = s.Replace("-uuml;", "ü");
            s = s.Replace("-Uuml;", "Ü");
            s = s.Replace("-ccedil;", "ç");
            s = s.Replace("-Ccedil;", "Ç");
            s = s.Replace(":", "");
            s = s.Replace(".", "");
            s = s.Replace("(", "");
            s = s.Replace(")", "");
            s = s.Replace(",", "");
            s = s.Replace("~", "");
            s = s.Replace("-rsquo;", "");
            s = s.Replace("-ndash;", "");
            s = s.Replace("-ldquo;", "");
            s = s.Replace("-rdquo;", "");
            s = s.Replace("-hellip;", "");
            s = s.Replace("-lsquo;", "");
            s = s.Replace("-nbsp;", " ");
            s = s.Replace("&nbsp;", " ");
            s = s.Trim();
            return s;
        }

        public static string HtmlTemizle(this string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }
        public static String NewLine2Br(this String s)
        {
            s = s.Replace(Environment.NewLine, SatirSonu);
            return s;
        }
        public static String Br2NewLine(this String s)
        {
            s = s.Replace(SatirSonu, Environment.NewLine);
            return s;
        }
        public static DateTime ToDateTime(this object s)
        {
            try
            {
                return Convert.ToDateTime(s);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
        public static bool ToBoolean(this object s)
        {
            try
            {
                return Convert.ToBoolean(s);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Bitmap ResimBoyutlandir(this Bitmap source, int maxWidth, int maxHeight)
        {
            int width, height;
            float aspectRatio = source.Width / (float)source.Height;

            if ((maxHeight > 0) && (maxWidth > 0))
            {
                if ((source.Width < maxWidth) && (source.Height < maxHeight))
                {
                    //Return unchanged image
                    return source;
                }
                else if (aspectRatio > 1)
                {
                    // Calculated width and height,
                    // and recalcuate if the height exceeds maxHeight
                    width = maxWidth;
                    height = (int)(width / aspectRatio);
                    if (height > maxHeight)
                    {
                        height = maxHeight;
                        width = (int)(height * aspectRatio);
                    }
                }
                else
                {
                    // Calculated width and height,
                    // and recalcuate if the width exceeds maxWidth
                    height = maxHeight;
                    width = (int)(height * aspectRatio);
                    if (width > maxWidth)
                    {
                        width = maxWidth;
                        height = (int)(width / aspectRatio);
                    }
                }
            }
            else if ((maxHeight == 0) && (source.Width > maxWidth))
            {
                // If MaxHeight is not provided (unlimited), and
                // the source width exceeds maxWidth,
                // then recalculate height
                width = maxWidth;
                height = (int)(width / aspectRatio);
            }
            else if ((maxWidth == 0) && (source.Height > maxHeight))
            {
                // If MaxWidth is not provided (unlimited), and the
                // source height exceeds maxHeight, then
                // recalculate width
                height = maxHeight;
                width = (int)(height * aspectRatio);
            }
            else
            {
                //Return unchanged image
                return source;
            }

            Bitmap newImage = BoyutlandirilmisResim(source, width, height);
            return newImage;
        }
        public static Bitmap BoyutlandirilmisResim(this Bitmap source, int width, int height)
        {
            //This function creates the thumbnail image.
            //The logic is to create a blank image and to
            // draw the source image onto it

            Bitmap thumb = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(thumb);

            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gr.CompositingQuality = CompositingQuality.HighQuality;

            gr.DrawImage(source, 0, 0, width, height);
            return thumb;
        }
        public static String RastgeleSayiUret(int adet)
        {
            Random random = new Random();
            string s = "";
            for (int i = 0; i < adet; i++)
            {
                int a = random.Next();
                switch (a)
                {
                    case 0:
                        int c = 65 + random.Next(26);
                        s = String.Concat(s, Convert.ToString(c));
                        break;
                    default:
                        s = String.Concat(s, random.Next(10).ToString());
                        break;
                }
            }
            return s;
        }
        public static String RastgeleMetinUret(int adet)
        {
            Random random = new Random();
            string s = "";
            for (int i = 0; i < adet; i++)
            {
                int a = random.Next(2);
                switch (a)
                {
                    case 0:
                        char c = Convert.ToChar(65 + random.Next(26));
                        s = String.Concat(s, Convert.ToString(c));
                        break;
                    default:
                        s = String.Concat(s, random.Next(10).ToString());
                        break;
                }
            }
            return s;
        }
        public static string SoldanMetinAl(this string metin, int uzunluk)
        {
            try
            {
                return metin.Length < uzunluk ? metin : metin.Substring(0, uzunluk);
            }
            catch (Exception)
            {
                return metin;
            }
        }
        public static Bitmap CreateThumbnail(string lcFilename, out bool degisti, int lnWidth, int lnHeight)
        {

            Bitmap bmpOut;

            try
            {
                Bitmap loBmp = new Bitmap(lcFilename);

                decimal lnRatio;
                int lnNewWidth;
                int lnNewHeight;

                if (loBmp.Width < lnWidth && loBmp.Height < lnHeight)
                {
                    degisti = false;
                    return loBmp;
                }
                degisti = true;
                if (loBmp.Width > loBmp.Height)
                {
                    lnRatio = (decimal)lnWidth / loBmp.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBmp.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBmp.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBmp.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }


                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBmp, 0, 0, lnNewWidth, lnNewHeight);

                loBmp.Dispose();
            }
            catch
            {
                degisti = false;
                return null;
            }
            return bmpOut;
        }
        public static bool IsDate(this string tarih)
        {
            try
            {
                if (tarih == null) throw new Exception();
                Convert.ToDateTime(tarih);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool IsEmail(this string inputEmail)
        {
            if (string.IsNullOrEmpty(inputEmail)) return false;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        public static T Cast<T>(object obj, T type)
        {
            return (T)obj;
        }
        public static int Kod(this int x)
        {
            int y = (((x + 1) * 2) + 3 - (x % 4)) + ((x % 2 == 0) ? 1 : -1);
            return y;
        }
        public static bool IsUrl(this string str)
        {
            Regex r = new Regex(@"(ftp|http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return r.IsMatch(str);
        }

        public static string StripHtml(string htmlString)
        {

            string pattern = @"<(.|\n)*?>";

            return Regex.Replace(htmlString, pattern, string.Empty);
        }
        public static string MetninOzeti(string gelenMetin, int karakterSinirlamasi)
        {
            string metinOzeti = "";
            string[] ayiraclar = new string[] { "<br>", "<br />", "." };
            int sayi = 0;
            int i = -1;
            gelenMetin = gelenMetin.Replace("&nbsp;", "");
            string[] parcalar = gelenMetin.Split(ayiraclar, StringSplitOptions.RemoveEmptyEntries); // StringSplitOptions.RemoveEmptyEntries bu ifade boþ olan deðeleri listeye atma anlamýna geliyor
            try
            {
                do
                {
                    i++;
                    sayi = sayi + parcalar[i].Length;

                    metinOzeti = metinOzeti + parcalar[i] + ".";
                } while (sayi < karakterSinirlamasi);

            }
            catch (Exception)
            {

                return metinOzeti;
            }

            return metinOzeti;
        }


        public static string AyIsmi(this int ay)
        {
            string sonuc;
            switch (ay)
            {
                case 1: sonuc = "Ocak"; break;
                case 2: sonuc = "Þubat"; break;
                case 3: sonuc = "Mart"; break;
                case 4: sonuc = "Nisan"; break;
                case 5: sonuc = "Mayýs"; break;
                case 6: sonuc = "Haziran"; break;
                case 7: sonuc = "Temmuz"; break;
                case 8: sonuc = "Aðustos"; break;
                case 9: sonuc = "Eylül"; break;
                case 10: sonuc = "Ekim"; break;
                case 11: sonuc = "Kasým"; break;
                case 12: sonuc = "Aralýk"; break;
                default: sonuc = "Hata Mesajý"; break;
            }
            return sonuc;
        }
        public static string AyIsmi(this int ay,bool kisa)
        {
            string sonuc;
            switch (ay)
            {
                case 1: sonuc = "Oca"; break;
                case 2: sonuc = "Þub"; break;
                case 3: sonuc = "Mar"; break;
                case 4: sonuc = "Nis"; break;
                case 5: sonuc = "May"; break;
                case 6: sonuc = "Haz"; break;
                case 7: sonuc = "Tem"; break;
                case 8: sonuc = "Aðu"; break;
                case 9: sonuc = "Eyl"; break;
                case 10: sonuc = "Eki"; break;
                case 11: sonuc = "Kas"; break;
                case 12: sonuc = "Ara"; break;
                default: sonuc = "Hata Mesajý"; break;
            }
            return sonuc;
        }
        public static string GunIsmi(int gun)
        {
            string sonuc;
            switch (gun)
            {
                case 1: sonuc = "Pazartesi"; break;
                case 2: sonuc = "Salý"; break;
                case 3: sonuc = "Çarþamba"; break;
                case 4: sonuc = "Perþembe"; break;
                case 5: sonuc = "Cuma"; break;
                case 6: sonuc = "Cumartesi"; break;
                case 7: sonuc = "Pazar"; break;
                default: sonuc = "Hata mesajý"; break;
            }
            return sonuc;
        }
    }
}
