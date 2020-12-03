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
            Response.Write(kInfo.Yetki+"-+"+ kInfo.KurumKodu);
            if (!kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin") &&
                !kInfo.Yetki.Contains("OkulYetkilisi") && !kInfo.Yetki.Contains("LgsIlKomisyonu") && !kInfo.Yetki.Contains("IlceMEMYetkilisi"))
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
        int kurumKodu = 0;
        if (Request.QueryString["SinavId"] != null)
        {
            sinavId = Request.QueryString["SinavId"].ToInt32();
        }
        if (Request.QueryString["KurumKodu"] != null)
        {
            kurumKodu = Request.QueryString["KurumKodu"].ToInt32();
        }
        TestOgrPuanDb veriDb = new TestOgrPuanDb();


        DataTable kayit = null;
        if (kInfo.Yetki.Contains("OkulYetkilisi"))
        {
             kayit = veriDb.KayitlariGetir(sinavId, kInfo.KurumKodu.ToInt32()); //sınırlandırma için kullanıcının kurum kodu olmalı.
        }
        else if(kInfo.Yetki.Contains("IlceMEMYetkilisi"))
        {
            IlcelerDb ilcelerDb = new IlcelerDb();
            IlcelerInfo ilce = ilcelerDb.KayitBilgiGetir(kInfo.IlceId);
            kayit = veriDb.KayitlariGetir(sinavId, kurumKodu,ilce.IlceAdi);
        }
        else if (kInfo.Yetki.Contains("Root") || kInfo.Yetki.Contains("Admin") || kInfo.Yetki.Contains("LgsIlKomisyonu"))
        {
            if (Request.QueryString["ilce"] != null)
            {
                string ilceAdi = Request.QueryString["ilce"];
                kayit = veriDb.KayitlariGetir(sinavId, kurumKodu, ilceAdi);
            }
        }
        

        rptKayitlar.DataSource = kayit;
        rptKayitlar.DataBind();
    }

    protected void rptKayitlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 100000);
            Literal ltrNet = (Literal)e.Item.FindControl("ltrNet");
            int sinifi = DataBinder.Eval(e.Item.DataItem, "Sinifi").ToInt32();
            int dogru = DataBinder.Eval(e.Item.DataItem, "Dogru").ToInt32();
            int yanlis = DataBinder.Eval(e.Item.DataItem, "Yanlis").ToInt32();

            int dogruYanlisOrani = sinifi >= 9 ? 4 : 3;
            decimal net = (dogru - ((decimal)yanlis / dogruYanlisOrani));

            ltrNet.Text = net.ToString("##.##");
        }
    }
}