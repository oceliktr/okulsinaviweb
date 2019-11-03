using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using DAL;
using ODM.Kutuphanem;
using ThoughtWorks.QRCode.Codec;

namespace ODM
{
    public partial class FormKareKodOlustur : Form
    {
        private readonly string ckKarekodDizin = IniIslemleri.VeriOku("CKDizinKontrol", "CKBulunduguAdres") + "\\CKKarekod\\";
        public FormKareKodOlustur()
        {
            InitializeComponent();
            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo sinfo = snvDb.AktifSinavAdi();
            lblSinavAdi.Text = string.Format("Sınav Adı: {0}", sinfo.SinavAdi);
            lblSinavNo.Text = string.Format("Sınav No: {0}", sinfo.SinavId);


            string ilAdi = IniIslemleri.VeriOku("Baslik", "IlAdi");
            Text = "Karekod Oluşturma Formu" + ilAdi.IlkHarfleriBuyut() + " Ölçme ve Değerlendirme Merkezi";

        }
        private void btnEncode_Click(object sender, EventArgs e)
        {
            if (cboCorrectionLevel.Text == "")
            {
                MessageBox.Show(@"Düzeltme seviyesi seçmediniz.", @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cbSayfaSayisi.Text == "")
            {
                MessageBox.Show(@"Sayfa sayısı seçmediniz.", @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                btnEncode.Enabled = true;
                if (!DizinIslemleri.DizinKontrol(ckKarekodDizin))
                    DizinIslemleri.DizinOlustur(ckKarekodDizin);
                else
                    DizinIslemleri.DizinIceriginiSil(ckKarekodDizin);

                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void FormKareKodKontrol_Activated(object sender, EventArgs e)
        {
            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo sinfo = snvDb.AktifSinavAdi();
            lblSinavAdi.Text = string.Format("Sınav Adı: {0}", sinfo.SinavAdi);
            lblSinavNo.Text = string.Format("Sınav No: {0}", sinfo.SinavId);
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo sinfo = snvDb.AktifSinavAdi();
            int sinavId = sinfo.SinavId;

            OgrencilerDb veriDb = new OgrencilerDb();
            List<OgrencilerInfo> info = veriDb.KayitlariDiziyeGetir(sinavId);


            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC,
                QRCodeScale = 7,
                QRCodeVersion = 7
            };
            
            string errorCorrect = cboCorrectionLevel.Text;
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            int a = 0;
            progressBar1.Maximum = info.Count * cbSayfaSayisi.Text.ToInt32();
            progressBar1.Value = 0;
            foreach (OgrencilerInfo ogr in info)
            {
                for (int i = 1; i <= cbSayfaSayisi.Text.ToInt32(); i++)
                {
                    string qrCode = string.Format("{0}{1}", i, ogr.OgrenciId);
                    pbQrCode.Image = qrCodeEncoder.Encode(qrCode);
                    string dosyaAdresi = string.Format(@"{0}{1}.png", ckKarekodDizin, qrCode);
                    pbQrCode.Image.Save(dosyaAdresi, ImageFormat.Png);

                    a++;
                    progressBar1.Value = a;

                    int yuzde = (int)((progressBar1.Value / (double)progressBar1.Maximum) * 100);
                    progressBar1.PerformStep();
                    progressBar1.CreateGraphics()
                        .DrawString("% " + yuzde, new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black,
                            new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));
                }
            }
            Application.DoEvents();
            DialogResult dialog = MessageBox.Show(@"Kare kodlar oluşturuldu. Karekodların bulunduğu dizini açmak ister misiniz?", @"Bilgi", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
                System.Diagnostics.Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(ckKarekodDizin));
            Close();
        }

        private void FormKareKodOlustur_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.None && e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
