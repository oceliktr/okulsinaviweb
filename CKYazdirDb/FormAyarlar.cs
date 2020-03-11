using ODM.CKYazdirDb.Entities;
using ODM.CKYazdirDb.Library;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ODM.CKYazdirDb
{
    public partial class FormAyarlar : Form
    {
        readonly AyarlarManager ayarlarManager = new AyarlarManager();
        public FormAyarlar()
        {
            InitializeComponent();
        }

        private void btnGozat_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string startPath = Application.StartupPath + "\\Files";
                    string dosyaAdi = startPath + "\\" + Path.GetFileName(ofd.FileName);

                    //klasör yoksa oluştur
                    if (!DizinIslemleri.DizinKontrol(startPath))
                        DizinIslemleri.DizinOlustur(startPath);


                    DizinIslemleri.DosyaKopyala(ofd.FileName, dosyaAdi, true);
                    pbLogo.ImageLocation = dosyaAdi;
                    txtLogo.Text = dosyaAdi;
                }
            }
        }

        private void FormAyarlar_Load(object sender, EventArgs e)
        {
            Ayarlar ayar = ayarlarManager.AyarlariGetir();
            txtSinavAdi.Text = ayar.SinavAdi;
            txtIlAdi.Text = ayar.IlAdi;
            pbLogo.ImageLocation = ayar.Logo;
            txtLogo.Text = ayar.Logo;
            txtSinifListesi.Text = ayar.SinifListesiSablon;

            if (ayar.DegerlendirmeTuru == DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32())
            {
                rbKazanim.Checked = true;
            }
            else
            {
                rbKonu.Checked = true;
            }
            txtOdmAdres.Text = ayar.OdmAdres;
            txtWeb.Text = ayar.OdmWeb;
            txtEposta.Text = ayar.OdmEmail;
        }
       
        private void label4_Click(object sender, EventArgs e)
        {
            Process.Start("http://erzurumodm.meb.gov.tr/www/erzurum-odm-ck-kodlama-yazilimi-guncellendi-v13/icerik/31");
        }



        private void btnSinifListesiGozat_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    string startPath = Application.StartupPath + "\\Files";
                    string dosyaAdi = startPath + "\\" + Path.GetFileName(ofd.FileName);

                    //klasör yoksa oluştur
                    if (!DizinIslemleri.DizinKontrol(startPath))
                        DizinIslemleri.DizinOlustur(startPath);


                    DizinIslemleri.DosyaKopyala(ofd.FileName, dosyaAdi, true);
                    txtSinifListesi.Text = dosyaAdi;

                }
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {

            Ayarlar ayar = ayarlarManager.AyarlariGetir();
            ayar.Id = 1;
            ayar.Logo = txtLogo.Text;
            ayar.IlAdi = txtIlAdi.Text;
            ayar.SinifListesiSablon = txtSinifListesi.Text;
            ayar.SinavAdi = txtSinavAdi.Text;
            ayar.DegerlendirmeTuru = rbKazanim.Checked ? DegerlendirmeTurleri.DegerlendirmeTuru.KazanimBazli.ToInt32() : DegerlendirmeTurleri.DegerlendirmeTuru.KonuBazli.ToInt32();
            ayar.OdmAdres = txtOdmAdres.Text;
            ayar.OdmWeb = txtWeb.Text;
            ayar.OdmEmail = txtEposta.Text;

            int res = ayarlarManager.Update(ayar);
            if (res == 0)
                MessageBox.Show("Kayıt yapılamadı.");
            else
                Close();
        }

       
    }
}
