using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class LGSSoruBank_Bilgilerim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Master.UyeId() == 0) Response.Redirect("~/ODM/Cikis.aspx");


            IlcelerDb ilcelerDb = new IlcelerDb();
            ddlIlce.DataSource = ilcelerDb.KayitlariGetir();
            ddlIlce.DataValueField = "Id";
            ddlIlce.DataTextField = "IlceAdi";
            ddlIlce.DataBind();
            ddlIlce.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));

            ddlKurum.Items.Insert(0, new ListItem("Önce İlçe Seçiniz", ""));

            BranslarDb brnsDb = new BranslarDb();
            ddlBrans.DataSource = brnsDb.KayitlariGetir();
            ddlBrans.DataValueField = "Id";
            ddlBrans.DataTextField = "BransAdi";
            ddlBrans.DataBind();
            ddlBrans.Items.Insert(0, new ListItem("Branş Seçiniz", ""));


            KullanicilarDb veriDb = new KullanicilarDb();
            KullanicilarInfo info = veriDb.KayitBilgiGetir(Master.UyeId());
            KurumlariGetir(info.IlceId);
            hfId.Value = info.Id.ToString();
            txtAdiSoyadi.Text = info.AdiSoyadi;
            ltrTcKimlik.Text = info.TcKimlik;
            txtGsm.Text = info.CepTlf;
            ddlIlce.SelectedValue = info.IlceId.ToString();
            try
            {
                ddlKurum.SelectedValue = info.KurumKodu;
                ddlKurumKodu.SelectedValue = info.KurumKodu;
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

            Master.UyariBilgilendirme("Şifre değiştirilmeyecekse boş bırakınız. Düzenlenmeyen alanlar için ÖDM ile iletişime geçiniz. İletişim bilgilerimiz giriş sayfasında bulunmaktadır.", phUyari);
        }
    }

    protected void btnKaydet_Click(object sender, EventArgs e)
    {
        string sifre = txtSifre.Text.ToTemizMetin();
        string email = txtEpostaAdresi.Text.ToTemizMetin();
        string gsm = txtGsm.Text.ToTemizMetin();
        string adiSoyadi = txtAdiSoyadi.Text.ToTemizMetin();

        int id = hfId.Value.ToInt32();

        KullanicilarDb veriDb = new KullanicilarDb();
        KullanicilarInfo info = new KullanicilarInfo
        {
            Email = email,
            CepTlf = gsm,
            AdiSoyadi = adiSoyadi
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

        info.Id = id;
        veriDb.KullaniciBilgiGuncelle(info);
        Master.UyariIslemTamam("Kullanıcı bilgileri güncellendi.", phUyari);


    }

    protected void ddlIlce_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int ilce = ddlIlce.SelectedValue.ToInt32();
        KurumlariGetir(ilce);
    }
    private void KurumlariGetir(int ilce)
    {
        KurumlarDb veriDb = new KurumlarDb();
        ddlKurum.DataSource = veriDb.OkullariGetir(ilce);
        ddlKurum.DataValueField = "KurumKodu";
        ddlKurum.DataTextField = "KurumAdi";
        ddlKurum.DataBind();
        ddlKurum.Items.Insert(0, new ListItem("Kurum Seçiniz", ""));


        ddlKurumKodu.DataSource = veriDb.OkullariGetir(ilce);
        ddlKurumKodu.DataValueField = "KurumKodu";
        ddlKurumKodu.DataTextField = "KurumKodu";
        ddlKurumKodu.DataBind();
        ddlKurumKodu.Items.Insert(0, new ListItem("Kurum Seçiniz", ""));
    }
}