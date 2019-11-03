using DAL;
using System;
using System.Windows.Forms;
using ODM.Kutuphanem;

namespace ODM
{
    public partial class FormBranslar : Form
    {
        public FormBranslar()
        {
            InitializeComponent();
        }
        int id;
        private bool duzenle;
        private void FormBranslar_Load(object sender, EventArgs e)
        {
            BranslarDb veriDb = new BranslarDb();
            dgvBranslar.DataSource = veriDb.KayitlariGetir();
            dgvBranslar.Columns[0].Visible = false;
            dgvBranslar.Columns[1].HeaderText = "Branşlar";
            dgvBranslar.Columns[1].Width = 255;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            BranslarDb veriDb = new BranslarDb();
            BranslarInfo info = new BranslarInfo();
            info.BransAdi=txtBrans.Text;
            if (duzenle == false)
            {
                veriDb.KayitEkle(info);
            }
            else
            {
                info.Id = id;
                veriDb.KayitGuncelle(info);
            }
            txtBrans.Text = "";
            duzenle = false;
            dgvBranslar.DataSource = veriDb.KayitlariGetir();
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BranslarDb veriDb = new BranslarDb();
            BranslarInfo info = veriDb.KayitBilgiGetir(id);
            txtBrans.Text = info.BransAdi;
            duzenle = true;
        }

        private void dgvBranslar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvBranslar.Rows[e.RowIndex].Cells[0].Value.ToInt32();
            }
            catch (Exception)
            {
                //
            }
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            duzenle = false;
            txtBrans.Text = "";
            BranslarDb veriDb = new BranslarDb();
            dgvBranslar.DataSource = veriDb.KayitlariGetir();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Kayıdı silmek istedğinizden emin misiniz.", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                BranslarDb veriDb= new BranslarDb();
                int kullanimSayisi = veriDb.KayitKontrol(id);
                if(kullanimSayisi==0)
                {   veriDb.KayitSil(id);
                    dgvBranslar.DataSource = veriDb.KayitlariGetir();
                }
                else
                    MessageBox.Show("Bu branşta sınav yapıldığı için silinemedi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvBranslar_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvBranslar.Rows[e.RowIndex].Cells[0].Value.ToInt32();
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
