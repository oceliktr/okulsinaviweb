using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.Library;

namespace CKYazdir
{
    public partial class FormTxtOlustur : Form
    {
        public readonly List<CevapveKutuk> cevapList = new List<CevapveKutuk>();

        public FormTxtOlustur()
        {
            InitializeComponent();
            lvBolumler.Columns.Add("Açıklama", 100);
            lvBolumler.Columns.Add("Başlangıç", 80);
            lvBolumler.Columns.Add("Karakter", 70);
            lvBolumler.Columns.Add("Değer", 70);

        }

        private string seciliDosya = "";
        private void btnDataAc_Click(object sender, EventArgs e)
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
                txtData.Text = "";
                if (ofData.ShowDialog() == DialogResult.OK)
                {
                    seciliDosya = ofData.FileName;
                    if (seciliDosya.Length > 0)
                        txtData.Text = File.ReadLines(seciliDosya).First();
                }
                this.Text = seciliDosya;

                ndSutun.Value = 0;
                ndKarakter.Value = 0;
                lvBolumler.Items.Clear();
                cbAlanAdi.Text = "";

                ofData.Dispose();
            }

            lvBolumler.Activation = ItemActivation.OneClick;
            //Listview tek tıklamada aktif hale gelir.

            lvBolumler.View = View.Details;
            // Listview üzerindeki sütun isimleri görünmesi için değiştirilmesi gereklidir.

            lvBolumler.GridLines = true;
            //Listview üzerinde ayırt edici çizgilerin görünmesi için gereklidir.

            lvBolumler.FullRowSelect = true;
            // Listview üzerinde satırın tamamını seçebilmek için bu özelliğin true olması gerekir.


