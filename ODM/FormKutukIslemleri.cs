using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;


namespace ODM
{
    public partial class FormKutukIslemleri : Form
    {
        public class ComboValue
        {
            public string Value { get; set; }
            public string Text { get; set; }

            public ComboValue()
            {

            }
            public ComboValue(string value, string text)
            {
                Value = value;
                Text = text;
            }
        }
        public FormKutukIslemleri()
        {
            InitializeComponent();
            webBrowser1.Navigate(new Uri("https://eokulyd.meb.gov.tr/"));
            webBrowser1.WebBrowserShortcutsEnabled = false;
        }
        private void SiniflariGetir()
        {
            List<ComboValue> sinif = new List<ComboValue>
            {
                new ComboValue("-1", "Seçiniz"),
                new ComboValue("5", "5. Sınıf"),
                new ComboValue("110", "5. Sınıf (Yabancı Dil Ağırlıklı)"),
                new ComboValue("6", "6. Sınıf"),
                new ComboValue("7", "7. Sınıf"),
                new ComboValue("8", "8. Sınıf")
            };

            cbSinif.DataSource = sinif;
            cbSinif.DisplayMember = "Text";
            cbSinif.ValueMember = "Value";
            cbSinif.SelectedIndex = 0;
        }
        private void IlceleriGetir()
        {
            List<ComboValue> ilce = new List<ComboValue>
                {
                    new ComboValue("-1", "Seçiniz"),
                    new ComboValue("2", "AŞKALE"),
                    new ComboValue("3", "AZİZİYE"),
                    new ComboValue("4","ÇAT"),
                    new ComboValue("5","HINIS"),
                    new ComboValue("6","HORASAN"),
                    new ComboValue("7","İSPİR"),
                    new ComboValue("8","KARAÇOBAN"),
                    new ComboValue("9","KARAYAZI"),
                    new ComboValue("10","KÖPRÜKÖY"),
                    new ComboValue("11","NARMAN"),
                    new ComboValue("12","OLTU"),
                    new ComboValue("13","OLUR"),
                    new ComboValue("14","PALANDÖKEN"),
                    new ComboValue("15","PASİNLER"),
                    new ComboValue("16","PAZARYOLU"),
                    new ComboValue("17","ŞENKAYA"),
                    new ComboValue("18","TEKMAN"),
                    new ComboValue("19","TORTUM"),
                    new ComboValue("20","UZUNDERE"),
                    new ComboValue("21","YAKUTİYE")
                };

            cbIlce.DataSource = ilce;
            cbIlce.DisplayMember = "Text";
            cbIlce.ValueMember = "Value";
            cbIlce.SelectedIndex = 0;
        }
        private void FormKutukIslemleri_Load(object sender, EventArgs e)
        {
            txtUrl.Width = this.Width - 200;
            txtGuvenlikKodu.Focus();
            EklenenKurumlar();
            SiniflariGetir();
            IlceleriGetir();
        }
        private void EklenenKurumlar()
        {
            KutukIslemleriDB veriDb = new KutukIslemleriDB();
            dgvOkullar.DataSource = veriDb.EklenenKurumlariGetir();
        }
        private void btnGiris_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.GetElementById("txtKullaniciAd").SetAttribute("value", txtTcKimlik.Text);
            webBrowser1.Document.GetElementById("txtSifre").SetAttribute("value", txtSifre.Text);
            webBrowser1.Document.GetElementById("guvenlikKontrol").SetAttribute("value", txtGuvenlikKodu.Text);
            webBrowser1.Document.GetElementById("btnGiris").InvokeMember("click");
            panel1.Visible = false;
        }
        private void FormKutukIslemleri_Resize(object sender, EventArgs e)
        {
            txtUrl.Width = this.Width - 200;
        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }
        private void btnİleri_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }
        private void btnYenile_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }
        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }
        private void btnGit_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(txtUrl.Text);
        }
        private void btnDur_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }
        private void btnAra_Click(object sender, EventArgs e)
        {
            btnEkle.DropDown.Items.Add(txtUrl.Text);
        }
        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Control)
                    txtUrl.Text = "http://www." + txtUrl.Text + ".com";
                webBrowser1.Navigate(txtUrl.Text);
            }
        }
        private void Listele()
        {
            // Alan adlarını tanımla.
            string[] Alanlar = { "Tc Kimlik", "Adi", "Soyadi", "OkulNo", "Sinifi", "Şube" };
            // Hangi hücrelerin kullanacağını belirleyen diziyi tanımla
            // (Duruma göre arada hücre no atlanabilir. İlk hücre numarası 0'dır. Burada birinci hücre atlanıyor.)
            int[] HucreNumaralari = { 1, 2, 3, 4, 5, 6 };
            //(Alan adları dizisi ile HucreNumaralari dizisinin eleman sayısı aynı olmalı.  HucreNumaralari dizisinin
            // eleman sayısı fazla olursa hata çıkar.

            // Başlık satırı yoksa bunu false yap.
            bool BaslikSatiriVar = true;

            // Bir DataTable oluştur.
            DataTable dt = new DataTable();
            dt.Clear();
            // Alanlar dizisini gezerek her bir eleman için bir alan ekle.
            foreach (string alanAdi in Alanlar)
                dt.Columns.Add(alanAdi);

            // Tablonun id özelliği değerinin tablom olduğunu varsayarsak.
            if (dataGridView1.RowCount > 0)
            {
                for (int i = 0; i <= dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }
            }
            // Tabloyu al.
            HtmlElement tablo = webBrowser1.Document.GetElementById("dgListe");
            // Satırları satirlar koleksiyonuna yükle.
            if (tablo != null)
            {
                HtmlElementCollection satirlar = tablo.GetElementsByTagName("tr");

                // Satırları tek tek gez. Başlık satırı varsa 2. satırdan başla.
                for (int i = BaslikSatiriVar ? 1 : 0; i < satirlar.Count; i++)
                {
                    // DataTable için yeni bir kayıt oluştur.
                    DataRow kayit = dt.NewRow();
                    // Şimdiki satırın hücrelerini bir koleksiyona al.
                    HtmlElementCollection hucreler = satirlar[i].GetElementsByTagName("td");

                    // Hücreler içinde HucreNumaralari dizisinin boyu kadar bir gezinti yap.
                    for (int j = 0; j < HucreNumaralari.Length; j++)
                    {
                        // Sıradaki hücre numarasını (indisini) al.
                        int hucreNo = HucreNumaralari[j];
                        // hucreler koleksiyonundaki indisi hucreNo olan alanın değerini
                        // kaydın j. alanına ata.

                        kayit[j] = hucreler[hucreNo].InnerText;

                        // veya kısaca.
                        // kayit[j] = hucreler[HucreNumaralari[j]].InnerText;
                    }
                    dt.Rows.Add(kayit);
                }
                // Olay dinleyiciyi kaldır.
                webBrowser1.DocumentCompleted -= webBrowser1_DocumentCompleted;

                // Buradan sonra DataTable ile istediğini yapabilirsin.
                // Örneğin forma bir DataGridView koy. Alttaki satır kayıtları görüntüleyecektir.
                dataGridView1.DataSource = dt;
            }
        }
        private void KutukKayitYap()
        {
            if (webBrowser1.Document.GetElementById("IOMPageHeader1_lblKurumIlIlce") != null)
            {
                string kurumBilgi = webBrowser1.Document.GetElementById("IOMPageHeader1_lblKurumIlIlce").InnerText;
                //  string kurumBilgi = "ERZURUM / AŞKALE / Çiftlik Ortaokulu (722297)";
                string[] bilgi = kurumBilgi.Replace('(', '/').Replace(")", "").Split('/');

                string ilce = bilgi[1].Trim();
                string kurumAdi = bilgi[2].TrimEnd().TrimStart();
                string kurumKodu = bilgi[3].Trim();


                KutukIslemleriDB veriDb = new KutukIslemleriDB();

                if (cbSilme.Checked == false) //İşaretliyse silme.
                    veriDb.KayitSil(kurumKodu);
                //    0          1        2        3         4          5
                //"Tc Kimlik", "Adi", "Soyadi", "OkulNo", "Sinifi", "Aktif Öğrenci"
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    DataGridViewRow dr = dataGridView1.Rows[i];
                    string[] sinifSube = dr.Cells[4].Value.ToString().Replace(". Sınıf", "").Replace("Şubesi", "")
                        .Replace("-İmam Hatip", "").Replace("-İmam Hatip (Yabancı Dil Ağırlıklı Hazırlık Sınıfı)", "")
                        .Replace("-İmam Hatip (Yabancı Dil Ağırlıklı)", "").Split('/');
                    string sinifi = sinifSube[0].Replace(" (Yabancı Dil Ağırlıklı)", "")
                        .Replace(" (Yabancı Dil Ağırlıklı Hazırlık Sınıfı)", "").Trim();
                    string subesi = sinifSube[1].Trim();

                    KutukIslemleriInfo info = new KutukIslemleriInfo
                    {
                        Ilce = ilce,
                        KurumAdi = kurumAdi,
                        KurumKodu = kurumKodu,
                        TcKimlik = dr.Cells[0].Value.ToString(),
                        Adi = dr.Cells[1].Value.ToString(),
                        Soyadi = dr.Cells[2].Value.ToString(),
                        OkulNo = dr.Cells[3].Value.ToString(),
                        Sinif = sinifi,
                        Sube = subesi
                    };

                    veriDb.KayitEkle(info);
                }
            }
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString().Contains("Main.aspx"))
                webBrowser1.Navigate(new Uri("https://eokulyd.meb.gov.tr//IlkOgretim/OGR/IOG00001.aspx"));
        }
        private void btnEkle_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            txtUrl.Text = e.ClickedItem.Text;
            webBrowser1.Navigate(txtUrl.Text);
        }
        private void btnListeleKaydet_Click(object sender, EventArgs e)
        {
            Listele();
            KutukKayitYap();
            EklenenKurumlar();

            if (cbSilme.Checked == false) //İşaretliyse silme.
                webBrowser1.Url = new Uri("https://eokulyd.meb.gov.tr//IlkOgretim/OGR/IOG00001.aspx");
        }
        private void FormKutukIslemleri_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G && e.KeyCode == Keys.Alt && e.KeyCode == Keys.Control)
            {
                btnSinifSec.PerformClick();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.H && e.KeyCode == Keys.Alt && e.KeyCode == Keys.Control)
            {
                btnListeleKaydet.PerformClick();
                e.Handled = true;
            }
        }
        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.G && e.KeyCode == Keys.Alt && e.KeyCode == Keys.Control)
            {
                btnSinifSec.PerformClick();
            }
            if (e.KeyCode == Keys.H && e.KeyCode == Keys.Alt && e.KeyCode == Keys.Control)
            {
                btnListeleKaydet.PerformClick();
            }
        }
        private void btnSinifSec_Click(object sender, EventArgs e)
        {
            if (webBrowser1.Document.GetElementById("ddlSinifi") != null)
            {
                string kurumBilgi = webBrowser1.Document.GetElementById("IOMPageHeader1_lblKurumIlIlce").InnerText;
                string[] bilgi = kurumBilgi.Replace('(', '/').Replace(")", "").Split('/');

                string kurumAdi = bilgi[2].TrimEnd().TrimStart();

                string sinif = kurumAdi.Contains("İmam Hatip") ? "106" : cbSinif.SelectedValue.ToString();
                webBrowser1.Document.GetElementById("ddlSinifi").SetAttribute("value", sinif);
                webBrowser1.Document.GetElementById("ddlDurumu").SetAttribute("value", "0");
                webBrowser1.Document.GetElementById("btnAra").InvokeMember("click");
            }
            if (webBrowser1.Document.GetElementById("ddlIlce") != null)
            {
                HtmlElement elem = webBrowser1.Document.GetElementById("ddlIlce");
                elem.SetAttribute("selectedIndex", cbIlce.SelectedValue.ToString());
                elem.RaiseEvent("onChange");
            }
        }
        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            txtGuvenlikKodu.Focus();
        }

        private void btnDbAktar_Click(object sender, EventArgs e)
        {
            FormKutukDbAktar frm = new FormKutukDbAktar();
            frm.ShowDialog();
        }
    }
}
