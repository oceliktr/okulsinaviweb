using Newtonsoft.Json;
using System;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Okul_SinaviYonetim_SinavYonetimRoot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            IlcelerDb ilcelerDb = new IlcelerDb();

            ddlIlceler.DataSource = ilcelerDb.KayitlariGetir();
            ddlIlceler.DataValueField = "Id";
            ddlIlceler.DataTextField = "IlceAdi";
            ddlIlceler.DataBind();
            ddlIlceler.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
            ddlKurumlar.Items.Insert(0, new ListItem("Önce İlçe Seçiniz", ""));
        }
    }
    protected void ddlIlceler_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int ilce = ddlIlceler.SelectedValue.ToInt32();
        KurumlarDb veriDb = new KurumlarDb();
        ddlKurumlar.DataSource = veriDb.OkullariGetir(ilce);
        ddlKurumlar.DataValueField = "KurumKodu";
        ddlKurumlar.DataTextField = "KurumAdi";
        ddlKurumlar.DataBind();
        ddlKurumlar.Items.Insert(0, new ListItem("Kurum Seçiniz", ""));
    }
   

    [WebMethod]
    public static string SinavSil(string SinavId)
    {
        int id = SinavId.ToInt32();
        JsonMesaj soList;
        
        TestSinavlarDb sinavDb = new TestSinavlarDb();
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        TestSinavlarInfo sonuc = sinavDb.KayitBilgiGetir(id, kInfo.KurumKodu.ToString());
        if (sonuc.Id == 0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Kayıt bulunamadı.",
            };
            return JsonConvert.SerializeObject(soList);
        }

        int res = sinavDb.KayitSil(id);
        if (res > 0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "ok",
                Mesaj = "Kayıt silindi.",
            };
            return JsonConvert.SerializeObject(soList);
        }

        soList = new JsonMesaj
        {
            Sonuc = "no",
            Mesaj = "Kayıt silinemedi.",
        };
        return JsonConvert.SerializeObject(soList);
    }


}