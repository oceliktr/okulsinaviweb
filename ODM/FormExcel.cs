using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DAL;
using ODM.Kutuphanem;

namespace ODM
{
    public partial class FormExcel : Form
    {
        private static readonly string ckDizin = IniIslemleri.VeriOku("CKDizinKontrol", "CKBulunduguAdres");

        public FormExcel()
        {
            InitializeComponent();

            SinavlarDb snvDb = new SinavlarDb();
            List<SinavlarInfo> snvInfo = snvDb.KayitlariDiziyeGetir();
            snvInfo.Insert(0, new SinavlarInfo(0, "Sınav Seçiniz"));
            cbSinavlar.DataSource = snvInfo;
            cbSinavlar.DisplayMember = "SinavAdi";
            cbSinavlar.ValueMember = "Id";
            cbSinavlar.SelectedIndex = 0;


            string ilAdi = IniIslemleri.VeriOku("Baslik", "IlAdi");
            this.Text = "Excel İşlemleri Formu " + ilAdi.IlkHarfleriBuyut() + " Ölçme ve Değerlendirme Merkezi";
        }

        readonly List<OgrencilerInfo> ogrenciler = new List<OgrencilerInfo>();
        private void btnVerileriCek_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog
            {
                Filter = "Microsoft Excel Çalışma Kitabı (*.xls;*.xlsx)|*.xls;*.xlsx",
                Title = "Öğrenci listesinin bulunduğu Excel dosyasını seçiniz. Çalışma kitabı adı 'Sayfa1' olmalıdır."
            };

            if (o.ShowDialog() != DialogResult.OK) return;
            try
            {
                btnVerileriCek.Enabled = false;

                OleDbConnection xlsxbaglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + o.FileName + "; Extended Properties='Excel 12.0 Xml;HDR=YES'");
                DataTable tablo = new DataTable();

                xlsxbaglanti.Open();
                tablo.Clear();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sayfa1$]", xlsxbaglanti);
                da.Fill(tablo);
                dgvHata.DataSource = tablo;
                dgvHata.Columns[0].Width = 85;
                dgvHata.Columns[1].Width = 300;
                dgvHata.Columns[2].Width = 100;
                dgvHata.Columns[3].Width = 150;
                dgvHata.Columns[4].Width = 145;
                dgvHata.Columns[5].Width = 50;
                dgvHata.Columns[6].Width = 50;
                dgvHata.Columns[7].Width = 50;
                xlsxbaglanti.Close();

