using System;
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
using DAL;
using ODM.Kutuphanem;

namespace ODM
{
    public partial class FormKutukDbAktar : Form
    {
        private static readonly string ckDizin = IniIslemleri.VeriOku("CKDizinKontrol", "CKBulunduguAdres");
        private readonly int sinavId;
        private readonly string sinavAdi;
        public FormKutukDbAktar()
        {
            InitializeComponent();

            SinavlarDb snvDb = new SinavlarDb();
            SinavlarInfo sinfo = snvDb.AktifSinavAdi();
            sinavId = sinfo.SinavId;
            sinavAdi = sinfo.SinavAdi;
        }
        private void FormKutukDbAktar_Load(object sender, EventArgs e)
        {

            OgrencilerDb ogrDb = new OgrencilerDb();
            int ogrenciSayisi = ogrDb.OgrenciSayisi(sinavId);
            KutukIslemleriDB veriDb = new KutukIslemleriDB();
            KutukIslemleriInfo info = veriDb.OkulOgrenciSayisi();
            lblKutukBilgi.Text = string.Concat("Kütük tablosunda ", info.OkulSayisi, " adet okula ait ", info.OgrenciSayisi, " öğrenci bulunmaktadır.");
            lblOgrenciSayisi.Text = string.Concat("Dikkat:", sinavAdi, " sınavı için öğrenci tablosunda ", ogrenciSayisi, " adet öğrenci bulunmaktadır.");
        }
        private void btnDBYukle_Click(object sender, EventArgs e)
        {
            bgwOgrenciYukle.RunWorkerAsync();
        }
        private void btnKutukVerileriniSil_Click(object sender, EventArgs e)
        {
            bgwKutukSil.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            KutukIslemleriDB veriDb = new KutukIslemleriDB();
            List<KutukIslemleriInfo> kutuk = veriDb.KayitlariDiziyeGetir();
            List<KutukIslemleriInfo> kutukOkullar = veriDb.OkullariDiziyeGetir();
            txtRapor.Text = "";
            progressBar1.Maximum = kutukOkullar.Count;
            progressBar1.Value = 0;
            int a = 0;
            foreach (var okul in kutukOkullar)
            {
                a++;
                progressBar1.Value = a;

                string kurumTuru = okul.KurumAdi.Contains("mam Hatip") ? "İmam Hatip Ortaokulu" : "Ortaokul";
                string tur = "";
                if (okul.KurumAdi.Contains("Ortaokul"))
                    tur = "Ortaokul";
                else if (okul.KurumAdi.Contains("Lise"))
                    tur = "Lise";
                else if (okul.KurumAdi.Contains("İlkokul"))
                    tur = "İlkokul";

                KurumlarDb okulDb = new KurumlarDb();
                if (!okulDb.KayitKontrol(okul.KurumKodu))
                {
                    IlcelerDb idb = new IlcelerDb();
                    IlcelerInfo infoI = idb.KayitBilgiGetir(okul.Ilce);
                    //KurumKodu,KurumAdi,Email,IlceId,KurumTuru,Tur
                    KurumlarInfo infoK = new KurumlarInfo();
                    infoK.KurumKodu = okul.KurumKodu;
                    infoK.KurumAdi = okul.KurumAdi;
                    infoK.Email = okul.KurumKodu + "@meb.k12.tr";
                    infoK.IlceId = infoI.Id;
                    infoK.KurumTuru = kurumTuru;
                    infoK.Tur = tur;

                    okulDb.KayitEkle(infoK);

                    txtRapor.Text += okul.KurumKodu + " - " + okul.KurumAdi + Environment.NewLine;
                }
                Application.DoEvents();
            }

            OgrencilerDb ogrDb = new OgrencilerDb();
            int i = 10000;

            progressBar1.Maximum = kutuk.Count;
            progressBar1.Value = 0;
            a = 0;

            foreach (var ktk in kutuk)
            {
                a++;
                progressBar1.Value = a;

                OgrencilerInfo info = new OgrencilerInfo
                {
                    // SinavId,OgrenciId,TcKimlik,Adi,Soyadi,KurumKodu,OgrOkulNo,Sinifi,Sube) 
                    SinavId = sinavId,
                    OgrenciId = i,
                    TcKimlik = ktk.TcKimlik,
                    Adi = ktk.Adi,
                    Soyadi = ktk.Soyadi,
                    KurumKodu = ktk.KurumKodu.ToInt32(),
                    OgrOkulNo = ktk.OkulNo.ToInt32(),
                    Sinifi = ktk.Sinif.ToInt32(),
                    Sube = ktk.Sube
                };
                ogrDb.KayitEkle(info);
                i++;

                Application.DoEvents();
            }

            KutukSilmeIslemleri();
        }
        private void bgwKutukSil_DoWork(object sender, DoWorkEventArgs e)
        {
            KutukSilmeIslemleri();
        }
        private void KutukSilmeIslemleri()
        {
            string dosyaAdi = ckDizin + "\\" + sinavId + "_nolu_ogrenci_kutuk.sql";
            DialogResult dialog =
                MessageBox.Show(
                    @"Kütük verileri " + ckDizin +
                    " dizinine kaydedilip veritabanından silinecektir. \nSilmek istediğinizden emin misiniz?", @"Bilgi",
                    MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                int a = 0;
                KutukIslemleriDB veriDb = new KutukIslemleriDB();
                List<KutukIslemleriInfo> ogrenciler = veriDb.KayitlariDiziyeGetir();

                StreamWriter sqlSW = new StreamWriter(dosyaAdi, false, Encoding.UTF8);
                foreach (KutukIslemleriInfo xls in ogrenciler)
                {
                    a++;
                    progressBar1.Value = a;

                    string sql = string.Concat(
                        "INSERT INTO `kutukislemleri` (`Ilce`, `KurumKodu`, `KurumAdi`, `TcKimlik`, `Adi`, `Soyadi`, `OkulNo`, `Sinif`, `Sube`) VALUES('",xls.Ilce, "','", xls.KurumKodu, "','", xls.KurumAdi, "','", xls.TcKimlik, "','", xls.Adi, "','",xls.Soyadi, "','", xls.OkulNo, "','", xls.Sinif, "','", xls.Sube, "');");
                    sqlSW.WriteLine(sql + Environment.NewLine);

                    lblBilgi.Text = "Sql dosyası oluşturuluyor. Eklenen kayıt :" + xls.KurumAdi;
                }
                sqlSW.Close();

                lblBilgi.Text = "Kütük siliniyor...";
                veriDb.KayitSil();
                lblBilgi.Text = "Kütük silindi.";

                dialog = MessageBox.Show(@"Kütük silindi." + "\n" + "Oluşturulan sql dosyasını açmak ister misiniz?", @"Bilgi",
                    MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                    Process.Start(dosyaAdi);
            }
        }
    }
}
