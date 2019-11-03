using System.Runtime.InteropServices;
using System.Text;
using DAL;

namespace ODM.Kutuphanem
{
    public static class IniIslemleri
    {
        static string _dosyaAdi = "C:\\ODM25\\ayarlar.ini";
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        public static bool VeriYaz(string kategori, string anaktar, string deger)
        {
            string dosyaYolu = "C:\\ODM25";
            if (!DizinIslemleri.DizinKontrol(dosyaYolu))
                DizinIslemleri.DizinOlustur(dosyaYolu);

            bool Return = WritePrivateProfileString(kategori, anaktar, deger, _dosyaAdi);
            return Return;
        }

        public static string VeriOku(string kategori, string anahtar)
        {
            StringBuilder sb = new StringBuilder(500);

            GetPrivateProfileString(kategori, anahtar, "", sb, sb.Capacity, _dosyaAdi);
            string veri = sb.ToString();
            sb.Clear();
            return veri;
        }
        [DllImport("kernel32.dll")]
        static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
    }
}

