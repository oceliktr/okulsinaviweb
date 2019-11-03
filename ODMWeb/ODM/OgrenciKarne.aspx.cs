using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class ODM_OgrenciKarne : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BAKANLIĞA GÖNDERİLEN ÖĞRENCİ KARNESİ
            OgrenciKarneDB ogrKrnDb = new OgrenciKarneDB();
            rptOgrenciler.DataSource = ogrKrnDb.KayitlariGetir(4);
            rptOgrenciler.DataBind();
        }
    }

    protected void rptOgrenciler_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Literal ltrGeciciTc = (Literal)e.Item.FindControl("ltrGeciciTc");
            Literal ltrUyrugu = (Literal)e.Item.FindControl("ltrUyrugu");

            Literal ltrIlceAdi = (Literal)e.Item.FindControl("ltrIlceAdi");
            Literal ltrKurumKodu = (Literal)e.Item.FindControl("ltrKurumKodu");
            Literal ltrKurumAdi = (Literal)e.Item.FindControl("ltrKurumAdi");
            Literal ltrTKitapciktur = (Literal)e.Item.FindControl("ltrTKitapciktur");
            Literal ltrTDogru = (Literal)e.Item.FindControl("ltrTDogru");
            Literal ltrTYanlis = (Literal)e.Item.FindControl("ltrTYanlis");
            Literal ltrTBos = (Literal)e.Item.FindControl("ltrTBos");
            Literal ltrMKitapciktur = (Literal)e.Item.FindControl("ltrMKitapciktur");
            Literal ltrMDogru = (Literal)e.Item.FindControl("ltrMDogru");
            Literal ltrMYanlis = (Literal)e.Item.FindControl("ltrMYanlis");
            Literal ltrMBos = (Literal)e.Item.FindControl("ltrMBos");
            Literal ltrFKitapciktur = (Literal)e.Item.FindControl("ltrFKitapciktur");
            Literal ltrFDogru = (Literal)e.Item.FindControl("ltrFDogru");
            Literal ltrFYanlis = (Literal)e.Item.FindControl("ltrFYanlis");
            Literal ltrFBos = (Literal)e.Item.FindControl("ltrFBos");
            
            OgrenciKarneDB ogrKrnDb = new OgrenciKarneDB();
            int ogrenciId = DataBinder.Eval(e.Item.DataItem, "OgrenciId").ToInt32();

            OgrencilerDb ogrDb = new OgrencilerDb();
            OgrencilerInfo info = ogrDb.KayitBilgiGetir(ogrenciId, 4);
            ltrGeciciTc.Text = info.TcKimlik;
            ltrUyrugu.Text = info.Uyrugu;

            KurumlarDb krmDb = new KurumlarDb();
            KurumlarInfo infoK = krmDb.KayitBilgiGetir(info.KurumKodu.ToString());
            ltrIlceAdi.Text = infoK.IlceAdi;
            ltrKurumAdi.Text = infoK.KurumAdi;
            ltrKurumKodu.Text = info.KurumKodu.ToString();

            OgrenciKarneInfo krnInfoT = ogrKrnDb.KayitBilgiGetir(4,1,ogrenciId);
            ltrTKitapciktur.Text = krnInfoT.KitapcikTuru;
            ltrTDogru.Text = krnInfoT.DogruSayisi.ToString();
            ltrTYanlis.Text = krnInfoT.YanlisSayisi.ToString();
            ltrTBos.Text = krnInfoT.Bos.ToString();
            OgrenciKarneInfo krnInfoM = ogrKrnDb.KayitBilgiGetir(4, 2, ogrenciId);
            ltrMKitapciktur.Text = krnInfoM.KitapcikTuru;
            ltrMDogru.Text = krnInfoM.DogruSayisi.ToString();
            ltrMYanlis.Text = krnInfoM.YanlisSayisi.ToString();
            ltrMBos.Text = krnInfoM.Bos.ToString();

            OgrenciKarneInfo krnInfoF = ogrKrnDb.KayitBilgiGetir(4, 3, ogrenciId);
            ltrFKitapciktur.Text = krnInfoF.KitapcikTuru;
            ltrFDogru.Text = krnInfoF.DogruSayisi.ToString();
            ltrFYanlis.Text = krnInfoF.YanlisSayisi.ToString();
            ltrFBos.Text = krnInfoF.Bos.ToString();
        }
    }
}