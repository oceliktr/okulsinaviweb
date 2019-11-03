using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;
using DAL;
using ODM.Kutuphanem;

namespace ODM
{
    public partial class FormSinifListesi : Form
    {
        private readonly int sinavId;
        private string seciliDizin;
        public FormSinifListesi()
        {
            InitializeComponent();
            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo sinfo = snvDb.AktifSinavAdi();
            lblSinavAdi.Text = string.Format("Sınav Adı: {0}", sinfo.SinavAdi);
            lblSinavNo.Text = string.Format("Sınav No: {0}", sinfo.SinavId);
            sinavId = sinfo.SinavId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Resim Dosyaları (*.jpg;*.png)|*.jpg;*.png",
                Title = "Sınıf Listesi için arka plan dosyası seçiniz"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı kapat
                folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory; //başlangıç dizini
                folderDialog.Description = @"Cevap kağıtlarının oluşturulacağı dizini seçiniz.";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    seciliDizin = folderDialog.SelectedPath;

                    bgwSinifListesi.RunWorkerAsync();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            lblSinavAdi.Text = string.Format("{0}x{1}", e.X, e.Y);
        }

        private void bgwSinifListesi_DoWork(object sender, DoWorkEventArgs e)
        {

            int a = 0;
            int x = 367; //başlangıç x koordinatı
            int y = 272;//başlangıç y koordinatı
            const int w = 38;
            const int h = 38;
            int artim = 49;// iki boşluk arasındaki fark
            string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

            //Sınav numarasına göre Öğrenci bilgilerini çekelim
            OgrencilerDb veriDb = new OgrencilerDb();
            List<OgrencilerInfo> info = veriDb.SinavaGirenSubeler(sinavId);

            KurumlarDb krmDb = new KurumlarDb();

            progressBar1.Maximum = info.Count;
            progressBar1.Value = 0;

            foreach (OgrencilerInfo ogr in info)
            {
                string sube = ogr.Sube;
                string sinifi = ogr.Sinifi.ToString();
                string kurumKodu = ogr.KurumKodu.ToString();
                KurumlarInfo krm = krmDb.KayitBilgiGetir(kurumKodu);
                string ilceAdi = krm.IlceAdi;
                string kurumAdi = krm.KurumAdi;
                a++;
                progressBar1.Value = a;
                //bitmap sınıfından bir nesne üreterek pictureboxu tanımlıyoruz.
                Bitmap img = new Bitmap(pictureBox1.Image);
                //çizilecek nesne tanımlanıyor
                Graphics ckImage = Graphics.FromImage(img);

                //dolgu renk ve font ayarlarını yap
                Brush dolgu = new SolidBrush(Color.Black);
                Font f = new Font(Font.FontFamily, 23, FontStyle.Regular);
                ckImage.DrawString(ilceAdi, f, Brushes.Black, 1220, 370); //İlçe Adı
                ckImage.DrawString(kurumAdi, f, Brushes.Black, 1220, 440); //Kurum Adı
                ckImage.DrawString(sinifi + " - " + sube, f, Brushes.Black, 1220, 502); //Sınıf Şube
                ckImage.FillEllipse(dolgu, 808, 419, w, h); //optik 5. sınıf

                int subeKonumu = harfler.IndexOf(sube, StringComparison.Ordinal);
                //optik şube
                int subeX = 906;
                ckImage.FillEllipse(dolgu, subeX + (artim * subeKonumu), 713, w, h);


                //Optik Kurumkodu işaretleme döngüsü
                for (int sag = 0; sag < 8; sag++)
                {
                    for (int asagi = 0; asagi < 10; asagi++)
                    {
                        for (int z = 0; z < kurumKodu.Length; z++)
                        {
                            //sadece gelen değerleri işaretlemek için sorgu
                            if (sag == 7 - (kurumKodu.Length - (z + 1)) && asagi == Convert.ToInt32(kurumKodu.Substring(z, 1)))
                            {
                                ckImage.FillEllipse(dolgu, x + (artim * sag), y + (artim * asagi), w, h);
                                ckImage.DrawString(asagi.ToString(), f, Brushes.Black, x + (sag * artim) + 5, 225);
                            }
                        }

                    }
                }
                List<OgrencilerInfo> ogrList = veriDb.KayitlariDiziyeGetir(sinavId, kurumKodu.ToInt32(), sinifi.ToInt32(), sube);

                for (var index = 0; index < ogrList.Count; index++)
                {
                    if (ogrList[index].OgrOkulNo != 0) //yedek olmayan öğrenciler için !=0
                    {
                        ckImage.DrawString(ogrList[index].OgrOkulNo.ToString(), f, Brushes.Black, 457,
                            915 + (artim * index)); //Sınıf Şube
                        ckImage.DrawString(ogrList[index].Adi + " " + ogrList[index].Soyadi, f, Brushes.Black, 615,
                            915 + (artim * index)); //Sınıf Şube}
                    }
                }
                img.SetResolution(300, 300);
                img.Save(seciliDizin + "\\" + ilceAdi + "-" + kurumAdi + "-" + sinifi + "-" + sube + ".jpg", ImageFormat.Jpeg);
                ckImage.Dispose();
                Application.DoEvents();
            }
            progressBar1.Value = 0;
            MessageBox.Show("Sınıf listesi oluşturuldu.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //  pictureBox1.Image = img;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument1 = new PrintDocument();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.Print();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pictureBox1.Image, 0, 0);
        }
    }
}
