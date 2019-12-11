using CKYazdir;
using Microsoft.VisualBasic;
using ODM.CKYazdirDb.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ODM.CKYazdirDb.Library;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb
{
    public partial class FormAna : Form
    {
        public FormAna()
        {
            InitializeComponent();
        }
        private void BtnKutuk_Click(object sender, EventArgs e)
        {
            FormKutukIslemleri frm = new FormKutukIslemleri();
            frm.ShowDialog();
        }

        private void BtnAyarlar_Click(object sender, EventArgs e)
        {
            FormAyarlar frm = new FormAyarlar();
            frm.ShowDialog();
        }

        private void BtnCkOlustur_Click(object sender, EventArgs e)
        {
            FormCkOlustur frm = new FormCkOlustur();
            frm.ShowDialog();
        }



        private void FormAna_Load(object sender, EventArgs e)
        {
            //background nesnesi sağlıklı çalışması için gerekli. 
            CheckForIllegalCrossThreadCalls = false;
        }

        private void BtnTextOlustur_Click(object sender, EventArgs e)
        {
            FormTxtOlustur frm = new FormTxtOlustur();
            frm.ShowDialog();
        }

        private void BtnCevapYukle_Click(object sender, EventArgs e)
        {
            FormCevaplariYukle frm = new FormCevaplariYukle();
            frm.ShowDialog();
        }

        private void BtnKazanimlar_Click(object sender, EventArgs e)
        {
            FormKazanimlar frm = new FormKazanimlar();
            frm.ShowDialog();
        }

        private void BtnBrans_Click(object sender, EventArgs e)
        {
            FormBranslar frm = new FormBranslar();
            frm.ShowDialog();
        }

        private void btnDegerlenirmeKarne_Click(object sender, EventArgs e)
        {
            FormSonDegerlendirme frm = new FormSonDegerlendirme();
            frm.ShowDialog();
        }

        private string sinavAdi = "";
        private string dosyaAdi = "";
        private void btnSqlExport_Click(object sender, EventArgs e)
        {
            string sinavInputBox = Interaction.InputBox("Sınav Adı", "Bu sınav için bir açıklama giriniz", "... tarihinde yapılan izleme sınavı");
            if (sinavInputBox.Length < 5)
            {
                MessageBox.Show("Sınavı tanımlayan bir isim giriniz. Örneğin 12 Kasım 2019 tarihli İzleme Sınavı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                sinavAdi = sinavInputBox;
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.ShowNewFolderButton = true; //yeni klasör oluşturmayı aç
                    folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                    folderDialog.SelectedPath =
                        Environment.SpecialFolder.Desktop
                            .ToString(); //başlangıç dizini programın bulunduğu dizin => AppDomain.CurrentDomain.BaseDirectory
                    folderDialog.Description = @"Veri dosyasının oluşturulacağı dizini seçiniz.";
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        dosyaAdi = folderDialog.SelectedPath + @"\ckdata.ck";

                        backgroundWorker1.RunWorkerAsync();

                    }
                }
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            KutukManager kutukManager = new KutukManager();
            BransManager bransManager = new BransManager();
            DogruCevaplarManager dogruCevaplarManager = new DogruCevaplarManager();
            KazanimManager kazanimManager = new KazanimManager();
            KarneSonucManager karneSonucManager = new KarneSonucManager();
            //OgrenciCevapManager ogrenciCevapManager = new OgrenciCevapManager();

            var kutukList = kutukManager.List();
            var dogruCvplarList = dogruCevaplarManager.List();
            //var ocList = ogrenciCevapManager.List();
            var karneSonucList = karneSonucManager.List();
            var bransList = bransManager.List();
            var kazanimlarList = kazanimManager.List();

            int kayitSayisi = kutukList.Count + dogruCvplarList.Count + bransList.Count + kazanimlarList.Count+ karneSonucList.Count;
            int a = 0;
            progressBar1.Maximum = kayitSayisi;

            int sinavId = kutukList.First().SinavId; //Sınavın numarasını aldık

            StreamWriter yaz = new StreamWriter(dosyaAdi);

            yaz.WriteLine("{SinavAdi}|" + sinavId + "|" + sinavAdi);

            //Kütük
            foreach (var kutuk in kutukList)
            {
                a++;
                progressBar1.Value = a;
                lblBilgi.Text = "Kütük tablosu hazırlanıyor.";
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
                lblBilgi.Text = "Doğru cevaplar tablosu hazırlanıyor.";

                yaz.WriteLine("{DogruCevaplar}|" + sinavId + "|" + dogruCevap.Sinif + "|" + dogruCevap.BransId + "|" + dogruCevap.KitapcikTuru + "|" + dogruCevap.Cevaplar);
            }

            //KarneSonuclari
            foreach (var sonuc in karneSonucList)
            {
                a++;
                progressBar1.Value = a;
                lblBilgi.Text = "Karne sonuçları tablosu hazırlanıyor.";

                yaz.WriteLine("{KarneSonuclari}|" + sinavId + "|" + sonuc.BransId + "|" + sonuc.Ilce + "|" + sonuc.KurumKodu + "|" + sonuc.Sinif + "|" + sonuc.Sube + "|" + sonuc.KitapcikTuru + "|" + sonuc.SoruNo + "|" + sonuc.Dogru + "|" + sonuc.Yanlis + "|" + sonuc.Bos);
            }
            //Branslar
            foreach (var brans in bransList)
            {
                a++;
                progressBar1.Value = a;
                lblBilgi.Text = "Branşlar tablosu hazırlanıyor.";

                yaz.WriteLine("{Branslar}|" + sinavId + "|" + brans.Id + "|" + brans.BransAdi);
            }
            //Kazanimlar
            foreach (var kazanim in kazanimlarList)
            {
                a++;
                progressBar1.Value = a;
                lblBilgi.Text = "Kazanımlar tablosu hazırlanıyor.";
                yaz.WriteLine("{Kazanimlar}|" + kazanim.Id + "|" + sinavId + "|" + kazanim.Sinif + "|" + kazanim.BransId + "|" + kazanim.KazanimNo + "|" + kazanim.KazanimAdi + "|" + kazanim.KazanimAdiOgrenci + "|" + kazanim.Sorulari);
            }

            lblBilgi.Text = "Dosyaya yazma işlemi tamamlanıyor.";
            yaz.Close();
            progressBar1.Value = 0;
            lblBilgi.Text = "Verilerin dışarı aktarılması tamamlandı." + dosyaAdi + " adresindeki dosyayı webe yükleyiniz";
        }

    }
}
