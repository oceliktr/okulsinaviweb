using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using AForge.Imaging.Filters;

namespace ODM.Kutuphanem
{
    public class Koordinat
    {
        public int KrdX { get; set; }
        public int KrdY { get; set; }
        public Koordinat()
        { }

        public Koordinat(int krdX, int krdY)
        {
            KrdX = krdX;
            KrdY = krdY;
        }
    }
    public class EgimiDuzeltveBoyutlandir
    {
        static string dosyaAdi;
        static int ustX;
        static int ustY;
        static int altX;
        static int altY;
        static int pikselFarki;
        static float aci;
        static Bitmap bmp;
        static readonly List<Koordinat> krd = new List<Koordinat>();

        /// <summary>
        /// Resmin soldan en küçük (x) koordinatý üzerinde en üst ve en alt noktasýndaki siyah þeklin eðimini kaldýrýr.
        /// </summary>
        /// <param name="ilkResimAdresi">ilk resim dosya adresi</param>
        /// <param name="yeniDosyaAdresi">yeni resim dosya adresi</param>
        /// <param name="imgWidth">Oluþturulacak resmin geniþliði</param>
        /// <param name="imgHeight">Oluþturulacak resmin yüksekliði</param>
        public static void Kaydet(string ilkResimAdresi, string yeniDosyaAdresi, int imgWidth, int imgHeight)
        {
            dosyaAdi = ilkResimAdresi;
            bmp = Siyahla(new Bitmap(dosyaAdi));

            UstXY();
            AltXY();
            pikselFarki = ustX - altX;

            // Baþlangýç için bir açý deðeri tanýmladým. Manuel denemelerde cevap kaðýtlarýnýn açýsý -0,50 derecelerde olduðundan varsayýlan olarak bunu atadým.
            aci = (float)Convert.ToDecimal("-0,50"); //varsayýlan açý
            //piksel farký 0 olana kadar döngü çalýþsýn
            //TODO:Sonsuz döngüye düþmemesi için döngü sayýsý verilebilir.
            while (pikselFarki != 0)
            {
                /* açýyý hesapla
                 * her döngüde piksel farkýna göre açýyý 0,10 oranýnda artýrýr veya azaltýr 
                 * eðer piksel farký 4ten az ise daha hassas açý için açýyý 0,01 oranýnda artýrýr veya azaltýr
                 */
                aci = pikselFarki > 0
                    ? (pikselFarki > 4
                        ? aci - (float)Convert.ToDecimal("0,10")
                        : aci - (float)Convert.ToDecimal("0,01"))
                    : (pikselFarki < -4
                        ? (float)Convert.ToDecimal("0,10") + aci
                        : (float)Convert.ToDecimal("0,01") + aci);
                //Resmi çevir
                using (Image newImage = RotateImage(new Bitmap(dosyaAdi), aci, ustX, ustY))
                {
                    bmp = Siyahla(new Bitmap(newImage));
                }
                AltXY();


                Application.DoEvents();

                pikselFarki = ustX - altX;
            }

            Application.DoEvents();

            //Açý bulunduktan sonra üst ve alt noktadaki piksel farký 0 dan farklý ise ayný açýda çevir
            Image newImage2 =  RotateImage(new Bitmap(dosyaAdi), aci, ustX, ustY);
            Bitmap orjBmp = new Bitmap(newImage2);

            using (Bitmap currentTile = new Bitmap(imgWidth, imgHeight))
            {
                currentTile.SetResolution(orjBmp.HorizontalResolution, orjBmp.VerticalResolution);
                using (Graphics currentTileGraphics = Graphics.FromImage(currentTile))
                {
                    currentTileGraphics.Clear(Color.Black);
                    Rectangle absentRectangleArea = new Rectangle(ustX, ustY, imgWidth, imgHeight);
                    currentTileGraphics.DrawImage(orjBmp, 0, 0, absentRectangleArea, GraphicsUnit.Pixel);
                }
                currentTile.Save(yeniDosyaAdresi);
            }

            

        }
        //Resmin en üst noktasýndaki siyah þeklin konumunu bulur
        private static void UstXY()
        {
            //Koordinat sýnýfýnýn deðerlerini sil. Her yeni açý hesaplamada gereklidir. Yoksa piksel sayýsý her döngüde kat kat artar.
            krd.Clear();
            //resmin 7de bir geniþliði 100den küçükse 100 deðilse resmin geniþliðinin 7de biri kadarda ara
            double width = Convert.ToDouble(bmp.Width) / 7;
            int genislik = width < 100 ? 100 : Convert.ToInt32(width);

            for (int i = 0; i <= genislik; i++)
            {
                for (int j = 0; j <= genislik; j++)
                {
                    Color piksel = bmp.GetPixel(i, j);
                    //siyah pikselleri seç
                    if (piksel.R == 0 && piksel.G == 0 && piksel.B == 0)
                    {
                        //koordinatlarý daha sonra hesaplamak için diziye aktar
                        krd.Add(new Koordinat(i, j));
                    }
                }
            }


            const int kare = 15;
            int adet = 0;
            //diziye aktarýlan siyah noktalarýn koordinatlarýnýn X eksenini en küçüðe göre sýrala
            var lstKrdx = (from k in krd orderby k.KrdX ascending select k).GroupBy(i => i.KrdX).Select(x => x.First());
            foreach (var g in lstKrdx)
            {
                //x ekseninin tekrar saysýný bul
                int xSayisi = (from s in krd where s.KrdX == g.KrdX select s).Count();
                //kare sayýsý kare deðiþkeninden fazla olan ilk deðeri al
                if (xSayisi >= kare && adet == 0)
                {
                    adet++;
                    ustX = g.KrdX;
                }
            }
            adet = 0;
            //diziye aktarýlan siyah noktalarýn koordinatlarýnýn Y eksenini en küçüðe göre sýrala
            var lstKrdy = (from k in krd orderby k.KrdY ascending select k).GroupBy(i => i.KrdY).Select(x => x.First());
            foreach (var g in lstKrdy)
            {
                //y ekseninin tekrar saysýný bul
                int ySayisi = (from s in krd where s.KrdY == g.KrdY select s).Count();
                //kare sayýsý kare deðiþkeninden fazla olan ilk deðeri al
                if (ySayisi >= kare && adet == 0)
                {
                    adet++;
                    ustY = g.KrdY;
                }
            }

            Application.DoEvents();
        }
        //Resmin en alt noktasýndaki siyah þeklin konumunu bulur
        private static void AltXY()
        {
            krd.Clear();

            //resmin 7de bir geniþliði 100den küçükse 100 deðilse resmin geniþliðinin 7de biri kadarda ara
            double width = Convert.ToDouble(bmp.Width) / 7;
            int genislik = width < 100 ? 100 : Convert.ToInt32(width);

            for (int i = 0; i <= genislik; i++)
            {
                for (int j = bmp.Height - genislik; j <= bmp.Height - 1; j++)
                {
                    Color piksel = bmp.GetPixel(i, j);
                    //siyah pikselleri seç
                    if (piksel.R == 0 && piksel.G == 0 && piksel.B == 0)
                    {
                        //koordinatlarý daha sonra hesaplamak için diziye aktar
                        krd.Add(new Koordinat(i, j));
                    }
                }
            }

            Application.DoEvents();


            const int kare = 15;
            int adet = 0;
            //diziye aktarýlan siyah noktalarýn koordinatlarýnýn X eksenini en küçüðe göre sýrala
            var lstKrdx = (from k in krd orderby k.KrdX ascending select k).GroupBy(i => i.KrdX).Select(x => x.First());
            foreach (var g in lstKrdx)
            {
                //x ekseninin tekrar saysýný bul
                int xSayisi = (from s in krd where s.KrdX == g.KrdX select s).Count();
                //kare sayýsý kare deðiþkeninden fazla olan ilk deðeri al
                if (xSayisi >= kare && adet == 0)
                {
                    adet++;
                    altX = g.KrdX;
                }
            }
            adet = 0;
            //diziye aktarýlan siyah noktalarýn koordinatlarýnýn Y eksenini en küçüðe göre sýrala
            var lstKrdy = (from k in krd orderby k.KrdY ascending select k).GroupBy(i => i.KrdY).Select(x => x.First());
            foreach (var g in lstKrdy)
            {
                //y ekseninin tekrar saysýný bul
                int ySayisi = (from s in krd where s.KrdY == g.KrdY select s).Count();
                //kare sayýsý kare deðiþkeninden fazla olan ilk deðeri al
                if (ySayisi >= kare && adet == 0)
                {
                    adet++;
                    altY = g.KrdY;
                }
            }

            Application.DoEvents();
        }
        /// <summary>
        /// Eðimi bulmak için döngü ile açýyý hesaplama metodu 
        /// </summary>
        private static void ResimdekiAciyiBul()
        {

        }
        private static void ResmiKirp(int w, int h, string yeniDosyaAdresi)
        {

        }
        /// <summary>
        /// Resmi belli açýya göre çevirme metodu
        /// </summary>
        /// <param name="image"></param>
        /// <param name="aciDerece">Açý</param>
        /// <param name="x">X koordinatý</param>
        /// <param name="y">Y koordinatý</param>
        /// <returns></returns>
        private static Bitmap RotateImage(Image image, float aciDerece, int x, int y)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            PointF offset = new PointF(x, y);

            //döndürülmüþ görüntüyü tutmak için yeni bir boþ bitmap oluþtur
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);

            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //boþ bitmapten bir grafik nesnesi hazýrla
            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                //Resim belirlenene açýda döndükten sonra taþan alanlarý beyaza boya
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
        private static Bitmap Siyahla(Bitmap bmpx)
        {
            //otsu filitresi uygulayarak resmi siyahla.
            OtsuThreshold otsuFiltre = new OtsuThreshold();
            Bitmap filtreliResim =
                otsuFiltre.Apply(bmpx.PixelFormat != PixelFormat.Format8bppIndexed
                    ? Grayscale.CommonAlgorithms.BT709.Apply(bmpx)
                    : bmpx);
            return filtreliResim;
        }
    }
}