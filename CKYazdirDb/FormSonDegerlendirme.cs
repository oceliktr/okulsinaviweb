using Microsoft.Office.Interop.Excel;
using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.Entities;
using ODM.CKYazdirDb.Library;
using ODM.CKYazdirDb.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

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
        private bool islemiDurdur;

        private int saat;
        private int dakika;
        private int saniye;

        private readonly List<OpaqKontrol> opaqList = new List<OpaqKontrol>(); //opaq numaralarının tutuacağı dizi
                                                                               //Bu dizi ile optik formu gelmeyen öğrencileri tespit edeceğiz

        private int anaBar;
        private int anaBarToplamAsama = 16;

        #region Değerlendirme İşlemleri

        private void btnDegerlendirme1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofData = new OpenFileDialog())
            {
                ofData.Reset();
                ofData.ReadOnlyChecked = true;
                ofData.Multiselect = true;
                ofData.ShowReadOnly = true;
                ofData.Filter = "Cevap text dosyası (*.txt;*.dat)|*.txt;*.dat";
                ofData.Title = "Cevap text dosyasını seçiniz.";
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
                            timer1.Enabled = true; //sayaç başlasın

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
            string raporUrl = raporDizinAdresi + @"Rapor.txt";

            opaqList.Clear();//daha önce liste oluşturulmuş ise temizle



            var lines = File.ReadLines(dosyaAdi, Encoding.UTF8).ToList();

            pbAna.Maximum = anaBarToplamAsama;
            anaBar = 0;

            //Mükerrer Kontrol işlemleri
            anaBar++;
            pbAna.Value = anaBar;
            MukerrerKayitKontrol(lines, raporUrl);
            if (islemiDurdur)
            {
                Process.Start("notepad.exe", raporUrl);

                GecenSureyiDurdur();

                pbAna.Value = 0;
                toolSslKalanSure.Text = "Cevap text dosyasında mükerrer kayıtlar bulunduğundan işlem durduruldu.";
                return; //mükerrer kayıtları göster ve işlemi durdur
            }

            //Öğrenci bilgilerini text dosyasından alıp veritabanında kontrol et
            anaBar++;
            pbAna.Value = anaBar;
            List<ResultList> opaqIdKontrol = TextListesiniKutuktenKontrolEt(lines);
            anaBar++;
            pbAna.Value = anaBar;
            //Kütükte bilgisi olmayan öğrenciler ve hatalı işaretlemelerin raporu varsa yazdır.
            if (opaqIdKontrol.Count > 0)
            {
                StreamWriter resultYaz = new StreamWriter(raporUrl);
                int a = 0;
                progressBar1.Value = 0;
                int islemSayisi = opaqIdKontrol.Count;
                progressBar1.Maximum = islemSayisi;

                foreach (var kutuk in opaqIdKontrol)
                {
                    a++;
                    progressBar1.Value = a;
                    toolSslKalanSure.Text = $"Kütükle eşleşmeyen kayıtlar dosyaya yazılıyor.  {a} / {islemSayisi}";

                    resultYaz.WriteLine(kutuk.Key + " " + kutuk.Result);
                }


                resultYaz.Close();
                resultYaz.Dispose();

                Process.Start("notepad.exe", raporUrl);

                if (islemiDurdur)
                {
                    GecenSureyiDurdur();

                    islemiDurdur = false; //Kütükte olmayan öğrenciler varsa düzeltilmesi için durdur.
                    pbAna.Value = 0;
                    progressBar1.Value = 0;
                    toolSslKalanSure.Text = "Raporda düzeltilmesi gerekenleri bulunduğundan işlem durduruldu.";
                    return;
                }
            }

            //Öğrenci cevaplarını text dosyasından alıp veritabanına kaydeden aşama.
            anaBar++;
            pbAna.Value = anaBar;
            List<ResultList> result = OgrenciCevaplariniKutugeKaydet(lines);

            anaBar++;
            pbAna.Value = anaBar;
            //Kütükte bilgisi olmayan öğrenciler ve hatalı işaretlemelerin raporu varsa yazdır.
            if (result.Count > 0)
            {
                StreamWriter resultYaz = new StreamWriter(raporUrl);
                int a = 0;
                progressBar1.Value = 0;
                int islemSayisi = result.Count;
                progressBar1.Maximum = islemSayisi;

                foreach (var kutuk in result)
                {
                    a++;
                    progressBar1.Value = a;
                    toolSslKalanSure.Text = $"Uyarılar dosyaya yazılıyor.  {a} / {islemSayisi}";

                    resultYaz.WriteLine(kutuk.Key + " " + kutuk.Result);
                }


                resultYaz.Close();
                resultYaz.Dispose();

                Process.Start("notepad.exe", raporUrl);

                if (islemiDurdur)
                {
                    GecenSureyiDurdur();

                    islemiDurdur = false; //Kütükte olmayan öğrenciler varsa düzeltilmesi için durdur.
                    pbAna.Value = 0;
                    progressBar1.Value = 0;
                    toolSslKalanSure.Text = "Raporda düzeltilmesi gerekenleri bulunduğundan işlem durduruldu.";
                    return;
                }
            }


            //2 aşama
            ExcelRaporOlustur();

            anaBar++;
            pbAna.Value = anaBar;
            List<KarneSonuc> karneSonucList = OgrenciCevaplariniDegerlendir();

            raporUrl = raporDizinAdresi + "KarneSonuc_1.ck";
            StreamWriter yaz = new StreamWriter(raporUrl);

            //okul ve şube karneleri için hesaplama yapar karne sonuç dosyasına yazar
            anaBar++;
            pbAna.Value = anaBar;
            SubeDuzeyindeKarneSonuclariniKaydet(karneSonucList, yaz);

            //il ilçe kazanım ortalaması % değerlendirmesi
            anaBar++;
            pbAna.Value = anaBar;
            List<IlIlceDogruYanlisBosModeli> ilIlceOrtalamalariList = IlIlceBasariHesapla(karneSonucList);

            anaBar++;
            pbAna.Value = anaBar;
            IlceBasariKaydet(ilIlceOrtalamalariList, yaz);
            yaz.Close();

            //pbAna.Value için 4 aşama metod içinde tanımlandı
            DogruYanlislariHesapla();

            anaBar++;
            pbAna.Value = anaBar;
            CkDataOlustur();

            pbAna.Value = 0;
            toolSslKalanSure.Text = "Tamamlandı. " + anaBar;
            GecenSureyiDurdur();

        }

        private void GecenSureyiDurdur()
        {
            timer1.Enabled = false; //geçen süreyi durdur
            saat = 0;
            dakika = 0;
            saniye = 0;
        }

        private void MukerrerKayitKontrol(List<string> lines, string raporUrl)
        {

            islemiDurdur = false; //değerlendirme tekrar çalıştırıldığında mükerrer kayıt kontrolunden önce true değerini false yapalım

            int islemSayisi = lines.Count;
            progressBar1.Maximum = islemSayisi;
            int a = 0;
            progressBar1.Value = 0;

            StreamWriter yaz = new StreamWriter(raporUrl);
            foreach (var item in lines)
            {
                a++;
                progressBar1.Value = a;
                toolSslKalanSure.Text = $"Mükerrer kayıt kontrolü yapılıyor. {a} / {islemSayisi}";

                string[] satir = item.Split('#');
                string opaqStr = satir[0].Replace(" ", "").Trim();
                string oturumStr = satir[2].Replace(" ", "1").Trim();//geçici süreç için
                long opaqInt = opaqStr.ToInt64();
                int oturum = oturumStr.ToInt32();

                var kontrol = opaqList.Find(x => x.OpaqId == opaqInt && x.Oturum == oturum);
                if (kontrol == null)
                {
                    opaqList.Add(new OpaqKontrol(opaqInt, oturum)); //optik formu gelmiş diziye ekle.
                }
                else
                {
                    islemiDurdur = true;
                    yaz.WriteLine(opaqStr + " nolu opaq mükerrerdir.");
                }
            }


            progressBar1.Value = 0;
            yaz.Close();
            yaz.Dispose();
            toolSslKalanSure.Text = "Tamamlandı";

        }

        /// <summary>
        /// CK olmayan öğrenciler, eksik bilgisi olan öğrenciler
        /// </summary>
        private void ExcelRaporOlustur()
        {
            KutukManager kutukManager = new KutukManager();
            int islemSayisi = kutukManager.List().Count;
            progressBar1.Maximum = islemSayisi;
            int a = 0;
            progressBar1.Value = 0;


            List<KutukRapor> ckOlmayanlar = new List<KutukRapor>();

            anaBar++;
            pbAna.Value = anaBar;

            foreach (var q in kutukManager.List())
            {
                a++;
                progressBar1.Value = a;
                toolSslKalanSure.Text = $"Gelen optikler kütükteki kayıtlarla eşleştiriliyor. {a} / {islemSayisi}";
                // kütükteki kayıtlar ile txtteki opaq nolar karşılaştırılıyor. Eşleşmeyenler diziye ekleniyor.
                var kutuk = opaqList.Find(x => x.OpaqId == q.OpaqId);

                if (kutuk == null)
                {
                    ckOlmayanlar.Add(new KutukRapor(q.OpaqId, q.IlceAdi, q.KurumKodu, q.KurumAdi, q.OgrenciNo, q.Adi, q.Soyadi, q.Sinifi, q.Sube, "", "Txt dosyasında bilgisi yok"));
                }
                if (q.KitapcikTuru == "" && q.KatilimDurumu != "0")
                    ckOlmayanlar.Add(new KutukRapor(q.OpaqId, q.IlceAdi, q.KurumKodu, q.KurumAdi, q.OgrenciNo, q.Adi, q.Soyadi, q.Sinifi, q.Sube, q.KatilimDurumu, "Kitapçık türü yok"));
                if (q.KatilimDurumu == "0")
                    ckOlmayanlar.Add(new KutukRapor(q.OpaqId, q.IlceAdi, q.KurumKodu, q.KurumAdi, q.OgrenciNo, q.Adi, q.Soyadi, q.Sinifi, q.Sube, q.KatilimDurumu, "Sinava Girmedi"));

            }

            anaBar++;
            pbAna.Value = anaBar;

            if (ckOlmayanlar.Count > 0)
            {
                string excelDosyaAdi = raporDizinAdresi + "Eksik Bilgisi Olan Öğrenciler.xlsx";

                islemSayisi = ckOlmayanlar.Count;
                progressBar1.Maximum = islemSayisi;
                a = 0;

                Microsoft.Office.Interop.Excel.Application aplicacion =
                    new Microsoft.Office.Interop.Excel.Application();
                Workbook calismaKitabi = aplicacion.Workbooks.Add();
                Worksheet calismaSayfasi = (Worksheet)calismaKitabi.Worksheets.Item[1];
                calismaKitabi.SaveAs(excelDosyaAdi, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);



                calismaSayfasi.Name = "Kitap1";

                //satır, sütun
                calismaSayfasi.Cells[1, 1] = "Opaq/Tc Kimlik";
                calismaSayfasi.Cells[1, 2] = "İlçe Adı";
                calismaSayfasi.Cells[1, 3] = "Kurum Kodu";
                calismaSayfasi.Cells[1, 4] = "Kurum Adı";
                calismaSayfasi.Cells[1, 5] = "No";
                calismaSayfasi.Cells[1, 6] = "Adı";
                calismaSayfasi.Cells[1, 7] = "Soyadı";
                calismaSayfasi.Cells[1, 8] = "Sınıfı";
                calismaSayfasi.Cells[1, 9] = "Şubesi";
                calismaSayfasi.Cells[1, 10] = "Katılım Durumu";
                calismaSayfasi.Cells[1, 11] = "Açıklama";


                for (int i = 0; i < islemSayisi; i++)
                {
                    a++;
                    progressBar1.Value = a;

                    toolSslKalanSure.Text = $"eksik bilgisi olan öğrencilerin bilgileri excele aktarılıyor. {a} / {islemSayisi}";

                    string katilimDurum = ckOlmayanlar[i].KatilimDurumu == "0" ? "Sınava Girmedi" : "Sınava Girdi";
                    calismaSayfasi.Cells[i + 2, 1] = ckOlmayanlar[i].OpaqId;
                    calismaSayfasi.Cells[i + 2, 2] = ckOlmayanlar[i].IlceAdi;
                    calismaSayfasi.Cells[i + 2, 3] = ckOlmayanlar[i].KurumKodu;
                    calismaSayfasi.Cells[i + 2, 4] = ckOlmayanlar[i].KurumAdi;
                    calismaSayfasi.Cells[i + 2, 5] = ckOlmayanlar[i].OgrenciNo;
                    calismaSayfasi.Cells[i + 2, 6] = ckOlmayanlar[i].Adi;
                    calismaSayfasi.Cells[i + 2, 7] = ckOlmayanlar[i].Soyadi;
                    calismaSayfasi.Cells[i + 2, 8] = ckOlmayanlar[i].Sinifi;
                    calismaSayfasi.Cells[i + 2, 9] = ckOlmayanlar[i].Sube;
                    calismaSayfasi.Cells[i + 2, 10] = katilimDurum;
                    calismaSayfasi.Cells[i + 2, 11] = ckOlmayanlar[i].Sonuc;

                }

                calismaKitabi.Save();
                calismaKitabi.Close(true);
                aplicacion.Quit();
                toolSslKalanSure.Text = "Excele aktarıldı";

                Process.Start(excelDosyaAdi);
            }



            progressBar1.Value = 0;
        }
        private List<ResultList> TextListesiniKutuktenKontrolEt(List<string> lines)
        {
            List<ResultList> result = new List<ResultList>();
            KutukManager kutukManager = new KutukManager();

            islemiDurdur = false; //daha önce true yapılmış olabilir.

            int islemSayisi = lines.Count;
            progressBar1.Maximum = islemSayisi;
            int a = 0;
            progressBar1.Value = 0;

            //Öğrenci cevaplarını text dosyasından alıp veritabanında kontrol aşama.
            foreach (string file in lines)
            {
                a++;
                progressBar1.Value = a;

                toolSslKalanSure.Text = $"Text kütük eşleştirmesi yapılıyor. {a} / {islemSayisi}";

                string[] cevapBilgisi = file.Split('#');
                string opaqIdStr = cevapBilgisi[0].Replace(" ", "").Trim();

                long opaqId = opaqIdStr.ToInt64();


                Kutuk kutuk = kutukManager.Find(x => x.OpaqId == opaqId);

                if (kutuk == null)
                {

                    //Kütükte yoksa dizie al.
                    result.Add(new ResultList(cevapBilgisi[0], "Kütükte bulunamadı. Değerlendirmeye devam etmek için bu kaydı düzeltiniz."));
                    islemiDurdur = true;
                }

                Application.DoEvents();
            }

            return result;
        }
        private List<ResultList> OgrenciCevaplariniKutugeKaydet(List<string> lines)
        {
            List<ResultList> result = new List<ResultList>();

            islemiDurdur = false; //daha önce true yapılmış olabilir.


            int islemSayisi = lines.Count;
            progressBar1.Maximum = islemSayisi;
            int a = 0;
            progressBar1.Value = 0;

            //Öğrenci cevaplarını text dosyasından alıp veritabanına kaydeden aşama.
            foreach (string file in lines)
            {
                a++;
                progressBar1.Value = a;

                toolSslKalanSure.Text = $"Cevaplar kütüğe kaydediliyor. {a} / {islemSayisi}";

                //OpaqId/TC#KatılımD#Oturum#DersId#KitTur#Cevaplar#DersId#KitTur#Cevaplar

                string[] cevapBilgisi = file.Split('#');
                string opaqIdStr = cevapBilgisi[0].Replace(" ", "").Trim();
                string katilimDurumu = "";
                //  string oturum = cevapBilgisi[2].Replace(" ", "").Trim();
                string cevaplar = "";

                long opaqId = opaqIdStr.ToInt64();
                for (int t = 1; t < cevapBilgisi.Length; t += 3)
                {
                    //          Ders Id                 Kitapçık Türü               Cevaplar
                    cevaplar += cevapBilgisi[t] + "#" + cevapBilgisi[t + 1] + "#" + cevapBilgisi[t + 2] + "#";
                }

                //En sona eklenen # işareti silelim.
                if (cevaplar.Substring(cevaplar.Length - 1, 1) == "#")
                    cevaplar = cevaplar.Substring(0, cevaplar.Length - 1);


                bool cevapVerdimi = (cevaplar.Contains("A") || cevaplar.Contains("B") || cevaplar.Contains("C") || cevaplar.Contains("D") || cevaplar.Contains("E"));

                if (katilimDurumu != "0" && cevapVerdimi)
                {
                    katilimDurumu = ""; //Katılım durumu farklı değer gelmiş. Girmeyenler için 0 işaretlenmesi yeterli. Düzeltelim.
                }
                if (katilimDurumu != "0" && cevapVerdimi == false)
                {
                    katilimDurumu = "0"; //Katılım durumu farklı değer gelmiş. Cevap vermediği için Girmeyenler için 0 işaretlenmesi yeterli. Düzeltelim.
                }

                if (katilimDurumu == "0" && cevapVerdimi)
                {
                    katilimDurumu = "";
                    result.Add(new ResultList(opaqIdStr, "Sınava girmedi olarak işaretlenmiş. Girdi olarak düzeltildi."));
                }


                KutukManager kutukManager = new KutukManager();
                Kutuk kutuk = kutukManager.Find(x => x.OpaqId == opaqId);

                if (kutuk != null)
                {
                    kutuk.KatilimDurumu = katilimDurumu;
                    kutuk.Cevaplar = cevaplar;
                    kutukManager.Update(kutuk);
                }
                else
                {
                    //Kütükte yoksa dizie al.
                    result.Add(new ResultList(opaqIdStr, "Kütükte bulunamadı. Değerlendirmeye devam etmek için bu kaydı düzeltiniz."));
                    islemiDurdur = true;
                }

                Application.DoEvents();
            }

            return result;
        }
        private List<KarneSonuc> OgrenciCevaplariniDegerlendir()
        {

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


                toolSslKalanSure.Text = $"Öğrenci cevapları değerlendiriliyor. {a} / {ogrCevaplar.Count}";


                if (ogr.KatilimDurumu != "0" && ogr.KurumKodu != 0)
                {
                    if (ogr.Cevaplar != null)
                    {
                        string[] cevap = ogr.Cevaplar.Split('#');
                        // Öğreninin cevapları 2#A#CBBBBBACACBABCBAACAB#2#A#CBBBBBACACBABCBAACAB
                        // DersId#KitTur#Cevaplar#DersId#KitTur#Cevaplar gibi

                        for (int i = 0; i < cevap.Length; i += 3) //Öğrenci cevaplarında dersler üç bölüm o yüzden üçer ilerlemesi gerekiyor
                        {
                            int bransId = cevap[i].ToInt32();
                            string kitapcikTuru = cevap[i + 1];
                            string bransOgrenciCevap = cevap[i + 2].Replace(";", "");

                            if (!string.IsNullOrEmpty(kitapcikTuru))
                            {
                                if (bransOgrenciCevap != "")
                                {
                                    DogruCevap dcvp = dogruCevapList.FirstOrDefault(x => x.Sinif == ogr.Sinifi && x.BransId == bransId && x.KitapcikTuru == kitapcikTuru);
                                    if (dcvp != null)
                                    {
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
                                                    x.KitapcikTuru == kitapcikTuru);

                                                KarneSonuc ks = new KarneSonuc()
                                                {
                                                    Ilce = ogr.IlceAdi,
                                                    BransId = bransId,
                                                    KitapcikTuru = kitapcikTuru,
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
                                }
                            }
                            Application.DoEvents();
                        }
                    }
                }

                Application.DoEvents();
            }
            progressBar1.Value = 0;

            return karneSonucList;
        }
        private void SubeDuzeyindeKarneSonuclariniKaydet(List<KarneSonuc> karneSonucList, StreamWriter yaz)
        {
            KutukManager kutukManager = new KutukManager();
            var kutukList = kutukManager.List();

            AyarlarManager ayarlar = new AyarlarManager();
            var ayar = ayarlar.AyarlariGetir();
            int sinavId = kutukList.First().SinavId; //Sınavın numarasını aldık

            yaz.WriteLine("{SinavAdi}|" + sinavId + "|" + ayar.SinavAdi + "|" + ayar.DegerlendirmeTuru);


            progressBar1.Maximum = karneSonucList.Count;
            progressBar1.Value = 0;
            int a = 0;

            foreach (var ogr in karneSonucList)
            {
                a++;
                progressBar1.Value = a;


                toolSslKalanSure.Text = $"Şube düzeyinde karne sonuçları kaydediliyor. {a} / {karneSonucList.Count} ";

                yaz.WriteLine("{KarneSonuclari}|" + ogr.BransId + "|" + ogr.Ilce + "|" + ogr.KurumKodu + "|" +
                              ogr.Sinif + "|" + ogr.Sube + "|" + ogr.KitapcikTuru + "|" + ogr.SoruNo + "|" + ogr.Dogru +
                              "|" + ogr.Yanlis + "|" + ogr.Bos);
            }
            progressBar1.Value = 0;
        }
        private List<IlIlceDogruYanlisBosModeli> IlIlceBasariHesapla(List<KarneSonuc> karneSonucList)
        {
            List<IlIlceDogruYanlisBosModeli> ilIlceOrtalamalariList = new List<IlIlceDogruYanlisBosModeli>();

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

                    toolSslKalanSure.Text = $"İl ilçe ortalaması hesaplanıyor. {a} / {islemSayisi}";


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

            progressBar1.Value = 0;
            return ilIlceOrtalamalariList;
        }
        private void IlceBasariKaydet(List<IlIlceDogruYanlisBosModeli> ilIlceOrtalamalariList, StreamWriter yaz)
        {
            int islemSayisi = ilIlceOrtalamalariList.Count;
            progressBar1.Maximum = islemSayisi;
            progressBar1.Value = 0;
            int a = 0;




            foreach (var ort in ilIlceOrtalamalariList)
            {
                a++;
                progressBar1.Value = a;

                toolSslKalanSure.Text = $"İlçe ortalaması kaydediliyor. {a} / {islemSayisi}";

                yaz.WriteLine("{IlceOrtalamasi}|" + ort.Ilce + "|" + ort.BransId + "|" + ort.Sinif + "|" + ort.Dogru + "|" + ort.Yanlis + "|" + ort.Bos + "|" + ort.KazanimId + "|" + ort.IlBasariYuzdesi + "|" + ort.IlceBasariYuzdesi);
                Application.DoEvents();
            }

            progressBar1.Value = 0;

        }
        private void DogruYanlislariHesapla()
        {
            List<OgrenciSonucModel> karneSonucList = new List<OgrenciSonucModel>();


            StreamWriter yaz = new StreamWriter(raporDizinAdresi + "DogruYanlisSayilari.txt");

            KutukManager kutukManager = new KutukManager();
            var ogrCevaplar = kutukManager.List();

            progressBar1.Maximum = ogrCevaplar.Count;
            progressBar1.Value = 0;

            anaBar++;
            pbAna.Value = anaBar;

            DogruCevaplarManager dogruCevaplarDb = new DogruCevaplarManager();
            var dogruCevapList = dogruCevaplarDb.List();

            int a = 0;

            foreach (var ogr in ogrCevaplar) //Öğrenci cevaplarını tek tek al
            {
                a++;
                progressBar1.Value = a;

                toolSslKalanSure.Text = $"Öğrenci cevapları değerlendiriliyor. {a} / {ogrCevaplar.Count}";


                if (ogr.KatilimDurumu != null && ogr.KatilimDurumu != "0" && ogr.KurumKodu != 0)
                {
                    string[] cevap = ogr.Cevaplar.Split('#');
                    //öğreninin cevapları 2#B#BCAABCDACB#4#B#CAABDBACBA#1#B#AABDBCABDC#3#B#ACBDC#6#CBDAB#7#B#DACBB gibi
                    //DERSİD#KİTAPÇIK TÜRÜ#CEVAP

                    //Branş branş cevapları alır
                    for (int i = 0; i < cevap.Length; i += 3)
                    {
                        int bransId = cevap[i].ToInt32();
                        string kitapcikTuru = cevap[i + 1];
                        if (!string.IsNullOrEmpty(kitapcikTuru))
                        {
                            string bransOgrenciCevap = cevap[i + 2].Replace(";", ""); //öğrencinin cevabı

                            int dogru = 0;
                            int yanlis = 0;
                            int bos = 0;
                            DogruCevap dcvp = dogruCevapList.FirstOrDefault(x =>
                                x.Sinif == ogr.Sinifi && x.BransId == bransId && x.KitapcikTuru == kitapcikTuru);
                            if (dcvp != null)
                            {
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
                            }

                            karneSonucList.Add(new OgrenciSonucModel(ogr.OpaqId, ogr.IlceAdi, ogr.KurumKodu, ogr.KurumAdi, bransId,
                                ogr.Sinifi, ogr.Sube, kitapcikTuru, ogr.OgrenciNo, ogr.Adi, ogr.Soyadi, ogr.KatilimDurumu,
                                dogru, yanlis, bos));

                            Application.DoEvents();
                        }
                    }
                }

                Application.DoEvents();
            }

            progressBar1.Value = 0;


            a = 0;
            var ogrenciList = karneSonucList.GroupBy(x => x.OpaqId).Select(x => x.First()).ToList();
            int islemSayisi = ogrenciList.Count;
            progressBar1.Maximum = islemSayisi;

            anaBar++;
            pbAna.Value = anaBar;

            ArrayList aList = new ArrayList();
            string[,] ogrenciXls = new string[islemSayisi, 40];
            //öğreni doğru yanlış sayıları
            foreach (var ogr in ogrenciList)
            {
                a++;
                progressBar1.Value = a;


                toolSslKalanSure.Text = $"Öğrenci doğru yanlış sayıları hesaplanıyor. {a} / {islemSayisi}";


                string ogrenciSonucStr = "";
                var branslar = karneSonucList.Where(x => x.OpaqId == ogr.OpaqId).GroupBy(x => x.BransId).Select(x => x.First())
                    .OrderBy(x => x.BransId).ToList();
                foreach (var brans in branslar)
                {
                    ogrenciSonucStr += brans.BransId + "|" + ogr.KitapcikTuru+"|" + brans.Dogru + "|" + brans.Yanlis + "|" + brans.Bos + "|";
                }

                string ogrenciDYB = ogr.Ilce + "|" + ogr.KurumKodu + "|" + ogr.KurumAdi + "|" + ogr.OpaqId + "|" +
                                    ogr.Adi + "|" +
                                    ogr.Soyadi + "|" + ogr.OgrenciNo + "|" + ogr.Sinif + "|" + ogr.Sube + "|" +
                                    ogr.KatilimDurumu + "|" + ogrenciSonucStr;

               // yaz.WriteLine("{OgrenciDYB}|" + ogrenciDYB);

                string[] ogrenciDybs = ogrenciDYB.Split('|');


            }


            a = 0;
            var okullar = karneSonucList.GroupBy(x => x.KurumKodu).Select(x => x.First()).ToList();
            islemSayisi = okullar.Count;
            progressBar1.Maximum = islemSayisi;

            anaBar++;
            pbAna.Value = anaBar;

            //okulların  doğru yanlış sayıları
            foreach (var okul in okullar)
            {
                a++;
                progressBar1.Value = a;
                toolSslKalanSure.Text = $"Okul doğru yanlış sayıları hesaplanıyor. {a} / {islemSayisi}";

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

                        sinifSonucStr += brans.BransId + "#" + okulDogru + "#" + okulYanlis + "#" + okulBos + "#";
                    }

                    yaz.WriteLine("{SinifDYB}|" + sinif.Ilce + "|" + okul.KurumKodu + "|" + okul.KurumAdi + "|" + sinif.Sinif +
                                  "|" + ogrSayisi + "|" + sinifSonucStr);
                }
            }

            a = 0;
            var ilceler = karneSonucList.GroupBy(x => x.Ilce).Select(x => x.First()).ToList();
            islemSayisi = ilceler.Count;
            progressBar1.Maximum = islemSayisi;

            anaBar++;
            pbAna.Value = anaBar;

            //ilçe doğru yanlış sayıları
            foreach (var ilce in ilceler)
            {
                a++;
                progressBar1.Value = a;

                toolSslKalanSure.Text = $"İlçe doğru yanlış sayıları hesaplanıyor. {a} / {islemSayisi}";

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

                        ilceSonucStr += brans.BransId + "#" + okulDogru + "#" + okulYanlis + "#" + okulBos + "#";
                    }

                    yaz.WriteLine("{IlceDYB}|" + sinif.Ilce + "|" + sinif.Sinif + "|" + ogrSayisi + "|" + ilceSonucStr);
                }
            }



            yaz.Close();
            yaz.Dispose();
            progressBar1.Value = 0;
        }
        private void CkDataOlustur()
        {
            string raporUrl = raporDizinAdresi + @"\KarneSonuc_2.ck";


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


            StreamWriter yaz = new StreamWriter(raporUrl);


            //Kütük
            foreach (var kutuk in kutukList)
            {
                a++;
                progressBar1.Value = a;

                toolSslKalanSure.Text = $"Kütük tablosu hazırlanıyor. {a} / {kayitSayisi}";

                yaz.WriteLine("{Kutuk}|" + kutuk.OpaqId + "|" + kutuk.IlceAdi + "|" + kutuk.KurumKodu + "|" +
                                   kutuk.KurumAdi + "|" + kutuk.OgrenciNo + "|" + kutuk.Adi + "|" + kutuk.Soyadi + "|" +
                                   kutuk.Sinifi + "|" + kutuk.Sube + "|" + kutuk.KatilimDurumu + "|" + kutuk.Cevaplar);
            }

            //DogruCevaplar
            foreach (var dogruCevap in dogruCvplarList)
            {
                a++;
                progressBar1.Value = a;

                toolSslKalanSure.Text = $"Doğru cevaplar tablosu hazırlanıyor. {a} / {kayitSayisi}";

                yaz.WriteLine("{DogruCevaplar}|" + dogruCevap.Sinif + "|" + dogruCevap.BransId + "|" + dogruCevap.KitapcikTuru + "|" + dogruCevap.Cevaplar);
            }

            //KarneSonuclari
            foreach (var sonuc in karneSonucList)
            {
                a++;
                progressBar1.Value = a;

                toolSslKalanSure.Text = $"Karne sonuçları tablosu hazırlanıyor. {a} / {kayitSayisi}";

                yaz.WriteLine("{KarneSonuclari}|" + "|" + sonuc.BransId + "|" + sonuc.Ilce + "|" + sonuc.KurumKodu + "|" + sonuc.Sinif + "|" + sonuc.Sube + "|" + sonuc.KitapcikTuru + "|" + sonuc.SoruNo + "|" + sonuc.Dogru + "|" + sonuc.Yanlis + "|" + sonuc.Bos);
            }
            //Branslar
            foreach (var brans in bransList)
            {
                a++;
                progressBar1.Value = a;

                toolSslKalanSure.Text = $"Branşlar tablosu hazırlanıyor. {a} / {kayitSayisi}";

                yaz.WriteLine("{Branslar}|" + brans.Id + "|" + brans.BransAdi);
            }
            //Kazanimlar
            foreach (var kazanim in kazanimlarList)
            {
                a++;
                progressBar1.Value = a;
                toolSslKalanSure.Text =
                    $"Kazanımlar tablosu hazırlanıyor. {a} / {kayitSayisi}";

                yaz.WriteLine("{Kazanimlar}|" + kazanim.Id + "|" + kazanim.Sinif + "|" + kazanim.BransId + "|" + kazanim.KazanimNo + "|" + kazanim.KazanimAdi + "|" + kazanim.KazanimAdiOgrenci + "|" + kazanim.Sorulari);
            }

            toolSslKalanSure.Text = "Dosyaya yazma işlemi tamamlanıyor.";
            yaz.Close();
            progressBar1.Value = 0;

        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblGecenSure.Text = string.Format("Geçen süre: {0}:{1}:{2}", saat.ToString("D2"), dakika.ToString("D2"), saniye.ToString("D2"));
            saniye++;
            if (saniye == 59)
            {
                saniye = 0;
                dakika++;
                if (dakika == 59)
                {
                    saat++;
                    dakika = 0;
                }
            }
        }

        private void btnDogruCevaplar_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı kapat
                folderDialog.Description = @"Doru yanlış dosyasının saklanacağı dizini seçiniz.";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    raporDizinAdresi = folderDialog.SelectedPath + "\\";
                    timer1.Enabled = true; //sayaç başlasın

                    //Birinci aşamada cevapları alıp cevapların kontrolünü yaparak kütüğe kaydeder.
                    bgwDogruYanlislariHesapla.RunWorkerAsync();

                }
                folderDialog.Dispose();
            }
        }

        private void bgwDogruYanlislariHesapla_DoWork(object sender, DoWorkEventArgs e)
        {
            pbAna.Maximum = 4;
            pbAna.Value = 0;
            DogruYanlislariHesapla();
            pbAna.Value = 0;
            toolSslKalanSure.Text = "Tamamlandı";
        }
    }
}
