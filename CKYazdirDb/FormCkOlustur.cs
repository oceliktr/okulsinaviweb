using CKYazdir;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using ODM.CKYazdirDb.Library;
using ThoughtWorks.QRCode.Codec;
using Application = System.Windows.Forms.Application;
using Font = System.Drawing.Font;

namespace ODM.CKYazdirDb
{
    public partial class FormCkOlustur : Form
    {
        private readonly List<OgrencilerInfo> ogrencilerKutuk = new List<OgrencilerInfo>();
        private Bitmap img;
        private string ckDizinAdresi = "";
        private string sinifListesiDizinAdresi = "";
        private const string kagitEbatDizin = "CKDosyalar\\";
        private static readonly AyarlarManager ayarlarManager = new AyarlarManager();
        readonly Ayarlar ayar = ayarlarManager.AyarlariGetir();
        public FormCkOlustur()
        {
            InitializeComponent();
        }

        private void CbIlceler_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ilceAdi = "";
            if (cbIlceler.SelectedValue != null)
                ilceAdi = cbIlceler.SelectedValue.ToString();
            cbOkullar.DataSource = null;
            if (ilceAdi != "0" && !string.IsNullOrEmpty(ilceAdi))
            {
                var okullar = ogrencilerKutuk.Where(x => x.IlceAdi.Equals(ilceAdi)).GroupBy(x => x.KurumKodu)
                    .Select(x => x.First()).OrderBy(x => x.KurumAdi).ToList();
                List<OgrencilerInfo> ogr = new List<OgrencilerInfo> { new OgrencilerInfo("0", "Okul Seçiniz") };
                ogr.AddRange(okullar.Select(t => new OgrencilerInfo(t.KurumKodu.ToString(), t.KurumAdi)));

                cbOkullar.ValueMember = "Text";
                cbOkullar.DisplayMember = "Value";

                foreach (var o in ogr)
                {
                    cbOkullar.Items.Add(o);
                }
                cbOkullar.DataSource = ogr;

                lblBilgi.Text = cbIlceler.Text + " ilçesi için CK ve sınıf listelerini oluşturabilirsiniz.";
            }
            else
            {
                lblBilgi.Text = "Tüm il için CK ve sınıf listelerini oluşturabilirsiniz";
            }
        }

