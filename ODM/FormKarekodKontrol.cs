using System;
using System.Drawing;
using System.Windows.Forms;
using ODM.Kutuphanem;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace ODM
{
    public partial class FormKarekodKontrol : Form
    {
        static readonly string ckDizin = IniIslemleri.VeriOku("CKDizinKontrol", "CKBulunduguAdres");
        public FormKarekodKontrol()
        {
            InitializeComponent();

            string ilAdi = IniIslemleri.VeriOku("Baslik", "IlAdi");
            Text = "Karekod Kontrol Formu - " + ilAdi.IlkHarfleriBuyut() + " Ölçme ve Değerlendirme Merkezi";

        }

        private void btnKarekod_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog
            {
                InitialDirectory = ckDizin,
                Filter = "Resim Dosyası|*.jpg;*.png;*.gif",
                Title = "Karekod dosyasını seçiniz."
            };

            if (o.ShowDialog() != DialogResult.OK) return;
            pcKarekodOcr.ImageLocation = o.FileName;
            try
            {
                QRCodeDecoder decoder = new QRCodeDecoder();
                string kareKod = decoder.decode(new QRCodeBitmapImage(new Bitmap(o.FileName)));
                txtSonuc.Text = kareKod;
            }
            catch (Exception ex)
            {
                txtSonuc.Text = "Hata:"+ex.Message;
            }
        }

        private void btnOcr_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog
            {
                InitialDirectory = ckDizin,
                Filter = "Resim Dosyası|*.jpg;*.png;*.gif",
                Title = "Karekod dosyasını seçiniz."
            };

            if (o.ShowDialog() != DialogResult.OK) return;
            pcKarekodOcr.ImageLocation = o.FileName;
            try
            {
                string kareKod = Ocr.OcrCevir(o.FileName, Ocr.Dil.Turkce);
                txtSonuc.Text = kareKod;
            }
            catch (Exception ex)
            {
                txtSonuc.Text = "Hata:" + ex.Message;
            }
        }

        private void txtSonuc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
