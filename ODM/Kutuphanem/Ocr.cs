using System;
using System.Diagnostics;
using Tesseract;

namespace ODM.Kutuphanem
{
    public class Ocr
    {
        public enum Dil
        {
            Turkce,
            Ingilizce
        }

        public static string OcrOku(string ocrDosyaAdresi, out int ogrenciNo)
        {
            string ocrTxt = OcrCevir(ocrDosyaAdresi, Dil.Turkce);
            string oturum = ocrTxt.Substring(0, 1);
            ogrenciNo = ocrTxt.Substring(1, ocrTxt.Length - 1).ToInt32();
            //ilk karekterden sonrası
            return oturum;
        }

        public static string OcrCevir(string imagePath, Dil dil)
        {
            string taz = "";
            string lng = "tur";
            if (dil == Dil.Turkce)
                lng = "tur";
            else if (dil == Dil.Ingilizce)
                lng = "eng";

            try
            {
                using (TesseractEngine engine = new TesseractEngine(@"./tessdata", lng, EngineMode.Default))
                {
                    using (Pix img = Pix.LoadFromFile(imagePath))
                    {
                        using (Page page = engine.Process(img))
                        {
                            return page.GetText().Trim();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                taz += String.Format("Beklenmeyen hata: {0}\n", e.Message);
                taz += String.Format("Detaylar: {0}", e);
                return taz;
            }
        }
    }
}
