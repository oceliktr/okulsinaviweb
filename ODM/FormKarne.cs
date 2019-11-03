using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DAL;
using ODM.Kutuphanem;

namespace ODM
{

    public partial class FormKarne : Form
    {
        public class KitapcikTuru
        {
            public string Turu { get; set; }
            public KitapcikTuru()
            {

            }
            public KitapcikTuru(string turu)
            {
                Turu = turu;
            }
        }
        private readonly int sinavId;
        private readonly List<KitapcikTuru> kitapcikTurleri;
        public FormKarne()
        {
            InitializeComponent();

            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo snvInfo = snvDb.AktifSinavAdi();
            sinavId = snvInfo.SinavId;

            List<KitapcikTuru> lst = new List<KitapcikTuru>();
            for (int i = 0; i < snvInfo.KitapcikTurleri.Length; i++)
            {
                lst.Add(new KitapcikTuru(snvInfo.KitapcikTurleri.Substring(i, 1)));
            }
            kitapcikTurleri = lst;


            lblSinavId.Text = string.Format("Sınav no: {0}", snvInfo.SinavId);
            lblSinavAdi.Text = string.Format("Sınav adı: {0}", snvInfo.SinavAdi);


            string ilAdi = IniIslemleri.VeriOku("Baslik", "IlAdi");
            Text = "Sınıf-Şube Karne Hazırlama Formu " + ilAdi.IlkHarfleriBuyut() + " Ölçme ve Değerlendirme Merkezi";
        }

