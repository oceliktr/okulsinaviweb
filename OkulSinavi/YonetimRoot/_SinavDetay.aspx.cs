using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim_SinavDetayRoot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                if (Request.QueryString["Id"].IsInteger())
                {
                    int id = Request.QueryString["Id"].ToInt32();
                   
                    TestSinavlarDb sinavDb = new TestSinavlarDb();
                    TestSinavlarInfo info = sinavDb.KayitBilgiGetir(id);
                    ltrSinavAdi.Text = info.SinavAdi;
                    ltrAciklama.Text = info.Aciklama;
                    ltrSinif.Text = info.Sinif.ToString();
                    ltrAktif.Text = info.Aktif == 0 ? "Öğrenciye Kapalı" : "Öğrenciye Açık";
                    ltrOturumTarcihi.Text =
                        info.OturumTercihi == 0 ? "Oturumlardaki saate göre" : "Oturumun sırasına göre";
                    if (info.OturumTercihi==1)
                    {
                        ltrOturumTarcihi.Text +="(iki oturum arası bekleme "+ info.BeklemeSuresi + " dakika)";
                    }

                    ltrPuanlama.Text = info.Puanlama.ToString();
                }
            }

        }
    }
}