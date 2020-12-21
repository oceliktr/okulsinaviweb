using Newtonsoft.Json;
using System;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Okul_SinaviYonetim_OkulOgrenciListesiRoot : System.Web.UI.Page
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
    public static string Sil(string OgrencId)
    {
        JsonMesaj soList;
        
        TestKutukDb kutukDb = new TestKutukDb();

        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();

        int donem = TestSeciliDonem.SeciliDonem().Id;
        
        TestKutukInfo sonuc = kutukDb.KayitBilgiGetir(donem,kInfo.KurumKodu.ToInt32(),OgrencId);
        if (sonuc.Id == 0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Kayıt bulunamadı.",
            };
            return JsonConvert.SerializeObject(soList);
        }

        TestOgrCevapDb ogrCevapDb= new TestOgrCevapDb();
        if (ogrCevapDb.KayitKontrol(OgrencId))
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Daha önce sınava girmiş öğrenci bilgileri silinemez.",
            };
            return JsonConvert.SerializeObject(soList);
        }

        //Kurum kodu ile kontrol yaptık. Eğer kurum kodu ile öğrenci bulunduysa bu okula aittir

        //Diğer tablolardaki öğrenci verilerini metod içerisinde siliniyor
        int res = kutukDb.KayitSil(OgrencId);
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