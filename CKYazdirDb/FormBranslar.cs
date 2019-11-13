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
    public partial class FormBranslar : Form
    {
        private readonly BransManager bransManager = new BransManager();
        private int bransId;
        public FormBranslar()
        {
            InitializeComponent();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (txtBransAdi.Text == "")
            {
                MessageBox.Show("Branş adını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBransAdi.Focus();
            }
            else
            {
                if (bransId == 0)
                {
                    Brans brans = new Brans()
                    {
                        BransAdi = txtBransAdi.Text
                    };
                    bransManager.Insert(brans);
                }
                else
                {
                    Brans brans = bransManager.Find(x => x.Id == bransId);
                    if (brans != null)
                    {
                        brans.BransAdi = txtBransAdi.Text;
                        bransManager.Update(brans);

                    }
                    else
                    {
                        MessageBox.Show("Düzenlenecek branş bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                txtBransAdi.Text = "";
                bransId = 0;
                txtBransAdi.Focus();
                btnKaydet.Text = "Kaydet";

                //Yeniden listele
                KayitlariListele();
            }
        }

        private void FormBranslar_Load(object sender, EventArgs e)
        {
          
        }

        private void KayitlariListele()
        {
            dgBranslar.DataSource = bransManager.List().OrderBy(x => x.BransAdi).ToList();

            dgBranslar.Columns[0].HeaderText = "Branş No";
            dgBranslar.Columns[0].Width = 100;
            dgBranslar.Columns[1].HeaderText = "Branş Adı";
            dgBranslar.Columns[1].Width = 350;
        }

        private void SilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:Branş silindiğinde etkilenecek tablolar kontrol edilecek;
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Seçilen branşı silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {

                int id = dgBranslar.SelectedRows[0].Cells[0].Value.ToInt32();
                Brans brans = bransManager.Find(x => x.Id == id);
                if (brans != null)
                {
                    bransManager.Delete(brans);
                    KayitlariListele();
                }
                else
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DüzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = dgBranslar.SelectedRows[0].Cells[0].Value.ToInt32();
            Brans brans = bransManager.Find(x => x.Id == id);
            if (brans != null)
            {
                bransId = id;
                txtBransAdi.Text = brans.BransAdi;

                btnKaydet.Text = "Değiştir";
            }
            else
            {
                MessageBox.Show("Düzenlenecek kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            txtBransAdi.Text = "";
            bransId = 0;
            txtBransAdi.Focus();
            btnKaydet.Text = "Kaydet";
            //Yeniden listele
            KayitlariListele();
        }

        private void DgBranslar_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DüzenleToolStripMenuItem_Click(null, null);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            KayitlariListele();
        }
    }
}
