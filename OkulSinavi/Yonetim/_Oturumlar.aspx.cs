using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Okul_SinaviYonetim_Oturumlar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }
            if (Request.QueryString["SinavId"] != null)
            {
                if (Request.QueryString["SinavId"].IsInteger())
                {
                    int sinavId = Request.QueryString["SinavId"].ToInt32();

                    TestOturumlarDb sinavDb = new TestOturumlarDb();
                    var oturumlar = sinavDb.Oturumlar(sinavId);
                    rptOturumlar.DataSource = oturumlar;
                    rptOturumlar.DataBind();


                    //Sınav sonuçlarını öğrenciler sınavı bitirdikten sonra gösterilmeli.
                    List<DateTime> sonOturumTarihleri = new List<DateTime>();

                    foreach (var o in oturumlar)
                    {
                        sonOturumTarihleri.Add(o.BitisTarihi.AddMinutes(o.Sure.ToDouble()));

                    }

                    var sonOturumBitis = sonOturumTarihleri.OrderByDescending(x => x).First();
                    ltrCevapAnahtariBilgi.Text = sonOturumBitis.TarihYaz();


                }
            }
        }
    }
    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin");
        return yetkili;
    }
   
}