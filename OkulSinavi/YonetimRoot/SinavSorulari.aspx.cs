using System;
using System.Web.Services;
using Newtonsoft.Json;

public partial class OkulSinavi_CevrimiciSinavYonetim_SinavSorulariRoot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            if (Request.QueryString["OturumId"] != null)
            {
                if (Request.QueryString["OturumId"].IsInteger())
                {
                    int oturumId = Request.QueryString["OturumId"].ToInt32();

                    TestOturumlarDb veriDb = new TestOturumlarDb();
                    var info = veriDb.KayitBilgiGetir(oturumId);
                    ltrOturumAdi.Text = info.OturumAdi;
                    hlOturum.NavigateUrl = "OturumYonetim.aspx?SinavId=" + info.SinavId;
                }
            }
        }
    }


    [WebMethod]
    public static string SoruSil(string SoruId)
    {
        int id = SoruId.ToInt32();
        JsonMesaj soList;
       
        TestSorularDb veriDb = new TestSorularDb();

        TestSorularInfo sonuc = veriDb.KayitBilgiGetir(id);
        if (sonuc.Id == 0)
        {
            soList = new JsonMesaj
            {
                Sonuc = "no",
                Mesaj = "Kayıt bulunamadı.",
            };
            return JsonConvert.SerializeObject(soList);
        }

        int res = veriDb.KayitSil(id);
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