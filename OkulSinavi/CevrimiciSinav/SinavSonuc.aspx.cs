using System;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class Sinav_SinavSonuc : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //TestKutukDb okullarDb = new TestKutukDb();
            //var sonuc = okullarDb.OgrenciBilgiGetir(749788, 11864728);
            //TestOgrenci ogr = new TestOgrenci
            //{
            //    OpaqId = sonuc.OpaqId,
            //    KurumKodu = sonuc.KurumKodu,
            //    Adi = sonuc.Adi,
            //    Soyadi = sonuc.Soyadi,
            //    Sinifi = sonuc.Sinifi
            //};
            //Session["Ogrenci"] = ogr;
            //yukarısı silinecek

            if (Session["Ogrenci"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            TestOturumlarDb oturumDb = new TestOturumlarDb();
            TestOgrenci ogrenci = (TestOgrenci)Session["Ogrenci"];
            //mükerrer oturumu kontrol için
            if (CacheHelper.KullaniciGirisKontrol(ogrenci.OpaqId) != ogrenci.GirisKey)
            {
               Response.Redirect("Default.aspx");
            }
            int sinavId = 0;
            if (Request.QueryString["t"] != "")
            {
                if (Request.QueryString["t"].IsInteger())
                {

                    int oturumId = Request.QueryString["t"].ToInt32();

                    TestOturumlarInfo oturum = oturumDb.KayitBilgiGetir(oturumId);
                    sinavId = oturum.SinavId;
                }
            }

            if (Request.QueryString["SinavId"] != "")
            {
                if (Request.QueryString["SinavId"].IsInteger())
                {
                    sinavId = Request.QueryString["SinavId"].ToInt32();

                }
            }

            if (sinavId != 0)
            {
                TestSinavlarDb sinavlar = new TestSinavlarDb();
                TestSinavlarInfo sinav = sinavlar.KayitBilgiGetir(sinavId);
                miniText.InnerText = "En fazla " + sinav.Puanlama;
                if (sinav.Sinif == ogrenci.Sinifi)
                {
                    TestOgrPuanDb testOgrCevapDb = new TestOgrPuanDb();
                    var ogrCevap = testOgrCevapDb.KayitBilgiGetir(sinavId, ogrenci.OpaqId);
                    if (ogrCevap.Id != 0)
                    {

                        ltrTestAdi.Text = sinav.SinavAdi;
                        ltrDogruSayisi.Text = ogrCevap.Dogru.ToString();
                        ltrYanlis.Text = ogrCevap.Yanlis.ToString();
                        ltrPuan.Text = ogrCevap.Puan>100? ogrCevap.Puan.ToString("##.###"):"<small>hesaplanmadı</small>";

                        int soruSayisi = 0;
                        TestSorularDb sorularDb = new TestSorularDb();
                        var oturumList = oturumDb.Oturumlar(sinavId);
                        foreach (var o in oturumList)
                        {
                            soruSayisi += sorularDb.SoruSayisi(o.Id);
                        }

                        rptOturumlar.DataSource = oturumList;
                        rptOturumlar.DataBind();

                        ltrSoruSayisi.Text = soruSayisi.ToString();

                    }
                    else
                    {
                        Response.Redirect("Sinavlar.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Sinavlar.aspx");
                }
            }

        }
    }

    protected void rptSorular_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //<span class='badge badge-success'>Shipped</span>

        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            int oturumId = DataBinder.Eval(e.Item.DataItem, "OturumId").ToInt32();
            int soruNo = DataBinder.Eval(e.Item.DataItem, "SoruNo").ToInt32();
            string dogruCevap = DataBinder.Eval(e.Item.DataItem, "Cevap").ToString();

            Literal ltrOgrenciCevap = (Literal)e.Item.FindControl("ltrOgrenciCevap");
            Literal ltrSonuc = (Literal)e.Item.FindControl("ltrSonuc");

            TestOgrenci ogrenci = (TestOgrenci)Session["Ogrenci"];
            TestOgrCevapDb testOgrCevapDb = new TestOgrCevapDb();
            var ogrCevap = testOgrCevapDb.KayitBilgiGetir(oturumId, ogrenci.OpaqId);
            if (ogrCevap.Id != 0)
            {
                string ogrenciCevap = ogrCevap.Cevap.Substring(soruNo - 1, 1);

                ltrOgrenciCevap.Text = ogrenciCevap;

                if (ogrenciCevap == " ")
                {
                    ltrSonuc.Text = "";
                }
                else if (ogrenciCevap == dogruCevap)
                {
                    ltrSonuc.Text = "<span class='badge badge-success'>Doğru</span>";
                }
                else if (ogrenciCevap != dogruCevap)
                {
                    ltrSonuc.Text = "<span class='badge badge-danger'>Yanlış</span>";
                }
            }
        }
    }

    protected void rptOturumlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Repeater rptSorular = (Repeater)e.Item.FindControl("rptSorular");

            int oturumId = DataBinder.Eval(e.Item.DataItem, "Id").ToInt32();
            TestSorularDb sorularDb = new TestSorularDb();
            rptSorular.DataSource = sorularDb.KayitlariGetir(oturumId);
            rptSorular.DataBind();
        }
    }
}
