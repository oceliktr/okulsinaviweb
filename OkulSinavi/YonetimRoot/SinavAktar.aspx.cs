using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Yonetim_SinavAktarRoot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();
            if (!kInfo.Yetki.Contains("Root"))
            {
                Response.Redirect("Default.aspx");
            }

            if (Request.QueryString["SinavId"] != null)
            {
                if (Request.QueryString["SinavId"].IsInteger())
                {
                    int id = Request.QueryString["SinavId"].ToInt32();

                    TestSinavlarDb sinavDb = new TestSinavlarDb();
                    TestSinavlarInfo info = sinavDb.KayitBilgiGetir(id);

                    ltrSinav.Text = info.SinavAdi;

                }
            }


            IlcelerDb ilcelerDb = new IlcelerDb();
            ddlIlce.DataSource = ilcelerDb.KayitlariGetir();
            ddlIlce.DataValueField = "Id";
            ddlIlce.DataTextField = "IlceAdi";
            ddlIlce.DataBind();
            ddlIlce.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
            ddlKurum.Items.Insert(0, new ListItem("Önce İlçe Seçiniz", ""));
        }
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
    }

    protected void btnKaydet_OnClick(object sender, EventArgs e)
    {
        if (Request.QueryString["SinavId"] != null)
        {
            if (Request.QueryString["SinavId"].IsInteger())
            {
                int id = Request.QueryString["SinavId"].ToInt32();
                string kurumKodu = ddlKurum.SelectedValue;
                TestSinavlarDb sinavDb = new TestSinavlarDb();
                TestOturumlarDb oturumDb = new TestOturumlarDb();
                TestSorularDb sorularDb = new TestSorularDb();

                TestSinavlarInfo info = sinavDb.KayitBilgiGetir(id);
                info.Kurumlar = "," + kurumKodu + ",";


                var oturumlar = oturumDb.Oturumlar(id);

                //Yeni sınavı ekle
                long sinavIdYeni = sinavDb.KayitEkle(info);

                foreach (var oturum in oturumlar)
                {
                    oturum.SinavId = sinavIdYeni.ToInt32();
                    long oturumIdYeni = oturumDb.KayitEkle(oturum);

                    List<TestSorularInfo> sorular = sorularDb.KayitlariDizeGetir(oturum.Id);
                    foreach (var soru in sorular)
                    {
                        soru.OturumId = oturumIdYeni.ToInt32();
                        sorularDb.KayitEkle(soru);
                    }
                }
                Master.UyariBilgilendirme("Sınav seçili okula aktarıldı",phUyari);
            }
        }

    }
}