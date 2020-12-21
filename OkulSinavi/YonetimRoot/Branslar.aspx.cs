using System;
using System.Web.UI.WebControls;

public partial class OkulSinavi_BranslarRoot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Master.Yetki().Contains("Root"))
                Response.Redirect("/Yonetim/Giris.aspx");

            KayitlariListele();
        }
    }
    private void KayitlariListele()
    {
        BranslarDb veriDb = new BranslarDb();
        rptBranslar.DataSource = veriDb.KayitlariGetir();
        rptBranslar.DataBind();
    }
    protected void btnVazgec_OnClick(object sender, EventArgs e)
    {
        FormuTemizle();
    }

    private void FormuTemizle()
    {
        txtBransAdi.Text = "";
        hfId.Value = "0";
        btnKaydet.Text = "Kaydet";
        ltrKayitBilgi.Text = "Yeni Branş Kayıt Formu";


        tabliSayfalar.Attributes.Add("class", "nav-link active");
        Sayfalar.Attributes.Add("class", "tab-pane active");
        tabliKayit.Attributes.Add("class", "nav-link ");
        Kayit.Attributes.Add("class", "tab-pane ");
    }

    protected void btnKaydet_OnClick(object sender, EventArgs e)
    {
        string bransAdi = txtBransAdi.Text;
        int katSayi = txtKatsayi.Text.ToInt32();

        int id = hfId.Value.ToInt32();

        BranslarDb veriDb = new BranslarDb();
        BranslarInfo info = new BranslarInfo
        {
            BransAdi = bransAdi,
            KatSayi = katSayi
        };

        // Yeni bir kayıt ise.
        if (id == 0)
        {
            veriDb.KayitEkle(info);
            Master.UyariIslemTamam("Yeni bir kullanıcı eklendi.", phUyari);
            FormuTemizle();
        }
        else
        {
            info.Id = id;
            veriDb.KayitGuncelle(info);
            Master.UyariIslemTamam("Değişiklikler kaydedildi", phUyari);
            FormuTemizle();
        }
        KayitlariListele();
     
        tabliSayfalar.Attributes.Add("class", "nav-link active");
        Sayfalar.Attributes.Add("class", "tab-pane active");
        tabliKayit.Attributes.Add("class", "nav-link");
        Kayit.Attributes.Add("class", "tab-pane ");
    }

    protected void rptBranslar_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = e.CommandArgument.ToInt32();
        hfId.Value = id.ToString();
        BranslarDb veriDb = new BranslarDb();

        if (e.CommandName.Equals("Sil"))
        {
           
                veriDb.KayitSil(id);

                KayitlariListele();
                Master.UyariIslemTamam("Kayıt başarıyla silindi.", phUyari);
               FormuTemizle();
            
        }
        else if (e.CommandName.Equals("Duzenle"))
        {
            BranslarInfo info = veriDb.KayitBilgiGetir(id);
          
            hfId.Value = info.Id.ToString();
            txtBransAdi.Text = info.BransAdi;
            txtKatsayi.Text = info.KatSayi.ToString();

            btnKaydet.Text = "Bilgileri Değiştir";
            ltrKayitBilgi.Text = string.Format("Branş Düzenleme Formu [{0}]", info.BransAdi);


            tabliSayfalar.Attributes.Add("class", "nav-link");
            Sayfalar.Attributes.Add("class", "tab-pane ");
            tabliKayit.Attributes.Add("class", "nav-link active");
            Kayit.Attributes.Add("class", "tab-pane active");
        }
    }
}