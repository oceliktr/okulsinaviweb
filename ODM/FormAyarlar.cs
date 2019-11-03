using System;
using System.Windows.Forms;
using DAL;
using ODM.Kutuphanem;
using ODM.AktifSinavServisRef;

namespace ODM
{
    public partial class FormAyarlar : Form
    {
        private class Sinavlar
        {

        }
        public FormAyarlar()
        {
            InitializeComponent();

        }
        private void btnCKKontrolDizin_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı kapat
                folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory; //başlangıç dizini
                folderDialog.Description = @"Kontrol edilen cevap kağıdı evraklarının saklanacağı dizini seçiniz.";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtCKBulunduguDizin.Text = folderDialog.SelectedPath;
                    IniIslemleri.VeriYaz("CKDizinKontrol", "CKBulunduguAdres", folderDialog.SelectedPath);
                }
            }
        }
        private void FormAyarlar_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.None && e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        private void FormAyarlar_Activated(object sender, EventArgs e)
        {
            txtCKBulunduguDizin.Text = IniIslemleri.VeriOku("CKDizinKontrol", "CKBulunduguAdres");
            txtCKW.Text = IniIslemleri.VeriOku("CKBoyut", "W");
            txtCKH.Text = IniIslemleri.VeriOku("CKBoyut", "H");
            txtIlAdi.Text = IniIslemleri.VeriOku("Baslik", "IlAdi");

            string ilAdi = IniIslemleri.VeriOku("Baslik", "IlAdi");
            Text = "Ayarlar - " + ilAdi.IlkHarfleriBuyut() + " Ölçme ve Değerlendirme Merkezi";

            SinavlarDb veriDb = new SinavlarDb();
            cbSinavlar.DataSource = veriDb.KayitlariGetir();
            cbSinavlar.DisplayMember = "SinavAdi";
            cbSinavlar.ValueMember = "Id";

            SinavlarInfo infoS = veriDb.AktifSinavAdi();
            int sinavId = infoS.SinavId;
            cbVeriGirisi.Checked = infoS.VeriGirisi == 1;
            cbSinavlar.SelectedValue = sinavId;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtCKW.Text.ToInt32() < 100 || txtCKH.Text.ToInt32() < 100)
            {
                MessageBox.Show(@"Geçerli değer giniz (Min:100)", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string webAdresi = "www.meb.gov.tr";
                if (txtWebAdresi.Text.Length > 0 && txtWebAdresi.Text.IsUrl())
                    webAdresi = txtWebAdresi.Text.Replace("http://","").Replace("https://","");

                int sinavId = cbSinavlar.SelectedValue.ToInt32();
                int veriGirisi = cbVeriGirisi.Checked ? 1 : 0;

                AyarlarDb ayrDb = new AyarlarDb();
                ayrDb.KayitGuncelle(sinavId, veriGirisi);


                if (AgIslemleri.InternetControl(webAdresi))
                {
                    AktifSinavSoapClient refr = new AktifSinavSoapClient();
                    refr.GuncelSinav(sinavId, veriGirisi, "B7BC3B9344001FF88AA061BDB901BE29");
                }
                else
                {
                    MessageBox.Show("İnternet bağlantınız yok veya "+webAdresi+" adresine erişilemedi.\n\nBu nedenle seçili sınav bilgileri web adresine gönderilemedi. Ancak diğer işlemler gerçekleştirildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                IniIslemleri.VeriYaz("Baslik", "WebAdresi", txtWebAdresi.Text);
                IniIslemleri.VeriYaz("Baslik", "IlAdi", txtIlAdi.Text.ToUpper());
                IniIslemleri.VeriYaz("Baslik", "WebAdresi", txtIlAdi.Text.ToUpper());
                IniIslemleri.VeriYaz("CKBoyut", "W", txtCKW.Text);
                IniIslemleri.VeriYaz("CKBoyut", "H", txtCKH.Text);
                Close();
            }
        }
    }
}
