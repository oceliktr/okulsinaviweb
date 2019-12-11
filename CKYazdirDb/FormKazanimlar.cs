using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.Library;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb
{
    public partial class FormKazanimlar : Form
    {
        private int kazanimId = 0;
        private readonly KazanimManager kazanimManager = new KazanimManager();
        private readonly BransManager bransManager = new BransManager();
        public FormKazanimlar()
        {
            InitializeComponent();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (cbBranslar.SelectedValue == null || cbBranslar.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Branş Seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (cbSinif.SelectedValue == null || cbSinif.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Sınıf Seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (txtKazanim.Text == "" || txtKazanimNo.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                string sorulari = txtSorulari.Text;

                //Son karakter virgül değil ise , ekleyelim.
                string sonKarakter = sorulari.Substring(sorulari.Length - 1, 1);
                if (sonKarakter != ",")
                    sorulari += ",";

                string kazanimOgrenci = txtKazanimOgrenci.Text == "" ? txtKazanim.Text:"";
                if (kazanimId == 0)
                {
                    Kazanim kazanim = new Kazanim()
                    {
                        BransId = cbBranslar.SelectedValue.ToInt32(),
                        KazanimAdi = txtKazanim.Text,
                        KazanimNo = txtKazanimNo.Text,
                        KazanimAdiOgrenci = kazanimOgrenci,
                        Sorulari = sorulari,
                        Sinif = cbSinif.SelectedValue.ToInt32()
                    };
                    kazanimManager.Insert(kazanim);
                }
                else
                {
                    Kazanim kazanim = kazanimManager.Find(x => x.Id == kazanimId);
                    if (kazanim != null)
                    {
                        kazanim.BransId = cbBranslar.SelectedValue.ToInt32();
                        kazanim.KazanimAdi = txtKazanim.Text;
                        kazanim.KazanimNo = txtKazanimNo.Text;
                        kazanim.KazanimAdiOgrenci = kazanimOgrenci;
                        kazanim.Sorulari = txtSorulari.Text;
                        kazanim.Sinif = cbSinif.SelectedValue.ToInt32();
                        kazanimManager.Update(kazanim);
                    }
                    else
                    {
                        MessageBox.Show("Düzenlenecek kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                kazanimId = 0;
                txtKazanimOgrenci.Text = "";
                txtKazanim.Text = "";
                txtKazanimNo.Text = "";
                txtSorulari.Text = "";
                btnKaydet.Text = "Kaydet";
                cbBranslar.Focus();

                KayitlariListele();
            }
        }

        private void KayitlariListele()
        {
            dgvKazanimlar.DataSource = kazanimManager.KazanimList().OrderBy(x => x.BransId).ToList();

            //Id = kznm.Id,
            //BransId = kznm.BransId,
            //Sinif = kznm.Sinif,
            //BransAdi = brns.BransAdi,
            //KazanimNo = kznm.KazanimNo,
            //KazanimAdi = kznm.KazanimAdi,
            //KazanimAdiOgrenci = kznm.KazanimAdiOgrenci

            dgvKazanimlar.Columns[0].Visible = false; //Id
            dgvKazanimlar.Columns[1].HeaderText = "Sınıf";
            dgvKazanimlar.Columns[1].Width = 50;
            dgvKazanimlar.Columns[2].Visible = false; //BransId
            dgvKazanimlar.Columns[6].Visible = false; //KazanimAdiOgrenci
            dgvKazanimlar.Columns[3].HeaderText = "Branş Adı";
            dgvKazanimlar.Columns[4].HeaderText = "Kazanım No";
            dgvKazanimlar.Columns[5].HeaderText = "Kazanım Adı";
            dgvKazanimlar.Columns[7].HeaderText = "Soruları";
            dgvKazanimlar.Columns[3].Width = 150;
            dgvKazanimlar.Columns[4].Width = 100;
            dgvKazanimlar.Columns[5].Width = 400;
            dgvKazanimlar.Columns[7].Width = 200;
        }

        private void FormKazanimlar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'odmckDataSet.Kazanimlar' table. You can move, or remove it, as needed.

            List<Brans> brns = new List<Brans> { new Brans(0, "Branş Seçiniz") };
            brns.AddRange(bransManager.List().Select(t => new Brans(t.Id, t.BransAdi)));

            cbBranslar.ValueMember = "Id";
            cbBranslar.DisplayMember = "BransAdi";

            foreach (var o in brns)
            {
                cbBranslar.Items.Add(new Brans(o.Id, o.BransAdi));
            }
            cbBranslar.DataSource = brns;


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

        }

        private void DüzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = dgvKazanimlar.SelectedRows[0].Cells[0].Value.ToInt32();
            Kazanim kazanim = kazanimManager.Find(x => x.Id == id);
            if (kazanim != null)
            {
                kazanimId = id;
                txtKazanimOgrenci.Text = kazanim.KazanimAdiOgrenci;
                txtKazanim.Text = kazanim.KazanimAdi;
                txtKazanimNo.Text = kazanim.KazanimNo;
                cbBranslar.SelectedValue = kazanim.BransId;
                txtSorulari.Text = kazanim.Sorulari;
                cbSinif.SelectedValue = kazanim.Sinif.ToString();
                btnKaydet.Text = "Değiştir";
            }
            else
            {
                MessageBox.Show("Düzenlenecek kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            kazanimId = 0;
            KayitlariListele();
            btnKaydet.Text = "Kaydet";
            txtKazanimOgrenci.Text = "";
            txtKazanimNo.Text = "";
            txtKazanim.Text = "";
            txtSorulari.Text = "";
            cbBranslar.Focus();
        }

        private void SilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:Kazanım silindiğinde etkilenecek tablolar kontrol edilecek;
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Seçilen kazanımı silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {

                int id = dgvKazanimlar.SelectedRows[0].Cells[0].Value.ToInt32();
                Kazanim kazanim = kazanimManager.Find(x => x.Id == id);
                if (kazanim != null)
                {
                    kazanimManager.Delete(kazanim);
                    KayitlariListele();
                }
                else
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            KayitlariListele();
        }

        private void tümünüSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KazanimManager kznmlist = new KazanimManager();
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Tümünü silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                kznmlist.TumunuSil();
                KayitlariListele();
            }
        }

        private void seçiliSınıfKazanımlarınıSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Seçilen sınıfa ait kazanımları silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                int sinif = dgvKazanimlar.SelectedRows[0].Cells[1].Value.ToInt32();

                KazanimManager kznmlist = new KazanimManager();

                var siniflar = kznmlist.List().Where(x => x.Sinif == sinif);
                foreach (var k in siniflar)
                {
                    kznmlist.Delete(k);
                }

                KayitlariListele();

            }
        }
    }
}
