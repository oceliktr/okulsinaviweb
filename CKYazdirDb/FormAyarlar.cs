using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ODM.CKYazdirDb.Business;
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

            List<ComboboxItem> sinif = new List<ComboboxItem> { new ComboboxItem("0", "Sınıf Seçiniz") };
            for (int i = 1; i <= 12; i++)
            {
                sinif.Add(new ComboboxItem(i.ToString(), i + ". sınıf"));
            }

            cbSinif.ValueMember = "Text";
            cbSinif.DisplayMember = "Value";

            foreach (var s in sinif)
            {
                cbSinif.Items.Add(new ComboboxItem(s.Value, s.Text));
            }
            cbSinif.DataSource = sinif;



            Ayarlar ayar = ayarlarManager.AyarlariGetir();
            txtSinavAdi.Text = ayar.SinavAdi;
            txtIlAdi.Text = ayar.IlAdi;
            pbLogo.ImageLocation = ayar.Logo;
            txtLogo.Text = ayar.Logo;
            txtSinifListesi.Text = ayar.SinifListesiSablon;
            cbSinif.SelectedValue = ayar.SablonTuru;
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
            SablonlariListele();
        }
        private void btnCkGozat_Click(object sender, EventArgs e)
        {
            int sinif = cbSinif.SelectedValue.ToInt32();
            if (sinif == 0)
            {
                MessageBox.Show("Sınıf Seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {

                        string dosyaAdi = ofd.FileName;

                        CkSablonManager sablonManager = new CkSablonManager();
                        
                        if (sablonManager.Find(x => x.Sinif == sinif) != null)
                        {
                            MessageBox.Show("Bu sınıf için bir şablon var", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            CkSablon ckSablon = new CkSablon
                            {
                                Sablon = dosyaAdi,
                                Sinif = sinif
                            };
                            sablonManager.Insert(ckSablon);

                            SablonlariListele();
                        }

                    }
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
            ayar.SinifListesiSablon = txtSinifListesi.Text;
            ayar.SablonTuru = cbSinif.SelectedValue.ToString();
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

        private void SablonlariListele()
        {
            //cevapları listele
            CkSablonManager sablonManager = new CkSablonManager();
            dataGridView1.DataSource = sablonManager.List().OrderBy(x => x.Sinif).ToList();

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Sınıf";
            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[2].HeaderText = "Şablon";
            dataGridView1.Columns[2].Width = 195;
        }

        private void seçileniSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Seçilen şablonu silmek istediğinizden emin misiniz? (Bilgisayardaki dosyayı silmez)", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {

                int id = dataGridView1.SelectedRows[0].Cells[0].Value.ToInt32();
                CkSablonManager sablonManager = new CkSablonManager();
                CkSablon cvp = sablonManager.Find(x => x.Id == id);
                sablonManager.Delete(cvp);
                SablonlariListele();
            }
        }

        private void tümünüSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Tümünü silmek istediğinizden emin misiniz? (Bilgisayardaki dosyayı silmez)", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                CkSablonManager sablonManager = new CkSablonManager();
                sablonManager.TumunuSil();
                SablonlariListele();
            }
        }
    }
}
