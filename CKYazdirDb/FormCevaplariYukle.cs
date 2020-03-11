using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.Entities;
using ODM.CKYazdirDb.Library;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb
{
    public partial class FormCevaplariYukle : Form
    {
        private readonly DogruCevaplarManager dogruCevaplarManager = new DogruCevaplarManager();
        private readonly BransManager bransManager = new BransManager();
        private int cevapId = 0;
        public FormCevaplariYukle()
        {
            InitializeComponent();
        }

        private void FormCevaplariYukle_Load(object sender, EventArgs e)
        {
            //background nesnesi sağlıklı çalışması için gerekli. 
            CheckForIllegalCrossThreadCalls = false;

            List<Brans> brns = new List<Brans> { new Brans(0, "Branş Seçiniz") };
            brns.AddRange(bransManager.List().Select(t => new Brans(t.Id, t.Id + "-" + t.BransAdi)));

            cbBranslar.ValueMember = "Id";
            cbBranslar.DisplayMember = "BransAdi";

            foreach (var o in brns)
            {
                cbBranslar.Items.Add(new Brans(o.Id, o.Id + "-" + o.BransAdi));
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
        private void BtnCevabiKaydet_Click(object sender, EventArgs e)
        {
            if (cbBranslar.SelectedValue == null || cbBranslar.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Branş Seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (cbSinif.SelectedValue == null || cbSinif.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Sınıf Seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (txtDogruCevaplar.Text == "" || txtKitapcikTuru.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (cevapId == 0)
                {
                    DogruCevap cvp = new DogruCevap()
                    {
                        Sinif = cbSinif.SelectedValue.ToInt32(),
                        BransId = cbBranslar.SelectedValue.ToInt32(),
                        KitapcikTuru = txtKitapcikTuru.Text,
                        Cevaplar = txtDogruCevaplar.Text
                    };
                    dogruCevaplarManager.Insert(cvp);
                }
                else
                {
                    DogruCevap kazanim = dogruCevaplarManager.Find(x => x.Id == cevapId);
                    if (kazanim != null)
                    {
                        kazanim.BransId = cbBranslar.SelectedValue.ToInt32();
                        kazanim.Cevaplar = txtDogruCevaplar.Text;
                        kazanim.KitapcikTuru = txtKitapcikTuru.Text;
                        kazanim.Sinif = cbSinif.SelectedValue.ToInt32();
                        dogruCevaplarManager.Update(kazanim);
                    }
                    else
                    {
                        MessageBox.Show("Düzenlenecek kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                CevaplariListele();

                txtKitapcikTuru.Text = "";
                txtDogruCevaplar.Text = "";
            }
        }
        private void CevaplariListele()
        {
            //cevapları listele
            dataGridView1.DataSource = dogruCevaplarManager.DogruCevaplarList().OrderBy(x=>x.BransId).ThenBy(x=>x.Sinif).ThenBy(x=>x.KitapcikTuru).ToList();

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false; //branş id
            dataGridView1.Columns[2].HeaderText = "Branşı";
            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[3].HeaderText = "Sınıf";
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].HeaderText = "Kitaçık Türü";
            dataGridView1.Columns[4].Width = 70;
            dataGridView1.Columns[5].HeaderText = "Cevaplar";
            dataGridView1.Columns[5].Width = 215;
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            CevaplariListele();
        }

        private void TümünüSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Tümünü silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                dogruCevaplarManager.TumunuSil();
                CevaplariListele();
            }

        }
        private void SeçileniSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Seçilen cevabı silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {

                int id = dataGridView1.SelectedRows[0].Cells[0].Value.ToInt32();
                DogruCevap cvp = dogruCevaplarManager.Find(x => x.Id == id);
                dogruCevaplarManager.Delete(cvp);
                CevaplariListele();
            }
        }
        private void TxtDogruCevaplar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int id = dataGridView1.SelectedRows[0].Cells[0].Value.ToInt32();
            DogruCevap dgCevap = dogruCevaplarManager.Find(x => x.Id == id);
            if (dgCevap != null)
            {
                cevapId = dgCevap.Id;
                txtDogruCevaplar.Text = dgCevap.Cevaplar;
                txtKitapcikTuru.Text = dgCevap.KitapcikTuru;
                cbBranslar.SelectedValue = dgCevap.BransId;
                cbSinif.SelectedValue = dgCevap.Sinif.ToString();
                btnCevabiKaydet.Text = "Değiştir";
            }
            else
            {
                MessageBox.Show("Düzenlenecek kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            cevapId = 0;
            CevaplariListele();
            btnCevabiKaydet.Text = "Kaydet";
            txtKitapcikTuru.Text = "";
            txtDogruCevaplar.Text = "";
            cbBranslar.Focus();
        }

        private void btnDosyadanYukle_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofData = new OpenFileDialog())
            {
                ofData.Reset();
                ofData.ReadOnlyChecked = true;
                ofData.Multiselect = true;
                ofData.ShowReadOnly = true;
                ofData.Filter = "Veri dosyası (*.txt;*.dat)|*.txt;*.dat";
                ofData.Title = "Veri dosyasını seçiniz.";
                ofData.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofData.CheckPathExists = true;

                if (ofData.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        int a = 0;
                        string[] lines = File.ReadAllLines(ofData.FileName, Encoding.UTF8);
                        foreach (var s in lines)
                        {
                            string[] veri = s.Split('#');

                            DogruCevap cvp = new DogruCevap()
                            {
                                Sinif = veri[0].ToInt32(),
                                BransId = veri[1].ToInt32(),
                                KitapcikTuru = veri[2],
                                Cevaplar = veri[3]
                            };
                            a+=  dogruCevaplarManager.Insert(cvp);
                        }

                        MessageBox.Show(a+" kayıt yüklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CevaplariListele();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Hata oluştu. " + exception.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                
                ofData.Dispose();
            }

        }
    }
}
