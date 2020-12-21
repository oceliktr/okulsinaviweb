using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class YonetimRoot_SinaviYapanOkullar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["SinavId"]!=null)
            {
                int sinavId = Request.QueryString["SinavId"].ToInt32();

                TestSinavlarDb sinavlarDb= new TestSinavlarDb();
                var sinav = sinavlarDb.KayitBilgiGetir(sinavId);
                string kurumlar = sinav.Kurumlar;
                int sinif = sinav.Sinif;
                
                string[] kurumkodu = kurumlar.Split(',');

                int ogrSayisi = 0;//sınava girebilecek toplam öğrenci sayısı
                
                List<DevamEdenOkullarModel> okullar = new List<DevamEdenOkullarModel>();
                foreach (var s in kurumkodu)
                {
                    if (s.IsInteger())
                    {
                        KurumlarDb kurumDb = new KurumlarDb();
                        DevamEdenOkullarModel kurum = kurumDb.KayitBilgiGetir(s,sinif);

                        ogrSayisi += kurum.OgrenciSayisi;
                        okullar.Add(new DevamEdenOkullarModel{IlceAdi = kurum.IlceAdi,KurumAdi = kurum.KurumAdi,OgrenciSayisi = kurum.OgrenciSayisi});
                    }
                }

                ltrToplamOgrenciSayisi.Text = ogrSayisi.ToString();
                rptKurumlar.DataSource = okullar;
                rptKurumlar.DataBind();
            }
        }
    }
}