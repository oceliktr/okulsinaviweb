using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ErzurumOdmMvc.CKKarneModel
{
    public class PdfIslemleri
    {
        public enum Renkler
        {
            Siyah = 1,
            Beyaz = 2,
            Gri = 3

        }

        private BaseColor Renklendirme(Renkler bgColor)
        {
            BaseColor bgc;
            switch (bgColor)
            {
                case Renkler.Siyah:
                    bgc = BaseColor.BLACK;
                    break;
                case Renkler.Beyaz:
                    bgc = BaseColor.WHITE;
                    break;
                case Renkler.Gri:
                    bgc = BaseColor.GRAY;
                    break;
                default:
                    bgc = BaseColor.BLACK;
                    break;
            }

            return bgc;
        }

        public void addCell(PdfPTable table, string text, int rowspan = 1, int colspan = 1, int hizalama = Element.ALIGN_LEFT, int fontSize = 7, Renkler fontColor = Renkler.Siyah, Renkler bgColor = Renkler.Beyaz,float yukseklik=10,int vertical= Element.ALIGN_MIDDLE,int fontStyle= Font.NORMAL)
        {
            BaseColor fc = Renklendirme(fontColor);
            BaseColor bgc = Renklendirme(bgColor);

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.HELVETICA, "CP1254", BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, fontSize, fontStyle, fc);


            PdfPCell cell = new PdfPCell(new Phrase(text, times))
            {
                BackgroundColor = bgc,
                Rowspan = rowspan,
                Colspan = colspan,
                HorizontalAlignment = hizalama,
                VerticalAlignment = vertical,
                MinimumHeight = yukseklik
            };
            table.AddCell(cell);
        }

        public void addParagraph(Document doc, string metin, int fontSize = 10, int hizalama = Element.ALIGN_LEFT,int fontStil= Font.NORMAL)
        {
            BaseFont helveticaTurkish = BaseFont.CreateFont("Helvetica", "CP1254", BaseFont.NOT_EMBEDDED);
            Font fontNormal = new Font(helveticaTurkish, fontSize,fontStil);

            Paragraph result = new Paragraph(new Phrase(metin, fontNormal)) { Alignment = hizalama };
            
            doc.Add(result);
        }

    }
}

