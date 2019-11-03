using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging.Filters;

namespace ODM.Kutuphanem
{
    public class ImageProcessing
    {
        public static Bitmap Blacken(Bitmap bmpx)
        {
            //otsu filitresi uygulayarak resmi siyahla.
            OtsuThreshold otsuFiltre = new OtsuThreshold();
            Bitmap filtreliResim =
                otsuFiltre.Apply(bmpx.PixelFormat != PixelFormat.Format8bppIndexed
                    ? Grayscale.CommonAlgorithms.BT709.Apply(bmpx)
                    : bmpx);
            return filtreliResim;
        }

        public static Bitmap CropBitmap(Bitmap bitmap, int x, int y, int w, int h)
        {
            Rectangle rect = new Rectangle(x, y, w, h);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }

        public static void ResimKirp(string path, int width, int height, int x, int y, string kirpilanDosyaAdresi)
        {
            using (Bitmap absentRectangleImage = (Bitmap)Image.FromFile(path))
            {
                using (Bitmap currentTile = new Bitmap(width, height))
                {
                    currentTile.SetResolution(absentRectangleImage.HorizontalResolution, absentRectangleImage.VerticalResolution);
                    using (Graphics currentTileGraphics = Graphics.FromImage(currentTile))
                    {
                        currentTileGraphics.Clear(Color.Black);
                        Rectangle absentRectangleArea = new Rectangle(x, y, width, height);
                        currentTileGraphics.DrawImage(absentRectangleImage, 0, 0, absentRectangleArea, GraphicsUnit.Pixel);
                    }
                    currentTile.Save(kirpilanDosyaAdresi);
                }
            }
        }

        public static Bitmap RotateImage(Image image, float aciDerece, int x, int y)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            PointF offset = new PointF(x, y);

            //döndürülmüş görüntüyü tutmak için yeni bir boş bitmap oluştur
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);

            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //boş bitmapten bir grafik nesnesi hazırla
            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                //Resim belirlenene açıda döndükten sonra taşan alanları beyaza boya
                g.Clear(Color.White);

                //Put the rotation point in the center of the image
                g.TranslateTransform(offset.X, offset.Y);

                //resmi çevir
                g.RotateTransform(aciDerece);

                //move the image back
                g.TranslateTransform(-offset.X, -offset.Y);

                //draw passed in image onto graphics object
                g.DrawImage(image, new PointF(0, 0));
            }
            return rotatedBmp;
        }

    }
}
