using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Security;
using System.Windows.Forms;
using ODM.CKYazdirDb.Library;
using ReadingExcelData;


namespace ODM.CKYazdirDb
{
    public partial class FormKutukIslemleri : Form
    {
        readonly KutukManager kutukDb = new KutukManager();
        public FormKutukIslemleri()
        {
            InitializeComponent();
        }

        private void BtnKutukDosyasiniAc_Click(object sender, EventArgs e)
        {
            btnKutukDosyasiniAc.Enabled = false;
            int kayitSayisi = kutukDb.List().Count();
            if (kayitSayisi > 0)
            {
                DialogResult dialog = MessageBox.Show($"Kütükte kayıtlı {kayitSayisi} kişi var. Eski kayıtların silinmesini ister misiniz?\n Kayıtlar eski sınava ait ise silinmesi gerekmektedir.", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialog == DialogResult.Yes)
                {
                    kutukDb.TumunuSil();

                    KayitlariListele();

                }
            }

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Reset();
                openFileDialog1.ReadOnlyChecked = true;
                openFileDialog1.Multiselect = true;
                openFileDialog1.ShowReadOnly = true;
                openFileDialog1.Filter = "Microsoft Excel Çalışma Kitabı (*.xls;*.xlsx)|*.xls;*.xlsx";
                openFileDialog1.Title =
                    "Öğrenci listesinin bulunduğu Excel dosyasını seçiniz. Çalışma kitabı adı 'Sheet1' olmalıdır.";
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog1.CheckPathExists = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    backgroundWorker1.RunWorkerAsync(openFileDialog1.FileNames);

                }
                else
                {
                    lblBilgi.Text = "E-Okuldan alınan kütük dosyalarını seçiniz..";
                    btnKutukDosyasiniAc.Enabled = true;
                }
                openFileDialog1.Dispose();
            }
            btnKutukDosyasiniAc.Enabled = true;

        }
        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            btnKutukDosyasiniAc.Enabled = false;
            //Gelen object tipini string[] tipine çevir
            string[] files = ((IEnumerable)e.Argument).Cast<object>().Select(x => x.ToString()).ToArray();

            foreach (var file in files)
            {
                try
                {
                    DataTable table = ExcelUtil.ExcelToDataTable(file);
                    progressBar1.Maximum = table.Rows.Count;
                    bool ozelEgitimOkul = false;

                    for (int row = 1; row <= table.Rows.Count; row++)
                    {
                        int opaqNo = table.Rows[row - 1][0].ToInt32();
                        string ilAdi = table.Rows[row - 1][1].ToString().Trim();
                        string ilceAdi = table.Rows[row - 1][3].ToString().Trim();
                        int kurumKodu = table.Rows[row - 1][4].ToInt32();
                        string kurumAdi = table.Rows[row - 1][6].ToString().Trim();
                        int ogrenciNo = table.Rows[row - 1][9].ToInt32();
                        string adi = table.Rows[row - 1][10].ToString().Trim();
                        string soyadi = table.Rows[row - 1][12].ToString().Trim();
                        string sube = table.Rows[row - 1][14].ToString().Trim();
                        int sinifi = table.Rows[row - 1][15].ToInt32();
                        int sinavId = table.Rows[row - 1][16].ToInt32();
                        int dersKodu = table.Rows[row - 1][18].ToInt32();
                        string barkod = table.Rows[row - 1][19].ToString().Trim();

                        if (sube.Contains("Zihinsel") || sube.Contains("Otistik"))
                            ozelEgitimOkul = true;


                        if (sube.Length > 1)
                        {
                            string kisaSube = sube.Split('/')[1];
                            sube = kisaSube.Split(' ')[1];
                        }

                        //  MessageBox.Show("Test:"+sube);
                        Kutuk ktk = new Kutuk
                        {
                            OpaqId = opaqNo,
                            IlAdi = ilAdi,
                            IlceAdi = ilceAdi,
                            KurumKodu = kurumKodu,
                            KurumAdi = kurumAdi,
                            OgrenciNo = ogrenciNo,
                            Adi = adi,
                            Soyadi = soyadi,
                            Sube = sube,
                            Sinifi = sinifi,
                            SinavId = sinavId,
                            DersKodu = dersKodu,
                            Barkod = barkod
                        };
                        kutukDb.Insert(ktk);
                        progressBar1.Value = row;
                        lblBilgi.Text = $"{row} kayıt eklendi.";
                    }

                    if (ozelEgitimOkul)
                        MessageBox.Show("Kütükte özel eğitim kurumlarına ait öğrenciler bulunmakta.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    progressBar1.Value = 0;

                    lblBilgi.Text = table.Rows.Count + " kayıt eklendi.";

                }
                catch (SecurityException ex)
                {
                    // Kullanıcı dosyaları okumak, yolları keşfetmek vb. Için uygun izinlere sahip değildir.
                    MessageBox.Show("Güvenlik hatası. Ayrıntılar için lütfen yöneticinize başvurun.\n\n" +
                                    " Hata: " + ex.Message + "\n\n" +
                                    " Detaylar:\n\n" + ex.StackTrace);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata: " + ex.Message);
                    btnKutukDosyasiniAc.Enabled = true;
                    Cursor = Cursors.Default;
                    return;
                }

            }
            KayitlariListele();

            btnKutukDosyasiniAc.Enabled = true;
            Cursor = Cursors.Default;
        }
        private void FormKutukIslemleri_Load(object sender, EventArgs e)
        {
            //background nesnesi sağlıklı çalışması için gerekli. 
            CheckForIllegalCrossThreadCalls = false;

            KayitlariListele();

            dgvKutuk.Columns[0].Visible = false;
            dgvKutuk.Columns[1].HeaderText = "Opaq Id";
            dgvKutuk.Columns[1].Width = 70;
            dgvKutuk.Columns[2].Visible = false;
            dgvKutuk.Columns[3].HeaderText = "İlçe";
            dgvKutuk.Columns[3].Width = 100;
            dgvKutuk.Columns[4].HeaderText = "Kurum Kodu";
            dgvKutuk.Columns[4].Width = 50;
            dgvKutuk.Columns[5].HeaderText = "Kurum Adı";
            dgvKutuk.Columns[5].Width = 200;
            dgvKutuk.Columns[6].HeaderText = "Öğrenci No";
            dgvKutuk.Columns[6].Width = 80;
            dgvKutuk.Columns[7].HeaderText = "Adı";
            dgvKutuk.Columns[7].Width = 150;
            dgvKutuk.Columns[8].HeaderText = "Soyadı";
            dgvKutuk.Columns[8].Width = 150;
            dgvKutuk.Columns[9].HeaderText = "Sınıf";
            dgvKutuk.Columns[9].Width = 50;
            dgvKutuk.Columns[10].HeaderText = "Şube";
            dgvKutuk.Columns[10].Width = 50;
            dgvKutuk.Columns[9].DisplayIndex = 9; //index numarası değiştirildi.
            dgvKutuk.Columns[11].Visible = false;
            dgvKutuk.Columns[12].Visible = false;
            dgvKutuk.Columns[13].Visible = false;//barkod
        }

        private void KayitlariListele()
        {
            KutukManager ktk = new KutukManager();
            dgvKutuk.DataSource = ktk.List();
            txtAra.Text = "";
        }

        private void seçiliİlçeyiSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Seçilen ilçeye ait verileri silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                string ilceAdi = dgvKutuk.SelectedRows[0].Cells[3].Value.ToString();

                KutukManager ktk = new KutukManager();

                var ilce = ktk.List().Where(x => x.IlceAdi == ilceAdi);
                foreach (var k in ilce)
                {
                    ktk.Delete(k);
                }

                KayitlariListele();
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            string ara = txtAra.Text;

            KutukManager ktk = new KutukManager();
            if (ara.IsInteger())
                dgvKutuk.DataSource = ktk.List().Where(x => x.OpaqId==ara.ToInt32()||x.KurumKodu==ara.ToInt32()).ToList();
            else
                dgvKutuk.DataSource = ktk.List().Where(x => x.Adi.Contains(ara) || x.Soyadi.Contains(ara)).ToList();
        }

        private void seçiliOkuluSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Seçilen okula ait verileri silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                int kurumKodu = dgvKutuk.SelectedRows[0].Cells[4].Value.ToInt32();

                KutukManager ktk = new KutukManager();

                var okul = ktk.List().Where(x => x.KurumKodu == kurumKodu);
                foreach (var k in okul)
                {
                    ktk.Delete(k);
                }

                KayitlariListele();

            }
        }

        private void seçiliKayıtıSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Seçilen öğrenciyi silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                int id = dgvKutuk.SelectedRows[0].Cells[0].Value.ToInt32();

                KutukManager ktk = new KutukManager();

                var k = ktk.Find(x => x.Id == id);
                ktk.Delete(k);

                KayitlariListele();
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvKutuk.SelectedRows.Count>0)
            {
                FormKutukKayit frm = new FormKutukKayit();
                frm.kutukId = dgvKutuk.SelectedRows[0].Cells[0].Value.ToInt32();

                frm.ShowDialog();
            }
        }

        private void yeniKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKutukKayit frm = new FormKutukKayit();
            frm.kutukId = 0;
            frm.ShowDialog();
        }
    }
}
