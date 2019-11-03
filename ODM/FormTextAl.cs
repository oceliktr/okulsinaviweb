using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DAL;
using MySql.Data.MySqlClient;
using ODM.Kutuphanem;

namespace ODM
{
    public partial class FormTextAl : Form
    {
        private static readonly string ckDizin = IniIslemleri.VeriOku("CKDizinKontrol", "CKBulunduguAdres");
        private int sinavId;
        public FormTextAl()
        {
            InitializeComponent();
            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo sinfo = snvDb.AktifSinavAdi();
            sinavId = sinfo.SinavId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog
            {
                Filter = "Text (*.txt)|*.txt",
                Title = "Text dosyasını seçiniz"
            };

            if (o.ShowDialog() != DialogResult.OK) return;

            StreamReader SW = new StreamReader(o.FileName);
            string satir;

            while ((satir = SW.ReadLine()) != null)
            {
                listBox1.Items.Add(satir);
                //  richTextBox1.AppendText(satir + "\n");
            }
            SW.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a = 0;
            progressBar1.Maximum = listBox1.Items.Count;
            progressBar1.Value = 0;

            foreach (var item in listBox1.Items)
            {
                a++;
                int oturum = item.ToString().Substring(0, 1).ToInt32();

                int ogrNo = item.ToString().Substring(1, 5).ToInt32();
                string kitapcikTuru = item.ToString().Substring(item.ToString().Length - 1, 1);
                string girmedi = item.ToString().Substring(item.ToString().Length - 2, 1);
                string cevaplar = item.ToString().Substring(6, item.ToString().Length - 8);

                //AbdusselamDB veriDb = new AbdusselamDB();

                OgrencilerDb ogrDb = new OgrencilerDb();
                OgrencilerInfo ogrInfo = ogrDb.KayitBilgiGetir(ogrNo, sinavId);

                if (girmedi != "G")
                {

                    int soruNo = 0;
                    for (int i = 6; i < item.ToString().Length - 2; i++)
                    {
                        int puani;
                        soruNo++;
                        string secenek = item.ToString().Substring(i, 1);

                        KitapcikCevapDB kcvpDb = new KitapcikCevapDB();
                        KitapcikCevapInfo kcInfo = kcvpDb.KayitBilgiGetir(sinavId, oturum, oturum, soruNo);

                        if (kitapcikTuru == "A")
                        {
                            puani = secenek == kcInfo.KitapcikA ? kcInfo.SoruPuani : 0;
                        }
                        else
                        {
                            puani = secenek == kcInfo.KitapcikB ? kcInfo.SoruPuani : 0;
                        }
                        //MessageBox.Show(soruNo + "-" + secenek +"-"+puani+ "- kt" + kitapcikTuru+"a:"+kcInfo.KitapcikA+" b:"+kcInfo.KitapcikB);
                        //AbdusselamInfo info = new AbdusselamInfo
                        //{
                        //    Oturum = oturum,
                        //    BransId = oturum,
                        //    OgrenciId = ogrNo,
                        //    SoruNo = soruNo,
                        //    SinavId = 6,
                        //    Secenek = secenek,
                        //    KurumKodu = ogrInfo.KurumKodu,
                        //    Puani = puani,
                        //    KitapcikTuru = kitapcikTuru

                        //};
                        //veriDb.KayitEkle(info);
                        Application.DoEvents();
                    }
                }
                Application.DoEvents();

                progressBar1.Value = a;
            }
            progressBar1.Value = 0;
            MessageBox.Show("Tamamlandı");
        }

        private void btnTxteCevir_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            CkDosyalarDB ckDb = new CkDosyalarDB();
            //  SonucOptikDB sncDb = new SonucOptikDB();
            //   KitapcikCevapDB kcvDb = new KitapcikCevapDB();
            //    OgrencilerDb ogrDb = new OgrencilerDb();

            List<CkDosyalarInfo> ckList = ckDb.KayitlariDiziyeGetir(sinavId);

            string dosyaAdi = ckDizin + "\\txt_rapor.txt";
            StreamWriter sqlSW = new StreamWriter(dosyaAdi, false, Encoding.UTF8);
            int toplamKayit = ckList.Count;
            progressBar1.Maximum = toplamKayit;
            progressBar1.Value = 0;
            int a = 0;
            foreach (var ck in ckList)
            {
                a++;
                //  OgrencilerInfo info = ogrDb.KayitBilgiGetir(ck.OgrenciId, ck.SinavId);
                string oturumTcKimlik = string.Concat(ck.Oturum, ck.KitapcikTuru, ck.OgrenciId);

                lblBilgi.Text = a + "/" + toplamKayit;//+ " " + info.Adi + " " + info.Soyadi + " notları alınıyor. ";

                //    List<KitapcikCevapInfo> soruSayisi = kcvDb.SinavdakiSoruNolar(sinavId, 5, ck.Oturum);
                int soruSayisi = ck.Oturum == 3 ? 20 : 25;

                string secenekler = "";
                for (var index = 1; index <= soruSayisi; index++)
                {
                    secenekler = index.ToString();
                    //var soru = soruSayisi[index];

                    //  string scnk = sncDb.SecenkGetir(sinavId, ck.Oturum, ck.OgrenciId, index);
                    string scnk = GetValue(ck.Oturum, ck.OgrenciId, index);
                    if (string.IsNullOrEmpty(scnk))
                        secenekler += "*";
                    else
                        secenekler += scnk;

                }
                if (ck.Girmedi == 1)
                    secenekler += "G";
                else
                    secenekler += " ";

                sqlSW.WriteLine(oturumTcKimlik + secenekler);
                Application.DoEvents();

                lblGecenSure.Text = string.Format("{0} saat, {1} dakika, {2} saniye", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);
                progressBar1.PerformStep();
            }
            sqlSW.Close();
            watch.Stop();
            progressBar1.Value = 0;
            Process.Start(dosyaAdi);

        }

