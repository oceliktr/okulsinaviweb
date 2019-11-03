using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DAL;
using ODM.Kutuphanem;

namespace ODM
{
    public partial class FormCevaplar : Form
    {
        private int sinavId;
        public FormCevaplar()
        {
            InitializeComponent();
            string ilAdi = IniIslemleri.VeriOku("Baslik", "IlAdi");
            Text = "Optik Form Cevaplar - " + ilAdi.IlkHarfleriBuyut() + " Ölçme ve Değerlendirme Merkezi";

            Oturumlar();
            BranslariGetir();
            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo snvInfo = snvDb.AktifSinavAdi();
            sinavId = snvInfo.SinavId;
            lblSinavId.Text = string.Format("Sınav no: {0}", snvInfo.SinavId);
            lblSinavAdi.Text = string.Format("Sınav adı: {0}", snvInfo.SinavAdi);
        }
        private void Oturumlar()
        {
            cbOturum.Items.Clear();

            FormCkKonumAl.ComboboxItem item = new FormCkKonumAl.ComboboxItem { Text = "Seçiniz", Value = "0" };
            cbOturum.Items.Add(item);
            item = new FormCkKonumAl.ComboboxItem { Text = "1. Oturum", Value = "1" };
            cbOturum.Items.Add(item);
            item = new FormCkKonumAl.ComboboxItem { Text = "2. Oturum", Value = "2" };
            cbOturum.Items.Add(item);
            item = new FormCkKonumAl.ComboboxItem { Text = "3. Oturum", Value = "3" };
            cbOturum.Items.Add(item);
            item = new FormCkKonumAl.ComboboxItem { Text = "4. Oturum", Value = "4" };
            cbOturum.Items.Add(item);
            item = new FormCkKonumAl.ComboboxItem { Text = "5. Oturum", Value = "5" };
            cbOturum.Items.Add(item);
            item = new FormCkKonumAl.ComboboxItem { Text = "3. Oturum", Value = "6" };
            cbOturum.Items.Add(item);
            cbOturum.SelectedIndex = 0;
        }
        private void BranslariGetir()
        {
            BranslarDb brnsDb = new BranslarDb();
            List<BranslarInfo> brns = brnsDb.KayitlariDiziyeGetir();
            brns.Insert(0, new BranslarInfo(0, "Branş Seçiniz"));

            cbBrans.DataSource = brns;
            cbBrans.DisplayMember = "BransAdi";
            cbBrans.ValueMember = "Id";
            cbBrans.SelectedIndex = 0;
        }

        private void Sorular()
        {
          //  OptikFormdakiSoruSayisi();
        }

        private void OptikFormdakiSoruSayisi(int sinavId)
        {
            KonumlarDB knmDb = new KonumlarDB();
            List<KonumlarInfo> knm = knmDb.KayitlariDiziyeGetir(sinavId);
            //opitk formdaki sorular
            List<KonumlarInfo> sorular =
                (from x in knm where x.Grup == "optik" && x.Secenek == "A" orderby x.SoruNo select x).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
