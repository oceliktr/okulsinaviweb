using Newtonsoft.Json;
using System;
using System.Web.Services;

public partial class Okul_SinaviYonetim_OkulOgrenciListesi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("OkulYetkilisi");
        return yetkili;
    }

    [WebMethod]
    public static string Sil(string OgrencId)
    {
        JsonMesaj soList;
        if (YetkiKontrol())
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Bunun için yetkiniz yoktur.",
            };
            return JsonConvert.SerializeObject(soList);
        }
        TestKutukDb kutukDb = new TestKutukDb();

        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();

        int donem = TestSeciliDonem.SeciliDonem().Id;
        if (TestSeciliDonem.SeciliDonem().VeriGirisi==0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Veri girişleri kapatıldığı için kayıt silinemedi.",
            };
            return JsonConvert.SerializeObject(soList);
        }
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