        private string GetValue(int bransId, int ogrId, int soruNo)
        {
            string ConnStr = "Database=odm25erzosmntr; Data Source=localhost; User ID=root; password=00100";
            MySqlConnection baglanti = new MySqlConnection(ConnStr);

            string cmdText = string.Format("select sonucoptik.Secenek from sonucoptik where SinavId={0} and BransId={1} and OgrenciId={2} and SoruNo={3}", sinavId, bransId, ogrId, soruNo);

            MySqlCommand cmd = new MySqlCommand();
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = cmdText;
            cmd.ExecuteNonQuery();

            string scnk = "";
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                scnk = dr["Secenek"].ToString();
            }

            baglanti.Dispose();
            baglanti.Close();

            return scnk;

        }

        private void btnDbKaydetEokul_Click(object sender, EventArgs e)
        {
            int a = 0;
            int kayitSayisi = listBox1.Items.Count;
            progressBar1.Maximum = kayitSayisi;
            progressBar1.Value = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (var item in listBox1.Items)
            {
                a++;
                string dataVeri = item.ToString();
                //string sinavKodu = dataVeri.Split('#')[0];
                int opaqId = dataVeri.Split('#')[1].ToInt32();
                string kitapcikTuru = dataVeri.Split('#')[3];
                int sinavaGirdiGirmedi = dataVeri.Split('#')[5].ToInt32();
                string cevap = dataVeri.Split('#')[6];

                int dersNo = dataVeri.Split('#')[2].Substring(0, 1).ToInt32();
                if (dersNo == 4) dersNo = 3;

                if (sinavaGirdiGirmedi == 1)
                {
                    OgrencilerDb ogrDb = new OgrencilerDb();
                    OgrencilerInfo ogrInfo = ogrDb.KayitBilgiGetir(opaqId, sinavId);

                    lblBilgi.Text = string.Format("{0}/{1} - {2} nolu ders {3} {4} isimli öğrenci", a, kayitSayisi, dersNo, ogrInfo.Adi, ogrInfo.Soyadi);
                    lblGecenSure.Text = String.Format("{0} saat, {1} dakika, {2} saniye", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);

                    try
                    {
                        int gecenDakika = watch.Elapsed.TotalMinutes.ToInt32();
                        int islem = kayitSayisi * gecenDakika;
                        double kalanSure = (double)islem / a;

                        double saat = (kalanSure - (kalanSure % 60)) / 60;
                        double dakika = kalanSure % 60;
                        lblBitisSuresi.Text = string.Format("Kalan tahmini süre : {0:0} saat, {1:0} dakika", saat, dakika);
                    }
                    catch (Exception)
                    {
                        lblBitisSuresi.Text = "Hesaplanıyor...";
                    }


                    int soruNo = 0;
                    SonucOptikDB veriDb = new SonucOptikDB();
                    for (int i = 0; i < cevap.Length; i++)
                    {
                        int puani;
                        soruNo++;
                        string secenek = cevap.Substring(i, 1);

                        KitapcikCevapDB kcvpDb = new KitapcikCevapDB();
                        KitapcikCevapInfo kcInfo = kcvpDb.KayitBilgiGetir(sinavId, dersNo, dersNo, soruNo);

                        if (kitapcikTuru == "A")
                        {
                            puani = secenek == kcInfo.KitapcikA ? kcInfo.SoruPuani : 0;
                        }
                        else
                        {
                            puani = secenek == kcInfo.KitapcikB ? kcInfo.SoruPuani : 0;
                        }
                        SonucOptikInfo info = new SonucOptikInfo
                        {
                            Oturum = dersNo,
                            BransId = dersNo,
                            OgrenciId = opaqId,
                            SoruNo = soruNo,
                            SinavId = sinavId,
                            Secenek = secenek,
                            KurumKodu = ogrInfo.KurumKodu,
                            Puani = puani,
                            KitapcikTuru = kitapcikTuru,
                            Sinif = ogrInfo.Sinifi,
                            Sube = ogrInfo.Sube
                        };
                        veriDb.KayitEkle(info);
                        Application.DoEvents();
                    }

                }
                Application.DoEvents();

                progressBar1.Value = a;
            }
            progressBar1.Value = 0;
            watch.Stop();
            MessageBox.Show("Tamamlandı");
        }
    }
}
