using System;

public partial class Okul_SinaviYonetim_SinavKayit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }
            TestDonemDb dnmDb = new TestDonemDb();
            ltrDonem.Text = "<label>Aktif Dönem: </label> " + dnmDb.AktifDonem().Donem;
            txtDonem.Text = dnmDb.AktifDonem().Donem;

            if (Request.QueryString["id"] != null)
            {
                if (Request.QueryString["id"].IsInteger())
                {
                    int id = Request.QueryString["id"].ToInt32();

                    TestSinavlarDb sinavDb = new TestSinavlarDb();
                    TestSinavlarInfo info = sinavDb.KayitBilgiGetir(id);
                    hfId.Value = info.Id.ToString();
                    txtSinavAdi.Text = info.SinavAdi;
                    txtAciklama.Text = info.Aciklama.Replace("<br>", Environment.NewLine);
                    ddlSinif.SelectedValue = info.Sinif.ToString();
                    ddlPuanlama.SelectedValue = info.Puanlama.ToString();
                    cbDurum.Checked = info.Aktif == 1;
                    ddlOturumTercihi.SelectedValue = info.OturumTercihi.ToString();
                    txtBeklemeSuresi.Text = info.BeklemeSuresi.ToString();
                    btnKaydet.Text = "Değiştir";

                    var ogrDonemi = dnmDb.KayitBilgiGetir(info.DonemId);
                    txtDonem.Text = ogrDonemi.Donem;
                }
            }

        }
    }

    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin");
        return yetkili;
    }

    protected void btnKaydet_OnClick(object sender, EventArgs e)
    {
        int sinif = ddlSinif.SelectedValue.ToInt32();
        TestDonemDb dnmDb = new TestDonemDb();
       int donem= dnmDb.AktifDonem().Id;

        TestSinavlarInfo info = new TestSinavlarInfo
        {
            SinavAdi = txtSinavAdi.Text.ToTemizMetin(),
            Aciklama = txtAciklama.Text.ToTemizMetin().Replace(Environment.NewLine, "<br>"),
            Sinif = sinif,
            Aktif = cbDurum.Checked ? 1 : 0,
            Puanlama = ddlPuanlama.SelectedValue.ToInt32(),
            BeklemeSuresi = txtBeklemeSuresi.Text.ToInt32(),
            OturumTercihi = ddlOturumTercihi.SelectedValue.ToInt32(),
            Id = hfId.Value.ToInt32(),
            DonemId = donem
        };

        TestSinavlarDb sinavDb = new TestSinavlarDb();

        if (hfId.Value == "0")
        {
            if (sinavDb.KayitEkle(info) > 0)
            {
                Master.UyariIslemTamam("Yeni bir sınav eklendi.", phUyari);
                FormuTemizle();
            }
            else
                Master.UyariTuruncu("Kayıt yapılamadı.", phUyari);
        }
        else
        {
            if (sinavDb.KayitGuncelle(info) > 0)
                Master.UyariIslemTamam("Değişiklikler kaydildi.", phUyari);
            else
                Master.UyariTuruncu("Güncelleme yapılamadı.", phUyari);
        }
        CacheHelper.AktifSinavlarKaldir(sinif);
    }

    private void FormuTemizle()
    {
        cbDurum.Checked = true;
        ddlSinif.SelectedValue = "";
        txtAciklama.Text = "";
        txtSinavAdi.Text = "";
        ddlPuanlama.SelectedValue = "";
    }
}