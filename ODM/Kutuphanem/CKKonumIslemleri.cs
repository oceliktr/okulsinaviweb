using System.Collections.Generic;

namespace ODM.Kutuphanem
{
    public class CkKonumInfo
    {
        public string SoruNo { get; set; }
        public string X1 { get; set; }
        public string Y1 { get; set; }
        public string X2 { get; set; }
        public string Y2 { get; set; }
        public string W { get; set; }
        public string H { get; set; }
        public string SayfaYuzu { get; set; }
        public string Brans { get; set; }
        public string SinavId { get; set; }
        public string Grup { get; set; }

        public CkKonumInfo()
        {
        }
        public CkKonumInfo(string soruNo, string x1, string y1, string w, string h, string sayfaYuzu, string brans, string sinavId)
        {
            SoruNo = soruNo;
            X1 = x1;
            Y1 = y1;
            W = w;
            H = h;
            SayfaYuzu = sayfaYuzu;
            Brans = brans;
            SinavId = sinavId;
        }
    }

    public static class CkKonumIslemleri
    {
        public static List<CkKonumInfo> KonumBilgileri()
        {

            List<CkKonumInfo> list = new List<CkKonumInfo>();
            string s1X1 = IniIslemleri.VeriOku("Soru1", "X1");
            string s1Y1 = IniIslemleri.VeriOku("Soru1", "Y1");
            string s1W = IniIslemleri.VeriOku("Soru1", "W");
            string s1H = IniIslemleri.VeriOku("Soru1", "H");
            string s1Brans = IniIslemleri.VeriOku("Soru1", "Brans");
            string s1SinavId = IniIslemleri.VeriOku("Soru1",  "SinavNo");
            string s1SayfaYuzu = IniIslemleri.VeriOku("Soru1", "SayfaYuzu");
            string s1Grup = IniIslemleri.VeriOku("Soru1", "SayfaYuzu");
            CkKonumInfo lst = new CkKonumInfo("1", s1X1, s1Y1, s1W, s1H, s1SayfaYuzu,s1Brans,s1SinavId);
            if (!string.IsNullOrEmpty(s1X1))
                list.Add(lst);

            string s2X1 = IniIslemleri.VeriOku("Soru2", "X1");
            string s2Y1 = IniIslemleri.VeriOku("Soru2", "Y1");
            string s2W = IniIslemleri.VeriOku("Soru2", "W");
            string s2H = IniIslemleri.VeriOku("Soru2", "H");
            string s2Brans = IniIslemleri.VeriOku("Soru2", "Brans");
            string s2SinavId = IniIslemleri.VeriOku("Soru2",  "SinavNo");
            string s2SayfaYuzu = IniIslemleri.VeriOku("Soru2", "SayfaYuzu");
            lst = new CkKonumInfo("2", s2X1, s2Y1, s2W, s2H, s2SayfaYuzu, s2Brans, s2SinavId);
            if (!string.IsNullOrEmpty(s2X1))
                list.Add(lst);

            string s3X1 = IniIslemleri.VeriOku("Soru3", "X1");
            string s3Y1 = IniIslemleri.VeriOku("Soru3", "Y1");
            string s3W = IniIslemleri.VeriOku("Soru3", "W");
            string s3H = IniIslemleri.VeriOku("Soru3", "H");
            string s3Brans = IniIslemleri.VeriOku("Soru3", "Brans");
            string s3SinavId = IniIslemleri.VeriOku("Soru3",  "SinavNo");
            string s3SayfaYuzu = IniIslemleri.VeriOku("Soru3", "SayfaYuzu");
            lst = new CkKonumInfo("3", s3X1, s3Y1, s3W, s3H, s3SayfaYuzu, s3Brans, s3SinavId);
            if (!string.IsNullOrEmpty(s3X1))
                list.Add(lst);

            string s4X1 = IniIslemleri.VeriOku("Soru4", "X1");
            string s4Y1 = IniIslemleri.VeriOku("Soru4", "Y1");
            string s4W = IniIslemleri.VeriOku("Soru4", "W");
            string s4H = IniIslemleri.VeriOku("Soru4", "H");
            string s4Brans = IniIslemleri.VeriOku("Soru4", "Brans");
            string s4SinavId = IniIslemleri.VeriOku("Soru4",  "SinavNo");
            string s4SayfaYuzu = IniIslemleri.VeriOku("Soru4", "SayfaYuzu");
            lst = new CkKonumInfo("4", s4X1, s4Y1, s4W, s4H, s4SayfaYuzu,s4Brans,s4SinavId);
            if (!string.IsNullOrEmpty(s4X1))
                list.Add(lst);

            string s5X1 = IniIslemleri.VeriOku("Soru5", "X1");
            string s5Y1 = IniIslemleri.VeriOku("Soru5", "Y1");
            string s5W = IniIslemleri.VeriOku("Soru5", "W");
            string s5H = IniIslemleri.VeriOku("Soru5", "H");
            string s5Brans = IniIslemleri.VeriOku("Soru5", "Brans");
            string s5SinavId = IniIslemleri.VeriOku("Soru5",  "SinavNo");
            string s5SayfaYuzu = IniIslemleri.VeriOku("Soru5", "SayfaYuzu");
            lst = new CkKonumInfo("5", s5X1, s5Y1, s5W, s5H, s5SayfaYuzu, s5Brans, s5SinavId);
            if (!string.IsNullOrEmpty(s5X1))
                list.Add(lst);

            string s6X1 = IniIslemleri.VeriOku("Soru6", "X1");
            string s6Y1 = IniIslemleri.VeriOku("Soru6", "Y1");
            string s6W = IniIslemleri.VeriOku("Soru6", "W");
            string s6H = IniIslemleri.VeriOku("Soru6", "H");
            string s6Brans = IniIslemleri.VeriOku("Soru6", "Brans");
            string s6SinavId = IniIslemleri.VeriOku("Soru6",  "SinavNo");
            string s6SayfaYuzu = IniIslemleri.VeriOku("Soru6", "SayfaYuzu");
            lst = new CkKonumInfo("6", s6X1, s6Y1, s6W, s6H, s6SayfaYuzu, s6Brans, s6SinavId);
            if (!string.IsNullOrEmpty(s6X1))
                list.Add(lst);

            string s7X1 = IniIslemleri.VeriOku("Soru7", "X1");
            string s7Y1 = IniIslemleri.VeriOku("Soru7", "Y1");
            string s7W = IniIslemleri.VeriOku("Soru7", "W");
            string s7H = IniIslemleri.VeriOku("Soru7", "H");
            string s7Brans = IniIslemleri.VeriOku("Soru7", "Brans");
            string s7SinavId = IniIslemleri.VeriOku("Soru7",  "SinavNo");
            string s7SayfaYuzu = IniIslemleri.VeriOku("Soru7", "SayfaYuzu");
            lst = new CkKonumInfo("7", s7X1, s7Y1, s7W, s7H, s7SayfaYuzu, s7Brans, s7SinavId);
            if (!string.IsNullOrEmpty(s7X1))
                list.Add(lst);

            string s8X1 = IniIslemleri.VeriOku("Soru8", "X1");
            string s8Y1 = IniIslemleri.VeriOku("Soru8", "Y1");
            string s8W = IniIslemleri.VeriOku("Soru8", "W");
            string s8H = IniIslemleri.VeriOku("Soru8", "H");
            string s8Brans = IniIslemleri.VeriOku("Soru8", "Brans");
            string s8SinavId = IniIslemleri.VeriOku("Soru8", "SinavNo");
            string s8SayfaYuzu = IniIslemleri.VeriOku("Soru8", "SayfaYuzu");
            lst = new CkKonumInfo("8", s8X1, s8Y1, s8W, s8H, s8SayfaYuzu, s8Brans, s8SinavId);
            if (!string.IsNullOrEmpty(s8X1))
                list.Add(lst);

            string s9X1 = IniIslemleri.VeriOku("Soru9", "X1");
            string s9Y1 = IniIslemleri.VeriOku("Soru9", "Y1");
            string s9W = IniIslemleri.VeriOku("Soru9", "W");
            string s9H = IniIslemleri.VeriOku("Soru9", "H");
            string s9Brans = IniIslemleri.VeriOku("Soru9", "Brans");
            string s9SinavId = IniIslemleri.VeriOku("Soru9",  "SinavNo");
            string s9SayfaYuzu = IniIslemleri.VeriOku("Soru9", "SayfaYuzu");
            lst = new CkKonumInfo("9", s9X1, s9Y1, s9W, s9H, s9SayfaYuzu, s9Brans, s9SinavId);
            if (!string.IsNullOrEmpty(s9X1))
                list.Add(lst);

            string s10X1 = IniIslemleri.VeriOku("Soru10", "X1");
            string s10Y1 = IniIslemleri.VeriOku("Soru10", "Y1");
            string s10W = IniIslemleri.VeriOku("Soru10", "W");
            string s10H = IniIslemleri.VeriOku("Soru10", "H");
            string s10Brans = IniIslemleri.VeriOku("Soru10", "Brans");
            string s10SinavId = IniIslemleri.VeriOku("Soru10",  "SinavNo");
            string s10SayfaYuzu = IniIslemleri.VeriOku("Soru10", "SayfaYuzu");
            lst = new CkKonumInfo("10", s10X1, s10Y1, s10W, s10H, s10SayfaYuzu, s10Brans, s10SinavId);
            if (!string.IsNullOrEmpty(s10X1))
                list.Add(lst);

            string sKarekodX1 = IniIslemleri.VeriOku("Karekod", "X1");
            string sKarekodY1 = IniIslemleri.VeriOku("Karekod", "Y1");
            string sKarekodW = IniIslemleri.VeriOku("Karekod", "W");
            string sKarekodH = IniIslemleri.VeriOku("Karekod", "H");
            string sKarekodBrans = IniIslemleri.VeriOku("Karekod", "Brans");
            string sKarekodSinavId = IniIslemleri.VeriOku("Karekod", "SinavNo");
            string sKarekodSayfaYuzu = IniIslemleri.VeriOku("Karekod", "SayfaYuzu");
            lst = new CkKonumInfo("Karekod", sKarekodX1, sKarekodY1, sKarekodW, sKarekodH, sKarekodSayfaYuzu, sKarekodBrans, sKarekodSinavId);
            if (!string.IsNullOrEmpty(sKarekodX1))
                list.Add(lst);

            string sOCRX1 = IniIslemleri.VeriOku("OCR", "X1");
            string sOCRY1 = IniIslemleri.VeriOku("OCR", "Y1");
            string sOCRW = IniIslemleri.VeriOku("OCR", "W");
            string sOCRH = IniIslemleri.VeriOku("OCR", "H");
            string sOCRBrans = "";//IniIslemleri.VeriOku("OCR", "Brans");
            string sOCRSinavId = "";// IniIslemleri.VeriOku("OCR", "SinavNo");
            string sOCRSayfaYuzu = "";// IniIslemleri.VeriOku("OCR", "SayfaYuzu");
            lst = new CkKonumInfo("OCR", sOCRX1, sOCRY1, sOCRW, sOCRH, sOCRSayfaYuzu, sOCRBrans, sOCRSinavId);
            if (!string.IsNullOrEmpty(sOCRX1))
                list.Add(lst);

            return list;
        }
    }
}