using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.Library;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb
{
    public partial class FormDegerlendirmeKarne : Form
    {
        enum KolonSayisi
        {
            Il = 4,
            Ilce = 6,
            Okul = 7,
            Sube = 8
        }
        private string seciliDizin;
        private string[] lines;
        private readonly CevapTxtManager cvpTxtManager = new CevapTxtManager();
        private readonly DogruCevaplarManager dogruCevaplarManager = new DogruCevaplarManager();
        private readonly KutukManager kutukManager = new KutukManager();
        private readonly KazanimManager kazanimManager = new KazanimManager();
        private readonly KarneSonucManager karneSonucManager = new KarneSonucManager();
        public FormDegerlendirmeKarne()
        {
            InitializeComponent();
        }

        private void btnDataAc_Click(object sender, EventArgs e)
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

                    cvpTxtManager.TumunuSil();

                    lines = File.ReadAllLines(ofData.FileName, Encoding.UTF8);

                    backgroundWorker1.RunWorkerAsync(argument: lines);

                }

                ofData.Dispose();
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            btnDegerlendir.Enabled = btnDataAc.Enabled = false;


            bool mukerrer = false;
            List<string> opakKontrol = new List<string>();
            //Önce mükerrer opq var mı kontrol edelim

            string dosya_yolu = Application.StartupPath + @"\mukerrer_opak_kontrol.txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);


            sw.WriteLine("----------- MÜKERRER OLAN OPAK IDLER -----------");

            progressBar1.Maximum = lines.Length;
            progressBar1.Value = 0;
            int a = 0;

            foreach (string file in lines)
            {
                a++;
                progressBar1.Value = a;
                string[] cevapBilgisi = file.Split('#');
                string opaqId = cevapBilgisi[0].Trim();

                int kontrol = opakKontrol.Count(x => x.Equals(opaqId));
                if (kontrol != 0) //mükerrer değil
                {
                    sw.WriteLine(opaqId);

                    mukerrer = true;
                }

            }
            progressBar1.Value = 0;


            sw.Flush();
            sw.Close();
            fs.Close();

            if (mukerrer)
            {
                Process.Start("explorer.exe", Path.GetFileName(dosya_yolu));

                MessageBox.Show(opakKontrol.Count + " adet mükkerrer Opaq olduğu için değerlendirme yapılmadı.");
            }

            if (mukerrer == false)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                int islemSayisi = lines.Length;
                progressBar1.Maximum = islemSayisi;
                progressBar1.Value = 0;
                a = 0;
                var rangePartitioner = System.Collections.Concurrent.Partitioner.Create(0, islemSayisi);

                List<CevapTxt> cevapTxtsList = new List<CevapTxt>();

                Parallel.ForEach(rangePartitioner, (range, loopState) =>
                {
                    // Loop over each range element without a delegate invocation.
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        try
                        {
                            toolSslKalanSure.Text = "(1/2) " + islemSayisi.KalanSureHesapla(a, watch);
                        }
                        catch (Exception)
                        {
                            toolSslKalanSure.Text = "Hesaplanıyor...";
                        }
                        //20649649#A#1#2#CBBBBBACACBABCBAACAB#2#CBBBBBACACBABCBAACAB
                        //OPAKNO/TC#KTUR#KD#DERSID#CEVAPLAR#DERSID#CEVAPLAR

                        Dictionary<int, string> cvpDic = new Dictionary<int, string>();

                        string[] cevapBilgisi = lines[i].Split('#');

                        string opaqStr = cevapBilgisi[0].Trim();
                        long opaqId = opaqStr.ToInt64();
                        string kitapcikTuru = cevapBilgisi[1].Trim();
                        int katilimDurumu = cevapBilgisi[2].ToInt32(); //KD: 0=KATILMADI  1=KATILDI


                        //bir seferliğine kod kasım 2019 için
                        //Kutuk kutuk = kutukManager.List().FirstOrDefault(x => x.OpaqId == opaqId);
                        //if (kutuk!=null)
                        //{

                        //5. bölümden sonra ders kodları ve cevaplar alınacak.
                        //Tüm cevapları Dictionary sınıfından bir diziye alalım.
                        for (int t = 3; t < cevapBilgisi.Length; t += 2)
                        {
                            //bir seferliğine kod kasım 2019 için
                            int dersKodu = cevapBilgisi[t].ToInt32();
                            //if (dersKodu == 3 && kutuk.Sinifi == 8)
                            //    dersKodu = 5;
                            //bir seferliğine kod kasım 2019 son

                            string cevaplar = cevapBilgisi[t + 1];
                            cvpDic.Add(dersKodu, cevaplar);
                            Application.DoEvents();
                        }

                        //cevap sayısınca öğrenci adına kayıt yapalım
                        foreach (var c in cvpDic)
                        {
                            cevapTxtsList.Add(new CevapTxt(opaqId, c.Key, kitapcikTuru, katilimDurumu, c.Value));

                            Application.DoEvents();
                        }
                        Application.DoEvents();

                        //}

                        a++;
                        progressBar1.Value = a;
                        lblBilgi.Text = a + " cevap okundu.";
                    }
                });
                islemSayisi = cevapTxtsList.Count;
                progressBar1.Maximum = islemSayisi;
                progressBar1.Value = 0;
                a = 0;
                foreach (var txt in cevapTxtsList)
                {
                    try
                    {
                        toolSslKalanSure.Text = "(2/2) " + islemSayisi.KalanSureHesapla(a, watch);
                    }
                    catch (Exception)
                    {
                        toolSslKalanSure.Text = "Hesaplanıyor...";
                    }
                    lblBilgi.Text = "Cevaplar kaydediliyor. " + a + " / " + cevapTxtsList.Count;
                    a++;
                    progressBar1.Value = a;
                    CevapTxt cvp = new CevapTxt()
                    {
                        OpaqId = txt.OpaqId,
                        BransId = txt.BransId,
                        KitapcikTuru = txt.KitapcikTuru,
                        KatilimDurumu = txt.KatilimDurumu,
                        Cevaplar = txt.Cevaplar
                    };
                    cvpTxtManager.Insert(cvp);
                }
                //foreach (string file in lines)
                //{
                //    try
                //    {
                //        toolSslKalanSure.Text = islemSayisi.KalanSureHesapla(a, watch);
                //    }
                //    catch (Exception)
                //    {
                //        toolSslKalanSure.Text = "Hesaplanıyor...";
                //    }
                //    //529#20649649#A#0#1#2#CBBBBBACACBABCBAACAB#2#CBBBBBACACBABCBAACAB
                //    //SINAVKODU#OPAKNO/TC#KTUR#CTIP#KD#DERSID#CEVAPLAR#DERSID#CEVAPLAR

                //    Dictionary<int, string> cvpDic = new Dictionary<int, string>();

                //    string[] cevapBilgisi = file.Split('#');

                //    string opaqStr = cevapBilgisi[1].Trim();
                //    // int sinavId = cevapBilgisi[0].Trim().ToInt32();
                //    long opaqId = opaqStr.ToInt64();
                //    string kitapcikTuru = cevapBilgisi[2].Trim();
                //    int katilimDurumu = cevapBilgisi[3].ToInt32(); //KD: 0=KATILMADI  1=KATILDI


                //    //bir seferliğine kod kasım 2019 için
                //    //Kutuk kutuk = kutukManager.List().FirstOrDefault(x => x.OpaqId == opaqId);
                //    //if (kutuk!=null)
                //    //{

                //    //5. bölümden sonra ders kodları ve cevaplar alınacak.
                //    //Tüm cevapları Dictionary sınıfından bir diziye alalım.
                //    for (int i = 4; i < cevapBilgisi.Length; i += 2)
                //    {
                //        //bir seferliğine kod kasım 2019 için
                //        int dersKodu = cevapBilgisi[i].ToInt32();
                //        //if (dersKodu == 3 && kutuk.Sinifi == 8)
                //        //    dersKodu = 5;
                //        //bir seferliğine kod kasım 2019 son

                //        string cevaplar = cevapBilgisi[i + 1];
                //        cvpDic.Add(dersKodu, cevaplar);
                //    }

                //    //cevap sayısınca öğrenci adına kayıt yapalım
                //    foreach (var c in cvpDic)
                //    {
                //        CevapTxt cvp = new CevapTxt()
                //        {
                //            SinavId = 0,
                //            OpaqId = opaqId,
                //            BransId = c.Key,
                //            KitapcikTuru = kitapcikTuru,
                //            CevapTipi = 0,
                //            KatilimDurumu = katilimDurumu,
                //            Cevaplar = c.Value
                //        };
                //        cvpTxtManager.Insert(cvp);
                //    }
                //    Application.DoEvents();

                //    //}

                //    a++;
                //    progressBar1.Value = a;
                //    lblBilgi.Text = a + " cevap okundu.";
                //}
                Application.DoEvents();

                progressBar1.Value = 0;

                watch.Stop();
                toolSslKalanSure.Text = "Tamamlandı...";
            }
            btnDegerlendir.Enabled = btnDataAc.Enabled = true;
        }


        private void btnDegerlendir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı aç
                folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderDialog.SelectedPath =
                    Environment.SpecialFolder.Desktop
                        .ToString(); //başlangıç dizini programın bulunduğu dizin => AppDomain.CurrentDomain.BaseDirectory
                folderDialog.Description = @"Karnelerin saklanacağı dizini seçiniz.";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    seciliDizin = folderDialog.SelectedPath;

                    bgwDegerlendir.RunWorkerAsync();

                }
                folderDialog.Dispose();
            }
        }

        private void bgwDegerlendir_DoWork(object sender, DoWorkEventArgs e)
        {
            btnDegerlendir.Enabled = btnDataAc.Enabled = false;

            /*
             * CevapTxt tablosundaki öğrenciler kütük tablosunda var mı?
             * DoğruCevaplar tablosunu kontrol et doğru cevaplar yüklenmiş mi?
             * CevapTxt tablosunu kontrol et öğrenci cevapları yüklenmiş mi?
             * Kazanımlar tablosunu kontrol et kazanımlar ve ilişkili sorular yüklenmiş mi?
             *
             * Sonra
             *
             * cevaptxt tablosunda öğrenci cevabını al. Kütükte bilgilerini bul.
             * cevaplarını cevaplar tablosundan kontrol et. doğru yanlış boş olarak al.
             * kazanım tablosunda ilgili kazanımın Id değerini al.
             *
             * Karne sonuçları tablosunda kontrol et. Kayıt varsa 1 artır yoksa yeni kayıt ekle
             *
             */


            List<ResultList> result = new List<ResultList>();
            var kutukList = kutukManager.List();
            var cevapTxtlerList = cvpTxtManager.List().Where(x => x.BransId == 2 && x.KitapcikTuru == "A").ToList();
            var dogruCevaplarList = dogruCevaplarManager.ListAsync();


            //if (cevapTxtlerList.Count == 0)
            //    result.Add(new ResultList($"cvptxt", $"Öğrenci cevapları yüklenmemiş"));
            //else
            //{

            //    int a = 0;
            //    progressBar1.Value = 0;
            //    progressBar1.Maximum = cevapTxtlerList.Count;

            //    ////Gerekli kayıtlar oluşturulmuş mu kontrol edelim.
            //    //foreach (var ctx in cevapTxtlerList)
            //    //{
            //    //    a++;
            //    //    progressBar1.Value = a;

            //    //    Kutuk ogrenci = kutukList.Find(x => x.OpaqId == ctx.OpaqId);
            //    //    if (ogrenci!=null)
            //    //    {
            //    //        DogruCevap cevaplar = dogruCevaplarManager.Find(x => x.Sinif == ogrenci.Sinifi && x.BransId == ctx.BransId);
            //    //        Kazanim kazanim = kazanimManager.Find(x => x.BransId == ctx.BransId);


            //    //        // DoğruCevaplar tablosunu kontrol et doğru cevaplar yüklenmiş mi?
            //    //        if (cevaplar == null)
            //    //        {
            //    //            //Daha önce bu branş için uyarı yapılmış mı?
            //    //            int query = result.Count(x => x.Key == $"cvp-{ctx.BransId}");

            //    //            if (query == 0)
            //    //                result.Add(new ResultList($"cvp-{ctx.BransId}", $"{ctx.BransId.DersAdi()} için doğru cevaplar bulunamadı."));
            //    //        }

            //    //        //Kazanımlar tablosunu kontrol et kazanımlar ve ilişkili sorular yüklenmiş mi?
            //    //        if (kazanim == null)
            //    //        {
            //    //            //Daha önce bu branş için uyarı yapılmış mı?
            //    //            int query = result.Count(x => x.Key == $"kznm-{ctx.BransId}");

            //    //            if (query == 0)
            //    //                result.Add(new ResultList($"kznm-{ctx.BransId}", $"{ctx.BransId.DersAdi()} branşı kazanımları bulunamadı."));
            //    //        }
            //    //    }
            //    //    //else
            //    //    //{
            //    //    //    //* CevapTxt tablosundaki öğrenciler kütük tablosunda var mı?
            //    //    //    //Daha önce bu öğrenci için uyarı yapılmış mı?
            //    //    //    int query = result.Count(x => x.Key == $"ktk-{ctx.OpaqId}");

            //    //    //    if (query == 0)
            //    //    //        result.Add(new ResultList($"ktk-{ctx.OpaqId}", $"{ctx.OpaqId} nolu öğrenci bulunamadı."));
            //    //    //}
            //    //}

            //    progressBar1.Value = 0;
            //}

            //if (result.Count > 0)
            //{
            //    string uyariMesaji = "";
            //    foreach (var r in result.OrderBy(x => x.Key))
            //    {
            //        uyariMesaji += r.Result + "\n";
            //    }

            //    MessageBox.Show(uyariMesaji, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}
            //else
            //{
            //Gerekli (doğru cevaplar kazanımlar kütük bilgileri) kayıtlar oluşturulmuş ise kazanım sonuç tablosunu doldur
            KarneSonuclariniIsle(result, cevapTxtlerList, kutukList, dogruCevaplarList);

            //    //karne sonuçları işlendikten sonra karneleri oluşturmaya başla
            //    KarneOlusturmaOlayi();
            //}

            btnDegerlendir.Enabled = btnDataAc.Enabled = true;
        }



        private void KarneSonuclariniIsle(List<ResultList> result, List<CevapTxt> cevapTxtlerList, List<Kutuk> kutukList, Task<List<DogruCevap>> dogruCevaplarList)
        {
            //Önce tüm eski kayıtları sil
            //   karneSonucManager.TumunuSil();
            //Uyarılar için uyarı dizisini temizle
            result.Clear();


            int a = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = cevapTxtlerList.Count;

            /*
                 * Sonra
                 * cevaptxt tablosunda öğrenci cevabını al. Kütükte bilgilerini bul.
                 * cevaplarını doğrucevaplar tablosundan kontrol et. doğru yanlış boş olarak al.
                 * kazanım tablosunda ilgili kazanımın Id değerini al.
                 *
                 * Karne sonuçları tablosunda kontrol et. Kayıt varsa 1 artır yoksa yeni kayıt ekle
                 */
                 Stopwatch watch = new Stopwatch();
                 watch.Start();
            foreach (var cvp in cevapTxtlerList)
            {
                a++;
                progressBar1.Value = a;
                lblBilgi.Text = $"{a}/{cevapTxtlerList.Count} kayıt değerlendirildi.";

                try
                {
                    lblBilgi.Text = $"{a} / {cevapTxtlerList.Count} : " + cevapTxtlerList.Count.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    lblBilgi.Text = "Hesaplanıyor...";
                }


                //Kütükte bilgilerini bul.
                Kutuk kutuk = kutukList.FirstOrDefault(x => x.OpaqId == cvp.OpaqId);
                if (kutuk == null)
                {
                    //Daha önce bu öğrenci için uyarı yapılmış mı?
                    int query = result.Count(x => x.Key == $"ktk-{cvp.OpaqId}");

                    if (query == 0)
                        result.Add(new ResultList($"ktk-{cvp.OpaqId}",
                            $"{cvp.OpaqId} nolu öğrenci kütük listesinde bulunamadı."));
                }
                else
                {
                    // doğrucevaplar tablosundan kontrol et. doğru yanlış boş olarak al.
                    DogruCevap dogruCevap =
                        dogruCevaplarList.Result.FirstOrDefault(x =>
                            x.Sinif == kutuk.Sinifi && x.BransId == cvp.BransId && x.KitapcikTuru == cvp.KitapcikTuru);

                    if (dogruCevap == null)
                    {
                        //Daha önce bu cevap için uyarı yapılmış mı?
                        int query = result.Count(x => x.Key == $"cvp-{cvp.BransId}-{cvp.KitapcikTuru}");

                        if (query == 0)
                            result.Add(new ResultList($"cvp-{cvp.BransId}-{cvp.KitapcikTuru}",
                                $"{cvp.BransId.DersAdi()} branşı {cvp.KitapcikTuru} kitapçığı için doğru cevaplar bulunamadı."));
                    }
                    else
                    {
                        //cevaplarını doğrucevaplar tablosundan kontrol et. doğru yanlış boş olarak al.
                        //Karne Sonuçları tablosuna ekle
                        //Karne sonuçları tablosunda şube bazında tek kayıt girmeli 
                        //sinav branş okul sinif şube soru kitapçık türü birlikte tek kayıt olmalı

                        string ogrenciCevabi = cvp.Cevaplar;

                        for (int i = 0; i < ogrenciCevabi.Length; i++)
                        {
                            int soruNo = i + 1;
                            KarneSonuc karneSonuc = karneSonucManager.Find(x =>
                               x.BransId == cvp.BransId &&
                                x.Ilce == kutuk.IlceAdi &&
                                x.KurumKodu == kutuk.KurumKodu &&
                                x.Sinif == kutuk.Sinifi &&
                                x.Sube == kutuk.Sube &&
                                x.SoruNo == soruNo &&
                                x.KitapcikTuru == cvp.KitapcikTuru);
                            int dogru = 0;
                            int yanlis = 0;
                            int bos = 0;
                            if (ogrenciCevabi.Substring(i, 1) == " ")
                            {
                                bos++;
                            }
                            else if (ogrenciCevabi.Substring(i, 1) == dogruCevap.Cevaplar.Substring(i, 1))
                            {
                                dogru++;
                            }
                            else
                            {
                                yanlis++;
                            }

                            if (karneSonuc == null)
                            {
                                //Yeni kayıt ekle
                                KarneSonuc ks = new KarneSonuc()
                                {
                                    Ilce = kutuk.IlceAdi,
                                    BransId = cvp.BransId,
                                    KitapcikTuru = cvp.KitapcikTuru,
                                    KurumKodu = kutuk.KurumKodu,
                                    Sinif = kutuk.Sinifi,
                                    Sube = kutuk.Sube,
                                    SoruNo = soruNo,
                                    Dogru = dogru,
                                    Yanlis = yanlis,
                                    Bos = bos
                                };
                                karneSonucManager.Insert(ks);
                            }
                            else
                            {
                                karneSonuc.Dogru += dogru;
                                karneSonuc.Yanlis += yanlis;
                                karneSonuc.Bos += bos;
                                //güncelle
                                karneSonucManager.Update(karneSonuc);
                            }

                            GC.SuppressFinalize(karneSonucManager);
                            if (karneSonuc != null)
                                GC.SuppressFinalize(karneSonuc);
                            Application.DoEvents();
                        }


                    }
                }

                Application.DoEvents();

                GC.SuppressFinalize(cvp);
            }
            watch.Stop();
            
            progressBar1.Value = 0;
        }
        private void KarneOlusturmaOlayi()
        {

            List<KarneSonuc> karneSonucList = karneSonucManager.List();

            AyarlarManager ayarlarManager = new AyarlarManager();
            Ayarlar ayar = ayarlarManager.AyarlariGetir();

            var branslar = karneSonucList.GroupBy(x => x.BransId).Select(x => x.First()).OrderBy(x => x.BransId).ToList();
            int a = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = branslar.Count;



            foreach (var brans in branslar)
            {
                a++;
                progressBar1.Value = a;
                //İL KARNESİ
                string pathStart = $@"{seciliDizin}\Karneler\";
                if (!DizinIslemleri.DizinKontrol(pathStart))
                    DizinIslemleri.DizinOlustur(pathStart);

                var siniflar = karneSonucList.Where(x => x.BransId == brans.BransId).GroupBy(x => x.Sinif).Select(x => x.First()).OrderBy(x => x.Sinif).ToList();
                foreach (var sinif in siniflar)
                {
                    lblBilgi.Text = $"{sinif.Sinif}. sınıf {brans.BransId.DersAdi()} dersi il karnesi oluşturuluyor";

                    string path = $@"{pathStart}\{ayar.IlAdi.ToUpper()} - {brans.BransId.DersAdi()} - {sinif.Sinif}. Sınıf İl Karnesi.pdf";
                    string sonSatirBaslik = $"{ayar.IlAdi.ToUpper()} İL KARNESİ";
                    KarneModel karneModel = KarneModeliOlustur(sinif.Sinif, brans.BransId, ayar);

                    Document doc = new Document();
                    PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                    doc.Open();

                    PdfOlustur(doc, ayar, sinif, brans, KolonSayisi.Il.ToInt32(), sonSatirBaslik, karneModel);
                    doc.Close();
                }


                //Sınava giren sınıfları ve o sınıflara ait branşları listele branş bazında soru sayısını listele
                var ilceler = karneSonucList.GroupBy(x => x.Ilce).Select(x => x.First()).OrderBy(x => x.Ilce);
                foreach (var ilce in ilceler)
                {
                    //İLÇE KARNESİ
                    pathStart = $@"{seciliDizin}\Karneler\{ilce.Ilce.ToUrl().ToUpper()}\";
                    if (!DizinIslemleri.DizinKontrol(pathStart))
                        DizinIslemleri.DizinOlustur(pathStart);

                    foreach (var sinif in siniflar)
                    {
                        lblBilgi.Text = $"{ilce.Ilce} ilçesi {sinif.Sinif}. sınıf {brans.BransId.DersAdi()} dersi ilçe karnesi oluşturuluyor";

                        string path = $@"{pathStart}\{ilce.Ilce} - {brans.BransId.DersAdi()} - {sinif.Sinif}. Sınıf Karnesi.pdf";
                        string sonSatirBaslik = $"{ilce.Ilce.ToUpper()} İLÇE KARNESİ";
                        KarneModel karneModel = KarneModeliOlustur(ilce.Ilce, sinif.Sinif, brans.BransId, ayar);

                        Document doc = new Document();
                        PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                        doc.Open();

                        PdfOlustur(doc, ayar, sinif, brans, KolonSayisi.Ilce.ToInt32(), sonSatirBaslik, karneModel);
                        doc.Close();
                    }

                    //var okullar = karneSonucList.Where(x => x.Ilce == ilce.Ilce).GroupBy(x => x.KurumKodu).Select(x => x.First()).OrderBy(x => x.KurumKodu);

                    //foreach (var okul in okullar)
                    //{
                    //    string okulAdi = kutukManager.Find(x => x.KurumKodu == okul.KurumKodu).KurumAdi;
                    //    siniflar = karneSonucList.Where(x => x.KurumKodu == okul.KurumKodu).GroupBy(x => x.Sinif).Select(x => x.First()).OrderBy(x => x.Sinif).ToList();
                    //    foreach (var sinif in siniflar)
                    //    {

                    //        //SINIF KARNESİ
                    //        pathStart = $@"{seciliDizin}\Karneler\{ilce.Ilce.ToUrl().ToUpper()}\{okulAdi.ToUrl().ToUpper()}\";
                    //        if (!DizinIslemleri.DizinKontrol(pathStart))
                    //            DizinIslemleri.DizinOlustur(pathStart);

                    //        lblBilgi.Text = $"{ilce.Ilce} ilçesi {okulAdi} {sinif.Sinif}. sınıf {brans.BransId.DersAdi()} dersi sınıf karnesi oluşturuluyor";

                    //        string path = $@"{pathStart}\{brans.BransId.DersAdi()} - {sinif.Sinif}. Sınıf Karnesi.pdf";
                    //        string sonSatirBaslik = $"{okulAdi.ToUpper()} {sinif.Sinif}. SINIF KARNESİ";
                    //        KarneModel karneModel = KarneModeliOlustur(ilce.Ilce, okul.KurumKodu, okulAdi, sinif.Sinif, brans.BransId, ayar);

                    //        Document doc = new Document();
                    //        PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                    //        doc.Open();

                    //        PdfOlustur(doc, ayar, sinif, brans, KolonSayisi.Okul.ToInt32(), sonSatirBaslik, karneModel);
                    //        doc.Close();

                    //        var subeler = karneSonucList.Where(x => x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif).GroupBy(x => x.Sube).Select(x => x.First()).OrderBy(x => x.Sube);

                    //        foreach (var sube in subeler)
                    //        {
                    //            //ŞUBE KARNESİ
                    //            lblBilgi.Text = $"{ilce.Ilce} ilçesi {okulAdi} {sinif.Sinif}-{sube.Sube} şubesi {brans.BransId.DersAdi()} dersi şube karnesi oluşturuluyor";

                    //            sonSatirBaslik = $"{okulAdi.ToUpper()} {sinif.Sinif}-{sube.Sube} SINIFI ŞUBE KARNESİ";
                    //            karneModel = KarneModeliOlustur(ilce.Ilce, okul.KurumKodu, okulAdi, sinif.Sinif, sube.Sube, brans.BransId, ayar);

                    //            //Her şube için branş bazında dsya oluştur.
                    //            path = $@"{pathStart}\{brans.BransId.DersAdi()} - {sinif.Sinif}-{sube.Sube} Şube Karnesi.pdf";

                    //            doc = new Document();
                    //            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                    //            doc.Open();

                    //            PdfOlustur(doc, ayar, sinif, brans, KolonSayisi.Sube.ToInt32(), sonSatirBaslik, karneModel);

                    //            //ÖĞRENCİ KARNESİ

                    //            List<Kazanim> kazanimlar = kazanimManager.List(x => x.BransId == brans.BransId && x.Sinif == sinif.Sinif);

                    //            var ogrenciListesi = kutukManager.List(x => x.KurumKodu == okul.KurumKodu && x.Sinifi == sinif.Sinif && x.Sube == sube.Sube);

                    //            //Öğrenci varsa yeni sayfa ekle
                    //            if (ogrenciListesi.Count > 0)
                    //                doc.NewPage();

                    //            for (var i = 0; i < ogrenciListesi.Count; i++)
                    //            {
                    //                Kutuk kutuk = ogrenciListesi[i];
                    //                CevapTxt ogrCevap = cvpTxtManager.Find(x => x.BransId == brans.BransId && x.OpaqId == kutuk.OpaqId);
                    //                if (ogrCevap != null)
                    //                {
                    //                    DogruCevap dogruCvplar = dogruCevaplarManager.Find(x =>
                    //                        x.BransId == brans.BransId && x.KitapcikTuru == ogrCevap.KitapcikTuru);


                    //                    lblBilgi.Text =
                    //                        $"{brans.BransId.DersAdi()} - {kutuk.Adi} {kutuk.Soyadi} karnesi hazırlanıyor";

                    //                    PdfOlustur(doc, ayar, brans.BransId.DersAdi(), kutuk, ogrCevap, dogruCvplar, kazanimlar);

                    //                    Application.DoEvents();
                    //                }

                    //                //son kayıt değil ise yeni sayfa ekle ve döngüye devam et.
                    //                if (ogrenciListesi.Count > i)
                    //                    doc.NewPage();
                    //            }

                    //            doc.Close();

                    //            Application.DoEvents();
                    //        }
                    //        Application.DoEvents();
                    //    }
                    //    Application.DoEvents();
                    //}
                    Application.DoEvents();
                }
                Application.DoEvents();
            }
            Application.DoEvents();

            progressBar1.Value = 0;
            lblBilgi.Text = "Karneler oluşturuldu.";
            DialogResult dialog = MessageBox.Show("Karneler oluşturuldu.\nŞimdi karnelerin oluşturulduğu dizini açmak ister misiniz?", @"Bilgi", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) Process.Start("explorer.exe", Path.GetDirectoryName(seciliDizin + @"\Karneler\"));
        }


        #region PDF Oluşturma İşlemleri

        /// <summary>
        /// Öğrenci karnesi için pdf oluşturur
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ayar"></param>
        /// <param name="dersAdi"></param>
        /// <param name="kutuk"></param>
        /// <param name="ogrCevap"></param>
        /// <param name="dogruCvplar"></param>
        /// <param name="kazanimlar"></param>
        private void PdfOlustur(Document doc, Ayarlar ayar, string dersAdi, Kutuk kutuk, CevapTxt ogrCevap, DogruCevap dogruCvplar, List<Kazanim> kazanimlar)
        {
            int soruSayisi = dogruCvplar.Cevaplar.Length;
            string ogrenciCevabi = ogrCevap.Cevaplar;
            string dogruCevaplar = dogruCvplar.Cevaplar;

            int dogruSayisi = 0;
            int yanlisSayisi = 0;
            int bosSayisi = 0;

            for (int i = 0; i < soruSayisi; i++)
            {
                if (ogrenciCevabi.Substring(i, 1) == " ")
                    bosSayisi++;
                else if (ogrenciCevabi.Substring(i, 1) == dogruCevaplar.Substring(i, 1))
                    dogruSayisi++;
                else
                    yanlisSayisi++;
            }


            PdfIslemleri pdf = new PdfIslemleri();

            #region Başlık

            pdf.addParagraph(doc, $"{ayar.IlAdi.ToUpper()}  İL MİLLİ EĞİTİM MÜDÜRLÜĞÜ", hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, "Ölçme Değerlendirme Merkezi", hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, $"{ayar.SinavAdi.ToUpper()} {dersAdi.ToUpper()} DERSİ ÖĞRENCİ KARNESİ", hizalama: Element.ALIGN_CENTER);

            #endregion

            #region Öğrenci Bilgileri Tablosu

            PdfPTable tableOgrenciBilgileri = new PdfPTable(7)
            {
                TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
                LockedWidth = true, // tablonun mutlak genişliğini sabitle
                SpacingBefore = 20f, //öncesinde boşluk miktarı
                SpacingAfter = 0f, //sonrasında boşluk miktarı
                HorizontalAlignment = Element.ALIGN_LEFT
            };

            float[] widthsOB = { 75f, 352f, 20f, 20f, 20f, 20f, 20f };
            tableOgrenciBilgileri.SetWidths(widthsOB);

            var cvp = ogrCevap.Cevaplar;

            pdf.addCell(tableOgrenciBilgileri, "ÖĞRENCİ BİLGİLERİ", colspan: 2, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
            pdf.addCell(tableOgrenciBilgileri, "Uygulama Sonucu", colspan: 5, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);

            pdf.addCell(tableOgrenciBilgileri, "ADI SOYADI :", hizalama: Element.ALIGN_RIGHT);
            pdf.addCell(tableOgrenciBilgileri, $"{kutuk.Adi} {kutuk.Soyadi} {(ogrCevap.KatilimDurumu == 0 ? "(Katılmadı)" : "")}");
            pdf.addCell(tableOgrenciBilgileri, "SS", hizalama: Element.ALIGN_CENTER);
            pdf.addCell(tableOgrenciBilgileri, "D", hizalama: Element.ALIGN_CENTER);
            pdf.addCell(tableOgrenciBilgileri, "Y", hizalama: Element.ALIGN_CENTER);
            pdf.addCell(tableOgrenciBilgileri, "B", hizalama: Element.ALIGN_CENTER);
            pdf.addCell(tableOgrenciBilgileri, "KT", hizalama: Element.ALIGN_CENTER);

            pdf.addCell(tableOgrenciBilgileri, "İLÇESİ / OKULU :", hizalama: Element.ALIGN_RIGHT);
            pdf.addCell(tableOgrenciBilgileri, $"{kutuk.IlceAdi} / {kutuk.KurumAdi}");
            pdf.addCell(tableOgrenciBilgileri, soruSayisi.ToString(), 2, hizalama: Element.ALIGN_CENTER);//soru sayısı
            pdf.addCell(tableOgrenciBilgileri, dogruSayisi.ToString(), 2, hizalama: Element.ALIGN_CENTER);//doğru
            pdf.addCell(tableOgrenciBilgileri, yanlisSayisi.ToString(), 2, hizalama: Element.ALIGN_CENTER);//yanlış
            pdf.addCell(tableOgrenciBilgileri, bosSayisi == 0 ? "" : bosSayisi.ToString(), 2, hizalama: Element.ALIGN_CENTER);//boş
            pdf.addCell(tableOgrenciBilgileri, $"{ogrCevap.KitapcikTuru}", 2, hizalama: Element.ALIGN_CENTER);//Kitapçık türü

            pdf.addCell(tableOgrenciBilgileri, "SINIFI / OKUL :", hizalama: Element.ALIGN_RIGHT);
            pdf.addCell(tableOgrenciBilgileri, $"{kutuk.Sinifi} / {kutuk.Sube}");
            pdf.addCell(tableOgrenciBilgileri, "");

            doc.Add(tableOgrenciBilgileri);

            #endregion

            #region Cevap Anahtarı

            int kolonSayisi = soruSayisi + 1;
            PdfPTable tableOgrenciCevaplari = new PdfPTable(kolonSayisi)
            {
                TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
                LockedWidth = true, // tablonun mutlak genişliğini sabitle
                SpacingBefore = 20f, //öncesinde boşluk miktarı
                SpacingAfter = 0f, //sonrasında boşluk miktarı
                HorizontalAlignment = Element.ALIGN_LEFT
            };

            float[] widthsOC = new float[kolonSayisi];
            widthsOC[0] = 60f;
            float kolonW = 467f / kolonSayisi;

            for (int i = 1; i < kolonSayisi; i++)
            {
                widthsOC[i] = kolonW;
            }
            tableOgrenciCevaplari.SetWidths(widthsOC);

            //boş hücre
            pdf.addCell(tableOgrenciCevaplari, "", bgColor: PdfIslemleri.Renkler.Gri);

            //Soru Numaraları
            for (int i = 1; i < kolonSayisi; i++)
            {
                pdf.addCell(tableOgrenciCevaplari, i.ToString(), fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
            }
            pdf.addCell(tableOgrenciCevaplari, "Cevap Anahtarı", hizalama: Element.ALIGN_LEFT, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, fontStyle: iTextSharp.text.Font.BOLD);
            //Cevap Anahtarı
            for (int i = 1; i < kolonSayisi; i++)
            {
                pdf.addCell(tableOgrenciCevaplari, dogruCevaplar.Substring(i - 1, 1), fontSize: 6, hizalama: Element.ALIGN_CENTER);
            }
            pdf.addCell(tableOgrenciCevaplari, "Öğrenci Cevabı", hizalama: Element.ALIGN_LEFT, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri, fontStyle: iTextSharp.text.Font.BOLD);
            //Öğrencinin Cevapları
            if (ogrCevap.KatilimDurumu == 1) //Sınava katılmış ise 
            {
                for (int i = 1; i < kolonSayisi; i++)
                {
                    pdf.addCell(tableOgrenciCevaplari, ogrenciCevabi.Substring(i - 1, 1), fontSize: 6, hizalama: Element.ALIGN_CENTER);
                }
            }
            doc.Add(tableOgrenciCevaplari);

            #endregion

            #region Kazamılar

            PdfPTable tableKazanimlar = new PdfPTable(3)
            {
                TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
                LockedWidth = true, // tablonun mutlak genişliğini sabitle
                SpacingBefore = 20f, //öncesinde boşluk miktarı
                SpacingAfter = 0f, //sonrasında boşluk miktarı
                HorizontalAlignment = Element.ALIGN_LEFT
            };

            float[] widthsKazanim = { 50f, 50f, 427f };

            tableKazanimlar.SetWidths(widthsKazanim);


            //Kazanım Karnesi 
            pdf.addCell(tableKazanimlar, "S. No", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
            pdf.addCell(tableKazanimlar, "Sonuç", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
            pdf.addCell(tableKazanimlar, "Kazanım", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);

            for (int i = 1; i <= soruSayisi; i++)
            {
                string sonuc = "";
                string kazanimAdi = "";
                if (ogrCevap.KatilimDurumu == 1) //Sınava katılmış ise 
                {
                    if (ogrenciCevabi.Substring(i - 1, 1) == " ")
                        sonuc = "B";
                    else if (ogrenciCevabi.Substring(i - 1, 1) == dogruCevaplar.Substring(i - 1, 1))
                        sonuc = "D";
                    else
                        sonuc = "Y";
                    string soruSql = ogrCevap.KitapcikTuru + i + ",";
                    Kazanim kazanim = kazanimlar.Find(x => x.Sorulari.Contains(soruSql));

                    if (kazanim != null)
                        kazanimAdi = kazanim.KazanimAdiOgrenci;
                }

                pdf.addCell(tableKazanimlar, i.ToString(), fontSize: 6, hizalama: Element.ALIGN_CENTER);
                pdf.addCell(tableKazanimlar, sonuc, fontSize: 6, hizalama: Element.ALIGN_CENTER);
                pdf.addCell(tableKazanimlar, kazanimAdi, fontSize: 6);
            }
            doc.Add(tableKazanimlar);

            #endregion

            #region Öğretmen Görüşü

            PdfPTable tableOG = new PdfPTable(3)
            {
                TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
                LockedWidth = true, // tablonun mutlak genişliğini sabitle
                SpacingBefore = 20f, //öncesinde boşluk miktarı
                SpacingAfter = 0f, //sonrasında boşluk miktarı
                HorizontalAlignment = Element.ALIGN_LEFT
            };

            float[] widthsOG = { 175f, 175f, 175f };

            tableOG.SetWidths(widthsOG);


            pdf.addCell(tableOG, "ÖĞRETMEN GÖRÜŞÜ", colspan: 3, bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);

            pdf.addCell(tableOG, "Olumlu Yönler", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
            pdf.addCell(tableOG, "Geliştirilmesi Gereken Yönler", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
            pdf.addCell(tableOG, "Yapması Gerekenler", bgColor: PdfIslemleri.Renkler.Gri, hizalama: Element.ALIGN_CENTER, fontStyle: iTextSharp.text.Font.BOLD);
            //Kazanım Karnesi 
            pdf.addCell(tableOG, " ", hizalama: Element.ALIGN_CENTER, yukseklik: 100);
            pdf.addCell(tableOG, " ", hizalama: Element.ALIGN_CENTER, yukseklik: 100);
            pdf.addCell(tableOG, " ", hizalama: Element.ALIGN_CENTER, yukseklik: 100);

            pdf.addCell(tableOG, "Ders Öğretmeni (Adı Soyadı İmza)", fontSize: 5, colspan: 3, hizalama: Element.ALIGN_RIGHT, yukseklik: 30, vertical: Element.ALIGN_TOP);

            doc.Add(tableOG);

            #endregion

            //-------------SON SATIR -----------------
            pdf.addParagraph(doc, "\n\n");
            pdf.addParagraph(doc, $"{ayar.IlAdi} Ölçme ve Değerlendirme Merkezi - Adres: {ayar.OdmAdres}\n", 7, Element.ALIGN_CENTER, iTextSharp.text.Font.ITALIC);
            pdf.addParagraph(doc, $"Web: {ayar.OdmWeb} E-Mail: {ayar.OdmEmail}", 7, Element.ALIGN_CENTER, iTextSharp.text.Font.ITALIC);

        }

        /// <summary>
        /// İl İlçe Okul Sınıf ve Şube Karneleri için pdf oluşturur
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ayar"></param>
        /// <param name="sinif"></param>
        /// <param name="brans"></param>
        /// <param name="column"></param>
        /// <param name="sonSatirBaslik"></param>
        /// <param name="karneModel"></param>
        private void PdfOlustur(Document doc, Ayarlar ayar, KarneSonuc sinif, KarneSonuc brans, int column, string sonSatirBaslik, KarneModel karneModel)
        {

            PdfIslemleri pdf = new PdfIslemleri();

            #region Başlık

            pdf.addParagraph(doc, $"{ayar.IlAdi.ToUpper()}  İL MİLLİ EĞİTİM MÜDÜRLÜĞÜ", hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, "Ölçme Değerlendirme Merkezi", hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, $"{ayar.SinavAdi.ToUpper()} {sinif.Sinif}. SINIF {brans.BransId.DersAdi().ToUpper()} DERSİ", hizalama: Element.ALIGN_CENTER);
            pdf.addParagraph(doc, sonSatirBaslik, hizalama: Element.ALIGN_CENTER);

            #endregion

            float[] widths4 = { 25f, 40f, 392f, 70f }; //il  
            float[] widths6 = { 25f, 40f, 285f, 30f, 30f, 117f }; //il - ilçe 
            float[] widths7 = { 25f, 40f, 255f, 30f, 30f, 30f, 117f }; //il - ilçe - okul
            float[] widths8 = { 25f, 40f, 225f, 30f, 30f, 30f, 30f, 117f }; //il - ilçe - okul - şube

            PdfPTable table = new PdfPTable(column)
            {
                TotalWidth = 522f, // puan cinsinden tablonun gerçek genişliği
                LockedWidth = true, // tablonun mutlak genişliğini sabitle
                SpacingBefore = 20f, //öncesinde boşluk miktarı
                SpacingAfter = 30f, //sonrasında boşluk miktarı
            };


            if (column == KolonSayisi.Il.ToInt32())
                table.SetWidths(widths4);
            else if (column == KolonSayisi.Ilce.ToInt32())
                table.SetWidths(widths6);
            else if (column == KolonSayisi.Okul.ToInt32())
                table.SetWidths(widths7);
            else
                table.SetWidths(widths8);


            //--------------Üst Başlık
            pdf.addCell(table, "", colspan: 3, fontSize: 6, bgColor: PdfIslemleri.Renkler.Gri);
            pdf.addCell(table, "Edinilme Oranı %", hizalama: Element.ALIGN_CENTER, colspan: column - 4, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 4) pdf.addCell(table, "", bgColor: PdfIslemleri.Renkler.Gri);
            //Üst Başlık İkinci Satır
            pdf.addCell(table, "No", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            pdf.addCell(table, "Kazanım No", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            pdf.addCell(table, "Kazanım", bgColor: PdfIslemleri.Renkler.Gri);
            pdf.addCell(table, "İl", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);

            if (column > 5) pdf.addCell(table, "İlçe", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 6) pdf.addCell(table, "Okul", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 7) pdf.addCell(table, "Şube", hizalama: Element.ALIGN_CENTER, bgColor: PdfIslemleri.Renkler.Gri);
            if (column > 4) pdf.addCell(table, "", bgColor: PdfIslemleri.Renkler.Gri);
            //--------------Üst Başlık Son

            //--------------satırlar
            foreach (var t in karneModel.TableModeller)
            {
                pdf.addCell(table, t.No, hizalama: Element.ALIGN_CENTER); //Satır numarası
                pdf.addCell(table, t.KazanimNo, hizalama: Element.ALIGN_CENTER);
                pdf.addCell(table, t.Kazanim);
                pdf.addCell(table, t.IlPuani, hizalama: Element.ALIGN_CENTER);
                if (column > 5) pdf.addCell(table, t.IlcePuani, hizalama: Element.ALIGN_CENTER); //ilçe
                if (column > 6) pdf.addCell(table, t.OkulPuani, hizalama: Element.ALIGN_CENTER); //okul
                if (column > 7) pdf.addCell(table, t.SubePuani, hizalama: Element.ALIGN_CENTER); //şube
                if (column > 4) pdf.addCell(table, t.Aciklama, hizalama: Element.ALIGN_CENTER); //açıklama
            }

            doc.Add(table);

            pdf.addParagraph(doc, karneModel.Raporu, 7);
            pdf.addParagraph(doc, "Çalışmalarınızda kolaylıklar diler, katkılarınız için teşekkür ederiz.", 7, Element.ALIGN_RIGHT, iTextSharp.text.Font.ITALIC);
            pdf.addParagraph(doc, "\n\n");
            pdf.addParagraph(doc, $"{ayar.IlAdi} Ölçme ve Değerlendirme Merkezi - Adres: {ayar.OdmAdres}\n", 7, Element.ALIGN_CENTER, iTextSharp.text.Font.ITALIC);
            pdf.addParagraph(doc, $"Web: {ayar.OdmWeb} E-Mail: {ayar.OdmEmail}", 7, Element.ALIGN_CENTER, iTextSharp.text.Font.ITALIC);


        }
        private KarneModel KarneModeliOlustur(int sinif, int brans, Ayarlar ayar)
        {
            int destekKazanimSayisi = 0;
            int telafiKazanimSayisi = 0;
            List<TableModel> model = new List<TableModel>();
            List<string> eksikKazanimlar = new List<string>();


            int i = 1;
            KarneSonucManager karneSonucManager = new KarneSonucManager();
            KazanimManager kazanimManager = new KazanimManager();

            foreach (var kazanim in kazanimManager.List(x => x.BransId == brans && x.Sinif == sinif))
            {
                int ilDogruSayisi = 0;
                int ilYanlisSayisi = 0;
                int ilBosSayisi = 0;


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

                    ilDogruSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                    ilYanlisSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                    ilBosSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                }

                int ilOgrenciSayisi = ilDogruSayisi + ilYanlisSayisi + ilBosSayisi;

                int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();

                string aciklama = "";
                if (ilBasariDurumu >= 0 && ilBasariDurumu < 31)
                {
                    aciklama = "Destekleme kursları yapılmalı";
                    eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi));
                    destekKazanimSayisi++;
                }
                else if (ilBasariDurumu >= 31 && ilBasariDurumu <= 49)
                {
                    aciklama = "Sınıf içi telafi eğitimi yapılmalı";
                    telafiKazanimSayisi++;
                }

                model.Add(new TableModel(i.ToString(), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), "", "", "", aciklama));


                i++;
            }

            string raporStr = $"{ayar.SinavAdi} {sinif}. sınıf {brans.DersAdi()} testi kazanımlarının ilimizde edinilme oranları,\n";

            if (eksikKazanimlar.Count > 0)
            {
                raporStr += "Özellikle;\n";
                foreach (var k in eksikKazanimlar)
                {
                    raporStr += $"- {k}\n";
                }

                raporStr += "kazanımlarının yapılma yüzdeleri çok düşüktür.\n";
            }

            string desteklemeStr = "";
            string telafiStr = "";
            if (destekKazanimSayisi > 0)
            {
                desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi +
                                " kazanım için destekleme kurslarının planlanıp uygulanması ";
                desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
            }

            if (telafiKazanimSayisi > 0)
            {
                telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
                            " kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
            }

            if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
            {
                raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
                               " tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.\n";
            }
            else
            {
                raporStr +=
                    $@"Edinilme düzeyi % 31’in altında kalan {destekKazanimSayisi} kazanım için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {telafiKazanimSayisi} kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.
                                                    Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.";
            }

            KarneModel karneModel = new KarneModel
            {
                EksikKazanimlar = eksikKazanimlar,
                TableModeller = model,
                Raporu = raporStr
            };


            return karneModel;
        }
        private KarneModel KarneModeliOlustur(string ilce, int sinif, int brans, Ayarlar ayar)
        {
            int destekKazanimSayisi = 0;
            int telafiKazanimSayisi = 0;
            List<TableModel> model = new List<TableModel>();
            List<string> eksikKazanimlar = new List<string>();


            int i = 1;
            KarneSonucManager karneSonucManager = new KarneSonucManager();
            KazanimManager kazanimManager = new KazanimManager();

            foreach (var kazanim in kazanimManager.List(x => x.BransId == brans && x.Sinif == sinif))
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

                    ilDogruSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                    ilYanlisSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                    ilBosSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                    ilceDogruSayisi += karneSonucManager.List(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                    ilceYanlisSayisi += karneSonucManager.List(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                    ilceBosSayisi += karneSonucManager.List(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                }

                int ilOgrenciSayisi = ilDogruSayisi + ilYanlisSayisi + ilBosSayisi;
                int ilceOgrenciSayisi = ilceDogruSayisi + ilceYanlisSayisi + ilceBosSayisi;

                int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();
                int ilceBasariDurumu = (((double)ilceDogruSayisi * 100) / ilceOgrenciSayisi).ToInt32();

                string aciklama = "";
                if (ilceBasariDurumu >= 0 && ilceBasariDurumu < 31)
                {
                    aciklama = "Destekleme kursları yapılmalı";
                    eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi));
                    destekKazanimSayisi++;
                }
                else if (ilceBasariDurumu >= 31 && ilceBasariDurumu <= 49)
                {
                    aciklama = "Sınıf içi telafi eğitimi yapılmalı";
                    telafiKazanimSayisi++;
                }

                model.Add(new TableModel(i.ToString(), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), ilceBasariDurumu.ToString("0"),
                    "", "", aciklama));


                i++;
            }

            string raporStr = $"{ayar.SinavAdi} {sinif}. sınıf {brans.DersAdi()} testi kazanımlarının {ilce.IlkHarfleriBuyut()} ilçesinde edinilme oranları,\n";

            if (eksikKazanimlar.Count > 0)
            {
                raporStr += "Özellikle;\n";
                foreach (var k in eksikKazanimlar)
                {
                    raporStr += $"- {k}\n";
                }

                raporStr += "kazanımlarının yapılma yüzdeleri çok düşüktür.\n";
            }

            string desteklemeStr = "";
            string telafiStr = "";
            if (destekKazanimSayisi > 0)
            {
                desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi +
                                " kazanım için destekleme kurslarının planlanıp uygulanması ";
                desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
            }

            if (telafiKazanimSayisi > 0)
            {
                telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
                            " kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
            }

            if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
            {
                raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
                               " tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.\n";
            }
            else
            {
                raporStr +=
                    $@"Edinilme düzeyi % 31’in altında kalan {destekKazanimSayisi} kazanım için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {telafiKazanimSayisi} kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.
                                                    Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.";
            }

            KarneModel karneModel = new KarneModel
            {
                EksikKazanimlar = eksikKazanimlar,
                TableModeller = model,
                Raporu = raporStr
            };


            return karneModel;
        }
        private KarneModel KarneModeliOlustur(string ilce, int kurumkodu, string kurumAdi, int sinif, int brans, Ayarlar ayar)
        {
            int destekKazanimSayisi = 0;
            int telafiKazanimSayisi = 0;
            List<TableModel> model = new List<TableModel>();
            List<string> eksikKazanimlar = new List<string>();


            int i = 1;
            KarneSonucManager karneSonucManager = new KarneSonucManager();
            KazanimManager kazanimManager = new KazanimManager();

            foreach (var kazanim in kazanimManager.List(x => x.BransId == brans && x.Sinif == sinif))
            {
                int ilDogruSayisi = 0;
                int ilYanlisSayisi = 0;
                int ilBosSayisi = 0;
                int ilceDogruSayisi = 0;
                int ilceYanlisSayisi = 0;
                int ilceBosSayisi = 0;
                int okulDogruSayisi = 0;
                int okulYanlisSayisi = 0;
                int okulBosSayisi = 0;

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

                    ilDogruSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                    ilYanlisSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                    ilBosSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                    ilceDogruSayisi += karneSonucManager.List(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                    ilceYanlisSayisi += karneSonucManager.List(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                    ilceBosSayisi += karneSonucManager.List(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                    okulDogruSayisi += karneSonucManager.List(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                    okulYanlisSayisi += karneSonucManager.List(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                    okulBosSayisi += karneSonucManager.List(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                }

                int ilOgrenciSayisi = ilDogruSayisi + ilYanlisSayisi + ilBosSayisi;
                int ilceOgrenciSayisi = ilceDogruSayisi + ilceYanlisSayisi + ilceBosSayisi;
                int okulOgrenciSayisi = okulDogruSayisi + okulYanlisSayisi + okulBosSayisi;

                int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();
                int ilceBasariDurumu = (((double)ilceDogruSayisi * 100) / ilceOgrenciSayisi).ToInt32();
                int okulBasariDurumu = (((double)okulDogruSayisi * 100) / okulOgrenciSayisi).ToInt32();

                string aciklama = "";
                if (okulBasariDurumu >= 0 && okulBasariDurumu < 31)
                {
                    aciklama = "Destekleme kursları yapılmalı";
                    eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi));
                    destekKazanimSayisi++;
                }
                else if (okulBasariDurumu >= 31 && okulBasariDurumu <= 49)
                {
                    aciklama = "Sınıf içi telafi eğitimi yapılmalı";
                    telafiKazanimSayisi++;
                }

                model.Add(new TableModel(i.ToString(), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), ilceBasariDurumu.ToString("0"),
                    okulBasariDurumu.ToString("0"), "", aciklama));


                i++;
            }

            string raporStr = $"{ayar.SinavAdi} {kurumAdi} {sinif}. sınıf {brans.DersAdi()} testi kazanımlarının edinilme oranları,\n";

            if (eksikKazanimlar.Count > 0)
            {
                raporStr += "Özellikle;\n";
                foreach (var k in eksikKazanimlar)
                {
                    raporStr += $"- {k}\n";
                }

                raporStr += "kazanımlarının yapılma yüzdeleri çok düşüktür.\n";
            }

            string desteklemeStr = "";
            string telafiStr = "";
            if (destekKazanimSayisi > 0)
            {
                desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi +
                                " kazanım için destekleme kurslarının planlanıp uygulanması ";
                desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
            }

            if (telafiKazanimSayisi > 0)
            {
                telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
                            " kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
            }

            if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
            {
                raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
                               " tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.\n";
            }
            else
            {
                raporStr +=
                    $@"Edinilme düzeyi % 31’in altında kalan {destekKazanimSayisi} kazanım için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {telafiKazanimSayisi} kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.
                                                    Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.";
            }

            KarneModel karneModel = new KarneModel
            {
                EksikKazanimlar = eksikKazanimlar,
                TableModeller = model,
                Raporu = raporStr
            };


            return karneModel;
        }
        private KarneModel KarneModeliOlustur(string ilce, int kurumkodu, string kurumAdi, int sinif, string sube, int brans, Ayarlar ayar)
        {
            int destekKazanimSayisi = 0;
            int telafiKazanimSayisi = 0;
            List<TableModel> model = new List<TableModel>();
            List<string> eksikKazanimlar = new List<string>();


            int i = 1;
            KarneSonucManager karneSonucManager = new KarneSonucManager();
            KazanimManager kazanimManager = new KazanimManager();

            foreach (var kazanim in kazanimManager.List(x => x.BransId == brans && x.Sinif == sinif))
            {
                int ilDogruSayisi = 0;
                int ilYanlisSayisi = 0;
                int ilBosSayisi = 0;
                int ilceDogruSayisi = 0;
                int ilceYanlisSayisi = 0;
                int ilceBosSayisi = 0;
                int okulDogruSayisi = 0;
                int okulYanlisSayisi = 0;
                int okulBosSayisi = 0;
                int subeDogruSayisi = 0;
                int subeYanlisSayisi = 0;
                int subeBosSayisi = 0;


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

                    ilDogruSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                    ilYanlisSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                    ilBosSayisi += karneSonucManager.List(x => x.BransId == brans && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                    ilceDogruSayisi += karneSonucManager.List(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                    ilceYanlisSayisi += karneSonucManager.List(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                    ilceBosSayisi += karneSonucManager.List(x => x.BransId == brans && x.Ilce == ilce && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                    okulDogruSayisi += karneSonucManager.List(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                    okulYanlisSayisi += karneSonucManager.List(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                    okulBosSayisi += karneSonucManager.List(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);

                    subeDogruSayisi += karneSonucManager.List(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.Sube == sube && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Dogru);
                    subeYanlisSayisi += karneSonucManager.List(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.Sube == sube && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Yanlis);
                    subeBosSayisi += karneSonucManager.List(x => x.BransId == brans && x.KurumKodu == kurumkodu && x.Sinif == sinif && x.Sube == sube && x.KitapcikTuru == kitapcikTuru && x.SoruNo == soruNo).Sum(x => x.Bos);
                }

                int ilOgrenciSayisi = ilDogruSayisi + ilYanlisSayisi + ilBosSayisi;
                int ilceOgrenciSayisi = ilceDogruSayisi + ilceYanlisSayisi + ilceBosSayisi;
                int okulOgrenciSayisi = okulDogruSayisi + okulYanlisSayisi + okulBosSayisi;
                int subeOgrenciSayisi = subeDogruSayisi + subeYanlisSayisi + subeBosSayisi;

                int ilBasariDurumu = (((double)ilDogruSayisi * 100) / ilOgrenciSayisi).ToInt32();
                int ilceBasariDurumu = (((double)ilceDogruSayisi * 100) / ilceOgrenciSayisi).ToInt32();
                int okulBasariDurumu = (((double)okulDogruSayisi * 100) / okulOgrenciSayisi).ToInt32();
                int subeBasariDurumu = (((double)subeDogruSayisi * 100) / subeOgrenciSayisi).ToInt32();

                string aciklama = "";
                if (subeBasariDurumu >= 0 && subeBasariDurumu < 31)
                {
                    aciklama = "Destekleme kursları yapılmalı";
                    eksikKazanimlar.Add(string.Format("'{0} {1}'", kazanim.KazanimNo, kazanim.KazanimAdi));
                    destekKazanimSayisi++;
                }
                else if (subeBasariDurumu >= 31 && subeBasariDurumu <= 49)
                {
                    aciklama = "Sınıf içi telafi eğitimi yapılmalı";
                    telafiKazanimSayisi++;
                }

                model.Add(new TableModel(i.ToString(), kazanim.KazanimNo, kazanim.KazanimAdi, ilBasariDurumu.ToString("0"), ilceBasariDurumu.ToString("0"),
                    okulBasariDurumu.ToString("0"), subeBasariDurumu.ToString("0"), aciklama));


                i++;
            }

            string raporStr =
                $"{ayar.SinavAdi} {brans.DersAdi()} testi kazanımlarının {kurumAdi} {sinif}-{sube} şubesinde edinilme oranları,\n";

            if (eksikKazanimlar.Count > 0)
            {
                raporStr += "Özellikle;\n";
                foreach (var k in eksikKazanimlar)
                {
                    raporStr += $"- {k}\n";
                }

                raporStr += "kazanımlarının yapılma yüzdeleri çok düşüktür.\n";
            }

            string desteklemeStr = "";
            string telafiStr = "";
            if (destekKazanimSayisi > 0)
            {
                desteklemeStr = "% 31’in altında kalan " + destekKazanimSayisi +
                                " kazanım için destekleme kurslarının planlanıp uygulanması ";
                desteklemeStr += telafiKazanimSayisi > 0 ? "; " : " ";
            }

            if (telafiKazanimSayisi > 0)
            {
                telafiStr = "% 31’den % 49’a kadar olan " + telafiKazanimSayisi +
                            " kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi ";
            }

            if (destekKazanimSayisi != 0 || telafiKazanimSayisi != 0)
            {
                raporStr += "Edinilme düzeyi " + desteklemeStr + telafiStr +
                               " tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.\n";
            }
            else
            {
                raporStr +=
                    $@"Edinilme düzeyi % 31’in altında kalan {destekKazanimSayisi} kazanım için destekleme kurslarının planlanıp uygulanması, % 31’den % 49’a kadar olan {telafiKazanimSayisi} kazanımın öğrenme eksikliklerinin sınıf içi etkinliklerle giderilmesi tavsiye edilmektedir.Tabloda bu kazanımların karşılarına gerekli açıklamalar yapılmıştır.
                                                    Kazanım karnesinin incelenmesi, öğrenme eksikliklerinin tespit edilmesi ve gerekli telafi eğitimlerin yapılması, projenin asıl amacı olan eğitimde niteliğinin artırılması ve öğrenme eksikliklerinin giderilmesi hedefine ulaşılmasını sağlayacaktır.";
            }

            KarneModel karneModel = new KarneModel
            {
                EksikKazanimlar = eksikKazanimlar,
                TableModeller = model,
                Raporu = raporStr
            };


            return karneModel;
        }


        #endregion

        private void FormDegerlendirmeKarne_Load(object sender, EventArgs e)
        {

            //background nesnesi sağlıklı çalışması için gerekli. 
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            karneSonucManager.TumunuSil();
        }
    }
}