                if (dgvHata.ColumnCount != 8)
                {
                    lblBilgi.Text = "Seçilen dosya veritabanı yapısına uygun değil. 'Kurum Kodu,Okul Adı,Tc Kimlik,Adı,Soyadı,Okul No,Sınıfı,Şubesi' başlık sıralamasında excel dosyası seçiniz";
                    MessageBox.Show(lblBilgi.Text, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bool hata = false;
                    string kurumKodu = "";
                    string kurumAdi = "";
                    string tcKimlik = "";
                    string adi = "";
                    string soyadi = "";
                    string okulNo = "";
                    string sinifi = "";
                    string sube = "";

                    for (int index = 0; index < dgvHata.Rows.Count; index++)
                    {
                        DataGridViewRow row = dgvHata.Rows[index];
                        for (int i = 0; i < dgvHata.Columns.Count; i++)
                        {
                            if (i % 8 == 0)
                                kurumKodu = row.Cells[dgvHata.Columns[i].HeaderText].Value.ToString().Trim();
                            if (i % 8 == 1)
                                kurumAdi = row.Cells[dgvHata.Columns[i].HeaderText].Value.ToString();
                            if (i % 8 == 2)
                                tcKimlik = row.Cells[dgvHata.Columns[i].HeaderText].Value.ToString().Trim();
                            if (i % 8 == 3)
                                adi = row.Cells[dgvHata.Columns[i].HeaderText].Value.ToString();
                            if (i % 8 == 4)
                                soyadi = row.Cells[dgvHata.Columns[i].HeaderText].Value.ToString();
                            if (i % 8 == 5)
                                okulNo = row.Cells[dgvHata.Columns[i].HeaderText].Value.ToString().Trim();
                            if (i % 8 == 6)
                                sinifi = row.Cells[dgvHata.Columns[i].HeaderText].Value.ToString().Trim();
                            if (i % 8 == 7)
                                sube = row.Cells[dgvHata.Columns[i].HeaderText].Value.ToString().Trim();
                        }
                        if (!kurumKodu.IsInteger())
                        {
                            MessageBox.Show("1. sütun " + index + 1 + ". satırda Kurum kodu alanı için boşluk veya sayısal olmayan değerler girilmiş. Girilen değer :" + kurumKodu, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            hata = true;
                        }
                        else if (tcKimlik.Length != 11 && tcKimlik != "0")
                        {
                            MessageBox.Show("3. sütun " + index + 1 + ". satırda geçerli bir Tc kimlik numarası girilmemiş. Girilen değer :" + tcKimlik, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            hata = true;
                        }
                        else if (!okulNo.IsInteger())
                        {
                            MessageBox.Show("4. sütun " + index + 1 + ". satırda Okul No alanı için boşluk veya sayısal olmayan değerler girilmiş. Girilen değer :" + okulNo, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            hata = true;
                        }
                        else if (!sinifi.IsInteger())
                        {
                            MessageBox.Show("5. sütun " + index + 1 + ". satırda Sınıfı alanı için boşluk veya sayısal olmayan değerler girilmiş. Girilen değer :" + sinifi, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            hata = true;
                        }
                        else if (sube.Length >= 3)
                        {
                            MessageBox.Show("8. sütun " + index + 1 + ". satırda şube bilgisi geçerli değil. Girilen değer :" + sube, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            hata = true;
                        }
                        else
                        {
                            ogrenciler.Add(new OgrencilerInfo(index + 1, kurumKodu.ToInt32(), kurumAdi, tcKimlik, adi, soyadi, okulNo.ToInt32(), sinifi.ToInt32(), sube));
                            hata = false;
                        }
                    }

                    if (hata == false)
                    {
                        btnDBYukle.Enabled = true;
                        cbSinavlar.Enabled = true;
                        lblBilgi.Text = "Excel dosyası başarıyla yüklendi. Şimdi veritabanına aktarabilirsiniz.";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormExcel_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.None && e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void btnDBYukle_Click(object sender, EventArgs e)
        {
            if (cbSinavlar.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Sınav seçiniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                dgvHata.Columns.Add("Sonuc", "Sonuç");

                dgvHata.Columns[0].Width = 70; //15
                dgvHata.Columns[1].Width = 280;//20
                dgvHata.Columns[2].Width = 90;//10
                dgvHata.Columns[3].Width = 130;//20
                dgvHata.Columns[4].Width = 130;//15
                dgvHata.Columns[5].Width = 50;
                dgvHata.Columns[6].Width = 50;
                dgvHata.Columns[7].Width = 50;
                dgvHata.Columns[8].Width = 80;
                bgwDbEkle.RunWorkerAsync();
            }
        }

        private void bgwDbEkle_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            try
            {
                int yeniOkulSayisi = 0;
                int ogrenciSayisi = ogrenciler.Count;
                int basamakSayisi = ogrenciler.Count;
                int uzunluk = 0;
                int topla = 0;

                while (basamakSayisi > 0)
                {
                    uzunluk++;
                    basamakSayisi = basamakSayisi / 10;
                };

                if (uzunluk == 1)
                    topla = 10;
                if (uzunluk == 2)
                    topla = 100;
                if (uzunluk == 3)
                    topla = 1000;
                if (uzunluk == 4)
                    topla = 10000;
                if (uzunluk == 5)
                    topla = 100000;
                if (uzunluk == 6)
                    topla = 1000000;
                if (uzunluk == 7)
                    topla = 10000000;

                int a = 0;
                progressBar1.Maximum = ogrenciSayisi;
                progressBar1.Value = 0;
                int sinavId = cbSinavlar.SelectedValue.ToInt32();
                OgrencilerDb veriDb = new OgrencilerDb();
                OgrencilerInfo info = new OgrencilerInfo();
                KurumlarDb krmDb = new KurumlarDb();

                string dosyaAdi = ckDizin + "\\" + sinavId + " nolu sinav ogrencileri.sql";
                string yeniOkullarDosyaAdi = ckDizin + "\\kaydedilecek kurumlar.txt";
                StreamWriter sqlSW = new StreamWriter(dosyaAdi, false, Encoding.UTF8);
                StreamWriter yeniOkulSW = new StreamWriter(yeniOkullarDosyaAdi, false, Encoding.UTF8);
                yeniOkulSW.WriteLine("Veritabanında olmayan okullar. Öğrencilerinin kaydı yapıldı ancak bu okulların veritabanına eklenmesi önemlidir." + Environment.NewLine);
                for (int i = 0; i < ogrenciler.Count; i++)
                {
                    OgrencilerInfo xls = ogrenciler[i];
                    a++;
                    progressBar1.Value = a;
                    if (veriDb.KayitKontrol(sinavId, xls.KurumKodu, xls.OgrOkulNo))
                    {
                        if (xls.OgrOkulNo == 0)
                            DbyeKaydet(info, sinavId, xls, topla, veriDb, i);
                        else
                            dgvHata.Rows[i].Cells["Sonuc"].Value = "kayıt vardı";
                    }
                    else
                    {
                        DbyeKaydet(info, sinavId, xls, topla, veriDb, i);
                    }
                    string okullar = string.Concat(xls.KurumKodu, "-", xls.KurumAdi);
                    string sql = string.Concat(
                        "INSERT INTO `ogrenciler` (`OgrenciId`, `SinavId`, `TcKimlik`, `Adi`, `Soyadi`, `OgrOkulNo`, `KurumKodu`, `Sinifi`, `Sube`, `CKagitKontrol`, `Girmedi`, `DosyaAdi`) VALUES(",
                        xls.OgrenciId + topla, ",", sinavId, ",'", xls.TcKimlik.Md5Sifrele(), "', '", xls.Adi, "', '", xls.Soyadi, "', ", xls.OgrOkulNo, ", ", xls.KurumKodu, ", ", xls.Sinifi, ", '", xls.Sube, "', NULL, 0, NULL);");

                    sqlSW.WriteLine(sql + Environment.NewLine);

                    if (!krmDb.KayitKontrol(xls.KurumKodu.ToString()))
                    {
                        yeniOkulSayisi++;
                        yeniOkulSW.WriteLine(okullar + Environment.NewLine);
                    }
                }
                sqlSW.Close();
                yeniOkulSW.Close();

                //yeni okul varsa dsyayı aç
                if (yeniOkulSayisi > 0)
                    Process.Start(yeniOkullarDosyaAdi);

                DialogResult dialog = MessageBox.Show(@"Veritabanına kaydetme işlemleri tamamlandı." + "\n" + "Web için oluşturulan '" + sinavId + " nolu sinav ogrencileri.sql' isimli dosyayı açmak ister misiniz?", @"Bilgi", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                    Process.Start(dosyaAdi);
                else MessageBox.Show("Web için oluşturulan " + sinavId + " nolu sinav ogrencileri.sql isimli dosya '" + ckDizin + "' adresinde saklanmaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                progressBar1.Value = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DbyeKaydet(OgrencilerInfo info, int sinavId, OgrencilerInfo xls, int topla, OgrencilerDb veriDb, int i)
        {
            info.SinavId = sinavId;
            info.OgrenciId = xls.OgrenciId + topla;
            //yedek öğrenciler için Tc Kimlik alanı 0 dır.
            info.TcKimlik = xls.TcKimlik == "0" ? null : xls.TcKimlik.Md5Sifrele();
            info.Adi = xls.Adi;
            info.Soyadi = xls.Soyadi;
            info.KurumKodu = xls.KurumKodu;
            info.OgrOkulNo = xls.OgrOkulNo;
            info.Sinifi = xls.Sinifi;
            info.Sube = xls.Sube;
            veriDb.KayitEkle(info);

            dgvHata.Rows[i].Cells["Sonuc"].Value = "kaydedildi";
        }
    }
}