            int sonSatirIndexi = txtData.TextLength;
            ndSutun.Maximum = ndKarakter.Maximum = sonSatirIndexi;
        }

        private void txtData_Click(object sender, EventArgs e)
        {
            if (txtData.SelectionStart < ndSutun.Maximum)
                ndSutun.Value = txtData.SelectionStart;
        }

        private void txtData_MouseUp(object sender, MouseEventArgs e)
        {
            if (ndKarakter.Maximum > txtData.SelectionLength)
                ndKarakter.Value = txtData.SelectionLength;
        }
        private void txtData_KeyUp(object sender, KeyEventArgs e)
        {
            if (ndKarakter.Maximum > txtData.SelectionLength)
                ndKarakter.Value = txtData.SelectionLength;

            if (ndSutun.Maximum > txtData.SelectionStart)
                ndSutun.Value = txtData.SelectionStart;
        }
        private void txtData_MouseDown(object sender, MouseEventArgs e)
        {
            if (ndSutun.Maximum > txtData.SelectionStart)
                ndSutun.Value = txtData.SelectionStart;
            // MessageBox.Show(txtData.SelectionStart.ToString());
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (ndKarakter.Value == 0 && txtSabitDeger.Text == "") //herhangi bir karakter seçilmemiş veya sabit değer girilmemiş ise
            {
                MessageBox.Show("Herhangi bir aralık seçilmedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (txtSabitDeger.Text != "") //eğer sabit değer girilmemişse karakter ve sutun değerlerini sıfırla
                {
                    ndSutun.Value = ndKarakter.Value = 0;
                }
                string sabitDeger = txtSabitDeger.Text;
                if (sabitDeger == "")
                    sabitDeger = "null";

                string[] row = { cbAlanAdi.Text, ndSutun.Value.ToString(), ndKarakter.Value.ToString(), sabitDeger };
                var satir = new ListViewItem(row);
                lvBolumler.Items.Add(satir);

                //seçili değerleri sil
                cbAlanAdi.Text = "";
                txtSabitDeger.Text = "";
                ndSutun.Value = ndKarakter.Value = 0;


                OrnekOlustur();
            }
        }

        private void OrnekOlustur()
        {
            if (lvBolumler.Items.Count > 0)
            {
                string ornekMetin = "";

                for (int i = 0; i < lvBolumler.Items.Count; i++)
                {
                    if (lvBolumler.Items[i].SubItems[3].Text != "null") //null değil ise sabit değeri yaz
                    {
                        ornekMetin += lvBolumler.Items[i].SubItems[3].Text + txtAraKarakter.Text;
                    }
                    else //null ise kesme noktlarından al
                    {
                        string deger = txtData.Text.Substring(lvBolumler.Items[i].SubItems[1].Text.ToInt32(), lvBolumler.Items[i].SubItems[2].Text.ToInt32());

                        if (lvBolumler.Items[i].SubItems[0].Text == "Girmedi") //Girmedi ise
                        {
                            //Gelen değer G ise 0, yoksa 1 yaz.
                            if (deger == "G")
                                ornekMetin += "0" + txtAraKarakter.Text;
                            else
                                ornekMetin += "1" + txtAraKarakter.Text;
                        }
                        else
                        {
                            ornekMetin += deger + txtAraKarakter.Text;
                        }

                    }

                }

                if (txtAraKarakter.Text == "")
                    label1.Text = "Örnek :" + ornekMetin;
                else
                {
                    if (cbSonunaEkle.Checked == false)
                        label1.Text = "Örnek :" + ornekMetin.Substring(0, ornekMetin.Length - 1);
                    else
                        label1.Text = "Örnek :" + ornekMetin;

                }
            }
        }

        private void ndKarakter_ValueChanged(object sender, EventArgs e)
        {
            OrnekOlustur();
            //txtData.Focus();
            //txtData.Select(ndSutun.Value.ToInt32(), ndKarakter.Value.ToInt32());
        }

        private void ndSutun_ValueChanged(object sender, EventArgs e)
        {
            OrnekOlustur();
            //txtData.Focus();
            //txtData.Select(ndSutun.Value.ToInt32(), ndKarakter.Value.ToInt32());
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // listView1.Items.Remove(listView1.SelectedItems[0]);

            foreach (ListViewItem bilgi in lvBolumler.SelectedItems)
            {
                bilgi.Remove();
                OrnekOlustur();
            }
        }

        private void yukarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewElemanTasi(lvBolumler, TasimaYonu.Yukari);
            OrnekOlustur();
        }
        private void aşağıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewElemanTasi(lvBolumler, TasimaYonu.Asagi);
            OrnekOlustur();
        }
        private enum TasimaYonu
        {
            Yukari = -1,
            Asagi = 1
        };

        private void ListViewElemanTasi(ListView sender, TasimaYonu yon)
        {
            //listenin içini kontrol et
            //listede değer yoksa false dön
            //aşağı yönde listenin en sonunda ise false dön
            //yukarı yönde listenin başında ise false dön
            //taşına bilir ise true dön
            bool kontrol = sender.SelectedItems.Count > 0 && (yon == TasimaYonu.Asagi || yon == TasimaYonu.Yukari) &&
                           ((yon == TasimaYonu.Asagi && sender.SelectedItems[sender.SelectedItems.Count - 1].Index < sender.Items.Count - 1) ||
                            (yon == TasimaYonu.Yukari && sender.SelectedItems[0].Index > 0));
            if (kontrol)
            {
                bool start = true;
                int first_idx = 0;
                List<ListViewItem> items = new List<ListViewItem>();

                // ambil data
                foreach (ListViewItem i in sender.SelectedItems)
                {
                    if (start)
                    {
                        first_idx = i.Index;
                        start = false;
                    }
                    items.Add(i);
                }

                sender.BeginUpdate();

                // hapus
                foreach (ListViewItem i in sender.SelectedItems) i.Remove();

                // insert
                if (yon == TasimaYonu.Yukari)
                {
                    int insert_to = first_idx - 1;
                    foreach (ListViewItem i in items)
                    {
                        sender.Items.Insert(insert_to, i);
                        insert_to++;
                    }
                }
                else
                {
                    int insert_to = first_idx + 1;
                    foreach (ListViewItem i in items)
                    {
                        sender.Items.Insert(insert_to, i);
                        insert_to++;
                    }
                }
                sender.EndUpdate();
            }
        }

        private void btnTextOlustur_Click(object sender, EventArgs e)
        {
            if (lvBolumler.Items.Count == 0)
            {
                MessageBox.Show("Herhangi bir aralık seçilmedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                {
                    saveFileDialog1.DefaultExt = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    saveFileDialog1.Title = @"Txt dosyalarının oluşturulacağı dizini seçiniz.";

                    saveFileDialog1.FileName = "Dosya Adı.txt";
                    saveFileDialog1.Filter = "Text Dosyası | *.txt";


                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());

                        try
                        {
                            string[] lines = File.ReadAllLines(seciliDosya, Encoding.UTF8);
                            foreach (string file in lines)
                            {
                                string yeniData = "";
                                for (int i = 0; i < lvBolumler.Items.Count; i++)
                                {

                                    if (lvBolumler.Items[i].SubItems[3].Text != "null") //sabit değer tanımlanmış ise
                                    {
                                        yeniData += lvBolumler.Items[i].SubItems[3].Text + txtAraKarakter.Text;
                                    }
                                    else //sabit değer tanımlanmamış ise kesme noktalarından kopyala
                                    {
                                        string deger = file.Substring(lvBolumler.Items[i].SubItems[1].Text.ToInt32(),lvBolumler.Items[i].SubItems[2].Text.ToInt32());

                                        if (lvBolumler.Items[i].SubItems[0].Text == "Girmedi") //Girmedi ise
                                        {
                                            //Gelen değer G ise 0, yoksa 1 yaz.
                                            if (deger== "G")
                                                yeniData += "0" + txtAraKarakter.Text;
                                            else
                                                yeniData += "1" + txtAraKarakter.Text;
                                        }
                                        else
                                        {
                                            yeniData += deger + txtAraKarakter.Text;
                                        }
                                    }

                                }

                                //sonunda karakter olmasın denilmiş ise sondaki karakteri sil
                                if (txtAraKarakter.Text != "" && cbSonunaEkle.Checked == false && yeniData.Length > 0)
                                    yeniData = yeniData.Substring(0, yeniData.Length - 1);

                                writer.WriteLine(yeniData);
                                Application.DoEvents();
                            }
                        }
                        catch (Exception)
                        {
                          //  MessageBox.Show("Hata: " + ex.Message);
                        }

                        writer.Dispose();
                        writer.Close();

                        DialogResult dialog = MessageBox.Show("Txt Dosyaları oluşturuldu. Dizini açmak ister misiniz?", @"Bilgi", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                            Process.Start("explorer.exe", Path.GetFileName(saveFileDialog1.FileName));
                    }
                }

            }
        }

        private void txtAraKarakter_TextChanged(object sender, EventArgs e)
        {
            OrnekOlustur();
        }

        private void cbSonunaEkle_CheckedChanged(object sender, EventArgs e)
        {
            OrnekOlustur();
        }
        private void BranslariListele()
        {
            BransManager bransManager= new BransManager();
            dgBranslar.DataSource = bransManager.List().OrderBy(x => x.Id).ToList();

            dgBranslar.Columns[0].HeaderText = "Branş No";
            dgBranslar.Columns[0].Width = 60;
            dgBranslar.Columns[1].HeaderText = "Branş Adı";
            dgBranslar.Columns[1].Width = 120;
        }
        private void FormTxtOlustur_Load(object sender, EventArgs e)
        {
            BranslariListele();
            blAciklama.Text =
                @"'Data Aç' butonu ile *.txt veya *.dat dosyasını seçiniz. Modül ilk satırı örnek olarak gösterecektir."+Environment.NewLine+
                "Oluşturmak istediğiniz format için her grup arasına kullanacağınız ara karakteri de girerek başlangıç noktasını ve karakter sayısını seçip ekle butonuna tıklayınız." + Environment.NewLine +
                "Katılım Durumu ('G') alanı için açıklama alanına 'Girmedi' yazınız. Modül girmeyenler için 0, girenler için 1 değerini atayacaktır." + Environment.NewLine +
                "İstenilen formatı örnekte görüldüğü gibi oluşturduktan sonra 'Text Oluştur' butonuna tıklayınız.\nBu uygulama için aşağıdaki formatta txt oluşturunuz." + Environment.NewLine +
            "20649649#A#1#2#CBBBBBACACBABCBAACAB#2#CBBBBBACACBABCBAACAB" + Environment.NewLine +
            "OPAKNO/TC#KTUR#KD#DERSID#CEVAPLAR#DERSID#CEVAPLAR";
        }

    }
}
