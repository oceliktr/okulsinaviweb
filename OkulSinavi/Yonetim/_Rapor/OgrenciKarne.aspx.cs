using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim_OgrenciKarne : System.Web.UI.Page
{
    string opaqId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int sinavId = 0;
            if (Request.QueryString["SinavId"] != "")
            {
                sinavId = Request.QueryString["SinavId"].ToInt32();
            }
            if (Request.QueryString["OpaqId"] != "")
            {
                opaqId = Request.QueryString["OpaqId"];
            }

            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();

            TestOgrPuanDb testOgrCevapDb = new TestOgrPuanDb();
            TestOgrPuanInfo ogrCevap =  testOgrCevapDb.KayitBilgiGetir(sinavId, kInfo.KurumKodu.ToInt32(), opaqId);


            if (ogrCevap.Id != 0) //bu okulu görme yetkisi varsa kayıt gelecektir yoksa 0 gelecektir.
            {

                ltrDogruSayisi.Text = ogrCevap.Dogru.ToString();
                ltrYanlis.Text = ogrCevap.Yanlis.ToString();
                ltrPuan.Text = ogrCevap.Puan > 100 ? ogrCevap.Puan.ToString("##.###") : "<small>hesaplanmadı</small>";

                int soruSayisi = 0;
                TestSorularDb sorularDb = new TestSorularDb();

                TestOturumlarDb oturumDb = new TestOturumlarDb();
                var oturumList = oturumDb.Oturumlar(sinavId);
                foreach (var o in oturumList)
                {
                    soruSayisi += sorularDb.SoruSayisi(o.Id);
                }

                rptOturumlar.DataSource = oturumList;
                rptOturumlar.DataBind();

                ltrSoruSayisi.Text = soruSayisi.ToString();

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

            TestOgrCevapDb testOgrCevapDb = new TestOgrCevapDb();
            var ogrCevap = testOgrCevapDb.KayitBilgiGetir(oturumId, opaqId);
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

        if (rptOturumlar.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                PlaceHolder phEmpty = (PlaceHolder)e.Item.FindControl("phEmpty");
                phEmpty.Visible = true;
            }

        }
    }
}