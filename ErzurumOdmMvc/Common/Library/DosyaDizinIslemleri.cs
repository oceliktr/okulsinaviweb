using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Common.Library
{
    /// <summary>
    /// Summary description for DizinIslemleri
    /// </summary>
    public static class DosyaDizinIslemleri
    {
        public static bool DizinKontrol(string dizin)
        {
            return Directory.Exists(dizin);
        }

        public static void DizinSil(string dizin)
        {
            Directory.Delete(dizin);
        }

        public static void DizinOlustur(string dizin)
        {
            Directory.CreateDirectory(dizin);
        }

        public static bool DosyaKontrol(string dosyaYolu)
        {
            return File.Exists(dosyaYolu);
        }

        public static void DosyaTasi(string dosyaYolu, string yeniDosyaYolu)
        {
            File.Move(dosyaYolu, yeniDosyaYolu);
        }

        public static bool DosyaSil(string dosyaYolu)
        {
            try
            {
                File.Delete(dosyaYolu);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void DizindekiDosyalariSil(string dizin)
        {
            try
            {
                var dizindekiDosyalar = Directory.GetFiles(dizin);

                foreach (var dosyaAdi in dizindekiDosyalar.Select(dosya => new FileInfo(dosya))
                    .Select(fileInfo => fileInfo.Name))
                    File.Delete(dizin + dosyaAdi);
            }
            catch (Exception)
            {
                //
            }

        }

        public static List<DosyaInfo> DizindekiDosyalariListele(string dizinAdresi)
        {
            DirectoryInfo dizin = new DirectoryInfo(dizinAdresi);
            FileInfo[] dosyalar = dizin.GetFiles("*.*", SearchOption.AllDirectories);
            return (from dsy in dosyalar
                    let dosyaYolu =
                        "/" + dsy.FullName
                            .Replace(HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty)
                            .Replace(@"\", "/")
                    select new DosyaInfo(dsy.Name, dosyaYolu, dsy.CreationTime)).ToList();
        }

        public static List<DosyaInfo> DizindekiDosyalariListele(string dizinAdresi, bool altDizindekilerle)
        {
            DirectoryInfo dizin = new DirectoryInfo(dizinAdresi);
            FileInfo[] dosyalar = dizin.GetFiles("*.*",
                altDizindekilerle ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            List<DosyaInfo> list = new List<DosyaInfo>();
            foreach (FileInfo dsy in dosyalar)
            {
                string dosyaYolu = "/" + dsy.FullName
                                       .Replace(HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"],
                                           String.Empty).Replace(@"\", "/");
                DosyaInfo lst = new DosyaInfo(dsy.Name, dosyaYolu, dsy.CreationTime);
                list.Add(lst);
            }

            return list;
        }

        public static List<DirectoryInfo> DizindekiKlasorleriListele(string dizinAdresi)
        {
            string[] dizindekiKlasorler = Directory.GetDirectories(dizinAdresi);

            List<DirectoryInfo> list = new List<DirectoryInfo>();
            foreach (var klasor in dizindekiKlasorler)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(klasor);
                list.Add(new DirectoryInfo(dirInfo.Name));
            }

            return list;
        }
    }

    public class DosyaInfo
    {
        public string DosyaAdi { get; set; }
        public string DosyaYolu { get; set; }
        public DateTime DosyaOlusturmaTarihi { get; set; }

        public DosyaInfo()
        {
        }

        public DosyaInfo(string dosyaAdi, string dosyaYolu, DateTime dosyaOlusturmaTarihi)
        {
            DosyaAdi = dosyaAdi;
            DosyaYolu = dosyaYolu;
            DosyaOlusturmaTarihi = dosyaOlusturmaTarihi;
        }
    }
}