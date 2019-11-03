using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using MySql.Data.MySqlClient;
using ODM.Kutuphanem;
using ThoughtWorks.QRCode.Codec;

namespace ODM
{
    public partial class FormCKHazirla : Form
    {
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }


        private static readonly string ckDizin = IniIslemleri.VeriOku("CKDizinKontrol", "CKBulunduguAdres") + "\\CevapKagitlari\\";

        readonly PrintDocument printDocument1 = new PrintDocument();
        private readonly int sinavId;
        private void IlceleriGetir()
        {
            IlcelerDb veriDb = new IlcelerDb();
            List<IlcelerInfo> ilce = veriDb.KayitlariDiziyeGetir();
            ilce.Insert(0, new IlcelerInfo(0, "İlçe Seçiniz"));

            cbIlce.DataSource = ilce;
            cbIlce.DisplayMember = "IlceAdi";
            cbIlce.ValueMember = "Id";
            cbIlce.SelectedIndex = 0;

            IlcedeSinavaGirenSubeleriGetir(0);
        }
        private void SinavaGirenOkullariGetir(int ilceId)
        {
            KurumlarDb veriDb = new KurumlarDb();
            List<KurumlarInfo> okul = veriDb.SinavaGirenOkullariDiziyeGetir(sinavId, ilceId);
            okul.Insert(0, new KurumlarInfo("0", "Seçiniz", 0, ""));

            cbOkul.DataSource = okul;
            cbOkul.DisplayMember = "KurumAdi";
            cbOkul.ValueMember = "KurumKodu";
            cbOkul.SelectedIndex = 0;
        }
        private void SinavaGirenSiniflariGetir()
        {
            OgrencilerDb veriDb = new OgrencilerDb();
            List<OgrencilerInfo> snf = veriDb.SinavaGirenSiniflar(sinavId);
            snf.Insert(0, new OgrencilerInfo(0, "Seçiniz"));

            cbSinif.DataSource = snf;
            cbSinif.DisplayMember = "SinifAdi";
            cbSinif.ValueMember = "Sinifi";
            cbSinif.SelectedIndex = 0;
        }
        private void SinavaGirenSubeleriGetir(int kurumKodu, int sinif)
        {
            OgrencilerDb veriDb = new OgrencilerDb();
            List<OgrencilerInfo> sube = veriDb.SinavaGirenSubeler2(sinavId, kurumKodu, sinif);
            sube.Insert(0, new OgrencilerInfo("Seçiniz"));

            cbSube.DataSource = sube;
            cbSube.DisplayMember = "SinifSube";
            cbSube.ValueMember = "SinifSube";
            cbSube.SelectedIndex = 0;
        }
        private void IlcedeSinavaGirenSubeleriGetir(int ilce)
        {
            OgrencilerDb veriDb = new OgrencilerDb();
            List<OgrencilerInfo> sube = veriDb.IlcedeSinavaGirenSubeler(sinavId, ilce);
            sube.Insert(0, new OgrencilerInfo("Seçiniz"));

            cbSube.DataSource = sube;
            cbSube.DisplayMember = "SinifSube";
            cbSube.ValueMember = "SinifSube";
            cbSube.SelectedIndex = 0;
        }
        private void SinavaGirenOgrenciler()
        {

            int ilceId = cbIlce.SelectedValue.ToInt32();
            int sinif = cbSinif.SelectedValue.ToInt32();
            if (sinif != 0 && ilceId != 0)
            {
                int kurumKodu = cbOkul.SelectedValue.ToInt32();
                string sube = cbSube.SelectedValue != null ? cbSube.SelectedValue.ToString() : "Tümü";

                OgrencilerDb veriDb = new OgrencilerDb();

                List<OgrencilerInfo> ogr = veriDb.KayitlariDiziyeGetir(sinavId, ilceId, kurumKodu, sinif, sube);
                ogr.Insert(0, new OgrencilerInfo(0, "Seçiniz", 0));

                cbOgrenciler.DataSource = ogr;
                cbOgrenciler.DisplayMember = "AdiSoyadi";
                cbOgrenciler.ValueMember = "OgrenciId";
                cbOgrenciler.SelectedIndex = 0;
            }
        }

