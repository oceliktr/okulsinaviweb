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
        /// Resmin soldan en k���k (x) koordinat� �zerinde en �st ve en alt noktas�ndaki siyah �eklin e�imini kald�r�r.
        /// </summary>
        /// <param name="ilkResimAdresi">ilk resim dosya adresi</param>
        /// <param name="yeniDosyaAdresi">yeni resim dosya adresi</param>
        /// <param name="imgWidth">Olu�turulacak resmin geni�li�i</param>
        /// <param name="imgHeight">Olu�turulacak resmin y�ksekli�i</param>
        public static void Kaydet(string ilkResimAdresi, string yeniDosyaAdresi, int imgWidth, int imgHeight)
        {
            dosyaAdi = ilkResimAdresi;
            bmp = Siyahla(new Bitmap(dosyaAdi));

            UstXY();
            AltXY();
            pikselFarki = ustX - altX;

            // Ba�lang�� i�in bir a�� de�eri tan�mlad�m. Manuel denemelerde cevap ka��tlar�n�n a��s� -0,50 derecelerde oldu�undan varsay�lan olarak bunu atad�m.
            aci = (float)Convert.ToDecimal("-0,50"); //varsay�lan a��
            //piksel fark� 0 olana kadar d�ng� �al��s�n
            //TODO:Sonsuz d�ng�ye d��memesi i�in d�ng� say�s� verilebilir.
            while (pikselFarki != 0)
            {
                /* a��y� hesapla
                 * her d�ng�de piksel fark�na g�re a��y� 0,10 oran�nda art�r�r veya azalt�r 
                 * e�er piksel fark� 4ten az ise daha hassas a�� i�in a��y� 0,01 oran�nda art�r�r veya azalt�r
                 */
                aci = pikselFarki > 0
                    ? (pikselFarki > 4
                        ? aci - (float)Convert.ToDecimal("0,10")
                        : aci - (float)Convert.ToDecimal("0,01"))
                    : (pikselFarki < -4
                        ? (float)Convert.ToDecimal("0,10") + aci
                        : (float)Convert.ToDecimal("0,01") + aci);
                //Resmi �evir
                using (Image newImage = RotateImage(new Bitmap(dosyaAdi), aci, ustX, ustY))
                {
                    bmp = Siyahla(new Bitmap(newImage));
                }
                AltXY();


                Application.DoEvents();

                pikselFarki = ustX - altX;
            }

            Application.DoEvents();

            //A�� bulunduktan sonra �st ve alt noktadaki piksel fark� 0 dan farkl� ise ayn� a��da �evir
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
        //Resmin en �st noktas�ndaki siyah �eklin konumunu bulur
        private static void UstXY()
        {
            //Koordinat s�n�f�n�n de�erlerini sil. Her yeni a�� hesaplamada gereklidir. Yoksa piksel say�s� her d�ng�de kat kat artar.
            krd.Clear();
            //resmin 7de bir geni�li�i 100den k���kse 100 de�ilse resmin geni�li�inin 7de biri kadarda ara
            double width = Convert.ToDouble(bmp.Width) / 7;
            int genislik = width < 100 ? 100 : Convert.ToInt32(width);

            for (int i = 0; i <= genislik; i++)
            {
                for (int j = 0; j <= genislik; j++)
                {
                    Color piksel = bmp.GetPixel(i, j);
                    //siyah pikselleri se�
                    if (piksel.R == 0 && piksel.G == 0 && piksel.B == 0)
                    {
                        //koordinatlar� daha sonra hesaplamak i�in diziye aktar
                        krd.Add(new Koordinat(i, j));
                    }
                }
            }


            const int kare = 15;
            int adet = 0;
            //diziye aktar�lan siyah noktalar�n koordinatlar�n�n X eksenini en k����e g�re s�rala
            var lstKrdx = (from k in krd orderby k.KrdX ascending select k).GroupBy(i => i.KrdX).Select(x => x.First());
            foreach (var g in lstKrdx)
            {
                //x ekseninin tekrar says�n� bul
                int xSayisi = (from s in krd where s.KrdX == g.KrdX select s).Count();
                //kare say�s� kare de�i�keninden fazla olan ilk de�eri al
                if (xSayisi >= kare && adet == 0)
                {
                    adet++;
                    ustX = g.KrdX;
                }
            }
            adet = 0;
            //diziye aktar�lan siyah noktalar�n koordinatlar�n�n Y eksenini en k����e g�re s�rala
            var lstKrdy = (from k in krd orderby k.KrdY ascending select k).GroupBy(i => i.KrdY).Select(x => x.First());
            foreach (var g in lstKrdy)
            {
                //y ekseninin tekrar says�n� bul
                int ySayisi = (from s in krd where s.KrdY == g.KrdY select s).Count();
                //kare say�s� kare de�i�keninden fazla olan ilk de�eri al
                if (ySayisi >= kare && adet == 0)
                {
                    adet++;
                    ustY = g.KrdY;
                }
            }

            Application.DoEvents();
        }
        //Resmin en alt noktas�ndaki siyah �eklin konumunu bulur
        private static void AltXY()
        {
            krd.Clear();

            //resmin 7de bir geni�li�i 100den k���kse 100 de�ilse resmin geni�li�inin 7de biri kadarda ara
            double width = Convert.ToDouble(bmp.Width) / 7;
            int genislik = width < 100 ? 100 : Convert.ToInt32(width);

            for (int i = 0; i <= genislik; i++)
            {
                for (int j = bmp.Height - genislik; j <= bmp.Height - 1; j++)
                {
                    Color piksel = bmp.GetPixel(i, j);
                    //siyah pikselleri se�
                    if (piksel.R == 0 && piksel.G == 0 && piksel.B == 0)
                    {
                        //koordinatlar� daha sonra hesaplamak i�in diziye aktar
                        krd.Add(new Koordinat(i, j));
                    }
                }
            }

            Application.DoEvents();


            const int kare = 15;
            int adet = 0;
            //diziye aktar�lan siyah noktalar�n koordinatlar�n�n X eksenini en k����e g�re s�rala
            var lstKrdx = (from k in krd orderby k.KrdX ascending select k).GroupBy(i => i.KrdX).Select(x => x.First());
            foreach (var g in lstKrdx)
            {
                //x ekseninin tekrar says�n� bul
                int xSayisi = (from s in krd where s.KrdX == g.KrdX select s).Count();
                //kare say�s� kare de�i�keninden fazla olan ilk de�eri al
                if (xSayisi >= kare && adet == 0)
                {
                    adet++;
                    altX = g.KrdX;
                }
            }
            adet = 0;
            //diziye aktar�lan siyah noktalar�n koordinatlar�n�n Y eksenini en k����e g�re s�rala
            var lstKrdy = (from k in krd orderby k.KrdY ascending select k).GroupBy(i => i.KrdY).Select(x => x.First());
            foreach (var g in lstKrdy)
            {
                //y ekseninin tekrar says�n� bul
                int ySayisi = (from s in krd where s.KrdY == g.KrdY select s).Count();
                //kare say�s� kare de�i�keninden fazla olan ilk de�eri al
                if (ySayisi >= kare && adet == 0)
                {
                    adet++;
                    altY = g.KrdY;
                }
            }

            Application.DoEvents();
        }
        /// <summary>
        /// E�imi bulmak i�in d�ng� ile a��y� hesaplama metodu 
        /// </summary>
        private static void ResimdekiAciyiBul()
        {

        }
        private static void ResmiKirp(int w, int h, string yeniDosyaAdresi)
        {

        }
        /// <summary>
        /// Resmi belli a��ya g�re �evirme metodu
        /// </summary>
        /// <param name="image"></param>
        /// <param name="aciDerece">A��</param>
        /// <param name="x">X koordinat�</param>
        /// <param name="y">Y koordinat�</param>
        /// <returns></returns>
        private static Bitmap RotateImage(Image image, float aciDerece, int x, int y)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            PointF offset = new PointF(x, y);

            //d�nd�r�lm�� g�r�nt�y� tutmak i�in yeni bir bo� bitmap olu�tur
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);

            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //bo� bitmapten bir grafik nesnesi haz�rla
            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                //Resim belirlenene a��da d�nd�kten sonra ta�an alanlar� beyaza boya
                g.Clear(Color.White);

                //Put the rotation point in the center of the image
                g.TranslateTransform(offset.X, offset.Y);

                //resmi �evir
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