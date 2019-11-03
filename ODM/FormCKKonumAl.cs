using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DAL;
using ODM.Kutuphanem;

namespace ODM
{
    public partial class FormCkKonumAl : Form
    {
        //Combobox Value değerine erişmek için kullanacağım sınıf.
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }

        }
        static readonly string ckBulunduguDizin = IniIslemleri.VeriOku("CKDizinKontrol", "CKBulunduguAdres");
        readonly string ckDizini = ckBulunduguDizin + "\\CevapKagitlari\\";
        private readonly int sinavId;
        bool yatayUst;
        Point yatayUstIlkkonum;
        bool yatayAlt;
        Point yatayAltIlkkonum;
        bool dikeySag;
        Point dikeySagIlkkonum;
        bool dikeySol;
        Point dikeySolIlkkonum;
        bool movi;
        Point moviIlkkonum;
        public FormCkKonumAl()
        {
            InitializeComponent();

            string ilAdi = IniIslemleri.VeriOku("Baslik", "IlAdi");
            Text = "Cevap Kağıdı Konum Alma Formu - " + ilAdi.IlkHarfleriBuyut() + " Ölçme ve Değerlendirme Merkezi";

            pbMove.Location = lblYatayUst.Location = lblDikeySol.Location = new Point(100, 100);
            lblYatayAlt.Location = new Point(100, 230);
            lblDikeySag.Location = new Point(300, 100);

            ndY.Value = ndX.Value = 100;
            ndW.Value = 300;
            ndH.Value = 230;

            rbAcikUclu.Checked = true;
            CKKonumKayitlari();
            BranslariGetir("au");
            SoruNumaralari();
            Oturumlar();

            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo snvInfo = snvDb.AktifSinavAdi();
            sinavId = snvInfo.SinavId;
            lblSinavId.Text = string.Format("Sınav no: {0}", snvInfo.SinavId);
            lblSinavAdi.Text = string.Format("Sınav adı: {0}", snvInfo.SinavAdi);

            if (DizinIslemleri.DizinKontrol(ckDizini))
            {
                //Cevap kağıtlarının listelendiği dizindeki ilk dosyayı seçerek konum tercihlerini yap
                DosyaInfo d = DizinIslemleri.DizindekiDosyalariListele(ckDizini).FirstOrDefault();

                if (d == null)
                {
                    MessageBox.Show(
                        @"Gösterilecek cevap kağıdı bulunamadı. Ana ekranda 'Dizin Seç' butonundan cevap kağıtlarına ait işlemleri yapınız.",
                        @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                else
                {
                    string dosya = ckDizini + d.DosyaAdi;
                    pcCk.ImageLocation = dosya;
                    cbOturum.Enabled = true;
                    cbSecenekler.Enabled = false;
                    cbSoruNo.Enabled = true;
                    btnKaydet.Enabled = true;
                    cbBrans.Enabled = true;
                    cbOturum.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show(@"Öncelikle ayarlar bölümünden Cevap kağıtlarının tutulacağı dizini seçiniz.", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            ComboboxItem soruNoItem = cbSoruNo.SelectedItem as ComboboxItem;
            ComboboxItem oturumItem = cbOturum.SelectedItem as ComboboxItem;
            ComboboxItem secenekItem = cbSecenekler.SelectedItem as ComboboxItem;

            if (cbBrans.SelectedIndex == 0)
                lblBilgi.Text = @"Branş seçmediniz.";
            else if (soruNoItem != null && soruNoItem.Value.ToString() == "0")
                lblBilgi.Text = @"Soru numarasını seçmediniz.";
            else if (oturumItem != null && oturumItem.Value.ToString() == "0")
                lblBilgi.Text = "Oturum seçmediniz.";
            else if ((secenekItem != null && secenekItem.Value.ToString() == "0") && rbOptik.Checked)
                lblBilgi.Text = "Seçenek seçmediniz.";
            else
            {
                string grup = rbAcikUclu.Checked ? "au" : "optik";

                int oturum = oturumItem.Value.ToInt32();
                string secenek = rbOptik.Checked ? secenekItem.Value.ToString() : "";
                int soruNo = soruNoItem.Value.ToInt32();
                int bransId = cbBrans.SelectedValue.ToInt32();

                KonumlarDB knmDb = new KonumlarDB();

                if (knmDb.KayitKontrol(sinavId, soruNo, bransId, oturum, secenek, grup))
                {
                    DialogResult dialog =
                        MessageBox.Show("Bu soru için daha önce kayıt yapılmış. Değiştirilmesini istiyor musunuz?",
                            @"Bilgi", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        KonumlarInfo knmInfo = new KonumlarInfo
                        {
                            BransId = bransId,
                            Grup = grup,
                            SoruNo = soruNo,
                            W = ndW.Value.ToInt32(),
                            H = ndH.Value.ToInt32(),
                            X1 = ndX.Value.ToInt32(),
                            Y1 = ndY.Text.ToInt32(),
                            Oturum = oturum,
                            Secenek = secenek,
                            SinavId = sinavId
                        };
                        knmDb.KayitGuncelle(knmInfo);
                        lblBilgi.Text = string.Format("{0} {1} için değişiklikler kaydedildi.", cbOturum.Text,
                            cbSoruNo.Text);
                    }
                }
                else
                {
                    KonumlarInfo knmInfo = new KonumlarInfo
                    {
                        BransId = bransId,
                        Grup = grup,
                        SoruNo = soruNo,
                        W = ndW.Value.ToInt32(),
                        H = ndH.Value.ToInt32(),
                        X1 = ndX.Value.ToInt32(),
                        Y1 = ndY.Text.ToInt32(),
                        Oturum = oturum,
                        Secenek = secenek,
                        SinavId = sinavId
                    };
                    knmDb.KayitEkle(knmInfo);

                    lblBilgi.Text = string.Format("{0} {1} kaydedildi.", cbOturum.Text, cbSoruNo.Text);
                }
            }
            CKKonumKayitlari();
        }
        private void FormCKKonumAl_Activated(object sender, EventArgs e)
        {
            lblBilgi.Text = "";
            CKKonumKayitlari();
        }

        #region Mouse Taşıma Olayları
        private void lblYatayUst_MouseDown(object sender, MouseEventArgs e)
        {
            yatayUst = true;
            yatayUstIlkkonum = e.Location;
        }
        private void lblDikeySag_MouseDown(object sender, MouseEventArgs e)
        {
            dikeySag = true;
            dikeySagIlkkonum = e.Location;
        }
        private void lblDikeySol_MouseDown(object sender, MouseEventArgs e)
        {
            dikeySol = true;
            dikeySolIlkkonum = e.Location;
        }
        private void lblYatayAlt_MouseDown(object sender, MouseEventArgs e)
        {
            yatayAlt = true;
            yatayAltIlkkonum = e.Location;
        }
        private void lblYatayAlt_MouseUp(object sender, MouseEventArgs e)
        {
            yatayAlt = false;
        }
        private void lblDikeySag_MouseUp(object sender, MouseEventArgs e)
        {
            dikeySag = false;
        }
        private void lblYatayUst_MouseUp(object sender, MouseEventArgs e)
        {
            yatayUst = false;
        }
        private void lblDikeySol_MouseUp(object sender, MouseEventArgs e)
        {
            dikeySol = false;
        }
        private void lblYatayUst_MouseMove(object sender, MouseEventArgs e)
        {
            if (yatayUst && cbTasima.Checked == false)
            {

                lblYatayUst.Top = e.Y + lblYatayUst.Top - (yatayUstIlkkonum.Y);
                lblDikeySol.Location = new Point(lblYatayUst.Location.X, lblYatayUst.Location.Y);
                lblDikeySag.Location = new Point((lblYatayUst.Location.X + lblYatayUst.Width), lblYatayUst.Location.Y);
                lblDikeySol.Height = lblDikeySag.Height = (lblYatayAlt.Location.Y - lblYatayUst.Location.Y);
                lblYatayUst.Width = lblDikeySag.Location.X - lblDikeySol.Location.X;

                Koordinatlar();
            }
        }
        private void lblDikeySag_MouseMove(object sender, MouseEventArgs e)
        {
            if (dikeySag && cbTasima.Checked == false)
            {
                lblDikeySag.Left = e.X + lblDikeySag.Left - (dikeySagIlkkonum.X);
                lblYatayAlt.Width = lblYatayUst.Width = lblDikeySag.Location.X - lblDikeySol.Location.X;

                Koordinatlar();
            }
        }
        private void lblYatayAlt_MouseMove(object sender, MouseEventArgs e)
        {
            if (yatayAlt && cbTasima.Checked == false)
            {
                lblYatayAlt.Top = e.Y + lblYatayAlt.Top - (yatayAltIlkkonum.Y);
                lblDikeySag.Location = new Point((lblYatayAlt.Location.X + lblYatayAlt.Width), lblYatayUst.Location.Y);
                lblDikeySol.Location = new Point(lblYatayUst.Location.X, lblYatayUst.Location.Y);
                lblDikeySol.Height = lblDikeySag.Height = lblYatayAlt.Location.Y - lblYatayUst.Location.Y;

                Koordinatlar();
            }
        }
        private void lblDikeySol_MouseMove(object sender, MouseEventArgs e)
        {
            if (dikeySol && cbTasima.Checked == false)
            {
                lblDikeySol.Left = e.X + lblDikeySol.Left - (dikeySolIlkkonum.X);
                lblYatayAlt.Width = lblYatayUst.Width = lblDikeySag.Location.X - lblDikeySol.Location.X;
                lblYatayUst.Location = new Point(lblDikeySol.Location.X, lblDikeySol.Location.Y);
                lblYatayAlt.Location = new Point(lblDikeySol.Location.X, lblDikeySol.Location.Y + lblDikeySol.Height);

                Koordinatlar();
            }
        }
        private void pbMove_MouseDown(object sender, MouseEventArgs e)
        {
            movi = true;
            moviIlkkonum = e.Location;
        }
        private void pbMove_MouseUp(object sender, MouseEventArgs e)
        {
            movi = false;
        }
        private void pbMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (movi && cbTasima.Checked == false)
            {
                pbMove.Left = (e.X + pbMove.Left) - (moviIlkkonum.X);
                pbMove.Top = ((e.Y + pbMove.Top) - (moviIlkkonum.Y));

                lblYatayUst.Location = new Point(pbMove.Location.X, ((pbMove.Location.Y)));
                lblDikeySol.Location = new Point(pbMove.Location.X, (pbMove.Location.Y));

                lblYatayAlt.Location = new Point((lblDikeySol.Location.X), (lblDikeySol.Location.Y + lblDikeySol.Height));
                lblDikeySag.Location = new Point((lblDikeySol.Location.X + lblYatayUst.Width), (lblYatayUst.Location.Y));

                Koordinatlar();
            }
        }
        #endregion

        private void Secenekler()
        {
            cbSecenekler.Items.Clear();
            ComboboxItem item = new ComboboxItem { Text = "Seçiniz", Value = 0 };
            cbSecenekler.Items.Add(item);
            item = new ComboboxItem { Text = "A", Value = "A" };
            cbSecenekler.Items.Add(item);
            item = new ComboboxItem { Text = "B", Value = "B" };
            cbSecenekler.Items.Add(item);
            item = new ComboboxItem { Text = "C", Value = "C" };
            cbSecenekler.Items.Add(item);
            item = new ComboboxItem { Text = "D", Value = "D" };
            cbSecenekler.Items.Add(item);
            item = new ComboboxItem { Text = "E", Value = "E" };
            cbSecenekler.Items.Add(item);
            cbSecenekler.SelectedIndex = 0;
        }
        private void Oturumlar()
        {
            cbOturum.Items.Clear();

            ComboboxItem item = new ComboboxItem { Text = "Seçiniz", Value = "0" };
            cbOturum.Items.Add(item);
            item = new ComboboxItem { Text = "1. Oturum", Value = "1" };
            cbOturum.Items.Add(item);
            item = new ComboboxItem { Text = "2. Oturum", Value = "2" };
            cbOturum.Items.Add(item);
            item = new ComboboxItem { Text = "3. Oturum", Value = "3" };
            cbOturum.Items.Add(item);
            item = new ComboboxItem { Text = "4. Oturum", Value = "4" };
            cbOturum.Items.Add(item);
            item = new ComboboxItem { Text = "5. Oturum", Value = "5" };
            cbOturum.Items.Add(item);
            item = new ComboboxItem { Text = "3. Oturum", Value = "6" };
            cbOturum.Items.Add(item);
            cbOturum.SelectedIndex = 0;
        }
        private void Koordinatlar()
        {
            ndX.Value = (lblYatayUst.Location.X - pcCk.Left);
            ndY.Value = (lblYatayUst.Location.Y - pcCk.Top);

            txtX2.Text = ((lblYatayAlt.Location.X + lblYatayAlt.Width) - pcCk.Left).ToString();
            txtY2.Text = (lblYatayAlt.Location.Y - pcCk.Top).ToString();

            ndW.Value = lblYatayUst.Width;
            ndH.Value = lblDikeySol.Height;
        }
        private void BranslariGetir(string grup)
        {
            BranslarDb brnsDb = new BranslarDb();
            List<BranslarInfo> brns = brnsDb.KayitlariDiziyeGetir();
            if (grup == "optik")
                brns.Insert(0, new BranslarInfo(0, "Tüm Branşlar"));
            brns.Insert(0, new BranslarInfo(0, "Branş Seçiniz"));

            cbBrans.DataSource = brns;
            cbBrans.DisplayMember = "BransAdi";
            cbBrans.ValueMember = "Id";
            cbBrans.SelectedIndex = 0;
        }
        private void SoruNumaralari()
        {
            cbSoruNo.Items.Clear();
            ComboboxItem item = new ComboboxItem { Text = "Seçiniz", Value = 0 };
            cbSoruNo.Items.Add(item);
            //TODO:Maksimum soru sayısı ayarlar bölümüne alınabilir.
            for (int i = 1; i <= 100; i++)
            {
                item = new ComboboxItem { Text = i + ". soru", Value = i };
                cbSoruNo.Items.Add(item);
            }
            cbSoruNo.SelectedIndex = 0;
        }
        private void CKKonumKayitlari()
        {

            ComboboxItem oturumItem = cbOturum.SelectedItem as ComboboxItem;
            int oturum = 0;
            if(oturumItem!=null)
             oturum=   oturumItem.Value.ToInt32();

            KonumlarDB knmDb = new KonumlarDB();
            string grup = rbAcikUclu.Checked ? "au" : "optik";
            dataGridView1.DataSource = knmDb.KayitlariGetir(sinavId,oturum, grup);

            //SinavId,Oturum,SoruNo,BransAdi,Id,Secenek
            if (grup == "au")
            {
                dataGridView1.Columns[0].HeaderText = "Sınav No";
                dataGridView1.Columns[0].Width = 90;
                dataGridView1.Columns[1].HeaderText = "Oturum";
                dataGridView1.Columns[1].Width = 90;
                dataGridView1.Columns[2].HeaderText = "Ders Adı";
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[4].HeaderText = "Soru No";
                dataGridView1.Columns[4].Width = 80;

                dataGridView1.Columns["Secenek"].Visible = false;
            }
            else
            {
                dataGridView1.Columns[0].HeaderText = "Sınav No";
                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].HeaderText = "Oturum";
                dataGridView1.Columns[1].Width = 60;
                dataGridView1.Columns[2].HeaderText = "Ders Adı";
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[4].HeaderText = "Soru No";
                dataGridView1.Columns[4].Width = 60;
                dataGridView1.Columns[5].HeaderText = "Seçenek";
                dataGridView1.Columns[5].Width = 60;
                dataGridView1.Columns["Secenek"].Visible = true;
            }
            dataGridView1.Columns["Id"].Visible = false;
        }
        private void FormCkKonumAl_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.None && e.KeyCode == Keys.Escape)
            {
                Close();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnKaydet.PerformClick();
            }
        }
        private void karekodKonumunuKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComboboxItem oturumItem = cbOturum.SelectedItem as ComboboxItem;
            int oturum = oturumItem.Value.ToInt32();
            if (oturum == 0)
            {
                MessageBox.Show("Karekod konumu hangi sayfaya ait olduğunu seçiniz.", @"Bilgi", MessageBoxButtons.OK);
            }
            else
            {
                KonumlarDB knmDb = new KonumlarDB();
                string grup = "karekod";
                KonumlarInfo knmInfo = DBTabloAlanlar(grup, oturum);
                if (knmDb.KayitKontrol(sinavId, 0, 0, oturum, "", grup))
                {
                    DialogResult dialog =
                        MessageBox.Show("Karekod konumu zaten kayıtlı. Değiştirilmesini istiyor musunuz?", @"Bilgi",
                            MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {

                        knmDb.KayitGuncelle(knmInfo);
                        lblBilgi.Text = "Karekod konumu için değişiklikler kaydedildi.";
                        MessageBox.Show(lblBilgi.Text, @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    knmDb.KayitEkle(knmInfo);

                    lblBilgi.Text = "Karekod konumu kaydedildi.";
                    MessageBox.Show(lblBilgi.Text, @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void oCRKonumunuKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComboboxItem oturumItem = cbOturum.SelectedItem as ComboboxItem;
            int oturum = oturumItem.Value.ToInt32();
            if (oturum == 0)
            {
                MessageBox.Show("OCR konumu hangi sayfaya ait olduğunu seçiniz.", @"Bilgi", MessageBoxButtons.OK);
            }
            else
            {
                KonumlarDB knmDb = new KonumlarDB();
                const string grup = "ocr";
                KonumlarInfo knmInfo = DBTabloAlanlar(grup, oturum);

                if (knmDb.KayitKontrol(sinavId, 0, 0, oturum, "", grup))
                {
                    DialogResult dialog = MessageBox.Show("OCR konumu zaten kayıtlı. Değiştirilmesini istiyor musunuz?", @"Bilgi", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        knmDb.KayitGuncelle(knmInfo);
                        lblBilgi.Text = "OCR konumu için değişiklikler kaydedildi.";
                        MessageBox.Show(lblBilgi.Text, @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    knmDb.KayitEkle(knmInfo);

                    lblBilgi.Text = "OCR konumu kaydedildi.";
                    MessageBox.Show(lblBilgi.Text, @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void sınavaGirmediKonumuKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ComboboxItem oturumItem = cbOturum.SelectedItem as ComboboxItem;
            int oturum = oturumItem.Value.ToInt32();
            if (oturum == 0)
            {
                MessageBox.Show("Sınava girmedi konumu hangi sayfaya ait olduğunu seçiniz.", @"Bilgi", MessageBoxButtons.OK);
            }
            else
            {
                KonumlarDB knmDb = new KonumlarDB();
                const string grup = "girmedi";
                KonumlarInfo knmInfo = DBTabloAlanlar(grup, oturum);

                if (knmDb.KayitKontrol(sinavId, 0, 0, oturum, "", grup))
                {
                    DialogResult dialog = MessageBox.Show("Sınava girmedi  konumu zaten kayıtlı. Değiştirilmesini istiyor musunuz?", @"Bilgi", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        knmDb.KayitGuncelle(knmInfo);
                        lblBilgi.Text = "Sınava girmedi konumu için değişiklikler kaydedildi.";
                        MessageBox.Show(lblBilgi.Text, @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    knmDb.KayitEkle(knmInfo);

                    lblBilgi.Text = "Sınava girmedi konumu kaydedildi.";
                    MessageBox.Show(lblBilgi.Text, @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Kayıt yapılacak alanların tanımlanmasını yapan metod
        /// </summary>
        /// <param name="grup">au=açık uclu optik=optik formu</param>
        /// <param name="oturum"></param>
        /// <returns></returns>
        private KonumlarInfo DBTabloAlanlar(string grup, int oturum)
        {
            KonumlarInfo knmInfo = new KonumlarInfo
            {
                BransId = 0,
                Grup = grup,
                SoruNo = 0,
                H = ndH.Value.ToInt32(),
                W = ndW.Value.ToInt32(),
                X1 = ndX.Value.ToInt32(),
                Y1 = ndY.Value.ToInt32(),
                Oturum = oturum,
                Secenek = "",
                SinavId = sinavId
            };
            return knmInfo;
        }

        //TODO seçeneklerin sayısını ayarlardan arttırılabilir.
        private void rbAcikUclu_CheckedChanged(object sender, EventArgs e)
        {
            BranslariGetir("au");
            cbSecenekler.Enabled = false;
            CKKonumKayitlari();
        //    Oturumlar();
            btnSonrakiSoru.Enabled = btnOncekiSoru.Enabled = btnSonrakiSecenek.Enabled = btnOncekiSecenek.Enabled = false;
        }
        private void rbOptik_CheckedChanged(object sender, EventArgs e)
        {
            BranslariGetir("optik");

            cbSecenekler.Enabled = true;
            CKKonumKayitlari();
            Secenekler();

            if (cbTasima.Checked)
                btnSonrakiSoru.Enabled = btnOncekiSoru.Enabled = btnSonrakiSecenek.Enabled = btnOncekiSecenek.Enabled = true;
        }
        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Silmek istediğinizden emin misiniz?", @"Bilgi", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                int id = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value.ToInt32();
                KonumlarDB veriDb = new KonumlarDB();
                veriDb.KayitSil(id);
                CKKonumKayitlari();
            }
        }
        private void ndX_ValueChanged(object sender, EventArgs e)
        {
            if (!movi && cbTasima.Checked)
            {
                lblDikeySol.Location = new Point(ndX.Value.ToInt32() + pcCk.Left, ndY.Value.ToInt32() + pcCk.Top);
                lblYatayUst.Location = new Point(ndX.Value.ToInt32() + pcCk.Left, (ndY.Value.ToInt32() + pcCk.Top));

                lblYatayAlt.Location = new Point(ndX.Value.ToInt32() + pcCk.Left, (ndY.Value.ToInt32() + lblDikeySol.Height + pcCk.Top));
                lblDikeySag.Location = new Point((ndX.Value.ToInt32() + lblYatayUst.Width + pcCk.Left), (ndY.Value.ToInt32() + pcCk.Top));

                pbMove.Location = new Point(ndX.Value.ToInt32() + pcCk.Left, ndY.Value.ToInt32() + pcCk.Top);
            }
        }
        private void ndY_ValueChanged(object sender, EventArgs e)
        {
            if (!movi && cbTasima.Checked)
            {

                lblDikeySol.Location = new Point(ndX.Value.ToInt32() + pcCk.Left, ndY.Value.ToInt32() + pcCk.Top);
                lblYatayUst.Location = new Point(ndX.Value.ToInt32() + pcCk.Left, (ndY.Value.ToInt32() + pcCk.Top));

                lblYatayAlt.Location = new Point(ndX.Value.ToInt32() + pcCk.Left, (ndY.Value.ToInt32() + lblDikeySol.Height + pcCk.Top));
                lblDikeySag.Location = new Point((ndX.Value.ToInt32() + lblYatayUst.Width + pcCk.Left), (ndY.Value.ToInt32() + pcCk.Top));

                pbMove.Location = new Point(ndX.Value.ToInt32() + pcCk.Left, ndY.Value.ToInt32() + pcCk.Top);
            }
        }
        private void ndW_ValueChanged(object sender, EventArgs e)
        {
            if (!movi && cbTasima.Checked)
            {
                lblYatayUst.Width = lblYatayAlt.Width = ndW.Value.ToInt32();
                lblDikeySag.Location = new Point((ndX.Value.ToInt32() + lblYatayUst.Width + pcCk.Left), (ndY.Value.ToInt32() + pcCk.Top));
            }
        }

        private void cbTasima_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTasima.Checked)
            {
                ndH.Enabled = true;
                ndW.Enabled = true;
                ndX.Enabled = true;
                ndY.Enabled = true;
                pbMove.Cursor = Cursors.Default;
                lblDikeySol.Cursor = lblDikeySag.Cursor = lblYatayAlt.Cursor = lblYatayUst.Cursor = Cursors.Default;
                if (rbOptik.Checked)
                    btnSonrakiSoru.Enabled = btnOncekiSoru.Enabled = btnSonrakiSecenek.Enabled = btnOncekiSecenek.Enabled = true;
            }
            else
            {
                ndH.Enabled = false;
                ndW.Enabled = false;
                ndX.Enabled = false;
                ndY.Enabled = false;
                pbMove.Cursor = Cursors.SizeAll;
                lblDikeySol.Cursor = lblDikeySag.Cursor = lblYatayAlt.Cursor = lblYatayUst.Cursor = Cursors.VSplit;

                btnSonrakiSoru.Enabled = btnOncekiSoru.Enabled = btnSonrakiSecenek.Enabled = btnOncekiSecenek.Enabled = false;
            }
        }
        private void ndH_ValueChanged(object sender, EventArgs e)
        {
            if (!movi && cbTasima.Checked)
            {
                lblDikeySol.Height = lblDikeySag.Height = ndH.Value.ToInt32();

                lblYatayAlt.Location = new Point(ndX.Value.ToInt32() + pcCk.Left, (ndY.Value.ToInt32() + ndH.Value.ToInt32() + pcCk.Top));

            }
        }
        private void btnOncekiSoru_Click(object sender, EventArgs e)
        {
            if (cbSoruNo.SelectedIndex - 1 > 0)
            {
                ndY.Value = ndY.Value - (ndDikeyAralik.Value + ndH.Value);
                cbSoruNo.SelectedIndex = cbSoruNo.SelectedIndex - 1;
            }
        }
        private void btnSonrakiSoru_Click(object sender, EventArgs e)
        {
            if (cbSoruNo.SelectedIndex + 1 < cbSoruNo.Items.Count)
            {
                ndY.Value = ndY.Value + (ndDikeyAralik.Value + ndH.Value);
                cbSoruNo.SelectedIndex = cbSoruNo.SelectedIndex + 1;
            }
        }
        private void btnOncekiSecenek_Click(object sender, EventArgs e)
        {
            if (cbSecenekler.SelectedIndex - 1 > 0)
            {
                ndX.Value = ndX.Value - (ndW.Value + ndYatayAralik.Value);
                cbSecenekler.SelectedIndex = cbSecenekler.SelectedIndex - 1;
            }
        }
        private void btnSonrakiSecenek_Click(object sender, EventArgs e)
        {
            if (cbSecenekler.SelectedIndex + 1 < cbSecenekler.Items.Count)
            {
                ndX.Value = ndX.Value + (ndW.Value + ndYatayAralik.Value);
                cbSecenekler.SelectedIndex = cbSecenekler.SelectedIndex + 1;
            }
        }
        private void işaretçiyiTaşıToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int id = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["Id"].Value.ToInt32();
            KonumlarDB veriDb = new KonumlarDB();
            KonumlarInfo info = veriDb.KayitBilgiGetir(id);

            if (movi == false && cbTasima.Checked == false)
            {
                pbMove.Left = (info.X1 + pcCk.Left);
                pbMove.Top = (info.Y1 + pcCk.Top);

                lblYatayUst.Location = new Point(pbMove.Location.X, ((pbMove.Location.Y)));
                lblDikeySol.Location = new Point(pbMove.Location.X, (pbMove.Location.Y));

                lblYatayAlt.Location = new Point((lblDikeySol.Location.X), (lblDikeySol.Location.Y + info.H));
                lblDikeySag.Location = new Point((lblDikeySol.Location.X + info.W), (lblYatayUst.Location.Y));

                lblYatayUst.Width = lblYatayAlt.Width = info.W;
                lblDikeySol.Height = lblDikeySag.Height = info.H;
                Koordinatlar();
            }
            cbBrans.SelectedValue = info.BransId;
            cbOturum.SelectedIndex = info.Oturum;
            cbSoruNo.SelectedIndex = info.SoruNo;
            if (rbOptik.Checked)
            {
                switch (info.Secenek)
                {
                    case "A":
                        cbSecenekler.SelectedIndex = 1;
                        break;
                    case "B":
                        cbSecenekler.SelectedIndex = 2;
                        break;
                    case "C":
                        cbSecenekler.SelectedIndex = 3;
                        break;
                    case "D":
                        cbSecenekler.SelectedIndex = 4;
                        break;
                    case "E":
                        cbSecenekler.SelectedIndex = 5;
                        break;
                }
            }
        }

        private void yeniCKYükleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog
            {
                Filter = "Resim Dosyaları (*.jpg;*.png)|*.jpg;*.png",
                Title = "Cevap kağıdı dosyasını seçiniz."
            };

            if (o.ShowDialog() == DialogResult.OK)
            {
                pcCk.Image = ImageProcessing.Blacken(new Bitmap(o.FileName));
            }

        }
        private void siyahlariSayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmap = new Bitmap(pcCk.Image);
            int a = 0;
            for (int i = ndX.Value.ToInt32(); i <= (ndX.Value.ToInt32() + ndW.Value.ToInt32()); i++)
            {
                for (int j = ndY.Value.ToInt32(); j <= (ndY.Value.ToInt32() + ndH.Value.ToInt32()); j++)
                {
                    Color piksel = bmap.GetPixel(i, j);
                    if (piksel.R <= 200 && piksel.G <= 200 && piksel.B <= 200)
                    {
                        a++;
                    }
                }
            }
            Application.DoEvents();
            MessageBox.Show(a.ToString());
        }

        private void kitapçıkTürüAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComboboxItem oturumItem = cbOturum.SelectedItem as ComboboxItem;
            int oturum = oturumItem.Value.ToInt32();
            if (oturum == 0)
            {
                MessageBox.Show("Kitapçık türü -A- konumu hangi sayfaya ait olduğunu seçiniz.", @"Bilgi", MessageBoxButtons.OK);
            }
            else
            {
                KonumlarDB knmDb = new KonumlarDB();
                const string grup = "A";
                KonumlarInfo knmInfo = DBTabloAlanlar(grup, oturum);

                if (knmDb.KayitKontrol(sinavId, 0, 0, oturum, "", grup))
                {
                    DialogResult dialog = MessageBox.Show("Kitapçık türü -A- konumu zaten kayıtlı. Değiştirilmesini istiyor musunuz?", @"Bilgi", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        knmDb.KayitGuncelle(knmInfo);
                        lblBilgi.Text = "Kitapçık türü -A- konumu için değişiklikler kaydedildi.";
                        MessageBox.Show(lblBilgi.Text, @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    knmDb.KayitEkle(knmInfo);

                    lblBilgi.Text = "Kitapçık türü -A- konumu kaydedildi.";
                    MessageBox.Show(lblBilgi.Text, @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void kitapçıkTürüBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComboboxItem oturumItem = cbOturum.SelectedItem as ComboboxItem;
            int oturum = oturumItem.Value.ToInt32();
            if (oturum == 0)
            {
                MessageBox.Show("Kitapçık türü -B- konumu hangi sayfaya ait olduğunu seçiniz.", @"Bilgi", MessageBoxButtons.OK);
            }
            else
            {
                KonumlarDB knmDb = new KonumlarDB();
                const string grup = "B";
                KonumlarInfo knmInfo = DBTabloAlanlar(grup, oturum);

                if (knmDb.KayitKontrol(sinavId, 0, 0, oturum, "", grup))
                {
                    DialogResult dialog = MessageBox.Show("Kitapçık türü -B- konumu zaten kayıtlı. Değiştirilmesini istiyor musunuz?", @"Bilgi", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        knmDb.KayitGuncelle(knmInfo);
                        lblBilgi.Text = "Kitapçık türü -B- konumu için değişiklikler kaydedildi.";
                        MessageBox.Show(lblBilgi.Text, @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    knmDb.KayitEkle(knmInfo);

                    lblBilgi.Text = "Kitapçık türü -B- konumu kaydedildi.";
                    MessageBox.Show(lblBilgi.Text, @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cbOturum_SelectedIndexChanged(object sender, EventArgs e)
        {

            CKKonumKayitlari();
        }

        private void FormCkKonumAl_Load(object sender, EventArgs e)
        {

        }
    }
}
