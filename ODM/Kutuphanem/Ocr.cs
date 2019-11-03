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
                taz += string.Format("Beklenmeyen hata: {0}\n", e.Message);
                taz += string.Format("Detaylar: {0}", e);
                return taz;
            }
        }

    }
}
