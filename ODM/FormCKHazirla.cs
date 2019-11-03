using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using ODM.Kutuphanem;
using ThoughtWorks.QRCode.Codec;

namespace ODM
{
    public partial class FormCKHazirla : Form
    {
        private readonly int sinavId;
        private string seciliDizin;
        public FormCKHazirla()
        {
            InitializeComponent();
            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo sinfo = snvDb.AktifSinavAdi();
            lblSinavAdi.Text = string.Format("Sınav Adı: {0}", sinfo.SinavAdi);
            lblSinavNo.Text = string.Format("Sınav No: {0}", sinfo.SinavId);
            sinavId = sinfo.SinavId;

            string ilAdi = IniIslemleri.VeriOku("Baslik", "IlAdi");
            Text = "Cevap Kağıdı Oluşturma Formu " + ilAdi.IlkHarfleriBuyut() + " Ölçme ve Değerlendirme Merkezi";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbOturum.Text=="")
            {
                MessageBox.Show("Oturum seçmediniz.");
            }
            else
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

                        bgwCkOlustur.RunWorkerAsync();
                    }
                }
            }
        }

        private void bgwCkOlustur_DoWork(object sender, DoWorkEventArgs e)
        {
            int a = 0;
            string oturum = cbOturum.Text.Substring(0, 1);
            DizinIslemleri.DizinOlustur(seciliDizin+"\\"+oturum);

           
            //Karekodun modlarını ayarla
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC,
                QRCodeScale = 7,
                QRCodeVersion = 5,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L
            };
            Rectangle rct = new Rectangle(620, 953, 300, 300);

            //optikte işaretlenecek alanların bilgileri
            const int w = 38;
            const int h = 38;
            int x = 1305; //başlangıç x koordinatı
            int y = 1786;//başlangıç y koordinatı
            int artim = 50;// iki boşluk arasındaki fark

            //Sınav numarasına göre Öğrenci bilgilerini çekelim
            OgrencilerDb veriDb = new OgrencilerDb();
            List<OgrencilerInfo> info = veriDb.KayitlariDiziyeGetir(sinavId);

            KurumlarDb krmDb = new KurumlarDb();

            progressBar1.Maximum = info.Count;
            progressBar1.Value = 0;

            foreach (OgrencilerInfo ogr in info)
            {
                a++;
                progressBar1.Value = a;

                //bitmap sınıfından bir nesne üreterek pictureboxu tanımlıyoruz.
                Bitmap img = new Bitmap(pictureBox1.Image);
                //çizilecek nesne tanımlanıyor
                Graphics ckImage = Graphics.FromImage(img);

                //dolgu renk ve font ayarlarını yap
                Brush dolgu = new SolidBrush(Color.Black);
                Font f = new Font(Font.FontFamily, 23, FontStyle.Regular);


                string ogrenciId = oturum + ogr.OgrenciId;
                KurumlarInfo krm = krmDb.KayitBilgiGetir(ogr.KurumKodu.ToString());
                //grafik çizimlerini başlatıyoruz.
                ckImage.DrawString(ogr.Adi + " " + ogr.Soyadi, f, Brushes.Black, 430, 510); //adı soyadı
                ckImage.DrawString(ogr.Sinifi + " - " + ogr.Sube, f, Brushes.Black, 430, 588); //sınıfı
                if (ogr.OgrOkulNo != 0)//yedek öğrencilerde öğrenci no 0 olduğu için öğr no 0 ise yazma
                    ckImage.DrawString(ogr.OgrOkulNo.ToString(), f, Brushes.Black, 430, 665); //no
                ckImage.DrawString(krm.IlceAdi, f, Brushes.Black, 430, 743); //ilçe
                ckImage.DrawString(krm.KurumAdi, f, Brushes.Black, 430, 822); //okulu
                ckImage.DrawString(ogrenciId, f, Brushes.Black, 1060, 1665); //ocr

                //QR kodunu grafik nesnesine tanımlayalım.
                ckImage.DrawImage(new Bitmap(qrCodeEncoder.Encode(ogrenciId, Encoding.UTF8)), rct);


                for (int sag = 0; sag < 6; sag++)
                {
                    for (int asagi = 0; asagi < 10; asagi++)
                    {
                        for (int z = 0; z < ogrenciId.Length; z++)
                        {
                            //sadece gelen değerleri işaretlemek için sorgu
                            if (sag == 5 - (ogrenciId.Length - (z + 1)) &&
                                asagi == Convert.ToInt32(ogrenciId.Substring(z, 1)))
                            {
                                ckImage.FillEllipse(dolgu, x + (artim * sag), y + (artim * asagi), w, h);
                            }
                        }

                    }
                }

                // pictureBox1.Image = img;
                img.SetResolution(300,300);
                img.Save(seciliDizin +"\\"+oturum+"\\"+ ogrenciId + ".jpg", ImageFormat.Jpeg);
                ckImage.Dispose();
                Application.DoEvents();
            }
            progressBar1.Value = 0;
            MessageBox.Show("Cevap kağıtları oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Resim Dosyaları (*.jpg;*.png)|*.jpg;*.png",
                Title = "Cevap kağıdı dosyasını seçiniz."
            };
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
            }
        }
    }
}
