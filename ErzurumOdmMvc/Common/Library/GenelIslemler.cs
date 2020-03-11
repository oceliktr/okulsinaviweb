using System;
using System.Text.RegularExpressions;
using System.Web;

namespace ErzurumOdmMvc.Common.Library
{
    public static class GenelIslemler
    {
        public static int anasayfaLimit = 13; //ana sayfadaki yazıların limiti. Manşet ve yazarlar hariçtir.

        public static string yuklenecekResimler = ".jpeg,.jpg,.png,.gif,.ico";
        public static string[] yuklenecekResimlers = { ".png", ".gif", ".jpg", ".jpeg," };
        public static string yuklenecekDosyalar = ".jpeg,.jpg,.png,.gif,.rar,.zip,.doc,.docx,.xls,.xlsx,.psd,.psdx,.pdf";

        public static string SosyalAglarMeta(string url, string fotograf, string baslik, string description)
        {
            baslik = baslik.Replace("'", "");
            description = description.Replace("'", "");

            Uri myuri = new Uri(url); //full url adresi http://erzurumportali.com/shf/4241/Osmanaga-Ramazan-Erzurum-Iftar-Menusu
            string pathQuery = myuri.PathAndQuery;
            string hostName = myuri.ToString().Replace(pathQuery, ""); //sadece domaini gösterir http://erzurumportali.com/

            string foto = DosyaDizinIslemleri.DosyaKontrol(HttpContext.Current.Server.MapPath(fotograf)) ? fotograf : "/img/foto-guncelleniyor.jpg";
            string metaTag = "<link href=\"" + hostName + foto + "\" rel=\"image_src\" />";
            metaTag += string.Format("<meta property=\"og:url\" content=\"{0}\" />", url);
            metaTag += "<meta property=\"og:type\" content=\"website\" />";
            metaTag += string.Format("<meta property=\"og:title\" content=\"{0}\" />", baslik);
            metaTag += string.Format("<meta property=\"og:description\" content=\"{0}\" />", description);
            metaTag += string.Format("<meta property=\"og:image\" content=\"{0}\" />", hostName + foto);
            metaTag += string.Format("<meta property=\"og:site_name\" content=\"{0}\" />", baslik);
            metaTag += "<meta property=\"fb:app_id\" content = \"136086393600962\">";
            metaTag += "<meta property=\"og:type\" content=\"article\" />";
            metaTag += "<meta name=\"twitter:site\" content=\"@Osmanceliktr25\" />";
            metaTag += string.Format("<meta name=\"twitter:url\" content=\"{0}\" />", url);
            metaTag += string.Format("<meta name=\"twitter:title\" content=\"{0}\" />", baslik);
            metaTag += string.Format("<meta name=\"twitter:description\" content=\"{0}\" />", description);
            metaTag += "<meta name=\"twitter:image:src\" content=\"" + hostName + foto + "\" />";
            metaTag += "<meta name=\"twitter:card\" content=\"summary_large_image\" />";
            metaTag += "<meta name=\"twitter:creator\" content=\"@Osmanceliktr25\" />";
            metaTag += "<link rel=\"canonical\" href=\"" + url + "\"/>";
            return metaTag;
        }


       
        public static bool isEmail(this string inputEmail)
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

        public static string ToURL(this string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            s = s.ToLower().Trim();
            s = s.IlkHarfleriBuyut();
            if (s.Length > 80)
                s = s.Substring(0, 80);
            s = s.Replace("ş", "s");
            s = s.Replace("Ş", "S");
            s = s.Replace("ğ", "g");
            s = s.Replace("Ğ", "G");
            s = s.Replace("İ", "I");
            s = s.Replace("ı", "i");
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
            s = s.Replace("?", "");
            s = s.Replace("&", "");
            Regex r = new Regex("[^a-zA-Z0-9_-]");
            if (r.IsMatch(s))
                s = r.Replace(s, "-");
            if (!string.IsNullOrEmpty(s))
            {
                while (s.IndexOf("__", StringComparison.Ordinal) > -1)
                    s = s.Replace("__", "-");
            }
            if (s.StartsWith("-")) s = s.Substring(1);
            if (s.EndsWith("-")) s = s.Substring(0, s.Length - 1);

            s = s.Replace("--", "-");
            return s;
        }
        public static string ToTag(this string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            s = s.ToLower().Trim();
            s = s.IlkHarfleriBuyut();
            if (s.Length > 80)
                s = s.Substring(0, 80);

            s = s.Replace("'", "");
            s = s.Replace("\"", "");
            s = s.Replace("-", "");
            s = s.Replace("'", "");
            s = s.Replace("?", "");
            s = s.Replace("&", "");
            s = s.Replace(" ", "-");

            s = s.Replace("--", "-");
            return s;
        }


    }
}
