using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Common.Library
{
    public class ResimBoyutlandir
    {
        /// <summary>
        /// Resmi yeniden boyutlandırma, dönüştürme ve kaydetme yöntemi.
        /// </summary>
        /// <param name = "image"> Bitmap görüntüsü. </param>
        /// <param name = "maxWidth"> yeniden boyutlandırma genişliği. </param>
        /// <param name = "maxHeight"> yüksekliği yeniden boyutlandır. </param>
        /// <param name = "quality"> kalite ayar değeri. </param>
        /// <param name = "filePath"> dosya yolu. </param>     
        public void Kaydet(HttpPostedFileBase image, int maxWidth, int maxHeight, int kalite, string dosyaYolu)
        {
            Bitmap sourceimage = (Bitmap)Image.FromStream(image.InputStream);

            // Resmin orijinal genişliğini ve yüksekliğini al
            int originalWidth = sourceimage.Width;
            int originalHeight = sourceimage.Height;

            // En boy oranını korumak için
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // En boy oranına göre yeni genişlik ve yükseklik
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Diğer formatları (CMYK dahil) RGB'ye dönüştürün.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Görüntüyü, Yüksek Kaliteye ayarlı kalite moduyla belirtilen boyutta çizer.
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(sourceimage, 0, 0, newWidth, newHeight);
            }

            // JPEG kodeğini temsil eden bir ImageCodecInfo nesnesi alın.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);

            // Quality parametresi için bir Encoder nesnesi oluşturun.
            Encoder encoder = Encoder.Quality;

            //Bir EncoderParameters nesnesi oluşturun. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Görüntüyü kalite seviyesine sahip bir JPEG dosyası olarak kaydedin.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, kalite);
            encoderParameters.Param[0] = encoderParameter;
            newImage.Save(dosyaYolu, imageCodecInfo, encoderParameters);
        }

        /// <summary>
        /// Verilen görüntü formatı için kodlayıcı bilgisi alma yöntemi.
        /// </summary>
        /// <param name="format">Görüntü formatı</param>
        /// <returns>görüntü codec bilgisi.</returns>
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
    }
}
