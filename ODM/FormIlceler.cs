using System;
using System.Windows.Forms;
using DAL;
using ODM.Kutuphanem;

namespace ODM
{
    public partial class FormIlceler : Form
    {
        int id;
        private bool duzenle;
        public FormIlceler()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            IlcelerDb veriDb = new IlcelerDb();
            IlcelerInfo info = new IlcelerInfo();
            info.IlceAdi = txtIlce.Text;
            if (duzenle == false)
            {
                veriDb.KayitEkle(info);
            }
            else
            {
                info.Id = id;
                veriDb.KayitGuncelle(info);
            }
            txtIlce.Text = "";
            duzenle = false;
            dgvIlceler.DataSource = veriDb.KayitlariGetir();
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            duzenle = false;
            txtIlce.Text = "";
            IlcelerDb veriDb = new IlcelerDb();
            dgvIlceler.DataSource = veriDb.KayitlariGetir();
        }

        private void FormIlceler_Load(object sender, EventArgs e)
        {
            IlcelerDb veriDb = new IlcelerDb();
            dgvIlceler.DataSource = veriDb.KayitlariGetir();
            dgvIlceler.Columns[0].Visible = false;
            dgvIlceler.Columns[1].HeaderText = "İlçeler";
            dgvIlceler.Columns[1].Width = 255;
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IlcelerDb veriDb = new IlcelerDb();
            IlcelerInfo info = veriDb.KayitBilgiGetir(id);
            txtIlce.Text = info.IlceAdi;
            duzenle = true;
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Kayıdı silmek istedğinizden emin misiniz.", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                IlcelerDb veriDb = new IlcelerDb();
                int kullanimSayisi = veriDb.KayitKontrol(id);
                if (kullanimSayisi == 0)
                {
                    veriDb.KayitSil(id);
                    dgvIlceler.DataSource = veriDb.KayitlariGetir();
                }
                else
                    MessageBox.Show("Bu ilçede kullanıcı veya kurum olduğu için silinemedi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvIlceler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvIlceler.Rows[e.RowIndex].Cells[0].Value.ToInt32();
                btnKaydet.Text = dgvIlceler.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception)
            {
               //
            }
        }

        private void dgvIlceler_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvIlceler.Rows[e.RowIndex].Cells[0].Value.ToInt32();
                btnKaydet.Text = dgvIlceler.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