        private void btnKarneHazirla_Click(object sender, EventArgs e)
        {
            RubrikDb rbrDb = new RubrikDb();
            List<RubrikInfo> brans = rbrDb.SinavdakiBranslar(sinavId);

            if (brans.Count == 0)
            {
                MessageBox.Show("Ruprik tamamlanmadığı görülüyor. Öncelikle ruprik tanımlamalarını yapınız.", "Uyarı", MessageBoxButtons.OK);
            }
            else
            {

                OgrencilerDb ogrDb = new OgrencilerDb();
                if (ogrDb.KayitKontrol(sinavId))
                {
                    backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Öğrenci bilgileri girilmediği görülüyor.", "Uyarı");
                }
            }

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            btnKarneHazirla.Enabled = false;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int a = 0;

            KitapcikCevapDB kcvDb = new KitapcikCevapDB();
            List<KitapcikCevapInfo> brans = kcvDb.SinavdakiBranslar(sinavId);

            //CkDosyalarDB ckDosya = new CkDosyalarDB();
            //List<CkDosyalarInfo> ckList = ckDosya.KayitlariDiziyeGetir(sinavId);
            //int ogrenciKarneSayisi = ckList.Where(x => x.Girmedi == 0).ToList().Count;

            OgrencilerDb ogrDb = new OgrencilerDb();
            SonucOptikDB osDb = new SonucOptikDB();
            List<OgrencilerInfo> subeList = ogrDb.SinavaGirenSubeler(sinavId);
            SinifKarneDB karneDb = new SinifKarneDB();
            //karneDb.KayitSil(sinavId);

            //OgrenciKarneDB ogrKrnDb = new OgrenciKarneDB();
            //ogrKrnDb.KayitSil(sinavId);

            KazanimKarneDB kznmKrnDb = new KazanimKarneDB();
            kznmKrnDb.KayitSil(sinavId);

            RubrikDb rbrDb = new RubrikDb();
            KazanimlarDB kznmDb = new KazanimlarDB();

            int subeSayisi = subeList.Count;
            int bransSayisi = brans.Count;

            KurumlarDb krmDb = new KurumlarDb();
            List<KurumlarInfo> okulList = krmDb.SinavaGirenOkullariDiziyeGetir(sinavId);

            int islemSayisi = ((subeSayisi * bransSayisi) * 2) + (okulList.Count * brans.Count);//+ ogrenciKarneSayisi;

            progressBar1.Maximum = islemSayisi;

            ////////// ÖĞRENCİ KARNESİ
            //foreach (var ogr in ckList.Where(x => x.Girmedi == 0))
            //{
            //    a++;
            //    progressBar1.PerformStep();

            //    OgrencilerInfo ogrInfo = ogrDb.KayitBilgiGetir(ogr.OgrenciId, sinavId);
            //    lblBilgi.Text = a + "/" + ogrenciKarneSayisi + " - " + ogrInfo.Adi + " " + ogrInfo.Soyadi + " karnesi oluşturuluyor.";

            //    Application.DoEvents();

            //    int dogruSayisi = osDb.KayitSayisi(sinavId, ogrInfo.KurumKodu, ogr.Oturum, ogr.KitapcikTuru, ogr.OgrenciId, 1);
            //    int yanlisSayisi = osDb.KayitSayisi(sinavId, ogrInfo.KurumKodu, ogr.Oturum, ogr.KitapcikTuru, ogr.OgrenciId, 0);
            //    int bosSayisi = osDb.BosKayitSayisi(sinavId, ogrInfo.KurumKodu, ogr.Oturum, ogr.KitapcikTuru, ogr.OgrenciId);

            //    OgrenciKarneInfo karne = new OgrenciKarneInfo
            //    {
            //        SinavId = sinavId,
            //        KurumKodu = ogrInfo.KurumKodu,
            //        KitapcikTuru = ogr.KitapcikTuru,
            //        Sinif = ogrInfo.Sinifi,
            //        Sube = ogrInfo.Sube,
            //        DogruSayisi = dogruSayisi,
            //        YanlisSayisi = yanlisSayisi,
            //        Bos = bosSayisi,
            //        OgrenciId = ogr.OgrenciId,
            //        BransId = ogr.Oturum
            //    };
            //    ogrKrnDb.KayitEkle(karne);

            //    Application.DoEvents();
            //    lblGecenSure.Text = string.Format("{0} saat, {1} dakika, {2} saniye", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);
            //    try
            //    {
            //        double gecenDakika = watch.Elapsed.TotalMinutes;
            //        double kalanIslem = islemSayisi - a;
            //        double kalanSure = (gecenDakika * kalanIslem) / a;

            //        double saat = (kalanSure - (kalanSure % 60)) / 60;
            //        double dakika = kalanSure % 60;
            //        lblBitisSuresi.Text = string.Format("Kalan tahmini süre : {0:0} saat, {1:0} dakika", saat, dakika);
            //    }
            //    catch (Exception)
            //    {
            //        lblBitisSuresi.Text = "Hesaplanıyor...";
            //    }
            //}
            //Application.DoEvents();

            /////////////SINIF KARNESİ
            //foreach (OgrencilerInfo sube in subeList)
            //{

            //    foreach (KitapcikCevapInfo b in brans)
            //    {
            //        foreach (var kt in kitapcikTurleri)
            //        {
            //            a++;

            //            progressBar1.PerformStep();
            //            List<KitapcikCevapInfo> soruSayisi = kcvDb.SinavdakiSoruNolar(sinavId, sube.Sinifi, b.BransId);

            //            foreach (var soru in soruSayisi)
            //            {
            //                lblBilgi.Text = a + "/" + islemSayisi + " - " + sube.IlceAdi + " - " + sube.KurumAdi +
            //                                " " + sube.Sinifi + "-" + sube.Sube + " şubesi - " + b.BransAdi +
            //                                " dersi " + kt.Turu + " kitapçığı " + soru.SoruNo +
            //                                " nolu soru hesaplanıyor - Sınıf Karnesi";

            //                Application.DoEvents();

            //                try
            //                {
            //                    List<SonucOptikInfo> kayitSayisi = osDb.KayitSayisi(sinavId, sube.KurumKodu, b.BransId, soru.SoruNo, kt.Turu, sube.Sinifi, sube.Sube).ToList();
            //                    int dogruSayisi = kayitSayisi[0].DogruSayisi.ToInt32();
            //                    int yanlisSayisi = kayitSayisi[0].YanlisSayisi.ToInt32();
            //                    int bosSayisi = kayitSayisi[0].BosSayisi.ToInt32();

            //                    SinifKarneInfo karne = new SinifKarneInfo
            //                    {
            //                        SinavId = sinavId,
            //                        KurumKodu = sube.KurumKodu,
            //                        KitapcikTuru = kt.Turu,
            //                        Sinif = sube.Sinifi,
            //                        Sube = sube.Sube,
            //                        Brans = b.BransId,
            //                        Soru = soru.SoruNo,
            //                        Dogru = dogruSayisi,
            //                        Yanlis = yanlisSayisi,
            //                        Bos = bosSayisi
            //                    };
            //                    karneDb.KayitEkle(karne);
            //                }
            //                catch (Exception)
            //                {
            //                    //
            //                }

            //                Application.DoEvents();
            //                lblGecenSure.Text = string.Format("Geçen süre : {0} saat, {1} dakika, {2} saniye", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);
            //            }

            //            try
            //            {
            //                double gecenDakika = watch.Elapsed.TotalMinutes;
            //                double kalanIslem = islemSayisi - a;
            //                double kalanSure = (gecenDakika * kalanIslem) / a;

            //                double saat = (kalanSure - (kalanSure % 60)) / 60;
            //                double dakika = kalanSure % 60;
            //                lblBitisSuresi.Text = string.Format("Kalan tahmini süre : {0:0} saat, {1:0} dakika\n\nBitiş saati (yaklaşık): {2}", saat, dakika, DateTime.Now.AddHours(saat).AddMinutes(dakika));
            //            }
            //            catch (Exception)
            //            {
            //                //
            //            }
            //        }
            //        Application.DoEvents();
            //    }

            //    Application.DoEvents();
            //}
            //Application.DoEvents();

            ////////////KAZANIM KARNESİ
            foreach (var brns in brans)
            {
                List<RubrikInfo> kazanimLst = rbrDb.SinavdakiKazanimlar(sinavId, brns.BransId);

                foreach (var okul in okulList)
                {
                    List<OgrencilerInfo> siniflar = ogrDb.SinavaGirenSiniflar(sinavId, okul.KurumKodu.ToInt32());
                    foreach (var snf in siniflar)
                    {
                        List<OgrencilerInfo> subeler = ogrDb.SinavaGirenSubeler(sinavId, okul.KurumKodu.ToInt32(), snf.Sinifi);
                        foreach (var sb in subeler)
                        {
                            a++;
                            progressBar1.PerformStep();
                            lblBilgi.Text = string.Concat(a, "/", islemSayisi, " - ", okul.IlceAdi, " - ", okul.KurumAdi, " ", sb.SinifSube, " şubesi ", brns.BransAdi, " dersi kazanımları hesaplanıyor. - Kazanım Karnesi");

                            Application.DoEvents();
                            foreach (var kznm in kazanimLst)
                            {
                                int kznmNo = kznm.Kazanimlar.Replace("|", "").ToInt32();

                                KazanimlarInfo kznmInfo = kznmDb.KayitBilgiGetir(kznmNo);
                                string kazanimNo = kznmInfo.AltOgrenmeAlani == 0 ? string.Concat(brns.BransAdi.Substring(0, 1), ".", kznmInfo.Sinif, ".", kznmInfo.OgrenmeAlani, ".", kznmInfo.KazanimNo) : string.Concat(brns.BransAdi.Substring(0, 1), ".", kznmInfo.Sinif, ".", kznmInfo.OgrenmeAlani, ".", kznmInfo.AltOgrenmeAlani, ".", kznmInfo.KazanimNo);
                                string kazanim = kznmInfo.Kazanim;


                                List<RubrikInfo> sorular = rbrDb.KazanimdakiSoruNolar(kznm.Kazanimlar, sinavId, snf.Sinifi, brns.BransId);


                                string sqlText = "";
                                foreach (var s in sorular)
                                {
                                    sqlText += "(Soru=" + s.SoruNo + " and KitapcikTuru='" + s.KitapcikTuru + "') or";
                                }

                                List<SinifKarneInfo> karne = karneDb.DogruYanlisBosSayisi(sinavId, okul.KurumKodu.ToInt32(), snf.Sinifi, sb.Sube, brns.BransId, sqlText)
                                    .ToList();
                                int dogruSayisi = karne[0].Dogru;
                                int yanlisSayisi = karne[0].Yanlis;
                                int bosSayisi = karne[0].Bos;
                                int ogrenciSayisi = dogruSayisi + yanlisSayisi + bosSayisi;

                                KazanimKarneInfo karneInfo = new KazanimKarneInfo
                                {
                                    SinavId = sinavId,
                                    KurumKodu = okul.KurumKodu.ToInt32(),
                                    KitapcikTuru = "",
                                    Sinif = snf.Sinifi,
                                    Sube = sb.Sube,
                                    DogruSayisi = dogruSayisi,
                                    YanlisSayisi = yanlisSayisi,
                                    BosSayisi = bosSayisi,
                                    OgrenciSayisi = ogrenciSayisi,
                                    BransId = brns.BransId,
                                    Kazanim = kazanim,
                                    KazanimNo = kazanimNo
                                };
                                kznmKrnDb.KayitEkle(karneInfo);

                                Application.DoEvents();

                            }
                            lblGecenSure.Text = string.Format("{0} saat, {1} dakika, {2} saniye", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);
                            try
                            {
                                double gecenDakika = watch.Elapsed.TotalMinutes;
                                double kalanIslem = islemSayisi - a;
                                double kalanSure = (gecenDakika * kalanIslem) / a;

                                double saat = (kalanSure - (kalanSure % 60)) / 60;
                                double dakika = kalanSure % 60;
                                lblBitisSuresi.Text = string.Format("Kalan tahmini süre : {0:0} saat, {1:0} dakika\n\nBitiş saati (yaklaşık): {2}", saat, dakika, DateTime.Now.AddHours(saat).AddMinutes(dakika));
                                           }
                            catch (Exception)
                            {
                                //
                            }
                            Application.DoEvents();
                        }
                    }

                }
                Application.DoEvents();
            }
            Application.DoEvents();

            btnKarneHazirla.Enabled = true;
            progressBar1.Value = 0;
            lblBilgi.Text = "Karne hesaplama işlemleri tamamlandı.";
            watch.Stop();

            //İşaretliyse işlem bittiğinde bilgisayarı kapat
            if (cbBilgisayariKapat.Checked)
            {
                Process.Start("shutdown", "-f -s");
            }
        }
        }
}
