using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim__Rapor_IlceOrtalamalari100uzeri : System.Web.UI.Page
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


                    //Önce öğrencileri diziye al
                    //ilçe ilçe puan hesapla
                    TestOgrPuanDb ogrPuanDb= new TestOgrPuanDb();
                    var ogrPuanlari = ogrPuanDb.KayitlariDiziyeGetir(sinavId).Where(x=>x.Puan>=100);
                    IEnumerable<OgrenciPuanIlceJoinModel> ilceler = ogrPuanlari.DistinctBy(x => x.IlceAdi);

                    List<TestIlceOrtalamasi> ilceOrtalamasi = new List<TestIlceOrtalamasi>();

                    foreach (var ilce in ilceler)
                    {
                        var dizi = ogrPuanlari.Where(x => x.IlceAdi == ilce.IlceAdi);
                        int ogrSayisi = dizi.Count();
                        double dogruOrtalamasi = dizi.Average(x => x.Dogru);
                        double yanlisOrtalamasi = dizi.Average(x => x.Yanlis);
                        decimal puanOrtalamasi = dizi.Average(x => x.Puan);
                        ilceOrtalamasi.Add(new TestIlceOrtalamasi(ilce.IlceAdi,ogrSayisi,dogruOrtalamasi,yanlisOrtalamasi,puanOrtalamasi));
                    }
                    rptKayitlar.DataSource = ilceOrtalamasi;
                    rptKayitlar.DataBind();
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
    protected void rptKayitlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
        }
    }
}