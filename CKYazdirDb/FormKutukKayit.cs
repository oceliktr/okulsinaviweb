using ODM.CKYazdirDb.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ODM.CKYazdirDb.Business;
using ODM.CKYazdirDb.Entities;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb
{
    public partial class FormKutukKayit : Form
    {
        private readonly List<OgrencilerInfo> ogrencilerKutuk = new List<OgrencilerInfo>();
        public int kutukId;
        public FormKutukKayit()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int opaq = txtOpaqId.Text.ToInt32();
            KutukManager kutukManager = new KutukManager();
            if (kutukId == 0)
            {
                var kontrol = kutukManager.Find(x => x.OpaqId == opaq);
                if (kontrol == null)
                {
                    var sinavId = kutukManager.Find(x => x.Id > 0).SinavId;
                    Kutuk kutuk = new Kutuk()
                    {
                        SinavId = sinavId,
                        OpaqId = opaq,
                        IlAdi = txtIl.Text,
                        IlceAdi = cbIlce.SelectedValue.ToString(),
                        KurumKodu = txtKurumKodu.Text.ToInt32(),
                        KurumAdi = txtKurumAdi.Text,
                        OgrenciNo = txtNo.Text.ToInt32(),
                        Adi = txtAdi.Text,
                        Soyadi = txtSoyadi.Text,
                        Sinifi = cbSinif.SelectedItem.ToInt32(),
                        Sube = cbSube.SelectedItem.ToString(),
                        DersKodu = 0,
                        Barkod = "",
                    };
                    if (kutukManager.Insert(kutuk) > 0)
                    {
                        MessageBox.Show("Kayıt eklendi.");

                        FormuTemizle();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt eklenemedi.");
                    }
                }
                else
                {
                    MessageBox.Show("Bu Opaq Id ile kayıt bulunmaktadır.");
                }
            }
            else
            {
                var kutuk = kutukManager.Find(x => x.Id == kutukId);
                kutuk.OpaqId = opaq;
                kutuk.IlAdi = txtIl.Text;
                kutuk.IlceAdi = cbIlce.SelectedValue.ToString();
                kutuk.KurumKodu = txtKurumKodu.Text.ToInt32();
                kutuk.KurumAdi = txtKurumAdi.Text;
                kutuk.OgrenciNo = txtNo.Text.ToInt32();
                kutuk.Adi = txtAdi.Text;
                kutuk.Soyadi = txtSoyadi.Text;
                kutuk.Sinifi = cbSinif.SelectedItem.ToInt32();
                kutuk.Sube = cbSube.SelectedItem.ToString();

                var kontrol = kutukManager.Find(x => x.OpaqId == opaq && x.Id != kutukId);
                if (kontrol == null)
                {
                    if (kutukManager.Update(kutuk) > 0)
                    {
                        MessageBox.Show("Değişiklikler kaydedildi.");
                        
                        FormuTemizle();
                    }
                    else
                    {
                        MessageBox.Show("Değişiklik yapılamadı.");
                    }
                }
                else
                {
                    MessageBox.Show("Bu Opaq Id ile kayıt bulunmaktadır.");
                }
            }
        }

        private void FormuTemizle()
        {
            kutukId = 0;
            txtOpaqId.Text = "";
            txtIl.Text = "";
            cbIlce.SelectedText = "";
            txtKurumKodu.Text = "";
            txtKurumAdi.Text = "";
            txtNo.Text = "";
            txtAdi.Text = "";
            txtSoyadi.Text = "";
            cbSinif.SelectedText = "";
            cbSube.SelectedText = "";

            btnKaydet.Text = "Kaydet";
        }

        private void btnVazgeç_Click(object sender, EventArgs e)
        {
            FormuTemizle();
            this.Close();
        }

        private void txtKurumAdi_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtKurumKodu.Text))
            {
                int kurumKodu = txtKurumKodu.Text.ToInt32();
                KutukManager kutukManager = new KutukManager();
                var kontrol = kutukManager.Find(x => x.KurumKodu == kurumKodu);
                if (kontrol != null)
                {
                    txtKurumAdi.Text = kontrol.KurumAdi;
                }
            }

        }

        private void FormKutukKayit_Load(object sender, EventArgs e)
        {
            KutukManager kutukDb = new KutukManager();
            foreach (var k in kutukDb.List())
            {
                ogrencilerKutuk.Add(new OgrencilerInfo(k.OpaqId, k.IlAdi, k.IlceAdi, k.KurumKodu, k.KurumAdi, k.OgrenciNo, k.Adi, k.Soyadi, k.Sinifi, k.Sube, k.SinavId, k.DersKodu, k.Barkod));
            }

            cbIlce.DataSource = null;
            List<OgrencilerInfo> ilceler = ogrencilerKutuk.GroupBy(x => x.IlceAdi).Select(x => x.First()).OrderBy(x => x.IlceAdi).ToList();
         
            txtIl.Text = ilceler.FirstOrDefault().IlAdi;

            List<OgrencilerInfo> ogr = new List<OgrencilerInfo> { new OgrencilerInfo("0", "İlçe Seçiniz") };
            ogr.AddRange(ilceler.Select(t => new OgrencilerInfo(t.IlceAdi, t.IlceAdi)));

            cbIlce.ValueMember = "Text";
            cbIlce.DisplayMember = "Value";
            foreach (var o in ogr)
            {
                cbIlce.Items.Add(new OgrencilerInfo(o.IlceAdi, o.IlceAdi));
            }
            cbIlce.DataSource = ogr;

            if (kutukId != 0)
            {
                Kutuk kutuk = kutukDb.Find(x => x.Id == kutukId);
               btnKaydet.Text = "Değiştir";
               txtIl.Text = kutuk.IlAdi;
               cbIlce.SelectedValue = kutuk.IlceAdi;
               txtOpaqId.Text = kutuk.OpaqId.ToString();
               txtAdi.Text = kutuk.Adi;
               txtSoyadi.Text = kutuk.Soyadi;
               txtKurumAdi.Text = kutuk.KurumAdi;
               txtKurumKodu.Text = kutuk.KurumKodu.ToString();
               txtNo.Text = kutuk.OgrenciNo.ToString();
               cbSinif.SelectedItem = kutuk.Sinifi.ToString();
               cbSube.SelectedItem = kutuk.Sube;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(kutukId.ToString());

        }

        private void txtNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
