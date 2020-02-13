namespace ErzurumOdmMvc.Common.Library
{
    public static class KarakterDuzenleyici
    {
        /// <summary>
        /// CK Editör ile bozulan karakterleri düzeltir.
        /// </summary>
        /// <param name="metin"></param>
        /// <returns></returns>
        public static string KarakterDuzenle(this string metin)
        {
            string Duzenlenmis = metin;
            Duzenlenmis = Duzenlenmis.Replace("&#304;", "I");
            Duzenlenmis = Duzenlenmis.Replace("&#305;", "i");
            Duzenlenmis = Duzenlenmis.Replace("&#214;", "Ö");
            Duzenlenmis = Duzenlenmis.Replace("&#246;", "ö");
            Duzenlenmis = Duzenlenmis.Replace("&Ouml;", "Ö");
            Duzenlenmis = Duzenlenmis.Replace("&ouml;", "ö");
            Duzenlenmis = Duzenlenmis.Replace("&#220;", "Ü");
            Duzenlenmis = Duzenlenmis.Replace("&#252;", "ü");
            Duzenlenmis = Duzenlenmis.Replace("&Uuml;", "Ü");
            Duzenlenmis = Duzenlenmis.Replace("&uuml;", "ü");
            Duzenlenmis = Duzenlenmis.Replace("&#199;", "Ç");
            Duzenlenmis = Duzenlenmis.Replace("&#231;", "ç");
            Duzenlenmis = Duzenlenmis.Replace("&Ccedil;", "Ç");
            Duzenlenmis = Duzenlenmis.Replace("&ccedil;", "ç");
            Duzenlenmis = Duzenlenmis.Replace("&#286;", "G");
            Duzenlenmis = Duzenlenmis.Replace("&#287;", "g");
            Duzenlenmis = Duzenlenmis.Replace("&#350;", "S");
            Duzenlenmis = Duzenlenmis.Replace("&#351;", "s");
            return Duzenlenmis;
        }
    }
}
