using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class OkulSinavi_CevrimiciSinavYonetim__Rapor_PuanHesaplanmayanlar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("~/Yonetim/Default.aspx");
            }

            if (Request.QueryString["SinavId"] != null)
            {
                if (Request.QueryString["SinavId"].IsInteger())
                {
                    int sinavId = Request.QueryString["SinavId"].ToInt32();

                    OturumIslemleri oturum = new OturumIslemleri();
                    KullanicilarInfo kInfo = oturum.OturumKontrol();
                    TestSinavlarDb sinavlarDb = new TestSinavlarDb();
                    TestSinavlarInfo sinavlarInfo = sinavlarDb.KayitBilgiGetir(sinavId,kInfo.KurumKodu);
                    ltrSinavAdi.Text = sinavlarInfo.SinavAdi;

                    TestOgrCevapDb testDb = new TestOgrCevapDb();
                    var liste = testDb.PuaniHesaplanmayanlariGetir(sinavId, kInfo.KurumKodu.ToInt32(), true);
                    Session["PuaniHesaplanmayanlar"] = liste.Count;

                    rptOgrenciler.DataSource = liste;
                    rptOgrenciler.DataBind();
                }
            }
        }
    }
    protected void rptOgrenciler_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
        }
    }
    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin");
        return yetkili;
    }

    [WebMethod]
    public static string Hesapla(int SinavId)
    {

        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        TestSinavlarDb sinavlarDb = new TestSinavlarDb();
        TestSinavlarInfo sinavlarInfo = sinavlarDb.KayitBilgiGetir(SinavId,kInfo.KurumKodu);
        if (sinavlarInfo.Id == 0)
        {
            JsonSonuc1 soList = new JsonSonuc1
            {
                Mesaj = "Sınav bulunamadı.",
                EksilenSayi = 0,
                Sonuc = "no"
            };
            return JsonConvert.SerializeObject(soList);
        }
        else
        {
            TestOgrCevapDb testDb = new TestOgrCevapDb();
            int ogrSay = 20;

            var kayitlar = testDb.PuaniHesaplanmayanlariGetir(SinavId, ogrSay);

            foreach (var info in kayitlar)
            {
                TestPuanHesaplamaIslemleri.PuanlamaHesapla(info.OpaqId, SinavId);
            }


            JsonSonuc1 soList = new JsonSonuc1
            {
                Mesaj = kayitlar.Count + " öğrencinin puanı hesaplandı.",
                EksilenSayi = kayitlar.Count,
                Sonuc = "ok"
            };
            return JsonConvert.SerializeObject(soList);
        }
    }


}

public class JsonSonuc1
{
    public string Mesaj { get; set; }
    public int EksilenSayi { get; set; }
    public string Sonuc { get; set; }
}