using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using ODM.Kutuphanem;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace ODM
{
    public partial class FormGiris : Form
    {

        private static readonly string ckDizin = IniIslemleri.VeriOku("CKDizinKontrol", "CKBulunduguAdres");
        private readonly string ocrDizin = ckDizin + @"\Ocr\";
        private readonly string cevapKagitlari = ckDizin + @"\CevapKagitlari\";
        private readonly string cevapDizin = ckDizin + @"\Cevaplar\";
        private static int sinavId;
        private string _seciliDizin = "";
        private int ikinciyogunluk = 650;
        private int yogunluk = 900;
        private int kitapikinciyogunluk = 1050;
        private int kitapyogunluk = 1500;
        private readonly string kitapcikTurleri;
        public FormGiris()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo sinfo = snvDb.AktifSinavAdi();
            sinavId = sinfo.SinavId;
            kitapcikTurleri = sinfo.KitapcikTurleri;

        }


        #region Dizin Seç Butonu
        private void pbDizinSec_Click(object sender, EventArgs e)
        {
            CkDosyalarDB dsyDb = new CkDosyalarDB();
            List<CkDosyalarInfo> ckDosyalar = dsyDb.KayitlariDiziyeGetir(sinavId);
            int dsyAdet = (from d in ckDosyalar where d.Boyutlandir == 0 select d).Count();

            if (dsyAdet > 0)
            {
                DialogResult dialog = MessageBox.Show(@"Daha önceden tamamlanmamış işlem bulunmaktadır. " + "\n" + "Tamamlamak için Evet, " + "\n" + "Yeniden başlamak için Hayır, " + "\n" + "Vazgeçmek için İptal butonuna tıklayın.", @"Bilgi", MessageBoxButtons.YesNoCancel);
                if (dialog == DialogResult.Yes)
                {
                    // Eğer kalan işlemin tamamlanmasını istiyorsa 
                    bgwDizinSec.RunWorkerAsync();
                }
                else if (dialog == DialogResult.No)
                {
                    DizinSecmeIslemleri(dsyDb);
                }
            }
            else if (dsyAdet == 0)
            {
                DialogResult dialog = MessageBox.Show(@"Tamamlanacak işlem yok yeni bir dizin seçmek ister misiniz?", @"Bilgi", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    DizinSecmeIslemleri(dsyDb);
                }
            }
            else
            {
                DizinSecmeIslemleri(dsyDb);
            }
        }

        private void DizinSecmeIslemleri(CkDosyalarDB dsyDb)
        {
            //Cevap kağıtlarının tutulacağı dizinin varlığını kontrol et.
            if (DizinIslemleri.DizinKontrol(ckDizin))
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

                        //Hafızada olan eski dosyaları silelim.
                        dsyDb.KayitSil(sinavId);

                        List<DosyaInfo> dsy = DizinIslemleri.DizindekiDosyalariListele(_seciliDizin);
                        //Dizindeki dosyaları hafızaya al.

                        progressBar1.Maximum = dsy.Count;
                        progressBar1.Value = 0;
                        lblBilgi.Text = "Cevap kağıtları hafızaya alınıyor";
                        foreach (DosyaInfo x in dsy)
                        {
                            string kalsor = x.DizinAdresi.Replace(_seciliDizin, "").Replace(@"\", "");
                            string yeniDosyaAdresi = kalsor + "_" + x.DosyaAdi;

                            string uzanti = Path.GetExtension(x.DosyaAdi);
                            if (uzanti != null && GenelIslemler.YuklenecekResimler.Contains(uzanti))
                            {
                                dsyDb.KayitEkle(x.DizinAdresi + @"\" + x.DosyaAdi, yeniDosyaAdresi, sinavId);
                            }
                            progressBar1.PerformStep();
                        }
                        Application.DoEvents();
                        progressBar1.Value = 0;
                        lblBilgi.Text = "Cevap kağıtları hafızaya alındı";

                        //Cevap kağıtlarının tutulacağı dizin varsa içeriğini sil yoksa oluştur.
                        if (!DizinIslemleri.DizinKontrol(cevapKagitlari))
                            DizinIslemleri.DizinOlustur(cevapKagitlari);
                        else
                            DizinIslemleri.DizinIceriginiSil(cevapKagitlari);

                        bgwDizinSec.RunWorkerAsync();
                    }
                }
            }
            else
            {
                MessageBox.Show(@"Öncelikle ayarlar bölümünden Cevap kağıtlarının tutulacağı dizini seçiniz.", @"Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormAyarlar frm = new FormAyarlar();
                frm.ShowDialog();
            }
        }
        /// <summary>
        /// Dizin seç button işlemleri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwDizinSec_DoWork(object sender, DoWorkEventArgs e)
        {
            pbDizinSec.Enabled = pbCKKontrol.Enabled = false;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            lblBilgi.Text = @"Cevap kağıtları düzleştirilip yeniden boyutlandırılıyor.";
            Application.DoEvents();

            int width = IniIslemleri.VeriOku("CKBoyut", "W").ToInt32();
            int height = IniIslemleri.VeriOku("CKBoyut", "H").ToInt32();

            CkDosyalarDB dsyDb = new CkDosyalarDB();
            List<CkDosyalarInfo> dsyList = dsyDb.KayitlariDiziyeGetir(sinavId);
            int a = 0;
            int islemSayisi = dsyList.Count(x => x.Boyutlandir == 0);
            progressBar1.Maximum =islemSayisi ;
            progressBar1.Value = 0;
            // Henüz işlem görmemiş dosyaları kontrol et.
            foreach (CkDosyalarInfo x in dsyList.Where(x => x.Boyutlandir == 0))
            {
                a++;
                string sonDosyaAdresi = cevapKagitlari + x.DosyaAdi;

                lblBilgi.Text = @"Cevap kağıtları düzleştirilip yeniden boyutlandırılıyor. İşlem gören dosya: " + x.DosyaAdi;
                try
                {
                    EgimiDuzeltveBoyutlandir.Kaydet(x.DizinAdresi, sonDosyaAdresi, width, height);

                    dsyDb.BoyutlandirmaIslemiGordu(x.DizinAdresi, x.DosyaAdi);
                }
                catch (Exception)
                {
                    //
                }
                progressBar1.PerformStep();
            lblGecenSure.Text = String.Format("{0} saat, {1} dakika, {2} saniye", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);

                try
                {
                    int gecenDakika = watch.Elapsed.TotalMinutes.ToInt32();
                    int islem = islemSayisi * gecenDakika;
                    double kalanSure = (double)islem / a;

                    double saat = (kalanSure - (kalanSure % 60)) / 60;
                    double dakika = kalanSure % 60;
                    lblBitisSuresi.Text = string.Format("Kalan tahmini süre : {0:0} saat, {1:0} dakika", saat, dakika);
                }
                catch (Exception)
                {
                    lblBitisSuresi.Text = "Hesaplanıyor...";
                }
            }

            lblBilgi.Text = @"Cevap kağıdı boyut kontrolü tamamlandı. Şimdi 'CK Konum Al' butonundan cevap kağıdına ait konumları belirleyiniz. Ardından Cevap kağıdı kontrolünü yapınız.";
            
            progressBar1.Value = 0;
            Application.DoEvents();
            watch.Stop();

            pbDizinSec.Enabled = pbCKKontrol.Enabled = true;

            //İşaretliyse CK Kontrol butonunu çalıştır.
            if (cbCkKontrolDevamEt.Checked)
                pbCKKontrol_Click(sender, e);
        }
        #endregion

        #region CK KONTROL BUTONU
        private void pbCKKontrol_Click(object sender, EventArgs e)
        {
            KonumlarDB knmDb = new KonumlarDB();

            List<KonumlarInfo> knm = knmDb.KayitlariDiziyeGetir(sinavId);
            List<KonumlarInfo> xsQr = (from cust in knm where cust.Grup == "karekod" select cust).ToList();
            List<KonumlarInfo> xsOcr = (from cust in knm where cust.Grup == "ocr" select cust).ToList();
            List<KonumlarInfo> xsGirmediKonumu = (from cust in knm where cust.Grup == "girmedi" select cust).ToList();
            
            if (xsQr.Count < 1)
            {
                MessageBox.Show(@"Karekod konum ayarı yapılmamış. Cevap kağıdı konum al menüsünden konum ayarını yapınız.", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormCkKonumAl frm = new FormCkKonumAl();
                frm.ShowDialog();
            }
            else if (xsOcr.Count < 1)
            {
                MessageBox.Show(@"OCR konum ayarı yapılmamış. Cevap kağıdı konum al menüsünden konum ayarını yapınız.", @"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormCkKonumAl frm = new FormCkKonumAl();
                frm.ShowDialog();
            }
            else if (xsGirmediKonumu.Count == 0)
            {
                MessageBox.Show("Sınava girmedi konum ayarı yapılmamış. Cevap kağıdı konum al menüsünden konum ayarını yapınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormCkKonumAl frm = new FormCkKonumAl();
                frm.ShowDialog();
            }
            else
            {
                CkDosyalarDB dsyDb = new CkDosyalarDB();
                List<CkDosyalarInfo> ckDosyalar = dsyDb.KayitlariDiziyeGetir(sinavId);
                int ckKontrolYapilmadi = (from d in ckDosyalar where d.CkKontrol == 0 select d).Count();
                int ckKontrolYapildi = (from d in ckDosyalar where d.CkKontrol == 1 select d).Count();

                if (ckKontrolYapilmadi > 0 && ckKontrolYapildi > 0)
                {
                    DialogResult dialog = MessageBox.Show(@"Daha önceden tamamlanmamış işlem bulunmaktadır."+"\n"+ "Tamamlamak için Evet, " + "\n" + "Yeniden başlamak için Hayır, " + "\n" + "Vazgeçmek için İptal butonuna tıklayın.", @"Bilgi", MessageBoxButtons.YesNoCancel);
                    if (dialog == DialogResult.Yes)
                    {
                        // Eğer kalan işlemin tamamlanmasını istiyorsa 
                        bgvCkKontrol.RunWorkerAsync();
                    }
                    else if (dialog == DialogResult.No)
                    {
                        CkSonuclariTemizle(dsyDb);
                    }
                }
                else if (ckKontrolYapilmadi == 0 && ckKontrolYapildi > 0)
                {
                    DialogResult dialog = MessageBox.Show(@"Kontrol edilecek cevap kağıdı bulunamadı. Mevcut kayıtları iptal edip yeniden kontrol edilsin mi?", @"Bilgi", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        CkSonuclariTemizle(dsyDb);
                    }
                }
                else
                {
                    CkSonuclariTemizle(dsyDb);
                }
            }
        }

        private void CkSonuclariTemizle(CkDosyalarDB dsyDb)
        {
            dsyDb.CkKontrolIslemiTemizle(sinavId);
            SonucAuDB auDb = new SonucAuDB();
            auDb.CevaplariSil(sinavId);
            SonucOptikDB optikDb = new SonucOptikDB();
            optikDb.KayitSil(sinavId);
            bgvCkKontrol.RunWorkerAsync();
        }
        private void bgvCkKontrol_DoWork(object sender, DoWorkEventArgs e)
        {
            //Çalışma süresini hesaplamak için.
            Stopwatch watch = new Stopwatch();
            watch.Start();

            pbDizinSec.Enabled = pbCKKontrol.Enabled = false;

            CkDosyalarDB ckDb = new CkDosyalarDB();
            List<CkDosyalarInfo> ckList = ckDb.KayitlariDiziyeGetir(sinavId).Where(x => x.CkKontrol == 0).ToList();
            int ckSayisi = ckList.Count;

            KonumlarDB knmDb = new KonumlarDB();
            List<KonumlarInfo> knm = knmDb.KayitlariDiziyeGetir(sinavId);

            List<KonumlarInfo> xsQr = (from cust in knm where cust.Grup == "karekod" select cust).ToList();
            int wQr = xsQr[0].W;
            int hQr = xsQr[0].H;
            int xQr = xsQr[0].X1;
            int yQr = xsQr[0].Y1;

            List<KonumlarInfo> xsOcr = (from cust in knm where cust.Grup == "ocr" select cust).ToList();
            int wOcr = xsOcr[0].W;
            int hOcr = xsOcr[0].H;
            int xOcr = xsOcr[0].X1;
            int yOcr = xsOcr[0].Y1;

            List<KonumlarInfo> girmediKonum = (from cust in knm where cust.Grup == "girmedi" select cust).ToList();

            progressBar1.Maximum = ckSayisi;
            progressBar1.Value = 0;
            int a = 0;
            foreach (CkDosyalarInfo ck in ckList)
            {
                a++;
                lblBilgi.Text = string.Concat("Cevap kağıtları kontrol ediliyor. (", a, "/", ckSayisi, ") Kontrol edilen dosya: ", ck.DosyaAdi);

                if (DizinIslemleri.DosyaKontrol(cevapKagitlari + ck.DosyaAdi))
                {
                    string dosyaAdresi = cevapKagitlari + ck.DosyaAdi;
                    string ocrdosyaAdresi = ocrDizin + ck.DosyaAdi;

                    if (DizinIslemleri.DosyaKontrol(dosyaAdresi))
                        ImageProcessing.ResimKirp(dosyaAdresi, wOcr, hOcr, xOcr, yOcr, ocrdosyaAdresi);

                    int ckOgrenciNo = 0;
                    int ckOturumNo = 0;
                    int girmedi = 0;
                    string ckKitapcikTuru = "";

                    #region KAREKOD OCR İŞLEMLERİ

                    try
                    {
                        Bitmap qrCopyImage = ImageProcessing.CropBitmap(new Bitmap(dosyaAdresi), xQr, yQr, wQr, hQr);
                        QRCodeDecoder decoder = new QRCodeDecoder();
                        string kareKod = decoder.decode(new QRCodeBitmapImage(qrCopyImage));
                       
                        ckOturumNo = kareKod.Substring(0, 1).ToInt32();
                        ckOgrenciNo = kareKod.Substring(1, kareKod.Length - 1).ToInt32();

                        if (!ckOturumNo.IsInteger())
                        {
                            ckOturumNo = Ocr.OcrOku(ocrdosyaAdresi, out ckOgrenciNo).ToInt32();
                        }
                        if (ckOgrenciNo == 0)
                        {
                            ckOturumNo = Ocr.OcrOku(ocrdosyaAdresi, out ckOgrenciNo).ToInt32();
                        }
                        Application.DoEvents();
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ckOturumNo = Ocr.OcrOku(ocrdosyaAdresi, out ckOgrenciNo).ToInt32();
                        }
                        catch (Exception)
                        {
                            //
                        }
                        Application.DoEvents();
                    }
                    ckDb.KayitGuncelle(ck.DizinAdresi, ck.DosyaAdi, ckOturumNo, ckOgrenciNo);
                    Application.DoEvents();

                    #endregion

                    //Karekod ve ocr okuma işlemlerinde bir problem yoksa (öğrenci ve oturum numarası almışsa)
                    if (ckOgrenciNo != 0 && ckOturumNo != 0)
                    {
                        //optik okuma için görüntü işleme yaparak rengi siyah beyaza çevir.
                        Bitmap bmap = ImageProcessing.Blacken(new Bitmap(cevapKagitlari + ck.DosyaAdi));

                        #region SINAVA GİRMEDİ OKUMA İŞLEMLERİ
                        //Girmedi seçeneğini işaretleyenleri alacak olan dizi
                        List<DiziSecenekler> secenekGirmedi = new List<DiziSecenekler>();

                        //Veritabanında bulunan optik seçenek konumlarını döngüye çağır
                        foreach (KonumlarInfo x in girmediKonum)
                        {
                            for (int i = x.X1; i <= (x.X1 + x.W); i++)
                            {
                                for (int j = x.Y1; j <= (x.Y1 + x.H); j++)
                                {
                                    Color piksel = bmap.GetPixel(i, j);
                                    //RGB renk değerleri 0 (siyah) olanları seç 
                                    if (piksel.R == 0 && piksel.G == 0 && piksel.B == 0)
                                    {
                                        //Sonraki döngüde ckDosyalar tablosuna kaydetmek için diziye al.
                                        secenekGirmedi.Add(new DiziSecenekler(x.Oturum, x.BransId, ck.OgrenciId,x.SoruNo, x.Secenek, piksel.R, piksel.G, piksel.B, i, j, ck.DosyaAdi));
                                    }
                                }
                            }
                            Application.DoEvents();
                        }

                        //girmeyenlerin konumlarını say. Diziye alınan siyah noktaları sayrak belirlenen sayıdan fala siyah nokta olanları db'ye kaydet.
                        List<DiziSecenekler> dsSoru = (from scnk in secenekGirmedi select scnk).ToList();
                      
                        if (dsSoru.Count > kitapyogunluk)
                        {
                            ckDb.SinavaGirmedi(ckOgrenciNo, ckOturumNo);
                            girmedi = 1;
                        }
                        Application.DoEvents();
                        #endregion

                        #region KİTAPÇIK TÜRÜ BELİRLEME İŞLEMLERİ
                        if (girmedi == 0)
                        {
                            List<string> ktpcikList = new List<string>();
                            for (int i = 0; i < kitapcikTurleri.Length; i++)
                            {
                                ktpcikList.Add(kitapcikTurleri.Substring(i, 1));
                            }
                            Application.DoEvents();

                            List<KitapcikTuru> ktpList = new List<KitapcikTuru>();

                            //Kitapçık türlerine göre siyah noktaları say ve diye al.
                            foreach (string kTur in ktpcikList)
                            {
                                //konumlar tablosundaki döngüdeki kitapçık türüne göre .
                                List<KonumlarInfo> kTurKonumu = (from x in knm where x.Grup == kTur select x).ToList();

                                foreach (KonumlarInfo x in kTurKonumu)
                                {
                                    for (int i = x.X1; i <= (x.X1 + x.W); i++)
                                    {
                                        for (int j = x.Y1; j <= (x.Y1 + x.H); j++)
                                        {
                                            Color piksel = bmap.GetPixel(i, j);
                                            //RGB renk değerleri 0 (siyah) olanları seç 
                                            if (piksel.R == 0 && piksel.G == 0 && piksel.B == 0)
                                            {
                                                //Sonraki döngüde OptikSonuc tablosuna kaydetmek için diziye al.
                                                ktpList.Add(new KitapcikTuru(kTur, piksel.R, piksel.G, piksel.B, i, j, ck.DosyaAdi));
                                            }
                                        }
                                    }
                                    Application.DoEvents();
                                }
                            }
                            Application.DoEvents();

                            //Diziye alınan siyah noktaları sayrak belirlenen sayıdan fazla siyah nokta olanları db'ye kaydet.
                            foreach (string kTur in ktpcikList)
                            {
                                List<KitapcikTuru> kitapcik = (from tur in ktpList where tur.Kitapcik == kTur select tur).ToList();
                                //MessageBox.Show(kitapcik.Count+" "+ ck.DosyaAdi);
                                if (kitapcik.Count > kitapyogunluk)
                                {
                                    ckDb.KitapcikTuru(ck.DosyaAdi, kTur);
                                    ckKitapcikTuru = kTur;
                                }
                                Application.DoEvents();
                            }
                            if (string.IsNullOrEmpty(ckKitapcikTuru))
                            {
                                foreach (string kTur in ktpcikList)
                                {
                                    List<KitapcikTuru> kitapcik = (from tur in ktpList where tur.Kitapcik == kTur select tur).ToList();

                                    if (kitapcik.Count > kitapikinciyogunluk && kitapcik.Count < kitapyogunluk)
                                    {
                                        ckDb.KitapcikTuru(ck.DosyaAdi, kTur);
                                        ckKitapcikTuru = kTur;
                                    }
                                    Application.DoEvents();
                                }
                            }
                        }
                        #endregion

                        #region OPTİK OKUMA İŞLEMLERİ

                        if (girmedi == 0 && ckKitapcikTuru != "")
                        {
                            //Cevap kağıdında optik konumları var mı yok mu kontrol edelim.
                            int optikKonumSayisi = (from x in knm where x.Grup == "optik" select x).Count();
                            if (optikKonumSayisi > 0)
                            {
                                SonucOptikDB osDb = new SonucOptikDB();

                                //bu cevap kağıdına ait eski optik sonuçlarını sil
                                osDb.KayitSil(sinavId, ckOturumNo, ckOgrenciNo);

                                //Öğrenci Bilgisi
                                OgrencilerDb ogrDb = new OgrencilerDb();
                                OgrencilerInfo ogrInfo = ogrDb.KayitBilgiGetir(ckOgrenciNo, sinavId);

                                //Soru sayısını tespit için sadece a seçeneğini seçerek soruları listele.
                                List<KonumlarInfo> branslar = knmDb.BranslariGetir(sinavId, ckOturumNo);
                                foreach (KonumlarInfo brns in branslar)
                                {
                                    List<KonumlarInfo> sorular =
                                        knmDb.OptikFormdakiSoruSayisi(sinavId, ckOturumNo, brns.BransId);

                                    foreach (KonumlarInfo soru in sorular)
                                    {
                                        SonucOptikInfo osInfo = new SonucOptikInfo
                                        {
                                            BransId = soru.BransId,
                                            OgrenciId = ckOgrenciNo,
                                            KurumKodu = ogrInfo.KurumKodu,
                                            Secenek = "",
                                            Oturum = ckOturumNo,
                                            SoruNo = soru.SoruNo,
                                            SinavId = sinavId,
                                            KitapcikTuru = ckKitapcikTuru,
                                            Puani = 0,
                                            Sinif = ogrInfo.Sinifi,
                                            Sube = ogrInfo.Sube
                                        };
                                        osDb.KayitEkle(osInfo);
                                        Application.DoEvents();
                                    }
                                    Application.DoEvents();

                                    //konumlar tablosundaki konumlardan sadece optik olanları seç.             
                                    List<KonumlarInfo> optikKonumlari = (from x in knm where x.Grup == "optik" && x.Oturum == ckOturumNo && x.BransId == brns.BransId orderby x.SoruNo select x).ToList();

                                    //Optik formdaki seçenekleri okuyacak dizi
                                    List<DiziSecenekler> secenekOptik = new List<DiziSecenekler>();

                                    //Veritabanında bulunan optik seçenek konumlarını döngüye çağır
                                    foreach (KonumlarInfo x in optikKonumlari)
                                    {
                                        for (int i = x.X1; i <= (x.X1 + x.W); i++)
                                        {
                                            for (int j = x.Y1; j <= (x.Y1 + x.H); j++)
                                            {
                                                Color piksel = bmap.GetPixel(i, j);
                                                //RGB renk değerleri 0 (siyah) olanları seç 
                                                if (piksel.R == 0 && piksel.G == 0 && piksel.B == 0)
                                                {
                                                    //Sonraki döngüde OptikSonuc tablosuna kaydetmek için diziye al.
                                                    secenekOptik.Add(new DiziSecenekler(x.Oturum, x.BransId,
                                                        ckOgrenciNo, x.SoruNo, x.Secenek, piksel.R, piksel.G, piksel.B,
                                                        i, j, ck.DosyaAdi));
                                                }
                                            }
                                        }
                                        Application.DoEvents();
                                    }
                                    Application.DoEvents();
                                    //optik konumları say. Diziye alınan siyah noktaları sayrak belirlenen sayıdan fala siyah nokta olanları db'ye kaydet.

                                    foreach (KonumlarInfo ok in optikKonumlari)
                                    {
                                        dsSoru = (from scnk in secenekOptik
                                                  where scnk.SoruNo == ok.SoruNo && scnk.Secenek == ok.Secenek &&
                                                        scnk.Dosya == ck.DosyaAdi
                                                  select scnk).ToList();
                                        if (dsSoru.Count > yogunluk)
                                        {
                                            KitapcikCevapDB kcvpDb = new KitapcikCevapDB();
                                            KitapcikCevapInfo kcInfo = kcvpDb.KayitBilgiGetir(sinavId, ckOturumNo,
                                                brns.BransId, ok.SoruNo);

                                            SonucOptikInfo os = osDb.KayitBilgiGetir(sinavId, brns.BransId, ckOgrenciNo,
                                                ok.SoruNo);
                                            string ogrenciSecenek = os.Secenek + ok.Secenek;
                                            //Seçenek 1 ise kitapcikcevap tablosundaki puanı yazılır.(Doğru ise doğru puanı yanlış ise yanlış puanı 0) 
                                            //TODO:KİTAPÇIK TÜRÜ OLAYI OTOMOTİKLEŞMESİ GEREK
                                            string dogruSecenek = ckKitapcikTuru == "A"
                                                ? kcInfo.KitapcikA
                                                : kcInfo.KitapcikB;
                                            int puani = ogrenciSecenek == dogruSecenek ? kcInfo.SoruPuani : 0;

                                            osDb.KayitGuncelle(sinavId,ckOturumNo,ckOgrenciNo,ok.SoruNo,brns.BransId, ogrenciSecenek, puani);
                                        }
                                        Application.DoEvents();
                                    }

                                    //Sonucoptik tablosundan bu cevap kağıdına ait boş seçenekleri getir.
                                    List<SonucOptikInfo> bosSecenekler = osDb.BosSecenekler(sinavId, ckOturumNo, brns.BransId, ckOgrenciNo);

                                    if (bosSecenekler.Count > 0)
                                    {
                                        foreach (SonucOptikInfo bosS in bosSecenekler)
                                        {
                                            List<DiziSecenekler> secenekSecenek = new List<DiziSecenekler>();

                                            optikKonumlari = (from x in knm
                                                              where x.Grup == "optik" && x.Oturum == ckOturumNo &&
                                                                    x.BransId == bosS.BransId && x.SoruNo == bosS.SoruNo
                                                              orderby x.SoruNo
                                                              select x).ToList();

                                            foreach (KonumlarInfo x in optikKonumlari)
                                            {
                                                for (int i = x.X1; i <= (x.X1 + x.W); i++)
                                                {
                                                    for (int j = x.Y1; j <= (x.Y1 + x.H); j++)
                                                    {
                                                        Color piksel = bmap.GetPixel(i, j);
                                                        //RGB renk değerleri 0 (siyah) olanları seç 
                                                        if (piksel.R == 0 && piksel.G == 0 && piksel.B == 0)
                                                        {
                                                            //Sonraki döngüde OptikSonuc tablosuna kaydetmek için diziye al.
                                                            secenekSecenek.Add(new DiziSecenekler(x.Oturum, x.BransId,
                                                                bosS.OgrenciId, x.SoruNo, x.Secenek, piksel.R, piksel.G,
                                                                piksel.B, i, j, ck.DosyaAdi));
                                                        }
                                                    }
                                                }
                                                Application.DoEvents();
                                            }
                                            Application.DoEvents();

                                            //optik konumları say. Diziye alınan siyah noktaları sayrak belirlenen sayıdan fala siyah nokta olanları db'ye kaydet.
                                            foreach (KonumlarInfo ok in optikKonumlari)
                                            {
                                                dsSoru = (from scnk in secenekSecenek
                                                          where scnk.SoruNo == ok.SoruNo && scnk.Secenek == ok.Secenek
                                                          select scnk).ToList();

                                                if (dsSoru.Count > ikinciyogunluk && dsSoru.Count < yogunluk)
                                                {
                                                    KitapcikCevapDB kcvpDb = new KitapcikCevapDB();
                                                    KitapcikCevapInfo kcInfo = kcvpDb.KayitBilgiGetir(sinavId,
                                                        ckOturumNo, ok.BransId, ok.SoruNo);

                                                    SonucOptikInfo os = osDb.KayitBilgiGetir(sinavId, ok.BransId,
                                                        ckOgrenciNo, ok.SoruNo);
                                                    string ogrenciSecenek = os.Secenek + ok.Secenek;
                                                    //Seçenek 1 ise kitapcikcevap tablosundaki puanı yazılır.(Doğru ise doğru puanı yanlış ise yanlış puanı 0) 

                                                    string dogruSecenek = ckKitapcikTuru == "A"
                                                        ? kcInfo.KitapcikA
                                                        : kcInfo.KitapcikB;
                                                    int puani = ogrenciSecenek == dogruSecenek ? kcInfo.SoruPuani : 0;

                                                    osDb.KayitGuncelle(sinavId, ckOturumNo, ckOgrenciNo, ok.SoruNo, brns.BransId, ogrenciSecenek, puani);
                                                   
                                                }
                                                Application.DoEvents();
                                            }
                                            Application.DoEvents();
                                        }
                                        Application.DoEvents();
                                    }
                                }
                            }
                        }

                        #endregion

                        #region AÇIK UCLU SORULARI KIRPMA İŞLEMLERİ

                        if (girmedi == 0)
                        {
                            //Cevap kağıdında açık uclu cevap konumları var mı yok mu kontrol edelim.
                            int auKonumSayisi = (from cust in knm where cust.Grup == "au" select cust).Count();
                            if (auKonumSayisi > 0)
                            {
                                SonucAuDB cvp = new SonucAuDB();
                                //Bu cevap kağıdına ait önceki kayıtları sil.
                                cvp.CevaplariSil(sinavId, ckOturumNo, ckOgrenciNo);
                                SonucAuInfo info = new SonucAuInfo();

                                try
                                {
                                    var auKonumlar = from cust in knm where cust.Oturum == ckOturumNo && cust.Grup == "au" select cust;

                                    foreach (KonumlarInfo ak in auKonumlar)
                                    {
                                        string rndMetin = GenelIslemler.RastgeleMetinUret(8);
                                        string uzanti = Path.GetExtension(dosyaAdresi);
                                        string kirpilacakCevapAdresi = string.Format("{0}_{1}_{2}{3}", ak.SoruNo, ck.OgrenciId, rndMetin, uzanti);
                                        string kirpilanDosyaAdresi = cevapDizin + kirpilacakCevapAdresi;
                                        info.SoruNo = ak.SoruNo.ToInt32();
                                        info.Dosya = kirpilacakCevapAdresi;
                                        info.OgrenciId = ckOgrenciNo;
                                        info.SinavId = ak.SinavId.ToInt32();
                                        info.BransId = ak.BransId.ToInt32();
                                        info.Oturum = ckOturumNo;

                                        cvp.KayitEkle(info);

                                        ImageProcessing.ResimKirp(dosyaAdresi, ak.W.ToInt32(), ak.H.ToInt32(), ak.X1.ToInt32(), ak.Y1.ToInt32(), kirpilanDosyaAdresi);
                                        Application.DoEvents();
                                    }
                                }
                                catch (Exception)
                                {
                                    //
                                }

                                Application.DoEvents();
                                DialogResult dialog =
                                    MessageBox.Show(lblBilgi.Text + "\n" + @"Şimdi cevapların kırpıldığı dizini açmak ister misiniz?", @"Bilgi",
                                        MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes) Process.Start("explorer.exe", Path.GetDirectoryName(cevapDizin));
                            }

                        }
                        #endregion

                    }

                    //Eğer öğrenci no,oturum no ve kitapçık tür değeri almışsa işlem gördü olarak işaretle.
                    if (ckOgrenciNo != 0 && ckOturumNo != 0 && (ckKitapcikTuru != "" || girmedi == 1))
                        ckDb.CkKontrolIslemiGordu(ck.DizinAdresi, ck.DosyaAdi);
                }
                progressBar1.PerformStep();

                lblGecenSure.Text = String.Format("{0} saat, {1} dakika, {2} saniye", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);
                try
                {
                    double gecenDakika = watch.Elapsed.TotalMinutes;
                    double kalanIslem =ckSayisi -a;
                    double kalanSure =(gecenDakika*kalanIslem)/a;

                    double saat = (kalanSure - (kalanSure % 60)) / 60;
                    double dakika = kalanSure % 60;
                    lblBitisSuresi.Text = string.Format("Kalan tahmini süre : {0:0} saat, {1:0} dakika", saat, dakika);
                }
                catch (Exception)
                {
                    lblBitisSuresi.Text = "Hesaplanıyor...";
                }
            }
            watch.Stop();
            lblBilgi.Text = "Cevap kağıtlarını kontrol işlemi tamamlandı.";
            progressBar1.Value = 0;

            SorunluCKTespit();
            pbDizinSec.Enabled = pbCKKontrol.Enabled = true;

            //İşaretliyse işlem bittiğinde bilgisayarı kapat
            if (cbBilgisayariKapat.Checked)
            {
                Process.Start("shutdown", "-f -s");
            }
        }

        private void SorunluCKTespit()
        {
            CkDosyalarDB ckDsyDb = new CkDosyalarDB();
            List<CkDosyalarInfo> sorunluDsyLst = ckDsyDb.SorunluKayitlariDiziyeGetir(sinavId);
            int eksikKalanlar = sorunluDsyLst.Count;
            if (eksikKalanlar > 0)
            {
                progressBar1.Maximum = eksikKalanlar;
                lblBilgi.Text = "Sorunlu CK dosyaları taranıyor.";

                string dosyaAdi = ckDizin + "\\sorunlu_dosyalar.txt";
                StreamWriter sqlSW = new StreamWriter(dosyaAdi, false, Encoding.UTF8);
                foreach (CkDosyalarInfo srnDsy in sorunluDsyLst)
                {
                    progressBar1.PerformStep();
                    sqlSW.WriteLine(srnDsy.DizinAdresi);
                    //sorunlu dosyalar işlem gördüyü yeniden kontrol etmek için.
                    ckDsyDb.CKIslemiGordu(srnDsy.DizinAdresi, 0);
                    Application.DoEvents();
                }
                Application.DoEvents();
                sqlSW.Close();
                lblBilgi.Text =
                    "Bazı cevap kağıtlarından bilgi alınamadı. Bu dosyalar kontrol edilerek Dizin seçme işlemini kalanları tamamlamak üzere tekrar başlatınız.";

                progressBar1.Value = 0;
                Process.Start(dosyaAdi);
            }
        }

        #endregion

        #region ANA FORMU TAŞIMA İŞLEMLERİ
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

        #region contextMenuStrip2
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
            FormSinifListesi frm = new FormSinifListesi();
            frm.ShowDialog();
        }
        private void optikFormCevaplarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCevaplar frm = new FormCevaplar();
            frm.ShowDialog();
        }
        private void textDosyasıAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTextAl frm = new FormTextAl();
            frm.ShowDialog();
        }
        

        private void formKarneToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormKarne frm = new FormKarne();
            frm.ShowDialog();
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
            DialogResult dialog = MessageBox.Show(@"Programdan çıkış yapmak istediğinizden emin misiniz?", @"Bilgi", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
                Application.Exit();
        }
        private void cbCkKontrolDevamEt_CheckedChanged(object sender, EventArgs e)
        {

            if (!cbCkKontrolDevamEt.Checked) return;
            DialogResult dialog = MessageBox.Show("Cevap kağıtları kontrol işlemlerine başlamadan önce Cevap Kağıdı konumları ayarlanması gerekmektedir."+ "\n"+"Cevap kağıtları konumları ayarlandığından emin misiniz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            cbCkKontrolDevamEt.Checked = dialog == DialogResult.Yes;
        }
        private void pbBilgi_Click(object sender, EventArgs e)
        {

        }

        private void branşlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBranslar frm = new FormBranslar();
            frm.ShowDialog();
        }

        private void ilçelerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIlceler frm = new FormIlceler();
            frm.ShowDialog();
        }

        private void formRaporToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRapor frm = new FormRapor();
            frm.ShowDialog();
        }

        private void kütükİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKutukIslemleri frm = new FormKutukIslemleri();
            frm.ShowDialog();
        }
    }
}
