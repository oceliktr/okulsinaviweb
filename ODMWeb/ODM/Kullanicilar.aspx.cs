using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace ODM
{
    public partial class OdmKullanicilar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Master.Yetki().Contains("Root"))
                    Response.Redirect("Giris.aspx");
                
                IlcelerDb ilcelerDb = new IlcelerDb();
                ddlIlce.DataSource = ilcelerDb.KayitlariGetir();
                ddlIlce.DataValueField = "Id";
                ddlIlce.DataTextField = "IlceAdi";
                ddlIlce.DataBind();
                ddlIlce.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));

                ddlIlceler.DataSource = ilcelerDb.KayitlariGetir();
                ddlIlceler.DataValueField = "Id";
                ddlIlceler.DataTextField = "IlceAdi";
                ddlIlceler.DataBind();
                ddlIlceler.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
                ddlKurum.Items.Insert(0, new ListItem("Önce İlçe Seçiniz", ""));
                ddlKurumlar.Items.Insert(0, new ListItem("Önce İlçe Seçiniz", ""));

                BranslarDb brnsDb = new BranslarDb();
                ddlBranslar.DataSource = ddlBrans.DataSource = brnsDb.KayitlariGetir();
                ddlBranslar.DataValueField = ddlBrans.DataValueField = "Id";
                ddlBranslar.DataTextField = ddlBrans.DataTextField = "BransAdi";
                ddlBrans.DataBind();
                ddlBranslar.DataBind();
                ddlBrans.Items.Insert(0, new ListItem("Branş Seçiniz", ""));
                ddlBranslar.Items.Insert(0, new ListItem("Branş Seçiniz", ""));
            }
        }
        private void KayitlariListele()
        {
            try
            {
                int ilce = ddlIlceler.SelectedValue.ToInt32();
                int brans = ddlBranslar.SelectedValue.ToInt32();
                string kurumlar = ddlKurumlar.SelectedValue;

                KullanicilarDb veriDb = new KullanicilarDb();
                rptKullanicilar.DataSource = veriDb.KayitlariGetir(ilce, kurumlar, brans);
                rptKullanicilar.DataBind();
            }
            catch (Exception)
            {
                //
            }
        }
        protected void rptKullanicilar_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = e.CommandArgument.ToInt32();
            hfId.Value = id.ToString();
            KullanicilarDb veriDb = new KullanicilarDb();

            if (e.CommandName.Equals("Sil"))
            {
                if (id != 1265)
                {
                    veriDb.KayitSil(id);

                    KayitlariListele();
                    Master.UyariIslemTamam("Kayıt başarıyla silindi.", phUyari);
                    FormuTemizle();
                }
                else
                {
                    Master.UyariKirmizi("Bu kayıt silinemez.", phUyari);
                }
            }
            else if (e.CommandName.Equals("Duzenle"))
            {
                KullanicilarInfo info = veriDb.KayitBilgiGetir(id);
                KurumlariGetir(info.IlceId);
                hfId.Value = info.Id.ToString();
                txtAdiSoyadi.Text = info.AdiSoyadi;
                txtCepTlf.Text = info.CepTlf;
                txtTcKimlik.Text = info.TcKimlik;

                ddlIlce.SelectedValue = info.IlceId.ToString();
                try
                {
                    ddlKurum.SelectedValue = info.KurumKodu;
                }
                catch
                {
                    //
                }
                try
                {
                    ddlBrans.SelectedValue = info.Bransi.ToString();
                }
                catch
                {
                    //
                }
                txtEpostaAdresi.Text = info.Email;

                if (info.Yetki.Contains("Root"))
                    cbRoot.Checked = true;
                if (info.Yetki.Contains("Admin"))
                    cbAdmin.Checked = true;
                if (info.Yetki.Contains("IlceMEMYetkilisi"))
                    cbIlceMEMYetkilisi.Checked = true;
                if (info.Yetki.Contains("OkulYetkilisi"))
                    cbOkulYetkilisi.Checked = true;
                if (info.Yetki.Contains("UstDegerlendirici"))
                    cbUstDegerlendirici.Checked = true;
                if (info.Yetki.Contains("Ogretmen"))
                    cbOgretmen.Checked = true;
                if (info.Yetki.Contains("SoruYazari"))
                    cbSoruYazari.Checked = true;
                if (info.Yetki.Contains("LgsYazari"))
                    cbLgsYazari.Checked = true;
                if (info.Yetki.Contains("LgsIlKomisyonu"))
                    cbLgsIlKomisyonu.Checked = true;
                if (info.Yetki.Contains("Puanlayici"))
                    cbPuanlayici.Checked = true;


                btnKaydet.Text = "Bilgileri Değiştir";
                ltrKayitBilgi.Text = string.Format("Kullanıcı Bilgilerini Düzenleme Formu [{0}]", info.AdiSoyadi);
                Master.UyariBilgilendirme("Şifre değiştirilmeyecekse boş bırakınız.", phUyari);

                tabliSayfalar.Attributes.Add("class", "");
                Sayfalar.Attributes.Add("class", "tab-pane ");
                tabliKayit.Attributes.Add("class", "active");
                Kayit.Attributes.Add("class", "tab-pane active");
            }
        }
        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            string adiSoyadi = txtAdiSoyadi.Text;
            string sifre = txtSifre.Text;
            string kurumkodu = ddlKurum.SelectedValue;
            int brans = ddlBrans.SelectedValue.ToInt32();
            string email = txtEpostaAdresi.Text;
            string tcKimlik = txtTcKimlik.Text;
            string cepTlf = txtCepTlf.Text;

            string yetki = "";
            if (cbRoot.Checked)
                yetki += "Root|";
            if (cbAdmin.Checked)
                yetki += "Admin|";
            if (cbIlceMEMYetkilisi.Checked)
                yetki += "IlceMEMYetkilisi|";
            if (cbOkulYetkilisi.Checked)
                yetki += "OkulYetkilisi|";
            if (cbUstDegerlendirici.Checked)
                yetki += "UstDegerlendirici|";
            if (cbOgretmen.Checked)
                yetki += "Ogretmen|";
            if (cbSoruYazari.Checked)
                yetki += "SoruYazari|";
            if (cbLgsYazari.Checked)
                yetki += "LgsYazari|";
            if (cbLgsIlKomisyonu.Checked)
                yetki += "LgsIlKomisyonu|";
            if (cbPuanlayici.Checked)
                yetki += "Puanlayici|";

            int ilce = ddlIlce.SelectedValue.ToInt32();
            int id = hfId.Value.ToInt32();

            KullanicilarDb veriDb = new KullanicilarDb();
            KullanicilarInfo info = new KullanicilarInfo
            {
                AdiSoyadi = adiSoyadi,
                IlceId = ilce,
                KurumKodu = kurumkodu,
                Bransi = brans,
                Yetki = yetki,
                Email = email,
                CepTlf = cepTlf
            };
            if (sifre != "")
            {
                info.Sifre = sifre.Md5Sifrele();
            }
            else
            {
                KullanicilarInfo infoVeri = veriDb.KayitBilgiGetir(id);
                info.Sifre = infoVeri.Sifre;
            }
            if (tcKimlik != "")
            {
                info.TcKimlik = tcKimlik;
            }
            else
            {
                KullanicilarInfo infoVeri = veriDb.KayitBilgiGetir(id);
                info.TcKimlik = infoVeri.TcKimlik;
            }
            // Yeni bir kayıt ise.
            if (id == 0)
            {
                if (sifre != "")
                {
                    if (veriDb.KayitKontrol(tcKimlik, 0))
                        Master.UyariTuruncu("Bu Tc Kimlik zaten kayıtlı.", phUyari);
                    else
                    {
                        info.TcKimlik = tcKimlik;
                        veriDb.KayitEkle(info);
                        Master.UyariIslemTamam("Yeni bir kullanıcı eklendi.", phUyari);
                        FormuTemizle();
                    }
                }
                else
                {
                    Master.UyariTuruncu("Yeni bir kullanıcı eklenirken şifre boş olmamalı.", phUyari);
                }
            }
            else
            {
                info.Id = id;
                veriDb.KayitGuncelle(info);
                Master.UyariIslemTamam("Kullanıcı bilgileri güncellendi.", phUyari);
                FormuTemizle();
            }
            KayitlariListele();

            tabliSayfalar.Attributes.Add("class", "active");
            Sayfalar.Attributes.Add("class", "tab-pane active");
            tabliKayit.Attributes.Add("class", "");
            Kayit.Attributes.Add("class", "tab-pane ");
        }
        protected void btnVazgec_Click(object sender, EventArgs e)
        {
            FormuTemizle();
        }
        private void FormuTemizle()
        {
            hfId.Value = "0";
            ddlKurum.SelectedValue = "";
            txtAdiSoyadi.Text = "";
            txtEpostaAdresi.Text = "";
            txtSifre.Text = "";
            ddlIlce.SelectedValue = "";
            txtTcKimlik.Text = "";
            txtCepTlf.Text = "";

            cbOgretmen.Checked = false;
            cbOkulYetkilisi.Checked = false;
            cbIlceMEMYetkilisi.Checked = false;
            cbAdmin.Checked = false;

            btnKaydet.Text = "Kaydet";
            ltrKayitBilgi.Text = "Yeni Kullanıcı Kayıt Formu";

            tabliSayfalar.Attributes.Add("class", "active");
            Sayfalar.Attributes.Add("class", "tab-pane active");
            tabliKayit.Attributes.Add("class", "");
            Kayit.Attributes.Add("class", "tab-pane ");
        }
        protected void ddlIlceler_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int ilce = ddlIlceler.SelectedValue.ToInt32();
            KurumlarDb veriDb = new KurumlarDb();
            ddlKurumlar.DataSource = veriDb.OkullariGetir(ilce);
            ddlKurumlar.DataValueField = "KurumKodu";
            ddlKurumlar.DataTextField = "KurumAdi";
            ddlKurumlar.DataBind();
            ddlKurumlar.Items.Insert(0, new ListItem("Kurum Seçiniz", ""));


        }
        protected void ddlIlce_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int ilce = ddlIlce.SelectedValue.ToInt32();
            KurumlariGetir(ilce);


            tabliSayfalar.Attributes.Add("class", "");
            Sayfalar.Attributes.Add("class", "tab-pane ");
            tabliKayit.Attributes.Add("class", "active");
            Kayit.Attributes.Add("class", "tab-pane active");
        }
        private void KurumlariGetir(int ilce)
        {
            KurumlarDb veriDb = new KurumlarDb();
            ddlKurum.DataSource = veriDb.OkullariGetir(ilce);
            ddlKurum.DataValueField = "KurumKodu";
            ddlKurum.DataTextField = "KurumAdi";
            ddlKurum.DataBind();
            ddlKurum.Items.Insert(0, new ListItem("Kurum Seçiniz", ""));
        }
        protected void btnListele_OnClick(object sender, EventArgs e)
        {
            KayitlariListele();

        }
        protected void rptKullanicilar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string kurumKodu = DataBinder.Eval(e.Item.DataItem, "KurumKodu").ToString();
                Literal ltrKurumAdi = (Literal)e.Item.FindControl("ltrKurumAdi");

                KurumlarDb kDb = new KurumlarDb();
                ltrKurumAdi.Text = kDb.KayitBilgiGetir(kurumKodu).KurumAdi;

                int bransi = DataBinder.Eval(e.Item.DataItem, "Bransi").ToInt32();
                Literal ltrBransi = (Literal)e.Item.FindControl("ltrBransi");

                BranslarDb bDb = new BranslarDb();
                ltrBransi.Text = bDb.KayitBilgiGetir(bransi).BransAdi;
            }
        }
        protected void cbOgretmen_OnCheckedChanged(object sender, EventArgs e)
        {
            if (cbOgretmen.Checked)
            {
                cbUstDegerlendirici.Enabled = false;
                cbUstDegerlendirici.Checked = false;
            }
            else
            {
                cbUstDegerlendirici.Enabled = true;
            }
        }
        protected void cbUstDegerlendirici_OnCheckedChanged(object sender, EventArgs e)
        {
            if (cbUstDegerlendirici.Checked)
            {
                cbOgretmen.Enabled = false;
                cbOgretmen.Checked = false;
            }
            else
            {
                cbOgretmen.Enabled = true;
            }
        }
    }
}