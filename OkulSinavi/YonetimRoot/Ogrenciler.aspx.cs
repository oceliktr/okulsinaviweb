using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim_OgrencilerRoot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }

            IlcelerDb ilcelerDb = new IlcelerDb();
            ddlIlce.DataSource = ilcelerDb.KayitlariGetir();
            ddlIlce.DataValueField = "Id";
            ddlIlce.DataTextField = "IlceAdi";
            ddlIlce.DataBind();
            ddlIlce.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));
            ddlKurum.Items.Insert(0, new ListItem("İlçe Seçiniz", ""));

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

        TestKutukInfo sonuc = kutukDb.KayitBilgiGetir(OgrencId);
        if (sonuc.Id == 0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Kayıt bulunamadı.",
            };
            return JsonConvert.SerializeObject(soList);
        }



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
    [WebMethod]
    public static string OgrenciAra(string tcKimlik, string ogrAdi, string ogrSoyadi)
    {
        int donem = TestSeciliDonem.SeciliDonem().Id;
        string opaqId = tcKimlik == "" ? "" : tcKimlik.Md5Sifrele();
        TestKutukDb veriDb = new TestKutukDb();
        DataTable kutukInfo = veriDb.OgrenciAra(donem, opaqId, ogrAdi, ogrSoyadi);
        if (kutukInfo.Rows.Count > 0)
        {
            if (kutukInfo.Rows.Count > 250)
            {
                return JsonConvert.SerializeObject(new { Mesaj = kutukInfo.Rows.Count + " kayıt bulundu. Daha detaylı bilgi giriniz.", Sonuc = "no" });
            }
            return JsonConvert.SerializeObject(new { kutukInfo, Sonuc = "ok" });
        }

        return JsonConvert.SerializeObject(new { Mesaj = "Aradığınız kriterlere uygun kayıt bulunamadı.", Sonuc = "no" });


    }

}