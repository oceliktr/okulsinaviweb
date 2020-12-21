using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim_OkulPuanDetay : Page
{
    private const int sayfaKayitSayisi = 50;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();

            if (!kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin") && !kInfo.Yetki.Contains("Ogretmen"))
            {
                Response.Redirect("Default.aspx");
            }

            KayitlariListele();
        }
    }

    private void KayitlariListele()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        int sinavId = 0;
        if (Request.QueryString["SinavId"] != null)
        {
            sinavId = Request.QueryString["SinavId"].ToInt32();
        }

        TestSinavlarDb sinavDb = new TestSinavlarDb();
        var sinav = sinavDb.KayitBilgiGetir(sinavId);
        ltrSinavAdi.Text = sinav.SinavAdi;
        Page.Title = sinav.SinavAdi+" Öğrenci Listesi";

        TestOgrPuanDb veriDb = new TestOgrPuanDb();
        DataTable kayit =  veriDb.KayitlariGetir(sinavId, kInfo.KurumKodu.ToInt32()); //sınırlandırma için kullanıcının kurum kodu olmalı.
        

        if (kInfo.Yetki.Contains("Ogretmen")) {
            phPuaniHesaplanmayanlar.Visible = false;
        }


        rptKayitlar.DataSource = kayit;
        rptKayitlar.DataBind();
    }

    protected void rptKayitlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();
            
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 100000);
            Literal ltrNet = (Literal)e.Item.FindControl("ltrNet");
            HyperLink hlOgrenciDetay = (HyperLink) e.Item.FindControl("hlOgrenciDetay");
            
            //Öğrenci detay linki sadece kurumyetkilisine gçsterilmesi için
            //kontrol sağlayalım
            int kurumKodu = DataBinder.Eval(e.Item.DataItem, "KurumKodu").ToInt32();
            hlOgrenciDetay.Visible = kurumKodu.ToString() == kInfo.KurumKodu;
            
            int sinifi = DataBinder.Eval(e.Item.DataItem, "Sinifi").ToInt32();
            int dogru = DataBinder.Eval(e.Item.DataItem, "Dogru").ToInt32();
            int yanlis = DataBinder.Eval(e.Item.DataItem, "Yanlis").ToInt32();

            int dogruYanlisOrani = sinifi >= 9 ? 4 : 3;
            decimal net = (dogru - ((decimal)yanlis / dogruYanlisOrani));

            ltrNet.Text = net.ToString("##.##");
        }
    }
}