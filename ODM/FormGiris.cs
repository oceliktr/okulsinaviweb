using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using DAL;
using ODM.Kutuphanem;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace ODM
{
    public partial class FormGiris : Form
    {
        public class DiziSecenekler
        {
            public DiziSecenekler()
            { }

            public DiziSecenekler(int ogrenciId, int soruNo, string secenek, byte r, byte g, byte b, int x, int y, string dosya)
            {
                OgrenciId = ogrenciId;
                SoruNo = soruNo;
                Secenek = secenek;
                R = r;
                G = g;
                B = b;
                X = x;
                Y = y;
                Dosya = dosya;
            }

            public int OgrenciId { get; set; }
            public int SoruNo { get; set; }
            public string Secenek { get; set; }
            public byte R { get; set; }
            public byte G { get; set; }
            public byte B { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public string Dosya { get; set; }
        }

        private static readonly string ckDizin = IniIslemleri.VeriOku("CKDizinKontrol", "CKBulunduguAdres");
        private readonly string kareKodDizin = ckDizin + @"\KareKod\";
        private readonly string ocrDizin = ckDizin + @"\Ocr\";
        private readonly string cevapKagitlari = ckDizin + @"\CevapKagitlari\";
        private readonly string okunmayanlar = ckDizin + @"\Okunmadi\";
        private readonly string cevapDizin = ckDizin + @"\Cevaplar\";
        private readonly List<string> _aList = new List<string>();
        private static int _sinavId;
        private string _seciliDizin = "";

        readonly KonumlarDB knmDb = new KonumlarDB();
        public FormGiris()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo sinfo = snvDb.AktifSinavAdi();
            _sinavId = sinfo.SinavId;
            this.Height = 317;
            this.Width = 815;


            dgvHata.ColumnCount = 2;
            dgvHata.Columns[0].Name = "DosyaAdi";
            dgvHata.Columns[1].Name = "Hata";

        }
        /// <summary>
        /// Görüntüyü Otsu Threshold sınıfı ile siyah beyaz renge ceçirir
        /// </summary>
        /// <param name="bmpx">Resim dosyası</param>
        /// <returns></returns>
        private static Bitmap Siyahla(Bitmap bmpx)
        {
            OtsuThreshold otsuFiltre = new OtsuThreshold();
            Bitmap filtreliResim =
                otsuFiltre.Apply(bmpx.PixelFormat != PixelFormat.Format8bppIndexed
                    ? Grayscale.CommonAlgorithms.BT709.Apply(bmpx)
                    : bmpx);
            return filtreliResim;
        }
        private static void ResimKirp(string path, int width, int height, int x, int y, string kirpilanDosyaAdresi)
        {
            using (Bitmap absentRectangleImage = (Bitmap)Image.FromFile(path))
            {
                using (Bitmap currentTile = new Bitmap(width, height))
                {
                    currentTile.SetResolution(absentRectangleImage.HorizontalResolution,
                        absentRectangleImage.VerticalResolution);
                    using (Graphics currentTileGraphics = Graphics.FromImage(currentTile))
                    {
                        currentTileGraphics.Clear(Color.Black);
                        Rectangle absentRectangleArea = new Rectangle(x, y, width, height);
                        currentTileGraphics.DrawImage(absentRectangleImage, 0, 0, absentRectangleArea,
                            GraphicsUnit.Pixel);
                    }
                    currentTile.Save(kirpilanDosyaAdresi);
                }
            }
        }

        #region Dizin Seç Butonu

        private void pbDizinSec_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowNewFolderButton = false; //yeni klasör oluşturmayı kapat
                folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory; //başlangıç dizini
                folderDialog.Description = @"Kontrol edilecek sınav kağıdı evraklarının bulunduğu dizini seçiniz.";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    _seciliDizin = folderDialog.SelectedPath;
                    if (DizinIslemleri.DizinKontrol(ckDizin))
                    {
                        if (!DizinIslemleri.DizinKontrol(cevapKagitlari)) DizinIslemleri.DizinOlustur(cevapKagitlari); else DizinIslemleri.DizinIceriginiSil(cevapKagitlari);
                        //  if (!DizinIslemleri.DizinKontrol(ckYeniBoyutlar)) DizinIslemleri.DizinOlustur(ckYeniBoyutlar); else DizinIslemleri.DizinIceriginiSil(ckYeniBoyutlar);

                        bgwDizinSec.RunWorkerAsync();

                    }
                    else
                    {
                        MessageBox.Show(@"Öncelikle ayarlar bölümünden Cevap kağıtlarının tutulacağı dizini seçiniz.",
                            @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        FormAyarlar frm = new FormAyarlar();
                        frm.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// Dizin seç button işlemleri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwDizinSec_DoWork(object sender, DoWorkEventArgs e)
        {
            pbOptikOkuma.Enabled = pbDizinSec.Enabled = pbCKKontrol.Enabled = pbAuKirpma.Enabled = false;
            CKBoyutlandir();
            pbOptikOkuma.Enabled = pbDizinSec.Enabled = pbCKKontrol.Enabled = pbAuKirpma.Enabled = true;
        }
        private void CKBoyutlandir()
        {
            lblBilgi.Text = @"Cevap kağıtları düzleştirilip yeniden boyutlandırılıyor.";
            Application.DoEvents();

            List<DosyaInfo> dsy = DizinIslemleri.DizindekiDosyalariListele(_seciliDizin);

            int width = IniIslemleri.VeriOku("CKBoyut", "W").ToInt32();
            int height = IniIslemleri.VeriOku("CKBoyut", "H").ToInt32();

            int a = 0;
            progressBar1.Maximum = dsy.Count;
            foreach (DosyaInfo x in dsy)
            {
                a++;
                string ilkDizinAdresi = x.DosyaYolu + @"\" + x.DosyaAdi;
                lblBilgi.Text = @"Cevap kağıtları düzleştirilip yeniden boyutlandırılıyor. İşlem gören dosya: " + x.DosyaAdi;
                try
                {
                    EgimiDuzeltveBoyutlandir.Kaydet(ilkDizinAdresi, cevapKagitlari + x.DosyaAdi.Replace("(", "").Replace(")", ""), width, height);
                    progressBar1.Value = a;

                    //int yuzde = (int)((progressBar1.Value / (double)progressBar1.Maximum) * 100);
                    progressBar1.PerformStep();
                }
                catch (Exception ex)
                {
                    object[] row = { ilkDizinAdresi, ex.Message };
                    dgvHata.Rows.Add(row);
                    Application.DoEvents();
                }
            }
            lblBilgi.Text = @"Cevap kağıdı boyut kontrolü tamamlandı. Şimdi Karekod,Ocr ve varsa açık uclu cevapların konumlarını belirleyiniz. Ardından Cevap kağıdı kontrolünü yapınız.";
            Application.DoEvents();
            progressBar1.Value = 0;

        }
        #endregion
        #region Karekod Ocr Kontrol Butonu

        private void pbCKKontrol_Click(object sender, EventArgs e)
        {
            //////////////////////KareKodveOcrKirpmaIslemleri

            List<KonumlarInfo> knm = knmDb.KayitlariDiziyeGetir(_sinavId);
            List<KonumlarInfo> xsQr = (from cust in knm where cust.Grup == "karekod" select cust).ToList();
            List<KonumlarInfo> xsOcr = (from cust in knm where cust.Grup == "ocr" select cust).ToList();
            List<KonumlarInfo> xsGirmediKonumu = (from cust in knm where cust.Grup == "girmedi" select cust).ToList();
            int wQr = xsQr[0].W;
            int wOcr = xsOcr[0].W;

            if (wQr <= 1)
            {
                MessageBox.Show(
                    @"Karekod konum ayarı yapılmamış. Cevap kağıdı konum al menüsünden konum ayarını yapınız.",
                    @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormCkKonumAl frm = new FormCkKonumAl();
                frm.ShowDialog();
            }
            else if (wOcr <= 1)
            {
                MessageBox.Show(
                    @"OCR konum ayarı yapılmamış. Cevap kağıdı konum al menüsünden konum ayarını yapınız.",
                    @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormCkKonumAl frm = new FormCkKonumAl();
                frm.ShowDialog();
            }
            else if (xsGirmediKonumu.Count == 0)
            {
                MessageBox.Show(
                    @"Sınava girmedi konum ayarı yapılmamış. Cevap kağıdı konum al menüsünden konum ayarını yapınız.",
                    @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormCkKonumAl frm = new FormCkKonumAl();
                frm.ShowDialog();
            }
            else
            {
                //Dizin kontrol ve silme işlemleri.
                if (!DizinIslemleri.DizinKontrol(kareKodDizin)) DizinIslemleri.DizinOlustur(kareKodDizin);
                else DizinIslemleri.DizinIceriginiSil(kareKodDizin);
                if (!DizinIslemleri.DizinKontrol(ocrDizin)) DizinIslemleri.DizinOlustur(ocrDizin);
                else DizinIslemleri.DizinIceriginiSil(ocrDizin);
                lblBilgi.Text = @"KareKod/OCR işlemleri başladı.";

                bgwKarekodOcrKontrol.RunWorkerAsync();
            }
        }
        /// <summary>
        /// Karekod Ocr kontrol button işlemleri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwKarekodOcrKontrol_DoWork(object sender, DoWorkEventArgs e)
        {
            pbOptikOkuma.Enabled = pbDizinSec.Enabled = pbCKKontrol.Enabled = pbAuKirpma.Enabled = false;
            KarekodKirp();
            OcrKirp();

            CkkontrolDb veriDb = new CkkontrolDb();
            veriDb.KayitSil();

            OgrencilerDb ogDb = new OgrencilerDb();
            ogDb.CkKontroTemizle(_sinavId);

            List<DosyaInfo> cKagitlari = DizinIslemleri.DizindekiDosyalariListele(cevapKagitlari);
            progressBar1.Maximum = cKagitlari.Count;
            progressBar1.Value = 0;

            List<OgrenciKayit> ogKayits = new List<OgrenciKayit>();
            int a = 0;
            lblBilgi.Text = @"Karekod / OCR ile öğrenci bilgileri eşleştiriliyor.";
            Application.DoEvents();
            foreach (DosyaInfo ck in cKagitlari)
            {
                a++;
                string kareKoddosyaAdresi = kareKodDizin + ck.DosyaAdi;
                string ocrdosyaAdresi = ocrDizin + ck.DosyaAdi;

                if (DizinIslemleri.DosyaKontrol(kareKoddosyaAdresi))
                {
                    try
                    {
                        QRCodeDecoder decoder = new QRCodeDecoder();
                        string kareKod = decoder.decode(new QRCodeBitmapImage(new Bitmap(kareKoddosyaAdresi)));
                        string sayfaYuzu = kareKod.Substring(0, 1);
                        int ogrenciNo = kareKod.Substring(1, kareKod.Length - 1).ToInt32();

                        if (!sayfaYuzu.IsInteger())
                        {
                            sayfaYuzu = OcrOku(ocrdosyaAdresi, out ogrenciNo);
                        }
                        if (ogrenciNo == 0)
                        {
                            sayfaYuzu = OcrOku(ocrdosyaAdresi, out ogrenciNo);
                        }
                        ogKayits.Add(new OgrenciKayit(ogrenciNo, sayfaYuzu, kareKoddosyaAdresi));

                        progressBar1.Value = a;

                        progressBar1.PerformStep();
                    }
                    catch (Exception)
                    {
                        try
                        {
                            var sayfaYuzu = OcrOku(ocrdosyaAdresi, out var ogrenciNo);

                            ogKayits.Add(new OgrenciKayit(ogrenciNo, sayfaYuzu, kareKoddosyaAdresi));

                            progressBar1.Value = a;

                            progressBar1.PerformStep();
                        }
                        catch (Exception ex)
                        {
                            object[] row = { ocrdosyaAdresi, ex.Message };
                            dgvHata.Rows.Add(row);
                            _aList.Add(ck.DosyaAdi);
                            Application.DoEvents();
                        }
                    }
                }
                else
                {
                    object[] row = { kareKoddosyaAdresi, "Dosya Yok" };
                    dgvHata.Rows.Add(row);
                    Application.DoEvents();
                }
            }

            lblBilgi.Text = @"Eşleşmeler veritabanına kaydediliyor.";
            Application.DoEvents();
            a = 0;
            progressBar1.Maximum = ogKayits.Count;
            progressBar1.Value = 0;

            foreach (OgrenciKayit z in ogKayits)
            {

                progressBar1.Value = a;
                veriDb.KayitEkle(z.OgrenciNo, z.SayfaYuzu, z.DosyaAdi);
            }


            lblBilgi.Text = @"Veriler kontrol ediliyor.";
            Application.DoEvents();
            VeriKontroluYap();
            lblBilgi.Text = @"Cevap kağıdı olmayan öğrenci listesi hazırlanıyor.";
            Application.DoEvents();
            CkOlmayanOgrencileriGetir(ogDb);
            Application.DoEvents();

            Application.DoEvents();
            // Sınava girmeyenlerle ilgili işlemler 
            SinavaGirmeyenler();
            // Sınava girmeyenlerle ilgili işlemler bitti.

            lblBilgi.Text = "Cevap kağıdındaki kontroller tamamlandı.";
            pbOptikOkuma.Enabled = pbDizinSec.Enabled = pbCKKontrol.Enabled = pbAuKirpma.Enabled = true;

        }

        private void SinavaGirmeyenler()
        {
            List<DiziSecenekler> secenek = new List<DiziSecenekler>();
            CkkontrolDb ckDb = new CkkontrolDb();


            //Cevap kağıtlarının bulunduğu dizinden dosya bilgilerini diziye al
            List<DosyaInfo> cevapKagit = DizinIslemleri.DizindekiDosyalariListele(cevapKagitlari);
            List<KonumlarInfo> knm = knmDb.KayitlariDiziyeGetir(_sinavId);
            //konumlar tablosundaki konumlardan sınava girmeyenleri seç.
            List<KonumlarInfo> optikKonumlari = (from x in knm where x.Grup == "girmedi" orderby x.SoruNo ascending select x).ToList();

            //Progres bas için cevap kağıdı ve konum sayıını çarp
            int a = 0;
            progressBar1.Maximum = cevapKagit.Count;
            progressBar1.Value = 0;

            //Öğrenci listesini diziye alalım.
            OgrencilerDb ogrDb = new OgrencilerDb();
            List<OgrencilerInfo> ogrList = ogrDb.KayitlariDiziyeGetir(_sinavId);

            //Cevap kağıtlarının bulunduğu dizindeki dosyaları döngüye çağırır
            foreach (DosyaInfo ck in cevapKagit)
            {
                lblBilgi.Text = "Sınava girmedi işaretlenen cevap kağıtları kontrol ediliyor. Kontrol edilen dosya: " +
                                ck.DosyaAdi;
                string kareKodDosyaAdresi = kareKodDizin + ck.DosyaAdi;
                //öğrenci bilgileri için karekod dosya adlarından bilgi al.
                CkkontrolInfo ckInfo = ckDb.KayitBilgiGetir(kareKodDosyaAdresi);

                //optik okuma için görüntü işleme yaparak rengi siyah beyaza çevir.
                Bitmap bmap = Siyahla(new Bitmap(cevapKagitlari + ck.DosyaAdi));

                //Veritabanında bulunan optik seçenek konumlarını döngüye çağır
                foreach (KonumlarInfo x in optikKonumlari)
                {
                    a++;
                    for (int i = x.X1; i <= (x.X1 + x.W); i++)
                    {
                        for (int j = x.Y1; j <= (x.Y1 + x.H); j++)
                        {
                            Color piksel = bmap.GetPixel(i, j);
                            //RGB renk değerleri 0 (siyah) olanları seç 
                            if (piksel.R == 0 && piksel.G == 0 && piksel.B == 0)
                            {
                                //Sonraki döngüde OptikSonuc tablosuna kaydetmek için diziye al.
                                secenek.Add(new DiziSecenekler(ckInfo.OgrId, x.SoruNo, x.Secenek, piksel.R, piksel.G, piksel.B,i, j, ck.DosyaAdi));

                                progressBar1.Value = a;

                                progressBar1.PerformStep();
                            }
                        }
                    }
                    Application.DoEvents();
                }

                lblBilgi.Text = "Sınava girmeyenler veritabanına kaydediliyor";
                foreach (var ogr in ogrList)
                {
                    //girmeyenlerin konumlarını say. Diziye alınan siyah noktaları sayrak belirlenen sayıdan fala siyah nokta olanları db'ye kaydet.
                    List<DiziSecenekler> dsSoru =(from scnk in secenek where scnk.OgrenciId == ogr.OgrenciId && scnk.Dosya == ck.DosyaAdi select scnk).ToList();
                    //TODO:600 değeri herhangi bir seçenekteki siyahlanmış piksel sayısıdır. Ayarlardan çekilebilir.
                    try
                    {
                        if (dsSoru.Count > 500)
                        {
                            ogrDb.SinavaGirmedi(ogr.OgrenciId, ck.DosyaAdi);
                        }
                    }
                    catch (Exception)
                    {
                      //  MessageBox.Show(ogr.Id + "-" + ck.DosyaAdi + "-" + ex.Message);
                    }
                    Application.DoEvents();
                }
            }
            lblBilgi.Text = "Cevap kağıdındaki sınava girmeyenler işaretlendi.";

            Application.DoEvents();
            progressBar1.Value = 0;
        }

        private static string OcrOku(string ocrdosyaAdresi, out int ogrenciNo)
        {
            string kareKod = Ocr.OcrCevir(ocrdosyaAdresi, Ocr.Dil.Turkce);
            string sayfaYuzu = kareKod.Substring(0, 1);
            ogrenciNo = kareKod.Substring(1, kareKod.Length - 1).ToInt32();
            //ilk karekterden sonrası
            return sayfaYuzu;
        }

        private void KarekodKirp()
        {
            lblBilgi.Text = @"Karekod kırpma işlemleri yapılıyor.";
            Application.DoEvents();

            List<KonumlarInfo> knm = knmDb.KayitlariDiziyeGetir(_sinavId);
            List<KonumlarInfo> xsQr = (from cust in knm where cust.Grup == "karekod" select cust).ToList();
            int wQr = xsQr[0].W;
            int hQr = xsQr[0].H;
            int xQr = xsQr[0].X1;
            int yQr = xsQr[0].Y1;

            List<DosyaInfo> secPath = DizinIslemleri.DizindekiDosyalariListele(cevapKagitlari);
            progressBar1.Maximum = secPath.Count;
            int a = 0;
            foreach (DosyaInfo s in secPath)
            {
                a++;
                string dosyaAdresi = s.DosyaYolu + "\\" + s.DosyaAdi;

                ResimKirp(dosyaAdresi, wQr, hQr, xQr, yQr, kareKodDizin + s.DosyaAdi);

                progressBar1.Value = a;

                progressBar1.PerformStep();
            }
            Application.DoEvents();
        }
        private void OcrKirp()
        {
            lblBilgi.Text = @"OCR kırpma işlemleri yapılıyor.";
            Application.DoEvents();

            List<KonumlarInfo> knm = knmDb.KayitlariDiziyeGetir(_sinavId);
            List<KonumlarInfo> xsOcr = (from cust in knm where cust.Grup == "ocr" select cust).ToList();

            int wOcr = xsOcr[0].W;
            int hOcr = xsOcr[0].H;
            int xOcr = xsOcr[0].X1;
            int yOcr = xsOcr[0].Y1;

            List<DosyaInfo> secPath = DizinIslemleri.DizindekiDosyalariListele(cevapKagitlari);

            progressBar1.Maximum = secPath.Count;
            int a = 0;

            foreach (DosyaInfo s in secPath)
            {
                a++;
                string dosyaAdresi = s.DosyaYolu + "\\" + s.DosyaAdi;
                ResimKirp(dosyaAdresi, wOcr, hOcr, xOcr, yOcr, ocrDizin + s.DosyaAdi);
                progressBar1.Value = a;

                progressBar1.PerformStep();
            }
            Application.DoEvents();
        }
        private void VeriKontroluYap()
        {
            CkkontrolDb veriDb = new CkkontrolDb();

            List<CkkontrolInfo> aa = veriDb.KayitlariDiziyeGetir();

            int a = 0;
            progressBar1.Maximum = aa.Count;
            progressBar1.Value = 0;

            OgrencilerDb ogDb = new OgrencilerDb();
            try
            {

                foreach (CkkontrolInfo x in aa)
                {
                    a++;
                    progressBar1.Value = a;

                    OgrencilerInfo info = ogDb.KayitBilgiGetir(x.OgrId);
                    string zz = info.CKagitKontrol + x.SayfaYuzu;
                    ogDb.CKagitKontrol(x.OgrId, zz);
                }

                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Kontrol Hata: " + ex.Message);
            }
        }
        private void CkOlmayanOgrencileriGetir(OgrencilerDb ogDb)
        {
            List<KonumlarInfo> knm = knmDb.KayitlariDiziyeGetir(_sinavId);
            //dizideki açık uclu veya optik tanımlı grupları seç
            IEnumerable<KonumlarInfo> xs = from x in knm where x.Grup == "au" || x.Grup == "optik" select x;

            //açık uclu veya optik tanımlı gruplardan kaç sayfa yüzü olduğunu bul.
            var lstKrdx = (from k in xs orderby k.SyfYuzu ascending select k).GroupBy(i => i.SyfYuzu).Select(x => x.First());

            //cevap kağıdı olmayan öğrencileri sql orgususun oluştur.
            string sql = lstKrdx.Aggregate("", (current, azs) => current + (" or ogrenciler.CKagitKontrol not like '%" + azs.SyfYuzu + "%'"));

            dgvCKEksikOlanlar.DataSource = ogDb.EksikCKlariGetir(_sinavId, sql);

            lblBilgi.Text = ogDb.EksikCKlariGetir(_sinavId, sql).Rows.Count > 0
                ? @"Cevapları kırpma işleminden önce listelenen öğrenci bilgilerini güncelleyiniz."
                : @"İşlem tamamlandı. Cevap kağıdındaki tüm konumlar belirlendiyse şimdi cevapları kırp butonuna tıklayınız.";
            Application.DoEvents();
            this.Height = 593;
        }
        private void kareKodKontrolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvHata.ColumnCount = 3;
            dgvHata.Columns[0].Name = "DosyaAdi";
            dgvHata.Columns[1].Name = "KareKod";
            dgvHata.Columns[2].Name = "Hata";

            List<DosyaInfo> dsy = DizinIslemleri.DizindekiDosyalariListele(kareKodDizin);
            foreach (DosyaInfo ck in dsy)
            {
                string kareKoddosyaAdresi = kareKodDizin + ck.DosyaAdi;
                try
                {
                    QRCodeDecoder decoder = new QRCodeDecoder();
                    string kareKod = decoder.decode(new QRCodeBitmapImage(new Bitmap(kareKoddosyaAdresi)));


                    object[] row = { kareKoddosyaAdresi, kareKod, "" };
                    dgvHata.Rows.Add(row);
                }
                catch (Exception ex)
                {

                    object[] row = { kareKoddosyaAdresi, "", ex.Message };
                    dgvHata.Rows.Add(row);
                }

            }
        }
        #endregion
        #region Cevapları Kırp Butonu

        private void pbAuKirpma_Click(object sender, EventArgs e)
        {
            if (!DizinIslemleri.DizinKontrol(cevapKagitlari))
                lblBilgi.Text = @"Cevap kağıtlarının bulunduğu dizin bulunamadı. Ayarlardan Cevap kağıtlarının tutulacağı dizini seçiniz.";
            else if (DizinIslemleri.DizindekiDosyalariListele(cevapKagitlari).Count < 1)
                lblBilgi.Text = @"Cevap kağıtlarının bulunduğu dizinde dosya bulunamadı.";
            else
            {
                if (!DizinIslemleri.DizinKontrol(cevapDizin)) DizinIslemleri.DizinOlustur(cevapDizin); else DizinIslemleri.DizinIceriginiSil(cevapDizin);

                bgwCKirp.RunWorkerAsync();
            }
        }
        private void bgwCKirp_DoWork(object sender, DoWorkEventArgs e)
        {
            pbOptikOkuma.Enabled = pbDizinSec.Enabled = pbCKKontrol.Enabled = pbAuKirpma.Enabled = false;
            CkkontrolDb ckDb = new CkkontrolDb();
            if (ckDb.KayitSayisi() == 0)
            {
                lblBilgi.Text = @"Karekod / OCR kontrol işlemleri henüz yapılmamış. Önce Karekod / OCR kontrolü yapınız.";
                MessageBox.Show(lblBilgi.Text, @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                OgrencilerDb ogrDb = new OgrencilerDb();
                List<DosyaInfo> cKagitlari = DizinIslemleri.DizindekiDosyalariListele(cevapKagitlari);
                List<OgrencilerInfo> sinavaGirmeyenlerList = ogrDb.SinavaGirmeyenleriDiziyeGetir(_sinavId);

                progressBar1.Maximum = cKagitlari.Count;
                progressBar1.Value = 0;

                CevaplarDb cvp = new CevaplarDb();
                cvp.CevaplariSil(_sinavId);
                CevaplarInfo info = new CevaplarInfo();
                int a = 0;

                lblBilgi.Text = @"Cevap kağıtlarını kırpma işlemi başladı.";
                Application.DoEvents();
                foreach (DosyaInfo ck in cKagitlari)
                {
                    a++;

                    string dosyaAdresi = ck.DosyaYolu + @"\" + ck.DosyaAdi;
                    string kareKodDosyaAdresi = kareKodDizin + ck.DosyaAdi;

                    try
                    {
                        CkkontrolInfo ckInfo = ckDb.KayitBilgiGetir(kareKodDosyaAdresi);

                        string sayfaYuzu = ckInfo.SayfaYuzu;
                        int ogrenciNo = ckInfo.OgrId;

                        List<OgrencilerInfo> girmeyenKagit = (from x in sinavaGirmeyenlerList where x.OgrenciId == ogrenciNo select x).ToList();

                        if (girmeyenKagit.Count == 0) //girmeyenKagit.Count == 0 ise sınava girmiştir.
                        {
                            List<KonumlarInfo> knm = knmDb.KayitlariDiziyeGetir(_sinavId);
                            IEnumerable<KonumlarInfo> xs = from cust in knm where cust.SyfYuzu == sayfaYuzu.ToInt32() && cust.Grup == "au" select cust;

                            foreach (KonumlarInfo ak in xs)
                            {
                                string rndMetin = GenelIslemler.RastgeleMetinUret(8);
                                string uzanti = Path.GetExtension(dosyaAdresi);
                                string kirpilacakCevapAdresi = string.Format("{0}_{1}_{2}{3}", ak.SoruNo, ogrenciNo, rndMetin, uzanti);
                                string kirpilanDosyaAdresi = cevapDizin + kirpilacakCevapAdresi;
                                info.SoruNo = ak.SoruNo.ToInt32();
                                info.Dosya = kirpilacakCevapAdresi;
                                info.OgrenciId = ogrenciNo;
                                info.SinavId = ak.SinavId.ToInt32();
                                info.BransId = ak.BransId.ToInt32();

                                cvp.KayitEkle(info);

                                ResimKirp(dosyaAdresi, ak.W.ToInt32(), ak.H.ToInt32(), ak.X1.ToInt32(), ak.Y1.ToInt32(), kirpilanDosyaAdresi);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        object[] row = { dosyaAdresi, ex.Message };
                        dgvHata.Rows.Add(row);
                        Application.DoEvents();
                    }
                    progressBar1.Value = a;

                    progressBar1.PerformStep();
                }
                progressBar1.Value = 0;
                lblBilgi.Text = @"Cevap kağıtlarını kırpma işlemi tamamlandı. Kırpılan dosyaları ve databasede cevaplar tablosunu web sitesine yükleyebilirsiniz.";
                Application.DoEvents();
                DialogResult dialog = MessageBox.Show(lblBilgi.Text + "\n" + @"Şimdi cevapların kırpıldığı dizini açmak ister misiniz?", @"Bilgi", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) Process.Start("explorer.exe", Path.GetDirectoryName(cevapDizin));
            }

            pbOptikOkuma.Enabled = pbDizinSec.Enabled = pbCKKontrol.Enabled = pbAuKirpma.Enabled = true;
        }
        #endregion
        #region Optik Okuma İşlemleri

        private void pbOptikOkuma_Click(object sender, EventArgs e)
        {
            CkkontrolDb ckDb = new CkkontrolDb();
            if (ckDb.KayitSayisi() == 0)
            {
                lblBilgi.Text = @"Karekod / OCR kontrol işlemleri henüz yapılmamış. Önce Karekod / OCR kontrolü yapınız.";
                MessageBox.Show(lblBilgi.Text, @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bgwOptikOkuma.RunWorkerAsync();
            }
        }

        private void bgwOptikOkuma_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DiziSecenekler> secenek = new List<DiziSecenekler>();
            CkkontrolDb ckDb = new CkkontrolDb();

            pbOptikOkuma.Enabled = pbDizinSec.Enabled = pbCKKontrol.Enabled = pbAuKirpma.Enabled = false;

            //Cevap kağıtlarının bulunduğu dizinden dosya bilgilerini diziye al
            List<DosyaInfo> cevapKagit = DizinIslemleri.DizindekiDosyalariListele(cevapKagitlari);
            List<KonumlarInfo> knm = knmDb.KayitlariDiziyeGetir(_sinavId);
            //konumlar tablosundaki konumlardan sadece optik olanları seç.
            List<KonumlarInfo> optikKonumlari =
                (from x in knm where x.Grup == "optik" orderby x.SoruNo ascending select x).ToList();

            //Progres bas için cevap kağıdı ve konum sayıını çarp
            int ckveKonumSayisi = cevapKagit.Count * optikKonumlari.Count;
            int a = 0;
            progressBar1.Maximum = ckveKonumSayisi;
            progressBar1.Value = 0;

            //bu sınava ait eski optik sonuçlarını sil
            OptikSonucDB osDb = new OptikSonucDB();
            osDb.KayitSil(_sinavId, true); //true false farketmez


            //Soru sayısını tespit için sadece a seçeneğini seçerek soruları listele.
            List<KonumlarInfo> sorular =
                (from x in knm where x.Grup == "optik" && x.Secenek == "A" orderby x.SoruNo select x).ToList();
            OgrencilerDb ogrDb = new OgrencilerDb();
            List<OgrencilerInfo> ogrList = ogrDb.KayitlariDiziyeGetir(_sinavId);

            //Girmedi alanı 0 olan yani sınava giren Öğrencilere konumlar tablosundaki soru sayısınca kayıt ekleyelim.
            foreach (var ogr in ogrList.Where(x=>x.Girmedi==0))
            {
                foreach (var soru in sorular)
                {
                    OptikSonucInfo osInfo = new OptikSonucInfo
                    {
                        BransId = soru.BransId,
                        OgrenciId = ogr.OgrenciId,
                        KurumKodu = ogr.KurumKodu,
                        Secenek = "",
                        SoruNo = soru.SoruNo,
                        SinavId = _sinavId
                    };
                    osDb.KayitEkle(osInfo);
                }
            }


            Application.DoEvents();

            //Sınava girmeyenleri okummamak için önce diziye alalım.
            List<OgrencilerInfo> sinavaGirmeyenlerList = ogrDb.SinavaGirmeyenleriDiziyeGetir(_sinavId);

            //Cevap kağıtlarının bulunduğu dizindeki dosyaları döngüye çağırır
            foreach (DosyaInfo ck in cevapKagit)
            {


                lblBilgi.Text = "Cevap kağıdındaki optikler okunuyor. Okunan dosya: " + ck.DosyaAdi;
                string kareKodDosyaAdresi = kareKodDizin + ck.DosyaAdi;
                //öğrenci bilgileri için karekod dosya adlarından bilgi al.
                CkkontrolInfo ckInfo = ckDb.KayitBilgiGetir(kareKodDosyaAdresi);

                List<OgrencilerInfo> girmeyenKagit = (from x in sinavaGirmeyenlerList where x.OgrenciId == ckInfo.OgrId select x).ToList();

                if (girmeyenKagit.Count == 0)
                {

                    //optik okuma için görüntü işleme yaparak rengi siyah beyaza çevir.
                    Bitmap bmap = Siyahla(new Bitmap(cevapKagitlari + ck.DosyaAdi));

                    //Veritabanında bulunan optik seçenek konumlarını döngüye çağır
                    foreach (KonumlarInfo x in optikKonumlari)
                    {
                        a++;
                        for (int i = x.X1; i <= (x.X1 + x.W); i++)
                        {
                            for (int j = x.Y1; j <= (x.Y1 + x.H); j++)
                            {
                                Color piksel = bmap.GetPixel(i, j);
                                //RGB renk değerleri 0 (siyah) olanları seç 
                                if (piksel.R == 0 && piksel.G == 0 && piksel.B == 0)
                                {
                                    //Sonraki döngüde OptikSonuc tablosuna kaydetmek için diziye al.
                                    secenek.Add(new DiziSecenekler(ckInfo.OgrId, x.SoruNo, x.Secenek, piksel.R, piksel.G,
                                        piksel.B, i, j, ck.DosyaAdi));

                                    progressBar1.Value = a;

                                    progressBar1.PerformStep();
                                }
                            }
                        }
                        Application.DoEvents();
                    }

                    //optik konumları say. Diziye alınan siyah noktaları sayrak belirlenen sayıdan fala siyah nokta olanları db'ye kaydet.
                    foreach (KonumlarInfo ok in optikKonumlari)
                    {
                        List<DiziSecenekler> dsSoru = (from scnk in secenek
                                                       where scnk.SoruNo == ok.SoruNo && scnk.Secenek == ok.Secenek && scnk.Dosya == ck.DosyaAdi
                                                       select scnk).ToList();
                        //TODO:600 değeri herhangi bir seçenekteki siyahlanmış piksel sayısıdır. Ayarlardan çekilebilir.
                        if (dsSoru.Count > 500)
                        {
                            OptikSonucInfo os = osDb.KayitBilgiGetir(_sinavId, ok.BransId, ckInfo.OgrId, ok.SoruNo);
                            string ogrenciSecenek = os.Secenek + ok.Secenek;
                            //Seçenek 1 ise konumlar tablosundaki puanı yazılır.(Doğru ise doğru puanı yanlış ise yanlış puanı 0) 
                            //Seçenek 1 karekter değil ise boş veya birden fazla cevaptır. Puan yine 0 olur
                            int puani = ogrenciSecenek.Length == 1 ? ok.SoruPuani : 0;

                            osDb.KayitGuncelle(ogrenciSecenek, puani, os.Id);
                        }
                        Application.DoEvents();
                    }
                }
            }

            lblBilgi.Text = "Cevap kağıdındaki optikleri okuma tamamlandı.";

            Application.DoEvents();
            progressBar1.Value = 0;
            pbOptikOkuma.Enabled = pbDizinSec.Enabled = pbCKKontrol.Enabled = pbAuKirpma.Enabled = true;
        }

        #endregion
        #region contextMenuStrip1

        private void okunmayanlariTasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DizinIslemleri.DizinKontrol(okunmayanlar))
                DizinIslemleri.DizinOlustur(okunmayanlar);
            else
                DizinIslemleri.DizinIceriginiSil(okunmayanlar);

            foreach (string file in _aList)
            {
                File.Move(kareKodDizin + file, okunmayanlar + file);
            }
            MessageBox.Show("Dokunamayan dosyalar taşındı");
            DialogResult dialog =
                MessageBox.Show(
                    @"Dokunamayan dosyalar taşındı. Taşınan dosyaların bulunduğu dizini açmak ister misiniz?", @"Bilgi",
                    MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
                Process.Start("explorer.exe", Path.GetDirectoryName(okunmayanlar));


        }
        #endregion
        #region Formu Taşıma İşlemleri
        int Mov;
        int mx;
        int my;
        private void FormGiris_MouseDown(object sender, MouseEventArgs e)
        {
            Mov = 1;
            mx = e.X;
            my = e.Y;
            Cursor.Current = Cursors.SizeAll;
        }
        private void FormGiris_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mov == 1)
                SetDesktopLocation(MousePosition.X - mx, MousePosition.Y - my);
        }
        private void FormGiris_MouseUp(object sender, MouseEventArgs e)
        {
            Mov = 0;
            Cursor.Current = Cursors.Default;
        }
        private void lblIlAdi_MouseDown(object sender, MouseEventArgs e)
        {
            Mov = 1;
            mx = e.X;
            my = e.Y;
            Cursor.Current = Cursors.SizeAll;
        }
        private void lblIlAdi_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mov == 1)
                SetDesktopLocation(MousePosition.X - mx - lblIlAdi.Left, MousePosition.Y - my - lblIlAdi.Top);

        }
        private void lblIlAdi_MouseUp(object sender, MouseEventArgs e)
        {
            Mov = 0;
            Cursor.Current = Cursors.Default;
        }
        private void lblOdm_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mov == 1)
                SetDesktopLocation((MousePosition.X - mx) - lblOdm.Left, (MousePosition.Y - my) - lblOdm.Top);
        }
        private void lblOdm_MouseDown(object sender, MouseEventArgs e)
        {
            Mov = 1;
            mx = e.X;
            my = e.Y;
            Cursor.Current = Cursors.SizeAll;
        }
        private void lblOdm_MouseUp(object sender, MouseEventArgs e)
        {
            Mov = 0;
            Cursor.Current = Cursors.Default;
        }
        #endregion

        private void FormCKIslemleri_Activated(object sender, EventArgs e)
        {

            lblIlAdi.Text = IniIslemleri.VeriOku("Baslik", "IlAdi");
            this.Text = lblIlAdi.Text.IlkHarfleriBuyut() + " " + lblOdm.Text;
        }
        private void FormGiris_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.None && e.KeyCode == Keys.Escape)
            {
                DialogResult dialog = MessageBox.Show(@"Programdan çıkış yapmak istediğinizden emin misiniz?", @"Bilgi", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) Close();
            }
        }
        private void pbCKKonumAl_Click(object sender, EventArgs e)
        {
            FormCkKonumAl frm = new FormCkKonumAl();
            frm.ShowDialog();
        }
        private void pbAyarlar_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(pbAyarlar.Left + this.Left, pbAyarlar.Top + this.Top);
        }
        private void pbCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAyarlar frm = new FormAyarlar();
            frm.ShowDialog();
        }

        private void karekodOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormKareKodOlustur frm = new FormKareKodOlustur();
            frm.ShowDialog();
        }

        private void karekodKontrolToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormKarekodKontrol frm = new FormKarekodKontrol();
            frm.ShowDialog();
        }

        private void excelİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExcel frm = new FormExcel();
            frm.ShowDialog();
        }

        private void cKOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCKHazirla frm = new FormCKHazirla();
            frm.ShowDialog();
        }

        private void sınıfListesiOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSinifListesi frm= new FormSinifListesi();
            frm.ShowDialog();
        }
    }
}
