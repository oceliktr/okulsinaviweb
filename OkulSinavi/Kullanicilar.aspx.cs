using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OkulSinavi
{
    public partial class OkulSinaviKullanicilar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Master.Yetki().Contains("Root"))
                    Response.Redirect("/Yonetim/Giris.aspx");

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
                ddlBrans.Items.Insert(0, new ListItem("Branş Seçiniz", "0"));
                ddlBranslar.Items.Insert(0, new ListItem("Branş Seçiniz", "0"));
            }
        }
        private void KayitlariListele()
        {
            try
            {
                int ilce = ddlIlceler.SelectedValue.ToInt32();
                int brans = ddlBranslar.SelectedValue.ToInt32();
                string kurumlar = ddlKurumlar.SelectedValue;
                string yetki = ddlYetki.SelectedValue;

                KullanicilarDb veriDb = new KullanicilarDb();
                rptKullanicilar.DataSource = veriDb.KayitlariGetir(ilce, kurumlar, brans, yetki);
                rptKullanicilar.DataBind();
            }
            catch (Exception ex)
            {
                Master.UyariKirmizi(ex.Message, phUyari);

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
                if (info.Yetki.Contains("OkulYetkilisi"))
                    cbOkulYetkilisi.Checked = true;


                btnKaydet.Text = "Bilgileri Değiştir";
                ltrKayitBilgi.Text = string.Format("Kullanıcı Bilgilerini Düzenleme Formu [{0}]", info.AdiSoyadi);
                Master.UyariBilgilendirme("Şifre değiştirilmeyecekse boş bırakınız.", phUyari);

                tabliSayfalar.Attributes.Add("class", "nav-link");
                Sayfalar.Attributes.Add("class", "tab-pane ");
                tabliKayit.Attributes.Add("class", "nav-link active");
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
            if (cbOkulYetkilisi.Checked)
                yetki += "OkulYetkilisi|";

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

                if (veriDb.KayitKontrol(tcKimlik, 0))
                    Master.UyariTuruncu("Bu Tc Kimlik zaten kayıtlı.", phUyari);
                else
                {
                    info.Sifre = sifre != "" ? sifre.Md5Sifrele() : null; //yeni kayıtta şifre yok ise null yap
                    info.TcKimlik = tcKimlik;
                    veriDb.KayitEkle(info);
                    Master.UyariIslemTamam("Yeni bir kullanıcı eklendi.", phUyari);
                    FormuTemizle();
                }

            }
            else
            {
                info.Id = id;
                KullanicilarInfo infoVeri = veriDb.KayitBilgiGetir(id);
                info.Sifre = sifre != "" ? sifre.Md5Sifrele() : infoVeri.Sifre; //boş ise eski şifreyi gir

                veriDb.KayitGuncelle(info);
                Master.UyariIslemTamam("Kullanıcı bilgileri güncellendi.", phUyari);
                FormuTemizle();
            }
            KayitlariListele();

            tabliSayfalar.Attributes.Add("class", "nav-link active");
            Sayfalar.Attributes.Add("class", "tab-pane active");
            tabliKayit.Attributes.Add("class", "nav-link ");
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

            cbOkulYetkilisi.Checked = false;
            cbAdmin.Checked = false;

            btnKaydet.Text = "Kaydet";
            ltrKayitBilgi.Text = "Yeni Kullanıcı Kayıt Formu";

            tabliSayfalar.Attributes.Add("class", "nav-link active");
            Sayfalar.Attributes.Add("class", "tab-pane active");
            tabliKayit.Attributes.Add("class", "nav-link ");
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


            tabliSayfalar.Attributes.Add("class", "nav-link ");
            Sayfalar.Attributes.Add("class", "tab-pane ");
            tabliKayit.Attributes.Add("class", "nav-link active");
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

    }
}