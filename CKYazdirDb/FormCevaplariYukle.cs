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
using ODM.CKYazdirDb.Library;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb
{
    public partial class FormCevaplariYukle : Form
    {
        private readonly DogruCevaplarManager dogruCevaplarManager = new DogruCevaplarManager();
      
        public FormCevaplariYukle()
        {
            InitializeComponent();
        }
       
        private void FormCevaplariYukle_Load(object sender, EventArgs e)
        {
            //background nesnesi sağlıklı çalışması için gerekli. 
            CheckForIllegalCrossThreadCalls = false;
        }
        private void BtnCevabiKaydet_Click(object sender, EventArgs e)
        {
            DogruCevap cvp = new DogruCevap()
            {
                SinavId = txtSinavId.Text.ToInt32(),
                BransId = txtDersId.Text.ToInt32(),
                KitapcikTuru = txtKitapcikTuru.Text,
                Cevaplar = txtDogruCevaplar.Text
            };
            dogruCevaplarManager.Insert(cvp);

            CevaplariListele();

            txtDersId.Text = "";
            txtKitapcikTuru.Text = "";
            txtDogruCevaplar.Text = "";
            txtSinavId.Focus();
            txtSinavId.SelectAll();
        }
        private void CevaplariListele()
        {
            //cevapları listele
            dataGridView1.DataSource = dogruCevaplarManager.List();

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Sınav No";
            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[2].HeaderText = "Ders Kodu";
            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[3].HeaderText = "Kitaçık Türü";
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].HeaderText = "Cevaplar";
            dataGridView1.Columns[4].Width = 330;
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            CevaplariListele();
        }
        private void TxtSinavId_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Yalnızca sayısal değer girişi
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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

       

    }
}
