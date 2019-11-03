using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ODM.CKYazdirDb.Library;
using ODM.CKYazdirDb.Model;

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
            Sablon sablon = new Sablon();
            //cbSablonTurleri.DataSource = sablon.Sablonlar();
            //cbSablonTurleri.DisplayMember = "SablonAdi";
            //cbSablonTurleri.ValueMember = "Id";

         

            List<Sablon> brns = new List<Sablon> { new Sablon("0", "Şablon Seçiniz") };
            brns.AddRange(sablon.Sablonlar().Select(t => new Sablon(t.Id, t.SablonAdi)));

            cbSablonTurleri.ValueMember = "Id";
            cbSablonTurleri.DisplayMember = "SablonAdi";

            foreach (var o in brns)
            {
                cbSablonTurleri.Items.Add(new Brans(o.Id.ToInt32(), o.SablonAdi));
            }
            cbSablonTurleri.DataSource = brns;


            Ayarlar ayar = ayarlarManager.AyarlariGetir();
            txtSinavAdi.Text = ayar.SinavAdi;
            txtIlAdi.Text = ayar.IlAdi;
            pbLogo.ImageLocation = ayar.Logo;
            txtLogo.Text = ayar.Logo;
            txtCk.Text = ayar.CkSablon;
            txtSinifListesi.Text = ayar.SinifListesiSablon;
            cbSablonTurleri.SelectedValue = ayar.SablonTuru;

            txtOdmAdres.Text = ayar.OdmAdres;
            txtWeb.Text = ayar.OdmWeb;
            txtEposta.Text = ayar.OdmEmail;

        }
        private void btnCkGozat_Click(object sender, EventArgs e)
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
                    txtCk.Text = dosyaAdi;
                }
            }
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
            ayar.CkSablon = txtCk.Text;
            ayar.SinifListesiSablon = txtSinifListesi.Text;
            ayar.SablonTuru = cbSablonTurleri.SelectedValue.ToString();
            ayar.SinavAdi = txtSinavAdi.Text;
            ayar.OdmAdres = txtOdmAdres.Text;
            ayar.OdmWeb = txtWeb.Text;
            ayar.OdmEmail = txtEposta.Text;

            int res = ayarlarManager.Update(ayar);
            if (res == 0)
                MessageBox.Show("Kayıt yapılamadı.");
            else
                Close();
        }

        private void FormAyarlar_Activated(object sender, EventArgs e)
        {

        }
    }
}