        public FormCKHazirla()
        {
            InitializeComponent();
            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo sinfo = snvDb.AktifSinavAdi();
            lblSinavAdi.Text = string.Format("Sınav Adı: {0}", sinfo.SinavAdi);
            lblSinavNo.Text = string.Format("Sınav No: {0}", sinfo.SinavId);
            sinavId = sinfo.SinavId;

            string ilAdi = IniIslemleri.VeriOku("Baslik", "IlAdi");
            Text = "Cevap Kağıdı Oluşturma Formu " + ilAdi.IlkHarfleriBuyut() + " Ölçme ve Değerlendirme Merkezi";
            IlceleriGetir();
        }
        private void FormCKHazirla_Load(object sender, EventArgs e)
        {
            this.cbYazicilar.DataSource = System.Drawing.Printing.PrinterSettings.InstalledPrinters.OfType<string>().ToArray();
            this.cbYazicilar.DisplayMember = "PrinterName";

            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
        }

        private void btnCkYazdir_Click(object sender, EventArgs e)
        {
            if (cbIlce.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("İlçe seçmediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (cbSinif.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Sınıf seçmediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (cbOturum.Text == "")
            {
                MessageBox.Show("Oturum seçmediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                int a = 0;
                string oturum = cbOturum.Text.Substring(0, 1);
                int ilceId = cbIlce.SelectedValue.ToInt32();
                string ilceAdi = cbIlce.Text;
               
                if (!DizinIslemleri.DizinKontrol(ckDizin+ ilceAdi + "\\"+oturum))
                    DizinIslemleri.DizinOlustur(ckDizin + ilceAdi + "\\" + oturum);
               
                int kurumKodu = cbOkul.SelectedValue.ToInt32();
                int sinif = cbSinif.SelectedValue.ToInt32();
                string sube = cbSube.SelectedValue.ToString();
                int ogrId = cbOgrenciler.SelectedValue.ToInt32();

                //optikte işaretlenecek alanların bilgileri
                const int w = 38;
                const int h = 38;
                int x = 1305; //başlangıç x koordinatı
                int y = 1786;//başlangıç y koordinatı
                int artim = 50;// iki boşluk arasındaki fark

                //Sınav numarasına göre Öğrenci bilgilerini çekelim
                OgrencilerDb veriDb = new OgrencilerDb();

                List<OgrencilerInfo> ogrenciler = veriDb.KayitlariDiziyeGetir(sinavId, ilceId, kurumKodu, sinif, sube, ogrId);

                progressBar1.Maximum = ogrenciler.Count;
                progressBar1.Value = 0;

                foreach (OgrencilerInfo ogr in ogrenciler)
                {
                    a++;
                    progressBar1.Value = a;

                    //bitmap sınıfından bir nesne üreterek pictureboxu tanımlıyoruz.
                    Bitmap img = new Bitmap(pictureBox1.Image);

                    //çizilecek nesne tanımlanıyor
                    Graphics ckImage = Graphics.FromImage(img);

                    //dolgu renk ve font ayarlarını yap
                    Brush dolgu = new SolidBrush(Color.Black);
                    Font f = new Font(Font.FontFamily, 23, FontStyle.Regular);
                    
                    string ogrenciId = oturum + ogr.OgrenciId;
                    //grafik çizimlerini başlatıyoruz.
                    ckImage.DrawString(ogr.Adi + " " + ogr.Soyadi, f, Brushes.Black, 430, 510); //adı soyadı
                    ckImage.DrawString(ogr.Sinifi + " - " + ogr.Sube, f, Brushes.Black, 430, 588); //sınıfı
                    if (ogr.OgrOkulNo != 0) //yedek öğrencilerde öğrenci no 0 olduğu için öğr no 0 ise yazma
                        ckImage.DrawString(ogr.OgrOkulNo.ToString(), f, Brushes.Black, 430, 665); //no
                    ckImage.DrawString(ogr.IlceAdi, f, Brushes.Black, 430, 743); //ilçe
                    ckImage.DrawString(ogr.KurumAdi, f, Brushes.Black, 430, 822); //okulu
                    ckImage.DrawString(ogrenciId, f, Brushes.Black, 1060, 1665); //ocr

                    for (int sag = 0; sag < 6; sag++)
                    {
                        for (int asagi = 0; asagi < 10; asagi++)
                        {
                            for (int z = 0; z < ogrenciId.Length; z++)
                            {
                                //sadece gelen değerleri işaretlemek için sorgu
                                if (sag == 5 - (ogrenciId.Length - (z + 1)) &&
                                    asagi == Convert.ToInt32(ogrenciId.Substring(z, 1)))
                                {
                                    ckImage.FillEllipse(dolgu, x + (artim * sag), y + (artim * asagi), w, h);
                                }
                            }

                        }
                    }

                    img.SetResolution(300, 300);
                    pictureBox1.Image = img;
                    pictureBox1.BackColor = Color.Transparent;
                    // Dosyanın çözünürlüğü

                    if (cbxDosyaOlustur.Checked)
                    {
                        string dosyaAdresi = string.Format(@"{0}{1}\{2}\{3}.png", ckDizin, ilceAdi, oturum, ogrenciId);
                        pictureBox1.Image.Save(dosyaAdresi, ImageFormat.Png);

                    }
                    else
                    {
                        printDocument1.PrintPage += (printDocument1_PrintPage);
                        printDocument1.PrinterSettings.PrinterName = cbYazicilar.SelectedValue.ToString();
                        printDocument1.Print();
                    }
                    ckImage.Clear(Color.Transparent);
                    ckImage.Dispose();
                    Application.DoEvents();
                }
                progressBar1.Value = 0;
                if (cbxDosyaOlustur.Checked)
                {
                    DialogResult dialog = MessageBox.Show(ogrenciler.Count + " öğrenci için cevap kağıdı oluşturuldu.\nŞimdi cevap kağıtlarının oluşturulduğu dizini açmak ister misiniz?", @"Bilgi", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes) Process.Start("explorer.exe", Path.GetDirectoryName(ckDizin));
                }
                else
                    MessageBox.Show(ogrenciler.Count + " öğrenci için cevap kağıdı yazdırılıyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bgwCkOlustur_DoWork(object sender, DoWorkEventArgs e)
        {

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pictureBox1.Image, 0, 0);
        }

        private void cbIlce_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ilceId = cbIlce.SelectedValue.ToInt32();
            SinavaGirenSiniflariGetir();
            SinavaGirenOkullariGetir(ilceId);
            IlcedeSinavaGirenSubeleriGetir(ilceId);
        }
        private void cbOkul_SelectedIndexChanged(object sender, EventArgs e)
        {
            int kurumKodu = cbOkul.SelectedValue.ToInt32();
            int sinif = cbSinif.SelectedValue.ToInt32();
            SinavaGirenSiniflariGetir();
            if (sinif != 0)
            {
                SinavaGirenSubeleriGetir(kurumKodu, sinif);
            }
            SinavaGirenOgrenciler();
        }
        private void cbSinif_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ilceId = cbIlce.SelectedValue.ToInt32();
            int kurumKodu = cbOkul.SelectedValue.ToInt32();
            int sinif = cbSinif.SelectedValue.ToInt32();
            if (sinif != 0)
            {
                if (kurumKodu == 0)
                {
                    IlcedeSinavaGirenSubeleriGetir(ilceId);
                }
                else
                    SinavaGirenSubeleriGetir(kurumKodu, sinif);
            }
            SinavaGirenOgrenciler();
        }
        private void cbSube_SelectedIndexChanged(object sender, EventArgs e)
        {
            SinavaGirenOgrenciler();
        }

        private void cbxDosyaOlustur_CheckedChanged(object sender, EventArgs e)
        {
            btnCkYazdir.Text = cbxDosyaOlustur.Checked ? "Cevap Kağıdı Oluştur" : "Cevap Kağıdı Yazdır";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string dosyaAdresi = string.Format(@"{0}{1}.png", ckDizin, 25);
            MessageBox.Show(dosyaAdresi);

        }
    }
}
