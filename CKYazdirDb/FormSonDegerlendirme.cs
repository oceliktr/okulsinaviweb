using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.Library;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb
{
    public partial class FormSonDegerlendirme : Form
    {
        public FormSonDegerlendirme()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();

        }

        private void FormSonDegerlendirme_Load(object sender, EventArgs e)
        {

            CheckForIllegalCrossThreadCalls = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int a = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();

            OgrenciCevapManager ocvpManager = new OgrenciCevapManager();
            var ogrCevaplar = ocvpManager.List();

            DogruCevaplarManager dogruCevaplarDb = new DogruCevaplarManager();
            var dogruCevapList = dogruCevaplarDb.List();

            KarneSonucManager karneSonucManager = new KarneSonucManager();
          //  karneSonucManager.TumunuSil();

            //Önce ilçeleri,okulları ve şubeleri grupla

            // var ilceler = ogrCevaplar.GroupBy(x => x.Ilce).Select(x => x.First()).OrderBy(x => x.Ilce).ToList();
             var ilceler = ogrCevaplar.Where(x=> x.Ilce == "YAKUTİYE").GroupBy(x => x.Ilce).Select(x => x.First()).OrderBy(x=>x.Ilce).ToList();
            progressBar1.Maximum = ilceler.Count;

            foreach (var ilce in ilceler)
            {
                a++;
                progressBar1.Value = a;

                if (!string.IsNullOrEmpty(ilce.Ilce))
                {
                    var okullar = ogrCevaplar.Where(x => x.Ilce == ilce.Ilce).GroupBy(x => x.KurumKodu).Select(x => x.First()).ToList();
                    progressBar2.Maximum = okullar.Count;

                    Stopwatch watch2 = new Stopwatch();
                    watch2.Start();
                    int b = 0;
                    foreach (var okul in okullar)
                    {
                        b++;
                        progressBar2.Value = b;
                        
                        var siniflar = ogrCevaplar.Where(x => x.KurumKodu == okul.KurumKodu).GroupBy(x => x.Sinif).Select(x => x.First()).ToList();
                        foreach (var sinif in siniflar)
                        {
                            try
                            {
                                toolSslKalanSure.Text = $"{ilce.Ilce} : " + ilceler.Count.KalanSureHesapla(a, watch);
                                toolSslOkul.Text = $"{b}/{okullar.Count} - {sinif.KurumKodu} okulu {sinif.Sinif}. sınıf : " + okullar.Count.KalanSureHesapla(b, watch2);
                            }
                            catch (Exception)
                            {
                                toolSslKalanSure.Text = "Hesaplanıyor...";
                                toolSslOkul.Text = "Hesaplanıyor...";
                            }
                            var subeler = ogrCevaplar.Where(x => x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif).GroupBy(x => x.Sube).Select(x => x.First()).ToList();
                            foreach (var sube in subeler)
                            {
                                var kTurleri = ogrCevaplar.Where(x => x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif && x.Sube == sube.Sube && x.KitapcikTuru != "").GroupBy(x => x.KitapcikTuru).Select(x => x.First()).ToList();

                                foreach (var ktur in kTurleri)
                                {
                                    var ogrenciler = ogrCevaplar.Where(x => x.KurumKodu == okul.KurumKodu && x.Sinif == sinif.Sinif && x.Sube == sube.Sube && sube.KitapcikTuru == ktur.KitapcikTuru && x.KitapcikTuru != "").ToList();
                                    pbOgrenci.Maximum = ogrenciler.Count;
                                    int o = 0;
                                    foreach (var ogr in ogrenciler)
                                    {
                                        o++;
                                        pbOgrenci.Value = o;
                                        label1.Text = $"{o}/{ogrenciler.Count}";
                                        string[] cevap = ogr.Cevaplar.Split('#');
                                        for (int i = 0; i < cevap.Length - 1; i += 2)
                                        {
                                            int bransId = cevap[i].ToInt32();
                                            string bransOgrenciCevap = cevap[i + 1];

                                            if (!string.IsNullOrEmpty(ogr.KitapcikTuru))
                                            {
                                                DogruCevap dcvp = dogruCevapList.FirstOrDefault(x => x.BransId == bransId && x.KitapcikTuru == ogr.KitapcikTuru);
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

                                                        //string mesaj = $"Branş: {bransId} Öğr Cvp: {bransOgrenciCevap} D Cvp: {dcvp.Cevaplar}\n" +
                                                        //               $"Ilce:{sube.Ilce} KurumKodu: {sube.KurumKodu} Sinif: {sube.Sinif} Sube: {sube.Sube} Kitapçık: {sube.KitapcikTuru} SoruNo :{j + 1}  D: {dogru} Y: {yanlis} B: {bos} KD :{sube.KatilimDurumu}";
                                                        //MessageBox.Show(mesaj);

                                                        KarneSonuc karneSonuc = karneSonucManager.Find(x =>
                                                            x.BransId == bransId &&
                                                            x.Ilce == ogr.Ilce &&
                                                            x.KurumKodu == ogr.KurumKodu &&
                                                            x.Sinif == ogr.Sinif &&
                                                            x.Sube == ogr.Sube &&
                                                            x.SoruNo == soruNo &&
                                                            x.KitapcikTuru == ogr.KitapcikTuru);

                                                        if (karneSonuc == null)
                                                        {
                                                            //Yeni kayıt ekle
                                                            KarneSonuc ks = new KarneSonuc()
                                                            {
                                                                Ilce = ogr.Ilce,
                                                                BransId = bransId,
                                                                KitapcikTuru = ogr.KitapcikTuru,
                                                                KurumKodu = ogr.KurumKodu,
                                                                Sinif = ogr.Sinif,
                                                                Sube = ogr.Sube,
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
                                                    }
                                                    Application.DoEvents();
                                                }
                                                Application.DoEvents();
                                            }
                                            Application.DoEvents();
                                        }
                                        Application.DoEvents();
                                    }
                                    Application.DoEvents();
                                }
                                Application.DoEvents();
                            }
                            Application.DoEvents();
                        }
                    }
                    Application.DoEvents();
                    watch2.Stop();
                }

                toolSslKalanSure.Text = "Bitti";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

           backgroundWorker2.RunWorkerAsync();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            int a = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            CevapTxtManager cevapTxtManager = new CevapTxtManager();
            OgrenciCevapManager ocvpManager = new OgrenciCevapManager();
            var ogrCevaplar = ocvpManager.List();
            progressBar1.Maximum = ogrCevaplar.Count;

            foreach (var cvp in ogrCevaplar)
            {
                a++;
                progressBar1.Value = a;
                try
                {
                    toolSslOkul.Text = $"{a}/{ogrCevaplar.Count} " + ogrCevaplar.Count.KalanSureHesapla(a, watch);
                }
                catch (Exception)
                {
                    toolSslKalanSure.Text = "Hesaplanıyor...";
                    toolSslOkul.Text = "Hesaplanıyor...";
                }

                int katilim = cvp.KatilimDurumu == "0" ? 0 : 1;
                string[] cevapBilgisi = cvp.Cevaplar.Split('#');
                for (int t = 0; t < cevapBilgisi.Length - 1; t += 2)
                {
                    int dersKodu = cevapBilgisi[t].ToInt32();
                    string cevaplar = cevapBilgisi[t + 1];

                    CevapTxt cvpTxt = new CevapTxt()
                    {
                        KitapcikTuru = cvp.KitapcikTuru,
                        KatilimDurumu = katilim,
                        OpaqId = cvp.OpaqId,
                        BransId = dersKodu,
                        Cevaplar = cevaplar
                    };
                    cevapTxtManager.Insert(cvpTxt);
                }


            }
            watch.Stop();
            toolSslOkul.Text = "Bitti";

        }
    }
}
