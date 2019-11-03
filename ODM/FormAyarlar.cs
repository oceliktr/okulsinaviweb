using System;
using System.Windows.Forms;
using ODM.Kutuphanem;


namespace ODM
{
    public partial class FormAyarlar : Form
    {
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

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtCKW.Text.ToInt32() < 100|| txtCKH.Text.ToInt32() < 100)
            {
                MessageBox.Show(@"Geçerli değer giniz (Min:100)", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                IniIslemleri.VeriYaz("Baslik", "IlAdi", txtIlAdi.Text.ToUpper());
                IniIslemleri.VeriYaz("CKBoyut", "W", txtCKW.Text);
                IniIslemleri.VeriYaz("CKBoyut", "H", txtCKH.Text);
                Close();
            }
        }
    }
}
