using System;
using System.Collections.Generic;
using System.Linq;

namespace ErzurumOdmMvc.Common.Enums
{
    //Root|Admin|IlceMEMYetkilisi|OkulYetkilisi|Ogretmen|SoruYazari|LgsYazari|LgsIlKomisyonu|
    public enum KullaniciSeviye
    {
        Root=0,
        Admin = 1,
        IlceMEMYetkilisi = 2,
        OkulYetkilisi = 3,
        Ogretmen = 4,
        LgsYazari = 6,
        LgsIlKomisyonu = 7
    }

    public static class Yetki
    {
        public static string Getir(string yetki)
        {
            string result = "";
            switch (yetki)
            {
                case "IlceMEMYetkilisi":
                    result = "İlçe MEM Yetkilisi";
                    break;
                case "OkulYetkilisi":
                    result = "Okul Yetkilisi";
                    break;
                case "Ogretmen":
                    result = "Öğretmen";
                    break;
                case "LgsYazari":
                    result = "Lgs Yazarı";
                    break;
                case "LgsIlKomisyonu":
                    result = "Lgs İl Komisyonu";
                    break;
                case "Admin":
                    result = "Admin (ÖDM Personeli)";
                    break;
                case "Root":
                    result = "Root";
                    break;
            }
            return result;
        }

        public static IEnumerable<KullaniciSeviye> KullaniciYetkileri()
        {
            var list = Enum.GetValues(typeof(KullaniciSeviye)).Cast<KullaniciSeviye>();

            return list;
        }
    }
}