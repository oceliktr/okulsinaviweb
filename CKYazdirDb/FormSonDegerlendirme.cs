using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.Library;
using ODM.CKYazdirDb.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ODM.CKYazdirDb
{
    public partial class FormSonDegerlendirme : Form
    {
        public FormSonDegerlendirme()
        {
            InitializeComponent();
        }
        private void FormSonDegerlendirme_Load(object sender, EventArgs e)
        {

            CheckForIllegalCrossThreadCalls = false;
        }

        string dosyaAdi = "";
        private string raporDizinAdresi = "";

        #region Değerlendirme 1 İşlemleri

        private void btnDegerlendirme1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofData = new OpenFileDialog())
            {
                ofData.Reset();
                ofData.ReadOnlyChecked = true;
                ofData.Multiselect = true;
                ofData.ShowReadOnly = true;
                ofData.Filter = "Cevap dosyası (*.txt;*.dat)|*.txt;*.dat";
                ofData.Title = "Cevap dosyasını seçiniz.";
                ofData.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofData.CheckPathExists = true;
                if (ofData.ShowDialog() == DialogResult.OK)
                {
                    dosyaAdi = ofData.FileName;

                    //Mükerrer kayıtlar kütükte olmayan öğrenciler gibi kayıtların tutulacağı dizini seçtiren işlemler
                    using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                    {
                        folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı kapat
                        folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                        folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        folderDialog.Description = @"Rapor dosyalarının saklanacağı dizini seçiniz.";
                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            raporDizinAdresi = folderDialog.SelectedPath + "\\";

                            //Birinci aşamada cevapları alıp cevapların kontrolünü yaparak kütüğe kaydeder.
                            bgwDegerlendirme1.RunWorkerAsync();
                        }
                        folderDialog.Dispose();
                    }
                }
                ofData.Dispose();
            }

        }
        private void bgwDegerlendirme1_DoWork(object sender, DoWorkEventArgs e)
        {
            string raporUrl = raporDizinAdresi + @"\Rapor.txt";

            Stopwatch watch = new Stopwatch();
            watch.Start();
            var lines = File.ReadLines(dosyaAdi, Encoding.UTF8).ToList();
            int islemSayisi = lines.Count;
            progressBar1.Maximum = islemSayisi;
            progressBar1.Value = 0;

            //Mükerrer Kontrol işlemleri
            if (MukerrerKayitKontrol(lines, raporUrl) > 0)
            {
                Process.Start("notepad.exe", raporUrl);

                return; //mükerrer kayıtlaeı göster ve işlemi durdur
            }

            //Öğrenci cevaplarını text dosyasından alıp veritabanına kaydeden aşama.

            List<ResultList> result = OgrenciCevaplariniKutugeKaydet(lines);

            //Kütükte bilgisi olmayan öğrenciler ve hatalı işaretlemelerin raporu varsa yazdır.
            if (result.Count > 0)
            {
                StreamWriter resultYaz = new StreamWriter(raporUrl);
                int a = 0;
                progressBar1.Value = 0;
                islemSayisi = result.Count;
                progressBar1.Maximum = islemSayisi;

                foreach (var kutuk in result)
                {
                    a++;
                    progressBar1.Value = a;
                    try
                    {
                        toolSslKalanSure.Text = $"Uyarılar yazılıyor. (3/3) {a} / {islemSayisi} : " +
                                                islemSayisi.KalanSureHesapla(a, watch);
                    }
                    catch (Exception)
                    {
                        toolSslKalanSure.Text = "Hesaplanıyor...";
                    }
                    resultYaz.WriteLine(kutuk.Key + " " + kutuk.Result);
                }
                toolSslKalanSure.Text = "Tamamlandı.";

                resultYaz.Close();
                resultYaz.Dispose();

                Process.Start("notepad.exe", raporUrl);

                return; //ve çık
            }


            raporUrl = raporDizinAdresi + "KarneSonuc.ck";
            StreamWriter yaz = new StreamWriter(raporUrl);

            List<KarneSonuc> karneSonucList = OgrenciCevaplariniDegerlendir();

            //okul ve şube karneleri için hesaplama yapar karne sonuç dosyasına yazar
            SubeDuzeyindeKarneSonuclariniKaydet(karneSonucList, yaz);

            //il ilçe kazanım ortalaması % değerlendirmesi
            List<IlIlceDogruYanlisBosModeli> ilIlceOrtalamalariList = IlIlceBasariHesapla(karneSonucList);

            IlIlceBasariKaydet(ilIlceOrtalamalariList, yaz);
            yaz.Close();

            OgrenciOrtalamalari();

            CKDataOlustur();

            toolSslKalanSure.Text = "Tamamlandı";

        }
        private int MukerrerKayitKontrol(List<string> lines, string raporUrl)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int islemSayisi = lines.Count;
            progressBar1.Maximum = islemSayisi;
            int a = 0;
            progressBar1.Value = 0;


            List<string> opaqList = new List<string>();
            int say = 0;
            StreamWriter yaz = new StreamWriter(raporUrl);
            foreach (var item in lines)
            {
                a++;
                progressBar1.Value = a;
                try
                {
                    toolSslKalanSure.Text = $"Mükerrer Kayıt Kontrolü (1/3) {a} / {islemSayisi} : " + islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }

                string[] satir = item.Split('#');
                string opaqStr = satir[0].Trim();

                var kontrol = opaqList.Find(x => x == opaqStr);
                if (kontrol == null)
                    opaqList.Add(opaqStr);
                else
                {
                    yaz.WriteLine(opaqStr + " nolu opaq mükerrerdir.");
                    say++;
                }
            }
            watch.Stop();

            progressBar1.Value = 0;
            yaz.Close();
            yaz.Dispose();
            toolSslKalanSure.Text = "Tamamlandı";
            return say;
        }
        private List<ResultList> OgrenciCevaplariniKutugeKaydet(List<string> lines)
        {
            List<ResultList> result = new List<ResultList>();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            int islemSayisi = lines.Count;
            progressBar1.Maximum = islemSayisi;
            int a = 0;
            progressBar1.Value = 0;

            //Öğrenci cevaplarını text dosyasından alıp veritabanına kaydeden aşama.
            foreach (string file in lines)
            {
                a++;
                progressBar1.Value = a;
                try
                {
                    toolSslKalanSure.Text = $"Cevaplar Kütüğe Kaydediliyor (2/3) {a} / {islemSayisi} : " +
                                            islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }

                string[] cevapBilgisi = file.Split('#');
                string opaqIdStr = cevapBilgisi[0].Trim();
                string kitapcikTuru = cevapBilgisi[1].Trim();
                string katilimDurumu = cevapBilgisi[2].Trim();
                string cevaplar = "";
                long opaqId = opaqIdStr.ToInt64();
                for (int t = 3; t < cevapBilgisi.Length; t += 2)
                {
                    cevaplar += cevapBilgisi[t] + "#" + cevapBilgisi[t + 1] + "#";
                }

                //En sona eklenen # işareti silelim.
                if (cevaplar.Substring(cevaplar.Length - 1, 1) == "#")
                    cevaplar = cevaplar.Substring(0, cevaplar.Length - 1);

                /*
                 * Kitapçık türü işaretlememiş ve cevap vermiş mi kontrol edip listeye ekle --değerlendirmeye alınmaz
                 * Girmedi işaretli ancak cevap vermiş ve de kitapçık türü işaretliyse girmedi olayını geçip değerlendirmeye al
                 * kitapçık türü işaretli cevap vermemiş ise girmedi işartliyse değerlendirmeye alma
                 */
                if (kitapcikTuru == "" && (cevapBilgisi.Contains("A") || cevapBilgisi.Contains("B") ||
                                           cevapBilgisi.Contains("C") || cevapBilgisi.Contains("D")))
                {
                    result.Add(new ResultList(opaqIdStr,
                        "Kitapçık türünü işaretlememiş")); //KİTAPÇIK TÜRÜNÜ İŞARETLEMEMMİŞ AMA CEVAP VERMİŞ.
                }

                if (kitapcikTuru != "" && katilimDurumu == "0" &&
                    (cevaplar.Contains("A") || cevaplar.Contains("B") || cevaplar.Contains("C") ||
                     cevaplar.Contains("D")))
                {

                    katilimDurumu = "";
                    result.Add(new ResultList(opaqIdStr,
                        "Kitapçık türü işaretli ve cevapta vermiş katılım durumunu sınava girdi yapıldı")); //KİTAPÇIK TÜRÜNÜ İŞARETLEMEMMİŞ AMA CEVAP VERMİŞ.

                }

                //Kitapçık türü işaretli girmedi işaretli değil fakat cevap vermemiş ise sınava girmedi kabul et.
                if (kitapcikTuru != "" && katilimDurumu != "0" &&
                    !(cevapBilgisi.Contains("A") || cevapBilgisi.Contains("B") || cevapBilgisi.Contains("C") ||
                      cevapBilgisi.Contains("D")))
                {
                    katilimDurumu = "0"; //Kitapçık işaretli cevapta vermemiş ise katılım durumunu girmedi yap. (girmedi=>0)
                    result.Add(new ResultList(opaqIdStr,
                        "Kitapçık türü işaretli fakat herhangi bir cevap vermemiş katılım durumunu sınava girmedi yapıldı"));
                }

                KutukManager kutukManager = new KutukManager();
                Kutuk kutuk = kutukManager.Find(x => x.OpaqId == opaqId);

                if (kutuk != null)
                {
                    kutuk.KitapcikTuru = kitapcikTuru;
                    kutuk.KatilimDurumu = katilimDurumu;
                    kutuk.Cevaplar = cevaplar;
                    kutukManager.Update(kutuk);
                }
                else
                {
                    //Kütükte yoksa dizie al.
                    result.Add(new ResultList(opaqIdStr, "Kütükte bulunamadı"));
                }

                Application.DoEvents();
            }

            return result;
        }

        #endregion

        #region Değerlendirme 2 İşlemleri
        private void btnDegerlendirme2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı kapat
                folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                folderDialog.Description = @"Karne sonuçlarının saklanacağı dizini seçiniz.";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    raporDizinAdresi = folderDialog.SelectedPath + "\\";

                    //Birinci aşamada cevapları alıp cevapların kontrolünü yaparak kütüğe kaydeder.
                    bgwDegerlendirme2.RunWorkerAsync();
                }
                folderDialog.Dispose();
            }


        }
        private void bgwDegerlendirme2_DoWork(object sender, DoWorkEventArgs e)
        {
            // raporAdet++;
            string raporUrl = raporDizinAdresi + $"KarneSonuc.ck";
            StreamWriter yaz = new StreamWriter(raporUrl);

            List<KarneSonuc> karneSonucList = OgrenciCevaplariniDegerlendir();

            //okul ve şube karneleri için hesaplama yapar karne sonuç dosyasına yazar
            SubeDuzeyindeKarneSonuclariniKaydet(karneSonucList, yaz);

            //il ilçe kazanım ortalaması % değerlendirmesi
            List<IlIlceDogruYanlisBosModeli> ilIlceOrtalamalariList = IlIlceBasariHesapla(karneSonucList);

            IlIlceBasariKaydet(ilIlceOrtalamalariList, yaz);
            yaz.Close();

            OgrenciOrtalamalari();

            CKDataOlustur();

            toolSslKalanSure.Text = "Tamamlandı";
        }
        private void CKDataOlustur()
        {
            string raporUrl = raporDizinAdresi + @"\ckdata.ck";

            AyarlarManager ayarlar = new AyarlarManager();
            var ayar = ayarlar.AyarlariGetir();

            KutukManager kutukManager = new KutukManager();
            BransManager bransManager = new BransManager();
            DogruCevaplarManager dogruCevaplarManager = new DogruCevaplarManager();
            KazanimManager kazanimManager = new KazanimManager();
            KarneSonucManager karneSonucManager = new KarneSonucManager();
            
            var kutukList = kutukManager.List();
            var dogruCvplarList = dogruCevaplarManager.List();
            var karneSonucList = karneSonucManager.List();
            var bransList = bransManager.List();
            var kazanimlarList = kazanimManager.List();

            int kayitSayisi = kutukList.Count + dogruCvplarList.Count + bransList.Count + kazanimlarList.Count + karneSonucList.Count;
            int a = 0;
            progressBar1.Maximum = kayitSayisi;

            int sinavId = kutukList.First().SinavId; //Sınavın numarasını aldık

            StreamWriter yaz = new StreamWriter(raporUrl);

            yaz.WriteLine("{SinavAdi}|" + sinavId + "|" + ayar.SinavAdi + "|" +ayar.DegerlendirmeTuru);

            //Kütük
            foreach (var kutuk in kutukList)
            {
                a++;
                progressBar1.Value = a;
                toolSslKalanSure.Text = "Kütük tablosu hazırlanıyor.";
                yaz.WriteLine("{Kutuk}|" + kutuk.OpaqId + "|" + kutuk.IlceAdi + "|" + kutuk.KurumKodu + "|" +
                                   kutuk.KurumAdi + "|" + kutuk.OgrenciNo + "|" + kutuk.Adi + "|" + kutuk.Soyadi + "|" +
                                   kutuk.Sinifi + "|" + kutuk.Sube + "|" + kutuk.SinavId + "|" + kutuk.KatilimDurumu + "|" + kutuk.KitapcikTuru + "|" + kutuk.Cevaplar);

                sinavId = kutuk.SinavId;
            }

            //DogruCevaplar
            foreach (var dogruCevap in dogruCvplarList)
            {
                a++;
                progressBar1.Value = a;
                toolSslKalanSure.Text = "Doğru cevaplar tablosu hazırlanıyor.";

                yaz.WriteLine("{DogruCevaplar}|" + sinavId + "|" + dogruCevap.Sinif + "|" + dogruCevap.BransId + "|" + dogruCevap.KitapcikTuru + "|" + dogruCevap.Cevaplar);
            }

            //KarneSonuclari
            foreach (var sonuc in karneSonucList)
            {
                a++;
                progressBar1.Value = a;
                toolSslKalanSure.Text = "Karne sonuçları tablosu hazırlanıyor.";

                yaz.WriteLine("{KarneSonuclari}|" + sinavId + "|" + sonuc.BransId + "|" + sonuc.Ilce + "|" + sonuc.KurumKodu + "|" + sonuc.Sinif + "|" + sonuc.Sube + "|" + sonuc.KitapcikTuru + "|" + sonuc.SoruNo + "|" + sonuc.Dogru + "|" + sonuc.Yanlis + "|" + sonuc.Bos);
            }
            //Branslar
            foreach (var brans in bransList)
            {
                a++;
                progressBar1.Value = a;
                toolSslKalanSure.Text = "Branşlar tablosu hazırlanıyor.";

                yaz.WriteLine("{Branslar}|" + sinavId + "|" + brans.Id + "|" + brans.BransAdi);
            }
            //Kazanimlar
            foreach (var kazanim in kazanimlarList)
            {
                a++;
                progressBar1.Value = a;
                toolSslKalanSure.Text = "Kazanımlar tablosu hazırlanıyor.";
                yaz.WriteLine("{Kazanimlar}|" + kazanim.Id + "|" + sinavId + "|" + kazanim.Sinif + "|" + kazanim.BransId + "|" + kazanim.KazanimNo + "|" + kazanim.KazanimAdi + "|" + kazanim.KazanimAdiOgrenci + "|" + kazanim.Sorulari);
            }

            toolSslKalanSure.Text = "Dosyaya yazma işlemi tamamlanıyor.";
            yaz.Close();
            progressBar1.Value = 0;
        }
        private List<KarneSonuc> OgrenciCevaplariniDegerlendir()
        {

            Stopwatch watch = new Stopwatch();
            watch.Start();

            KutukManager kutukManager = new KutukManager();
            var ogrCevaplar = kutukManager.List();

            progressBar1.Maximum = ogrCevaplar.Count;
            progressBar1.Value = 0;

            DogruCevaplarManager dogruCevaplarDb = new DogruCevaplarManager();
            var dogruCevapList = dogruCevaplarDb.List();

            int a = 0;
            List<KarneSonuc> karneSonucList = new List<KarneSonuc>();
            foreach (var ogr in ogrCevaplar) //Öğrenci cevaplarını tek tek al
            {
                a++;
                progressBar1.Value = a;

                try
                {
                    toolSslKalanSure.Text = $"Öğrenci cevapları değerlendiriliyor. (1/4) {a} / {ogrCevaplar.Count} : " + ogrCevaplar.Count.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }

                if (!string.IsNullOrEmpty(ogr.KitapcikTuru) && ogr.KatilimDurumu != "0" && ogr.KurumKodu != 0)
                {
                    string[]
                        cevap = ogr.Cevaplar
                            .Split('#'); //öğreninin cevapları 2#BCAABCDACB#4#CAABDBACBA#1#AABDBCABDC#3#ACBDC#6#CBDAB#7#DACBB gibi

                    for (int i = 0; i < cevap.Length; i += 2)
                    {
                        int bransId = cevap[i].ToInt32();
                        string bransOgrenciCevap = cevap[i + 1].Replace(";","");

                        if (bransOgrenciCevap!="")
                        {
                            if (ogr.Sinifi == 8 && bransId == 3)
                                bransId = 5;

                            DogruCevap dcvp = dogruCevapList.FirstOrDefault(x =>
                                x.Sinif == ogr.Sinifi && x.BransId == bransId && x.KitapcikTuru == ogr.KitapcikTuru);
                            for (int j = 0; j < bransOgrenciCevap.Length; j++) //j = soru numarası
                            {
                                if (ogr.KatilimDurumu != "0") //0 ise katılmadı
                                {
                                    int soruNo = j + 1;
                                    int dogru = 0;
                                    int yanlis = 0;
                                    int bos = 0;

                                    if (bransOgrenciCevap.Substring(j, 1) == " ")
                                    {
                                        bos++;
                                    }
                                    else
                                    {
                                        if (bransOgrenciCevap.Substring(j, 1) == dcvp.Cevaplar.Substring(j, 1))
                                        {
                                            dogru++;
                                        }
                                        else
                                        {
                                            yanlis++;
                                        }
                                    }

                                    var kontrol = karneSonucList.Find(x =>
                                        x.BransId == bransId &&
                                        x.Ilce == ogr.IlceAdi &&
                                        x.KurumKodu == ogr.KurumKodu &&
                                        x.Sinif == ogr.Sinifi &&
                                        x.Sube == ogr.Sube &&
                                        x.SoruNo == soruNo &&
                                        x.KitapcikTuru == ogr.KitapcikTuru);

                                    KarneSonuc ks = new KarneSonuc()
                                    {
                                        Ilce = ogr.IlceAdi,
                                        BransId = bransId,
                                        KitapcikTuru = ogr.KitapcikTuru,
                                        KurumKodu = ogr.KurumKodu,
                                        Sinif = ogr.Sinifi,
                                        Sube = ogr.Sube,
                                        SoruNo = soruNo,
                                        Dogru = dogru,
                                        Yanlis = yanlis,
                                        Bos = bos
                                    };
                                    if (kontrol == null)
                                    {
                                        //Yeni kayıt ekle

                                        karneSonucList.Add(ks);
                                    }
                                    else
                                    {
                                        ks.Dogru += kontrol.Dogru;
                                        ks.Yanlis += kontrol.Yanlis;
                                        ks.Bos += kontrol.Bos;
                                        //güncelle
                                        karneSonucList.Remove(kontrol);

                                        karneSonucList.Add(ks);
                                    }
                                }

                                Application.DoEvents();
                            }
                            
                        }
                        Application.DoEvents();
                    }
                }

                Application.DoEvents();
            }
            progressBar1.Value = 0;

            return karneSonucList;
        }
        private void SubeDuzeyindeKarneSonuclariniKaydet(List<KarneSonuc> karneSonucList, StreamWriter yaz)
        {
            progressBar1.Maximum = karneSonucList.Count;
            int a = 0;

            Stopwatch watch = new Stopwatch();
            watch.Start();

            foreach (var ogr in karneSonucList)
            {
                a++;
                progressBar1.Value = a;

                try
                {
                    toolSslKalanSure.Text = $"Şube düzeyinde karne sonuçları kaydediliyor. (2/4) {a} / {karneSonucList.Count} : " + karneSonucList.Count.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }

                yaz.WriteLine("{KarneSonuclari}|" + ogr.BransId + "|" + ogr.Ilce + "|" + ogr.KurumKodu + "|" +
                              ogr.Sinif + "|" + ogr.Sube + "|" + ogr.KitapcikTuru + "|" + ogr.SoruNo + "|" + ogr.Dogru +
                              "|" + ogr.Yanlis + "|" + ogr.Bos);
            }
            watch.Stop();
        }
        private List<IlIlceDogruYanlisBosModeli> IlIlceBasariHesapla(List<KarneSonuc> karneSonucList)
        {
            List<IlIlceDogruYanlisBosModeli> ilIlceOrtalamalariList = new List<IlIlceDogruYanlisBosModeli>();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            KazanimManager kazanimManager = new KazanimManager();
            var kazanimList = kazanimManager.List();

            List<KarneSonuc> ilceler = karneSonucList.GroupBy(x => x.Ilce).Select(x => x.First()).ToList();
            List<KarneSonuc> siniflar = karneSonucList.GroupBy(x => x.Sinif).Select(x => x.First()).ToList();
            int islemSayisi = ilceler.Count * siniflar.Count;
            progressBar1.Maximum = islemSayisi;
            progressBar1.Value = 0;
            int a = 0;

            foreach (var ilce in ilceler)
            {
                foreach (var sinif in siniflar)
                {
                    a++;
                    progressBar1.Value = a;
                    try
                    {
                        toolSslKalanSure.Text = $"İl ilçe ortalaması hesaplanıyor. (3/4) {a} / {islemSayisi} : " + islemSayisi.KalanSureHesapla(a, watch);
                    }
                    catch (Exception)
                    {
                        toolSslKalanSure.Text = "Hesaplanıyor...";
                    }

                    var branslar = karneSonucList.Where(x => x.Sinif == sinif.Sinif).GroupBy(x => x.BransId).Select(x => x.First()).OrderBy(x => x.BransId).ToList();
                    foreach (var brans in branslar)
                    {
                        foreach (var kazanim in kazanimList.Where(x => x.BransId == brans.BransId && x.Sinif == brans.Sinif))
                        {
                            int ilDogruSayisi = 0;
                            int ilYanlisSayisi = 0;
                            int ilBosSayisi = 0;

                            int ilceDogruSayisi = 0;
                            int ilceYanlisSayisi = 0;
                            int ilceBosSayisi = 0;

                            //Kazanım soru numaralarının tutulduğu alanın sonundaki virgülü silmem gerekiyor. (B1,B10,BA12,A13, gibi olan verileri)
                            //Çünkü dizide hata veriyor.

                            string kazanimSorulariSonKarakteri = kazanim.Sorulari.Substring(kazanim.Sorulari.Length - 1, 1);
                            //eğer son karakter değeri virgül ise onu kaldır
                            string ks = kazanimSorulariSonKarakteri == ","
                                ? kazanim.Sorulari.Substring(0, kazanim.Sorulari.Length - 1)
                                : kazanim.Sorulari;

                            string[] kazanimSorulari = ks.Split(','); //B1,B10,BA12,A13 gibi olan verileri
                            foreach (var s in kazanimSorulari)
                            {
                                string kitapcikTuru = s.Substring(0, 1);
                                int soruNo = s.Substring(1, s.Length - 1).ToInt32();

                                ilDogruSayisi += karneSonucList.Where(x =>
                                    x.BransId == brans.BransId && x.Sinif == brans.Sinif && x.KitapcikTuru == kitapcikTuru &&
                                    x.SoruNo == soruNo).Sum(x => x.Dogru);
                                ilYanlisSayisi += karneSonucList.Where(x =>
                                    x.BransId == brans.BransId && x.Sinif == brans.Sinif && x.KitapcikTuru == kitapcikTuru &&
                                    x.SoruNo == soruNo).Sum(x => x.Yanlis);
                                ilBosSayisi += karneSonucList.Where(x =>
                                    x.BransId == brans.BransId && x.Sinif == brans.Sinif && x.KitapcikTuru == kitapcikTuru &&
                                    x.SoruNo == soruNo).Sum(x => x.Bos);

                                ilceDogruSayisi += karneSonucList.Where(x =>
                                    x.BransId == brans.BransId && x.Ilce == ilce.Ilce && x.Sinif == brans.Sinif &&
                                    x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                                ilceYanlisSayisi += karneSonucList.Where(x =>
                                    x.BransId == brans.BransId && x.Ilce == ilce.Ilce && x.Sinif == brans.Sinif &&
                                    x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                                ilceBosSayisi += karneSonucList.Where(x =>
                                    x.BransId == brans.BransId && x.Ilce == ilce.Ilce && x.Sinif == brans.Sinif &&
                                    x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);
                            }

                            int ilOgrenciSayisi = ilDogruSayisi + ilYanlisSayisi + ilBosSayisi;

                            int ilBasariYuzdesi = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();

                            int ilceOgrenciSayisi = ilceDogruSayisi + ilceYanlisSayisi + ilceBosSayisi;

                            int ilceBasariYuzdesi = (((double)ilceDogruSayisi * 100) / ilceOgrenciSayisi).ToInt32();

                            ilIlceOrtalamalariList.Add(new IlIlceDogruYanlisBosModeli(brans.BransId, ilce.Ilce, brans.Sinif, kazanim.Id, ilceDogruSayisi, ilceYanlisSayisi, ilceBosSayisi,
                                ilBasariYuzdesi, ilceBasariYuzdesi));
                        }
                    }
                }
            }
            watch.Stop();

            return ilIlceOrtalamalariList;
        }
        private void IlIlceBasariKaydet(List<IlIlceDogruYanlisBosModeli> ilIlceOrtalamalariList, StreamWriter yaz)
        {
            int islemSayisi = ilIlceOrtalamalariList.Count();
            progressBar1.Maximum = islemSayisi;
            progressBar1.Value = 0;
            int a = 0;

            Stopwatch watch = new Stopwatch();
            watch.Start();

            foreach (var ort in ilIlceOrtalamalariList)
            {
                a++;
                progressBar1.Value = a;
                try
                {
                    toolSslKalanSure.Text = $"İl ilçe ortalaması kaydediliyor. (4/4) {a} / {islemSayisi} : " + islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }

                yaz.WriteLine("{IlceOrtalamasi}|" + ort.Ilce + "|" + ort.BransId + "|" + ort.Sinif + "|" + ort.Dogru + "|" + ort.Yanlis + "|" + ort.Bos + "|" + ort.KazanimId + "|" + ort.IlBasariYuzdesi + "|" + ort.IlceBasariYuzdesi);
                Application.DoEvents();
            }

            progressBar1.Value = 0;

        }

        #endregion

        private void btnOkulRapor_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofData = new OpenFileDialog())
            {
                ofData.Reset();
                ofData.ReadOnlyChecked = true;
                ofData.Multiselect = true;
                ofData.ShowReadOnly = true;
                ofData.Filter = "Karne Sonuç (*.txt;*.dat)|*.ck;*.txt;*.dat";
                ofData.Title = "Karne Sonuç dosyasını seçiniz.";
                ofData.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofData.CheckPathExists = true;
                if (ofData.ShowDialog() == DialogResult.OK)
                {
                    dosyaAdi = ofData.FileName;

                    using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                    {
                        folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı kapat
                        folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                        folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        folderDialog.Description = @"Rapor dosyalarının saklanacağı dizini seçiniz.";
                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            raporDizinAdresi = folderDialog.SelectedPath + "\\";

                            bgwOkulOrtalamalari.RunWorkerAsync();
                        }
                        folderDialog.Dispose();
                    }

                }

                ofData.Dispose();
            }
        }
      
        private void OkulRaporKaydet(List<KarneSonuc> karneSonucList)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<KarneSonuc> kurumlar = karneSonucList.GroupBy(x => x.KurumKodu).Select(x => x.First()).ToList();

            int islemSayisi = kurumlar.Count;
            progressBar1.Maximum = islemSayisi;
            progressBar1.Value = 0;
            int a = 0;

            StreamWriter yaz = new StreamWriter(raporDizinAdresi + "OkulOrtalamalari.txt");

            KutukManager kutukManager = new KutukManager();
            var kutukList = kutukManager.List();

            foreach (var okul in kurumlar)
            {
                a++;
                progressBar1.Value = a;
                try
                {
                    toolSslKalanSure.Text =
                        $"(2/2) {a} / {islemSayisi} : " + islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }

                var kutuk = kutukList.Find(x => x.KurumKodu == okul.KurumKodu);

                List<KarneSonuc> siniflar = karneSonucList.Where(x => x.KurumKodu == okul.KurumKodu)
                    .GroupBy(x => x.Sinif).Select(x => x.First()).ToList();

                foreach (var sinif in siniflar)
                {

                    int ogrenciSayisi = 0;
                    string bransDegerlendirme = "";

                    var branslar = karneSonucList.Where(x => x.Sinif == sinif.Sinif).GroupBy(x => x.BransId).Select(x => x.First()).OrderBy(x => x.BransId).ToList();
                    foreach (var brans in branslar)
                    {
                        int sinifDogru = karneSonucList.Where(x =>
                            x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif && x.SoruNo == 1 &&
                            x.BransId == brans.BransId).Sum(x => x.Dogru);
                        int siniYanlis = karneSonucList.Where(x =>
                            x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif && x.SoruNo == 1 &&
                            x.BransId == brans.BransId).Sum(x => x.Yanlis);
                        int sinifBos = karneSonucList.Where(x =>
                            x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif && x.SoruNo == 1 &&
                            x.BransId == brans.BransId).Sum(x => x.Bos);

                        ogrenciSayisi = sinifDogru + siniYanlis + sinifBos;


                        int dogruSayisi = karneSonucList.Where(x => x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif && x.BransId == brans.BransId).Sum(x => x.Dogru);
                        int yanlisSayisi = karneSonucList.Where(x => x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif && x.BransId == brans.BransId).Sum(x => x.Yanlis);
                        int bosSayisi = karneSonucList.Where(x => x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif && x.BransId == brans.BransId).Sum(x => x.Bos);

                        //double ortalamaDogru = dogruSayisi / ogrenciSayisi;
                        //double ortalamaYanlis = yanlisSayisi / ogrenciSayisi;
                        //double ortalamaBos = bosSayisi / ogrenciSayisi;

                        bransDegerlendirme += brans.BransId + "|" + dogruSayisi + "|" + yanlisSayisi + "|" + bosSayisi +
                                              "|";
                        //kurumkodu, ilçe , sınıf ,öğrenci sayısı, dogru yanlış boş
                    }

                    yaz.WriteLine("{OkulRapor}|" + okul.Ilce + "|" + okul.KurumKodu + "|" + kutuk.KurumAdi + "|" +
                                  sinif.Sinif + "|" + bransDegerlendirme + ogrenciSayisi);
                }
            }

            yaz.Close();
            toolSslKalanSure.Text = "Tamamlandı";
        }

       
       
      

        private void OgrenciOrtalamalari()
        {
            List<OgrenciSonucModel> karneSonucList = new List<OgrenciSonucModel>();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            StreamWriter yaz = new StreamWriter(raporDizinAdresi + "OgrenciOrtalamasi.txt");

            KutukManager kutukManager = new KutukManager();
            var ogrCevaplar = kutukManager.List();

            progressBar1.Maximum = ogrCevaplar.Count;
            progressBar1.Value = 0;

            DogruCevaplarManager dogruCevaplarDb = new DogruCevaplarManager();
            var dogruCevapList = dogruCevaplarDb.List();

            int a = 0;

            foreach (var ogr in ogrCevaplar) //Öğrenci cevaplarını tek tek al
            {
                a++;
                progressBar1.Value = a;

                try
                {
                    toolSslKalanSure.Text = $"Öğrenci cevapları değerlendiriliyor. {a} / {ogrCevaplar.Count} : " +
                                            ogrCevaplar.Count.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }

                if (!string.IsNullOrEmpty(ogr.KitapcikTuru) && ogr.KatilimDurumu != "0" && ogr.KurumKodu != 0)
                {
                    string[]
                        cevap = ogr.Cevaplar
                            .Split('#'); //öğreninin cevapları 2#BCAABCDACB#4#CAABDBACBA#1#AABDBCABDC#3#ACBDC#6#CBDAB#7#DACBB gibi

                    //Branş branş cevapları alır
                    for (int i = 0; i < cevap.Length; i += 2)
                    {
                        int bransId = cevap[i].ToInt32();
                        string bransOgrenciCevap = cevap[i + 1].Replace(";", ""); //öğrencinin cevabı

                        if (ogr.Sinifi == 8 && bransId == 3)
                            bransId = 5;

                        int dogru = 0;
                        int yanlis = 0;
                        int bos = 0;
                        DogruCevap dcvp = dogruCevapList.FirstOrDefault(x =>
                            x.Sinif == ogr.Sinifi && x.BransId == bransId && x.KitapcikTuru == ogr.KitapcikTuru);
                        for (int j = 0; j < bransOgrenciCevap.Length; j++) //j = soru numarası
                        {
                            if (ogr.KatilimDurumu != "0") //sınava girmiş ise. 0 ise katılmadı
                            {
                                if (bransOgrenciCevap.Substring(j, 1) == " ")
                                {
                                    bos++;
                                }
                                else
                                {
                                    if (bransOgrenciCevap.Substring(j, 1) == dcvp.Cevaplar.Substring(j, 1))
                                    {
                                        dogru++;
                                    }
                                    else
                                    {
                                        yanlis++;
                                    }
                                }
                            }

                            Application.DoEvents();
                        }

                        karneSonucList.Add(new OgrenciSonucModel(ogr.OpaqId, ogr.IlceAdi, ogr.KurumKodu, ogr.KurumAdi, bransId,
                            ogr.Sinifi, ogr.Sube, ogr.KitapcikTuru, ogr.OgrenciNo, ogr.Adi, ogr.Soyadi, ogr.KatilimDurumu,
                            dogru, yanlis, bos));

                        Application.DoEvents();
                    }
                }

                Application.DoEvents();
            }

            progressBar1.Value = 0;
            watch.Restart();

            a = 0;
            var ogrenciList = karneSonucList.GroupBy(x => x.OpaqId).Select(x => x.First()).ToList();
            int islemSayisi = ogrenciList.Count;
            progressBar1.Maximum = islemSayisi;
            //öğreni doğru yanlış sayıları
            foreach (var ogr in ogrenciList)
            {
                a++;
                progressBar1.Value = a;

                try
                {
                    toolSslKalanSure.Text = $"Öğrenci doğru yanlış sayıları hesaplanıyor. {a} / {islemSayisi} : " +
                                            islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }

                string ogrenciSonucStr = "";
                var branslar = karneSonucList.Where(x => x.OpaqId == ogr.OpaqId).GroupBy(x => x.BransId).Select(x => x.First())
                    .OrderBy(x => x.BransId).ToList();
                foreach (var brans in branslar)
                {
                    ogrenciSonucStr += brans.BransId + "|" + brans.Dogru + "|" + brans.Yanlis + "|" + brans.Bos + "|";
                }

                yaz.WriteLine("{OgrenciDYB}|" + ogr.Ilce + "|" + ogr.KurumKodu + "|" + ogr.KurumAdi + "|" + ogr.Adi + "|" +
                              ogr.Soyadi + "|" + ogr.OgrenciNo + "|" + ogr.Sinif + "|" + ogr.Sube + "|" + ogr.KitapcikTuru +
                              "|" + ogr.KatilimDurumu + "|" + ogrenciSonucStr);
            }

            watch.Restart();

            a = 0;
            var okullar = karneSonucList.GroupBy(x => x.KurumKodu).Select(x => x.First()).ToList();
            islemSayisi = okullar.Count;
            progressBar1.Maximum = islemSayisi;

            //okulların  doğru yanlış sayıları
            foreach (var okul in okullar)
            {
                a++;
                progressBar1.Value = a;

                try
                {
                    toolSslKalanSure.Text = $"Okul doğru yanlış sayıları hesaplanıyor. {a} / {islemSayisi} : " +
                                            islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }


                var siniflar = karneSonucList.Where(x => x.KurumKodu == okul.KurumKodu).GroupBy(x => x.Sinif)
                    .Select(x => x.First()).ToList();

                foreach (var sinif in siniflar)
                {
                    int ogrSayisi = karneSonucList.Where(x => x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif)
                        .GroupBy(x => x.OpaqId).Select(x => x.First()).Count();
                    var branslar = karneSonucList.Where(x => x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif)
                        .GroupBy(x => x.BransId).Select(x => x.First()).OrderBy(x => x.BransId).ToList();
                    string sinifSonucStr = "";
                    foreach (var brans in branslar)
                    {
                        int okulDogru = 0;
                        int okulYanlis = 0;
                        int okulBos = 0;

                        foreach (var ogr in karneSonucList.Where(x =>
                            x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif && x.BransId == brans.BransId).ToList())
                        {
                            okulDogru += ogr.Dogru;
                            okulYanlis += ogr.Yanlis;
                            okulBos += ogr.Bos;

                            //öğrenci şube ilçe okul il ortalamaları çıkarılacak
                        }

                        sinifSonucStr += brans.BransId + "|" + okulDogru + "|" + okulYanlis + "|" + okulBos + "|";
                    }

                    yaz.WriteLine("{SinifDYB}|" + sinif.Ilce + "|" + okul.KurumKodu + "|" + okul.KurumAdi + "|" + sinif.Sinif +
                                  "|" + ogrSayisi + "|" + sinifSonucStr);
                }
            }

            a = 0;
            var ilceler = karneSonucList.GroupBy(x => x.Ilce).Select(x => x.First()).ToList();
            islemSayisi = ilceler.Count;
            progressBar1.Maximum = islemSayisi;
            watch.Restart();


            //ilçe doğru yanlış sayıları
            foreach (var ilce in ilceler)
            {
                a++;
                progressBar1.Value = a;

                try
                {
                    toolSslKalanSure.Text = $"İlçe doğru yanlış sayıları hesaplanıyor. {a} / {islemSayisi} : " +
                                            islemSayisi.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                }

                var siniflar = karneSonucList.Where(x => x.Ilce == ilce.Ilce).GroupBy(x => x.Sinif).Select(x => x.First())
                    .ToList();

                foreach (var sinif in siniflar)
                {
                    int ogrSayisi = karneSonucList.Where(x => x.Ilce == ilce.Ilce && x.Sinif == sinif.Sinif)
                        .GroupBy(x => x.OpaqId).Select(x => x.First()).Count();
                    var branslar = karneSonucList.Where(x => x.Sinif == sinif.Sinif).GroupBy(x => x.BransId)
                        .Select(x => x.First()).OrderBy(x => x.BransId).ToList();
                    string ilceSonucStr = "";
                    foreach (var brans in branslar)
                    {
                        int okulDogru = 0;
                        int okulYanlis = 0;
                        int okulBos = 0;

                        foreach (var ogr in karneSonucList
                            .Where(x => x.Ilce == ilce.Ilce && x.Sinif == sinif.Sinif && x.BransId == brans.BransId).ToList())
                        {
                            okulDogru += ogr.Dogru;
                            okulYanlis += ogr.Yanlis;
                            okulBos += ogr.Bos;

                            //öğrenci şube ilçe okul il ortalamaları çıkarılacak
                        }

                        ilceSonucStr += brans.BransId + "|" + okulDogru + "|" + okulYanlis + "|" + okulBos + "|";
                    }

                    yaz.WriteLine("{IlceDYB}|" + sinif.Ilce + "|" + sinif.Sinif + "|" + ogrSayisi + "|" + ilceSonucStr);
                }
            }

            watch.Stop();

            yaz.Close();
            yaz.Dispose();
            toolSslKalanSure.Text = "Tamamlandı";
            progressBar1.Value = 0;
        }
    }
}
