using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


    /// <summary>
    /// Summary description for DizinIslemleri
    /// </summary>
    public static class DizinIslemleri
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
        public static void DizinIceriginiSil(string dizin)
        {
            var dizindekiDosyalar = Directory.GetFiles(dizin);

            foreach (var dosyaAdi in dizindekiDosyalar.Select(dosya => new FileInfo(dosya)).Select(fileInfo => fileInfo.Name))
                File.Delete(dizin + "/" + dosyaAdi);
        }
        public static List<DosyaInfo> DizindekiDosyalariListele(string dizinAdresi)
        {
            DirectoryInfo dizin = new DirectoryInfo(dizinAdresi);
            FileInfo[] dosyalar = dizin.GetFiles("*.*", SearchOption.AllDirectories); //Alt dizindekileri de listelemek için SearchOption.AllDirectories kullan
            List<DosyaInfo> list = new List<DosyaInfo>();
            foreach (FileInfo dsy in dosyalar)
            {
                DosyaInfo lst = new DosyaInfo(dsy.Name, dizinAdresi, dsy.CreationTime, dsy.DirectoryName);
                list.Add(lst);
            }
            return list;
        }

    }
    public class DosyaInfo
    {
        public string DosyaAdi { get; set; }
        public string DosyaYolu { get; set; }
        public string DizinAdresi { get; set; }
        public DateTime DosyaOlusturmaTarihi { get; set; }
        public DosyaInfo()
        {
        }
        public DosyaInfo(string dosyaAdi, string dosyaYolu, DateTime dosyaOlusturmaTarihi, string dizinAdresi)
        {
            DosyaAdi = dosyaAdi;
            DosyaYolu = dosyaYolu;
            DosyaOlusturmaTarihi = dosyaOlusturmaTarihi;
            DizinAdresi = dizinAdresi;
        }
    }
