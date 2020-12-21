using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CevrimiciSinav_OturumGetir : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int sinavId = 0;
            TestOturumlarDb oturumDb = new TestOturumlarDb();
            if (Request.QueryString["SinavId"] != null)
            {
                sinavId = Request.QueryString["SinavId"].ToInt32();

            }

            if (Request.QueryString["t"] != "")
            {
                if (Request.QueryString["t"].IsInteger())
                {
                    int oturumId = Request.QueryString["t"].ToInt32();
                    TestOturumlarInfo oturum = oturumDb.KayitBilgiGetir(oturumId);
                    sinavId = oturum.SinavId;

                }
            }

            rptOturumlar.DataSource = oturumDb.KayitlariGetir(sinavId);
            rptOturumlar.DataBind();
        }
    }

    protected void rptOturumlar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {

            int buOturumunSirasi = DataBinder.Eval(e.Item.DataItem, "SiraNo").ToInt32();
            int buOturumId = DataBinder.Eval(e.Item.DataItem, "Id").ToInt32();
            int sinavId = DataBinder.Eval(e.Item.DataItem, "SinavId").ToInt32();
            int sinavSuresi = DataBinder.Eval(e.Item.DataItem, "Sure").ToInt32();

            TestOgrenci ogrenci = (TestOgrenci)Session["Ogrenci"];

            var sinavInfo = CacheHelper.AktifSinavlar(ogrenci.KurumKodu.ToString(),ogrenci.Sinifi).FirstOrDefault(x => x.Id == sinavId);

            TestOgrCevapDb ogrCevapDb = new TestOgrCevapDb();
            var ogrCvpInfo = ogrCevapDb.KayitBilgiGetir(buOturumId, ogrenci.OpaqId);

            DateTime baslamaTarihi = DataBinder.Eval(e.Item.DataItem, "BaslamaTarihi").ToDateTime();
            DateTime bitisTarihi = DataBinder.Eval(e.Item.DataItem, "BitisTarihi").ToDateTime();

            if (ogrCvpInfo.Id != 0)//öğrenci daha önce sınava girmiş ise sınav bitene kadar giriş yapabilsin
            {
                bitisTarihi = DataBinder.Eval(e.Item.DataItem, "BitisTarihi").ToDateTime().AddMinutes(sinavSuresi);
            }
            HyperLink ltrBasla = (HyperLink)e.Item.FindControl("ltrBasla");
            Literal ltrBaslangicTarihi = (Literal)e.Item.FindControl("ltrBaslangicTarihi");
            ltrBaslangicTarihi.Text = baslamaTarihi.TarihYaz();

            if (sinavInfo.OturumTercihi == TestSinavTercihleri.OturumSaatlerineGoreGiris.ToInt32())
            {
                NewMethod(ltrBasla, buOturumId, ogrCvpInfo, baslamaTarihi, bitisTarihi);
            }
            else
            {
                if (buOturumunSirasi == 1)//ilk oturum ise normal kontrolleri yap
                {
                    NewMethod(ltrBasla, buOturumId, ogrCvpInfo, baslamaTarihi, bitisTarihi);
                }
                else //diğer oturumlar için
                {
                    var oturumlar = CacheHelper.Oturumlar(sinavId);

                    //önce oturum numarasını kontrol et
                    var oncekiOturum = oturumlar.FirstOrDefault(x => x.SiraNo < buOturumunSirasi);
                    var oncekiOturumdakiCevaplari = ogrCevapDb.KayitBilgiGetir(oncekiOturum.Id, ogrenci.OpaqId);
                    if (oncekiOturumdakiCevaplari.Id == 0)//önceki oturuma girmemiş ise
                    {
                        ltrBasla.Text = "<i class='fas fa-clock mr-1'></i>Önceki oturumu tamamlayınız";
                        ltrBasla.CssClass = "btn btn-default btn-sm btn-block";
                    }
                    else
                    {
                        baslamaTarihi = oncekiOturumdakiCevaplari.Bitti== SinavDurum.Bitti.ToInt32() 
                            ? oncekiOturumdakiCevaplari.Baslangic.AddMinutes(oncekiOturum.Sure + sinavInfo.BeklemeSuresi) 
                            : oncekiOturumdakiCevaplari.Bitis.Value.AddMinutes(sinavInfo.BeklemeSuresi);

                        ltrBaslangicTarihi.Text = baslamaTarihi.TarihYaz();

                        NewMethod(ltrBasla, buOturumId, ogrCvpInfo, baslamaTarihi, bitisTarihi);
                    }
                }
            }
        }
    }
    private static void NewMethod(HyperLink ltrBasla, int oturumId, TestOgrCevapInfo ogrCvpInfo, DateTime baslamaTarihi, DateTime bitisTarihi)
    {

        if (baslamaTarihi < GenelIslemler.YerelTarih() && bitisTarihi > GenelIslemler.YerelTarih())
        {
            ltrBasla.Text = "<i class='fas fa-chevron-circle-right mr-1'></i> Sınava Başla";
            ltrBasla.NavigateUrl = "Sinav.aspx?t=" + oturumId;
            ltrBasla.CssClass = "btn btn-primary btn-sm btn-block";
        }
        else
        {
            ltrBasla.NavigateUrl = "#";

            if (baslamaTarihi > GenelIslemler.YerelTarih())
            {
                ltrBasla.Text = "<i class='fas fa-clock mr-1'></i>Bekleyiniz";
                ltrBasla.CssClass = "btn btn-default btn-sm btn-block";
            }
            else
            {
                ltrBasla.Text = "<i class='fas fa-times mr-1'></i>Süre Bitti";
                ltrBasla.CssClass = "btn btn-danger btn-sm btn-block";
            }
        }

        if (ogrCvpInfo.Bitti== SinavDurum.Bitti.ToInt32()) //Bu sınavı bitirdi
        {
            ltrBasla.Text = "<i class='fas fa-thumbs-up mr-1'></i>Tamamladınız";
            ltrBasla.CssClass = "btn btn-success btn-sm btn-block";
            ltrBasla.NavigateUrl = "#";
        }
    }
}