        private void CbOkullar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kurumKodu = "";
            string ilceAdi = "";
            if (cbOkullar.SelectedValue != null)
            {
                ilceAdi = cbIlceler.SelectedValue.ToString();
                kurumKodu = cbOkullar.SelectedValue.ToString();
            }
            cbSinifi.DataSource = null;
            if (kurumKodu != "0" && !string.IsNullOrEmpty(kurumKodu))
            {
                //var siniflar = ogrenciler.Where(x => x.KurumKodu.Equals(kurumKodu) && x.IlceAdi.Equals(ilceAdi)).GroupBy(x => x.Sinifi).Select(x => x.First()).OrderBy(x => x.Sinifi).ToList();
                //List<OgrencilerInfo> ogr = new List<OgrencilerInfo> { new OgrencilerInfo("0", "Sınıf Seçiniz") };
                //ogr.AddRange(siniflar.Select(t => new OgrencilerInfo(t.Sinifi.ToString(), t.Sinifi + ". sınıf")));

                var siniflar = ogrencilerKutuk.Where(x => x.KurumKodu.ToString().Equals(kurumKodu) && x.IlceAdi.Equals(ilceAdi)).GroupBy(x => x.Sinifi)
                    .Select(x => x.First()).OrderBy(x => x.Sinifi).ToList();
                List<OgrencilerInfo> ogr = new List<OgrencilerInfo> { new OgrencilerInfo("0", "Sınıf Seçiniz") };
                ogr.AddRange(siniflar.Select(t => new OgrencilerInfo(t.Sinifi.ToString(), t.Sinifi + ". sınıf")));


                cbSinifi.ValueMember = "Text";
                cbSinifi.DisplayMember = "Value";

                foreach (var o in ogr)
                {
                    cbSinifi.Items.Add(o);
                }
                cbSinifi.DataSource = ogr;
                lblBilgi.Text = cbIlceler.Text + " ilçesi " + cbOkullar.Text +
                                " için CK ve sınıf listelerini oluşturabilirsiniz.";
            }
            else
                lblBilgi.Text = cbIlceler.Text + " ilçesi için CK ve sınıf listelerini oluşturabilirsiniz.";
        }

        private void CbSinifi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sinifi = "";
            string kurumKodu = "";
            if (cbSinifi.SelectedValue != null && sinifi != "")
            {
                sinifi = cbSinifi.SelectedValue.ToString();
                kurumKodu = cbOkullar.SelectedValue.ToString();
            }
            cbSube.DataSource = null;
            if (sinifi != "0" && !string.IsNullOrEmpty(sinifi))
            {
                var subeler = ogrencilerKutuk.Where(x => x.KurumKodu.ToString().Equals(kurumKodu) && x.Sinifi.ToString().Equals(sinifi))
                    .GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube).ToList();
                List<OgrencilerInfo> ogr = new List<OgrencilerInfo> { new OgrencilerInfo("0", "Şube Seçiniz") };
                ogr.AddRange(subeler.Select(t => new OgrencilerInfo(t.Sube, t.Sube)));

                cbSube.ValueMember = "Text";
                cbSube.DisplayMember = "Value";

                foreach (var o in ogr)
                {
                    cbSube.Items.Add(o);
                }
                cbSube.DataSource = ogr;
                lblBilgi.Text = cbIlceler.Text + " ilçesi " + cbOkullar.Text + " " + cbSinifi.Text +
                                "lar için CK ve sınıf listelerini oluşturabilirsiniz.";
            }
            else
                lblBilgi.Text = cbIlceler.Text + " ilçesi " + cbOkullar.Text +
                                " için CK ve sınıf listelerini oluşturabilirsiniz.";
        }

        private void CbSube_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sinifi = "";
            string kurumKodu = "";
            string sube = "";
            if (cbSube.SelectedValue != null)
            {
                sinifi = cbSinifi.SelectedValue.ToString();
                sube = cbSube.SelectedValue.ToString();

                kurumKodu = cbOkullar.SelectedValue.ToString();
            }
            cbOgrenciler.DataSource = null;
            if (sube != "0" && !string.IsNullOrEmpty(sube))
            {
                var ogrList = ogrencilerKutuk
                    .Where(x => x.KurumKodu.ToString().Equals(kurumKodu) && x.Sinifi.ToString().Equals(sinifi) && x.Sube.Equals(sube))
                    .GroupBy(x => x.OgrenciNo).Select(x => x.First()).OrderBy(x => x.Adi).ThenBy(x => x.Soyadi)
                    .ToList();
                List<OgrencilerInfo> ogr = new List<OgrencilerInfo> { new OgrencilerInfo("0", "Öğrenci Seçiniz") };
                ogr.AddRange(ogrList.Select(t => new OgrencilerInfo(t.OgrenciNo.ToString(), t.Adi + " " + t.Soyadi)));

                cbOgrenciler.ValueMember = "Text";
                cbOgrenciler.DisplayMember = "Value";

                foreach (var o in ogr)
                {
                    cbOgrenciler.Items.Add(o);
                }
                cbOgrenciler.DataSource = ogr;
                lblBilgi.Text = cbIlceler.Text + " ilçesi " + cbOkullar.Text + " " + cbSube.Text +
                                " şubesi için CK ve sınıf listelerini oluşturabilirsiniz.";
            }
            else
                lblBilgi.Text = cbIlceler.Text + " ilçesi " + cbOkullar.Text + " " + cbSinifi.Text +
                                "lar için CK ve sınıf listelerini oluşturabilirsiniz.";
        }

        private void CbOgrenciler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOgrenciler.SelectedValue != null && cbOgrenciler.SelectedValue.ToString() != "0")
                lblBilgi.Text = cbIlceler.Text + " ilçesi " + cbOkullar.Text + " " + cbSube.Text + " şubesi " +
                                cbOgrenciler.Text + " isimli öğrenci için CK oluşturabilirsiniz.";
            else
                lblBilgi.Text = cbIlceler.Text + " ilçesi " + cbOkullar.Text + " " + cbSube.Text +
                                " şubesi için CK oluşturabilirsiniz.";
        }

        private void BtnCKDosyaOlustur_Click(object sender, EventArgs e)
        {
            if (ogrencilerKutuk.Count == 0)
            {
                MessageBox.Show("Kütük seçmediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı kapat
                    folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                    folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    folderDialog.Description = @"CK dosyalarının saklanacağı dizini seçiniz.";
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {


                        ckDizinAdresi = folderDialog.SelectedPath + "\\";
                        if (!DizinIslemleri.DosyaKontrol(ayar.Logo))
                        {
                            MessageBox.Show("CK üzerine basılacak logo dosyası bulunamadı. \nAyarlar ekranından logoyu seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string sablonTuru = ayar.SablonTuru;
                        switch (sablonTuru)
                        {
                            case "3" when !DizinIslemleri.DosyaKontrol(ayar.CkSablon):
                                MessageBox.Show("CK A4 boyutlu şablon dosyası bulunamadı. \nAyarlar ekranından üç dersli CK A4 şablonunu seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            case "2" when !DizinIslemleri.DosyaKontrol(ayar.CkSablon):
                                MessageBox.Show("CK A5 boyutlu şablon dosyası bulunamadı. \nAyarlar ekranından CK A5 şablonunu seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            case "1" when !DizinIslemleri.DosyaKontrol(ayar.CkSablon):
                                MessageBox.Show("CK A4 boyutlu şablon dosyası bulunamadı. \nAyarlar ekranından CK A4 şablonunu seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            default: //a5 veya a4

                                pbCkA5Dosyasi.ImageLocation = ayar.CkSablon;
                                BgwCkOlustur.RunWorkerAsync();
                                break;
                        }
                    }
                }
            }
        }

        private void BgwCkOlustur_DoWork(object sender, DoWorkEventArgs e)
        {
            ButonlariKapat();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int a = 1;
            List<OgrencilerInfo> ogrenciler = OgrenciListesi(true);
            int islemSayisi = ogrenciler.Count;
            progressBar1.Maximum = islemSayisi;
            progressBar1.Value = 0;


            foreach (OgrencilerInfo ogr in ogrenciler)
            {

                CKCizimIslemleri(ogr);

                progressBar1.Value = a;
                lblBilgi.Text = string.Format("{0} - {1}  CK dosyası oluşturuluyor. ({2}/{3})", ogr.IlceAdi, ogr.KurumAdi, a, ogrenciler.Count);

                try
                {
                    toolSslKalanSure.Text = islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }
                Application.DoEvents();
                a++;
            }
            watch.Stop();
            toolSslKalanSure.Text = "Tamamlandı...";
            progressBar1.Value = 0;

            ButonlariAc();
            DialogResult dialog =
                MessageBox.Show(
                    a +
                    " adet cevap kağıdı oluşturuldu.\nŞimdi cevap kağıtlarının oluşturulduğu dizini açmak ister misiniz?",
                    @"Bilgi", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
                Process.Start("explorer.exe", Path.GetDirectoryName(ckDizinAdresi + "\\" + kagitEbatDizin));
        }

        private void ButonlariKapat()
        {
            btnSinifSubetoExcel.Enabled = false;
            btnCKDosyaOlustur.Enabled = false;
            btnSinifListesi.Enabled = false;

        }
        private void ButonlariAc()
        {
            btnSinifSubetoExcel.Enabled = true;
            btnCKDosyaOlustur.Enabled = true;
            btnSinifListesi.Enabled = true;

        }
        private List<OgrencilerInfo> OgrenciListesi(bool ogrNoVar)
        {
            string ilceAdi = cbIlceler.SelectedValue == null ? "0" : cbIlceler.SelectedValue.ToString();
            int kurumKodu = cbOkullar.SelectedValue == null ? 0 : cbOkullar.SelectedValue.ToInt32();
            int sinifi = cbSinifi.SelectedValue == null ? 0 : cbSinifi.SelectedValue.ToInt32();
            string sube = cbSube.SelectedValue == null ? "0" : cbSube.SelectedValue.ToString();
            int ogrId = cbOgrenciler.SelectedValue == null ? 0 : cbOgrenciler.SelectedValue.ToInt32();

            var ogrList = from o in ogrencilerKutuk select o;

            if (ilceAdi != "0") ogrList = ogrList.Where(p => p.IlceAdi == ilceAdi);
            if (kurumKodu != 0) ogrList = ogrList.Where(p => p.KurumKodu == kurumKodu);
            if (sinifi != 0) ogrList = ogrList.Where(p => p.Sinifi == sinifi);
            if (sube != "0") ogrList = ogrList.Where(p => p.Sube == sube);
            //Öğrenci numarası var mı yok mu? nedir burası
            if (ogrId != 0 && ogrNoVar) ogrList = ogrList.Where(p => p.OgrenciNo == ogrId);

            List<OgrencilerInfo> ogrencilerInfos = ogrList.OrderBy(x => x.IlceAdi).ThenBy(x => x.KurumAdi)
                .ThenBy(x => x.Sinifi)
                .ThenBy(x => x.Adi).ThenBy(x => x.Soyadi).ToList();
            return ogrencilerInfos;
        }
        private void CKCizimIslemleri(OgrencilerInfo ogr)
        {
            string dizin = $"{ckDizinAdresi}{kagitEbatDizin}{ogr.DersKodu}_{ogr.Sinifi}\\{ogr.IlceAdi}";
            if (!DizinIslemleri.DizinKontrol(dizin))
                DizinIslemleri.DizinOlustur(dizin);

            QRCodeEncoder qrCodeEncoder = QrCodeEncoder(); //karekod oluşturucu.

            int ogrBilgiX = 0;
            int ogrBilgiY = 0;
            int ogrBilgiH = 62;
            if (ayar.SablonTuru == "1")
            {
                //a4 kağıt için
                ogrBilgiX = 480;
                ogrBilgiY = 680;
            }
            else if (ayar.SablonTuru == "2")
            {
                //A5
                ogrBilgiX = 465;
                ogrBilgiY = 330;
            }
            else if (ayar.SablonTuru == "3")
            {
                //a4 kağıt için
                ogrBilgiX = 480;
                ogrBilgiY = 765;
            }

            //bitmap sınıfından bir nesne üreterek pictureboxu tanımlıyoruz.
            img = new Bitmap(pbCkA5Dosyasi.Image);

            //çizilecek nesne tanımlanıyor
            Graphics ckImage = Graphics.FromImage(img);

            //dolgu renk ve font ayarlarını yap
            Font f = new Font(Font.FontFamily, 23, FontStyle.Regular);

            //grafik çizimlerini başlatıyoruz.
            ckImage.DrawString(ogr.Adi + " " + ogr.Soyadi, f, Brushes.Black, ogrBilgiX, ogrBilgiY); //adı soyadı
            ckImage.DrawString(ogr.OgrenciNo.ToString(), f, Brushes.Black, ogrBilgiX, ogrBilgiY + (ogrBilgiH));
            ckImage.DrawString($"{ogr.Sinifi} / {ogr.Sube}", f, Brushes.Black, ogrBilgiX, ogrBilgiY + (ogrBilgiH * 2));
            ckImage.DrawString(ogr.KurumAdi, f, Brushes.Black, ogrBilgiX, ogrBilgiY + (ogrBilgiH * 3));
            ckImage.DrawString(ogr.IlceAdi, f, Brushes.Black, ogrBilgiX, ogrBilgiY + (ogrBilgiH * 4));
            if (ayar.SablonTuru != "3") //a4 üç ders değilse
                ckImage.DrawString(ogr.DersKodu.DersAdi(), f, Brushes.Black, ogrBilgiX, ogrBilgiY + (ogrBilgiH * 5)); //ders

            //dolgu renk ve font ayarlarını yap
            Brush dolgu = new SolidBrush(Color.Black);

            //bARKOD OLUŞTUR
            //Bitmap qrc = new Bitmap("");
            //if (ogr.Barkod != null)
            //    qrc = qrCodeEncoder.Encode(ogr.Barkod);

            //optikte işaretlenecek alanların bilgileri
            int w = 39;
            int h = 39;
            int x = 0; //başlangıç x koordinatı
            int y = 0; //başlangıç y koordinatı
            int artim = 50; // iki boşluk arasındaki fark
            if (ayar.SablonTuru == "1")
            {
                //A4 BUBBLE AYARLARI
                x = 1555;
                y = 2103;

                switch (ogr.DersKodu)
                {
                    case 1:
                        ckImage.FillEllipse(dolgu, 1405, 2154, w, h);
                        break;
                    case 2:
                        ckImage.FillEllipse(dolgu, 1405, 2354, w, h);
                        break;
                    case 4:
                        ckImage.FillEllipse(dolgu, 1405, 2554, w, h);
                        break;
                }
                string logoUrl = ayar.Logo;

                Bitmap logo = new Bitmap(logoUrl);
                //A4 Karekod Konumu
                //if (ogr.Barkod != null)
                //    ckImage.DrawImage(qrc, 1580, 1480, 300, 300);
                ckImage.DrawImage(logo, 730, 1320, 300, 300);
            }
            else if (ayar.SablonTuru == "2")
            {

                //sablons.Add(new Sablon(1, "Tek Dersli - A4"));
                //sablons.Add(new Sablon(2, "Tek Dersli - A5"));
                //sablons.Add(new Sablon(3, "Üç Dersli - A4"));

                x = 1104;
                y = 1554;

                switch (ogr.DersKodu)
                {
                    case 1:
                        ckImage.FillEllipse(dolgu, 954, 1605, w, h);
                        break;
                    case 2:
                        ckImage.FillEllipse(dolgu, 954, 1805, w, h);
                        break;
                    case 4:
                        ckImage.FillEllipse(dolgu, 954, 2005, w, h);
                        break;
                }

                Bitmap logo = new Bitmap(ayar.Logo);
                //A5 Karekod Konumu
                // ckImage.DrawImage(qrc, 1130, 960, 300, 300);
                ckImage.DrawImage(logo, 520, 750, 230, 230);
            }
            else if (ayar.SablonTuru == "3")
            {
                //A4 BUBBLE AYARLARI
                x = 1656;
                y = 2156;

                //3 derste ders dolgusun GEREK YOK
                //switch (ogr.DersKodu)
                //{
                //    case 1:
                //        ckImage.FillEllipse(dolgu, 1405, 2154, w, h);
                //        break;
                //    case 2:
                //        ckImage.FillEllipse(dolgu, 1405, 2354, w, h);
                //        break;
                //    case 4:
                //        ckImage.FillEllipse(dolgu, 1405, 2554, w, h);
                //        break;
                //}

            }
            string ogrenciId = ogr.OpaqId.ToString();

            //Ders bubble işaretle
            for (int sag = 0; sag < 11; sag++)
            {
                for (int asagi = 0; asagi < 10; asagi++)
                {
                    for (int z = 0; z < ogrenciId.Length; z++)
                    {
                        //sadece gelen değerleri işaretlemek için sorgu
                        if (sag == 10 - (ogrenciId.Length - (z + 1)) &&
                            asagi == Convert.ToInt32(ogrenciId.Substring(z, 1)))
                        {
                            ckImage.FillEllipse(dolgu, x + (artim * sag), y + (artim * asagi), w, h);
                            ckImage.DrawString(ogrenciId.Substring(z, 1), f, Brushes.Black, x + (50 * sag), y - artim); //opaq Id
                        }
                    }
                }
            }


            img.SetResolution(300, 300);

            string dosyaAdresi = $"{dizin}\\{ogr.KurumAdi} {ogr.Sinifi} - {ogr.Sube} Şubesi {ogr.OpaqId}.png";
            //string dosyaAdresi = string.Format(@"{0}\{1} {2} {3}.png", ckDizinAdresi + kagitEbatDizin + ogr.DersKodu + "\\" + ogr.IlceAdi, ogr.KurumAdi, $"{ogr.Sinifi}-{ogr.Sube}", ogr.OpaqId);
            img.Save(dosyaAdresi, ImageFormat.Png);

            ckImage.Clear(Color.Transparent);
            ckImage.Dispose();
            img.Dispose();
            //qrc.Dispose();
        }
        private static QRCodeEncoder QrCodeEncoder()
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeScale = 4,
                QRCodeVersion = 3
            };

            string errorCorrect = "L";
            switch (errorCorrect)
            {
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
                case "H":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
            }
            return qrCodeEncoder;
        }

        private void BtnSinifListesi_Click(object sender, EventArgs e)
        {
            if (ogrencilerKutuk.Count == 0)
            {
                MessageBox.Show("Kütük seçmediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı kapat
                    folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                    folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    folderDialog.Description = @"Sınıf listelerinin saklanacağı dizini seçiniz.";
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        sinifListesiDizinAdresi = folderDialog.SelectedPath + "\\";

                        if (!DizinIslemleri.DosyaKontrol(ayar.SinifListesiSablon))
                        {
                            MessageBox.Show("Sınıf listesi şablon dosyası bulunamadı. \nAyarlar ekranından sınıf listesi şablonunu seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        pbCkA5Dosyasi.ImageLocation = ayar.SinifListesiSablon;
                        BgwSinifListesi.RunWorkerAsync();
                    }
                }
            }
        }

        private void BgwSinifListesi_DoWork(object sender, DoWorkEventArgs e)
        {
            int okulBilgiX = 460;
            int okulBilgiY = 498;
            int okulBilgiH = 79;

            int koordinatY = 860;
            int koordinatH = 50;

            ButonlariKapat();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<OgrencilerInfo> ogrenciler = OgrenciListesi(false);

            var ilceler = (from ogr in ogrenciler select ogr).GroupBy(x => x.IlceAdi).Select(x => x.First())
                .OrderBy(x => x.IlceAdi);

            int subeSayisi = (from ilce in ilceler
                              let okullar =
                                  (from ogr in ogrenciler select ogr).Where(x => x.IlceAdi == ilce.IlceAdi).GroupBy(x => x.KurumKodu)
                                  .Select(x => x.First()).OrderBy(x => x.KurumAdi)
                              from okul in okullar
                              select (from ogr in ogrenciler select ogr)
                                  .Where(x => x.IlceAdi == ilce.IlceAdi && x.KurumKodu == okul.KurumKodu).GroupBy(x => x.Sube)
                                  .Select(x => x.First()).OrderBy(x => x.KurumAdi)
                into subeler
                              select subeler.Count()).Sum();
            var dersler = (from ogr in ogrenciler select ogr).GroupBy(x => x.DersKodu).Select(x => x.First())
                .OrderBy(x => x.DersKodu);

            int islemSayisi = subeSayisi * dersler.Count();
            progressBar1.Maximum = islemSayisi;
            int a = 0;

            foreach (var ders in dersler)
            {
                foreach (var ilce in ilceler)
                {
                    if (!DizinIslemleri.DizinKontrol(sinifListesiDizinAdresi + "SinifListeleri\\" + ders.DersKodu + "_" + ders.Sinifi +
                                                     "\\" + ilce.IlceAdi))
                        DizinIslemleri.DizinOlustur(sinifListesiDizinAdresi + "SinifListeleri\\" + ders.DersKodu + "_" + ders.Sinifi +
                                                    "\\" + ilce.IlceAdi);
                    var okullar = (from ogr in ogrenciler select ogr)
                        .Where(x => x.IlceAdi == ilce.IlceAdi && x.DersKodu == ders.DersKodu).GroupBy(x => x.KurumKodu)
                        .Select(x => x.First()).OrderBy(x => x.KurumAdi);
                    foreach (var okul in okullar)
                    {
                        var subeler = (from ogr in ogrenciler select ogr)
                            .Where(x => x.IlceAdi == ilce.IlceAdi && x.KurumKodu == okul.KurumKodu &&
                                        x.DersKodu == ders.DersKodu).GroupBy(x => new { x.Sube, x.DersKodu })
                            .Select(x => x.First()).OrderBy(x => x.KurumAdi);
                        foreach (var sb in subeler)
                        {
                            var ogrL = (from ogr in ogrenciler select ogr).Where(x => x.KurumKodu == okul.KurumKodu && x.Sube == sb.Sube && x.DersKodu == ders.DersKodu).GroupBy(x => x.OpaqId).Select(x => x.First()).OrderBy(x => x.OgrenciNo);



                            Bitmap img = new Bitmap(pbCkA5Dosyasi.Image);

                            //çizilecek nesne tanımlanıyor
                            Graphics ckImage = Graphics.FromImage(img);
                            int i = 1;
                            //dolgu renk ve font ayarlarını yap
                            Font f = new Font(Font.FontFamily, 18, FontStyle.Regular);
                            Font f2 = new Font(Font.FontFamily, 23, FontStyle.Regular);

                            ckImage.DrawString(sb.IlceAdi, f2, Brushes.Black, okulBilgiX, okulBilgiY); //ilçe adı
                            ckImage.DrawString(sb.KurumKodu + " - " + sb.KurumAdi, f2, Brushes.Black, okulBilgiX,
                                okulBilgiY + okulBilgiH); //kurum adı
                            ckImage.DrawString(ders.DersKodu.DersAdi(), f2, Brushes.Black, okulBilgiX,
                                okulBilgiY + okulBilgiH * 2); //ders
                            ckImage.DrawString($"{sb.Sinifi} / {sb.Sube}", f2, Brushes.Black, okulBilgiX,
                                okulBilgiY + okulBilgiH * 3); //şube

                            string sube = sb.Sube;
                            //karekod
                            QRCodeEncoder qrCodeEncoder = QrCodeEncoder(); //karekod oluşturucu.

                            Bitmap qrc = qrCodeEncoder.Encode(sb.KurumKodu + "#" + sube);
                            ckImage.DrawImage(qrc, 2050, 500, 250, 250);

                            foreach (var ogr in ogrL)
                            {
                                //grafik çizimlerini başlatıyoruz.
                                ckImage.DrawString(ogr.OgrenciNo.ToString(), f, Brushes.Black, 227, (koordinatY + (koordinatH * i))); //öğrenci no
                                ckImage.DrawString(ogr.Adi + " " + ogr.Soyadi, f, Brushes.Black, 380, (koordinatY + (koordinatH * i))); //adı soyadı
                                i++;
                            }
                            img.SetResolution(300, 300);
                            string dosyaAdresi = string.Format(@"{0}{1} {2}.png", sinifListesiDizinAdresi + "SinifListeleri\\" + ders.DersKodu + "_" + ders.Sinifi + "\\" + ilce.IlceAdi + "\\", sb.KurumAdi, sb.Sube);
                            // MessageBox.Show(ilce.IlceAdi+"-");
                            img.Save(dosyaAdresi, ImageFormat.Png);

                            ckImage.Clear(Color.Transparent);
                            ckImage.Dispose();
                            img.Dispose();
                            f.Dispose();
                            qrc.Dispose();
                            f2.Dispose();

                            a++;
                            progressBar1.Value = a;
                            lblBilgi.Text = $"{sb.IlceAdi} - {sb.KurumAdi} sınıf listesi oluşturuluyor. {a}/{islemSayisi}";

                            try
                            {
                                toolSslKalanSure.Text = subeSayisi.KalanSureHesapla(a, watch);
                            }
                            catch (Exception)
                            {
                                toolSslKalanSure.Text = "Hesaplanıyor...";
                            }

                            Application.DoEvents();
                        }
                    }
                }
            }
            progressBar1.Value = 0;
            watch.Stop();
            toolSslKalanSure.Text = "Tamamlandı...";

            ButonlariAc();

            DialogResult dialog = MessageBox.Show("Sınıf listeleri oluşturuldu. Dizini açmak ister misiniz?", @"Bilgi",
                MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
                Process.Start("explorer.exe", Path.GetDirectoryName(sinifListesiDizinAdresi));
        }

        private void BtnSinifSubetoExcel_Click(object sender, EventArgs e)
        {
            if (ogrencilerKutuk.Count == 0)
            {
                MessageBox.Show("Kütük seçmediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                string tarihStr = $"{DateTime.Now}";
                SaveFileDialog xlsFile = new SaveFileDialog
                {
                    Filter = "Microsoft Excel Çalışma Kitabı (*.xls;*.xlsx)|*.xls;*.xlsx",
                    FileName = $"Şube Öğrenci Sayıları {DateTime.Now.ToString().Replace(":", "").Replace(".", "")}.xls",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                if (xlsFile.ShowDialog() == DialogResult.OK)
                {
                    ExportarDataGridViewExcel(xlsFile.FileName);

                }
                //lblBilgi.Text = "Şube listesi oluşturuldu.";
            }
        }
        private void ExportarDataGridViewExcel(string dosyaAdi)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            lblBilgi.Text = "Şube listesi oluşturuluyor.";
            int s = 0;
            int o = 0;
            int il = 0;
            List<SinifSube> ilceBilgi = new List<SinifSube>();
            List<SinifSube> okulBilgi = new List<SinifSube>();
            List<SinifSube> subeBilgi = new List<SinifSube>();

            var siniflar = (from ogr in ogrencilerKutuk select ogr).GroupBy(x => x.Sinifi).Select(x => x.First()).OrderBy(x => x.Sinifi);
            foreach (var sinif in siniflar)
            {
                var ilceler = (from ogr in ogrencilerKutuk select ogr).GroupBy(x => x.IlceAdi).Select(x => x.First()).OrderBy(x => x.IlceAdi);
                foreach (var ilce in ilceler)
                {
                    o++;
                    int ilceOgrSayisi = (from ogr in ogrencilerKutuk select ogr).GroupBy(x => x.OpaqId).Select(x => x.First()).Count(x => x.IlceAdi == ilce.IlceAdi && x.Sinifi == sinif.Sinifi);
                    ilceBilgi.Add(new SinifSube(o, ilce.IlceAdi, 0, "", sinif.Sinifi,"", ilceOgrSayisi));

                    var okullar = (from ogr in ogrencilerKutuk select ogr).Where(x => x.IlceAdi == ilce.IlceAdi).GroupBy(x => x.KurumKodu).Select(x => x.First()).OrderBy(x => x.KurumAdi);
                    foreach (var okul in okullar)
                    {
                        o++;
                        int okulOgrSayisi = (from ogr in ogrencilerKutuk select ogr).GroupBy(x => x.OpaqId).Select(x => x.First()).Count(x => x.KurumKodu == okul.KurumKodu && x.Sinifi == sinif.Sinifi);
                        okulBilgi.Add(new SinifSube(o, okul.IlceAdi, okul.KurumKodu, okul.KurumAdi, sinif.Sinifi,
                            "", okulOgrSayisi));
                        var subeler = (from ogr in ogrencilerKutuk select ogr).Where(x => x.KurumKodu == sinif.KurumKodu && x.Sinifi == sinif.Sinifi).GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube);
                        foreach (var sube in subeler)
                        {
                            int ogrSayisi = (from ogr in ogrencilerKutuk select ogr).GroupBy(x => x.OpaqId)
                                .Select(x => x.First()).Count(x =>
                                    x.KurumKodu == sube.KurumKodu && x.Sinifi == sube.Sinifi && x.Sube == sube.Sube);
                            s++;
                            subeBilgi.Add(new SinifSube(s, sube.IlceAdi, sube.KurumKodu, sube.KurumAdi, sube.Sinifi,
                                sube.Sube, ogrSayisi));
                        }
                    }
                }
            }

            int a = 0;
            int ilceSayisi = ilceBilgi.Count;
            int okulSayisi = okulBilgi.Count;
            int subeSayisi = subeBilgi.Count;
            int islemSayisi = subeSayisi + okulSayisi+ilceSayisi;
            progressBar1.Maximum = islemSayisi;
            progressBar1.Value = 0;

            lblBilgi.Text = "Şube öğrenci sayıları listesi oluşturuluyor.";

            Microsoft.Office.Interop.Excel.Application aplicacion = new Microsoft.Office.Interop.Excel.Application();
            Workbook calismaKitabi = aplicacion.Workbooks.Add();
            Worksheet calismaSayfasi = (Worksheet)calismaKitabi.Worksheets.Item[1];

            calismaSayfasi.Name = "Şube Öğrenci Sayıları";

            calismaSayfasi.Cells[1, 1] = "Sıra No";
            calismaSayfasi.Cells[1, 2] = "İlçe Adı";
            calismaSayfasi.Cells[1, 3] = "Kurum Kodu";
            calismaSayfasi.Cells[1, 4] = "Kurum Adı";
            calismaSayfasi.Cells[1, 5] = "Sınıf";
            calismaSayfasi.Cells[1, 6] = "Şube";
            calismaSayfasi.Cells[1, 7] = "Şube Ö. Sayısı";

            for (int i = 0; i < subeSayisi; i++)
            {

                calismaSayfasi.Cells[i + 2, 1] = i + 1;
                calismaSayfasi.Cells[i + 2, 2] = subeBilgi[i].IlceAdi;
                calismaSayfasi.Cells[i + 2, 3] = subeBilgi[i].KurumKodu;
                calismaSayfasi.Cells[i + 2, 4] = subeBilgi[i].KurumAdi;
                calismaSayfasi.Cells[i + 2, 5] = subeBilgi[i].Sinif;
                calismaSayfasi.Cells[i + 2, 6] = subeBilgi[i].Sube;
                calismaSayfasi.Cells[i + 2, 7] = subeBilgi[i].OgrenciSayisi;

                a++;
                progressBar1.Value = a;

                try
                {
                    toolSslKalanSure.Text = islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }
            }

            //Yeni Sayfa Ekle
            Sheets xlSheets = calismaKitabi.Sheets as Sheets;
            var calismaSayfasi2 = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);

            //  Worksheet calismaSayfasi2 = (Worksheet)calismaKitabi.Worksheets.Item[1];


            calismaSayfasi2.Name = "Okul Öğrenci Sayıları";

            calismaSayfasi2.Cells[1, 1] = "Sıra No";
            calismaSayfasi2.Cells[1, 2] = "İlçe Adı";
            calismaSayfasi2.Cells[1, 3] = "Kurum Kodu";
            calismaSayfasi2.Cells[1, 4] = "Kurum Adı";
            calismaSayfasi2.Cells[1, 5] = "Sınıf";
            calismaSayfasi2.Cells[1, 6] = "Ö. Sayısı";

            for (int i = 0; i < okulSayisi; i++)
            {

                calismaSayfasi2.Cells[i + 2, 1] = i + 1;
                calismaSayfasi2.Cells[i + 2, 2] = okulBilgi[i].IlceAdi;
                calismaSayfasi2.Cells[i + 2, 3] = okulBilgi[i].KurumKodu;
                calismaSayfasi2.Cells[i + 2, 4] = okulBilgi[i].KurumAdi;
                calismaSayfasi2.Cells[i + 2, 5] = okulBilgi[i].Sinif;
                calismaSayfasi2.Cells[i + 2, 6] = okulBilgi[i].OgrenciSayisi;

                a++;
                progressBar1.Value = a;

                try
                {
                    toolSslKalanSure.Text = islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }
            }


            var calismaSayfasi3 = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);

            calismaSayfasi3.Name = "İlçe Öğrenci Sayıları";

            calismaSayfasi3.Cells[1, 1] = "Sıra No";
            calismaSayfasi3.Cells[1, 2] = "İlçe Adı";
            calismaSayfasi3.Cells[1, 3] = "Sınıf";
            calismaSayfasi3.Cells[1, 4] = "Ö. Sayısı";

            for (int i = 0; i < ilceSayisi; i++)
            {

                calismaSayfasi3.Cells[i + 2, 1] = i + 1;
                calismaSayfasi3.Cells[i + 2, 2] = ilceBilgi[i].IlceAdi;
                calismaSayfasi3.Cells[i + 2, 3] = ilceBilgi[i].Sinif;
                calismaSayfasi3.Cells[i + 2, 4] = ilceBilgi[i].OgrenciSayisi;

                a++;
                progressBar1.Value = a;

                try
                {
                    toolSslKalanSure.Text = islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }
            }

            toolSslKalanSure.Text = "Tamamlandı...";

            calismaKitabi.SaveAs(dosyaAdi, XlFileFormat.xlWorkbookNormal);
            calismaKitabi.Close(true);
            aplicacion.Quit();

            lblBilgi.Text = "Şube öğrenci sayıları listesi oluşturuldu.";

            Process.Start("explorer.exe", Path.GetFileName(dosyaAdi));

            progressBar1.Value = 0;
            watch.Stop();

        }

        private void btnDosyaAdresleri_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı kapat
                folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                folderDialog.Description = @"CK dosyalarının saklandığı dizini seçiniz.";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    DirectoryInfo dizin = new DirectoryInfo(folderDialog.SelectedPath);
                    FileInfo[] dosyalar = dizin.GetFiles("*.*", SearchOption.AllDirectories);

                    string dosya_yolu = Application.StartupPath + @"\ck_dizin.txt";
                    FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);

                    foreach (FileInfo dsy in dosyalar.OrderBy(x => x.FullName))
                    {
                        string dosyaYolu = dsy.FullName;
                        DosyaInfo lst = new DosyaInfo(dsy.Name, dosyaYolu, dsy.CreationTime, dsy.DirectoryName);
                        sw.WriteLine(dsy.FullName);
                    }
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                    Process.Start("explorer.exe", Path.GetFileName(dosya_yolu));
                }
            }
        }

        private void btnKutukCek_Click(object sender, EventArgs e)
        {
            KutukManager kutukDb = new KutukManager();
            foreach (var k in kutukDb.List())
            {
                ogrencilerKutuk.Add(new OgrencilerInfo(k.OpaqId, k.IlAdi, k.IlceAdi, k.KurumKodu, k.KurumAdi, k.OgrenciNo, k.Adi, k.Soyadi, k.Sinifi, k.Sube, k.SinavId, k.DersKodu, k.Barkod));
            }

            cbIlceler.DataSource = null;
            List<OgrencilerInfo> ilceler = ogrencilerKutuk.GroupBy(x => x.IlceAdi).Select(x => x.First()).OrderBy(x => x.IlceAdi).ToList();

            List<OgrencilerInfo> ogr = new List<OgrencilerInfo> { new OgrencilerInfo("0", "İlçe Seçiniz") };
            ogr.AddRange(ilceler.Select(t => new OgrencilerInfo(t.IlceAdi, t.IlceAdi)));

            cbIlceler.ValueMember = "Text";
            cbIlceler.DisplayMember = "Value";
            foreach (var o in ogr)
            {
                cbIlceler.Items.Add(new OgrencilerInfo(o.IlceAdi, o.IlceAdi));
            }
            cbIlceler.DataSource = ogr;

            //Sınıflar
            var siniflar = ogrencilerKutuk.GroupBy(x => x.Sinifi).Select(x => x.First()).OrderBy(x => x.Sinifi).ToList();
            List<OgrencilerInfo> snf = new List<OgrencilerInfo> { new OgrencilerInfo("0", "Sınıf Seçiniz") };
            snf.AddRange(siniflar.Select(t => new OgrencilerInfo(t.Sinifi.ToString(), t.Sinifi + ". sınıf")));


            cbSinifi.ValueMember = "Text";
            cbSinifi.DisplayMember = "Value";

            foreach (var o in snf)
            {
                cbSinifi.Items.Add(o);
            }
            cbSinifi.DataSource = snf;
        }
    }
}
