using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim__Ogrenciler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (YetkiKontrol())
            {
                Response.Redirect("Default.aspx");
            }
            KayitlariListele();
        }
    }
    private static bool YetkiKontrol()
    {
        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();
        bool yetkili = !kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin");
        return yetkili;
    }

    private void KayitlariListele()
    {
        int kurumKodu = 0;
        int sinif = 0;
        int donem = TestSeciliDonem.SeciliDonem().Id;
        if (Request.QueryString["KurumKodu"] != null)
        {
            kurumKodu = Request.QueryString["KurumKodu"].ToInt32();
        }
        if (Request.QueryString["Sinif"] != null)
        {
            sinif = Request.QueryString["Sinif"].ToInt32();
        }
        
        if (kurumKodu != 0 && sinif != 0)
        {
            TestKutukDb veriDb = new TestKutukDb();
            rptOgrenciler.DataSource = veriDb.KayitlariGetir(donem,kurumKodu, sinif); 
            rptOgrenciler.DataBind();
        }
    }

    protected void rptOgrenciler_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            GenelIslemler.SiraNumarasiForRepeater(e, "lblSira", 0, 10000);
        }
        if (rptOgrenciler.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                PlaceHolder phEmpty = (PlaceHolder)e.Item.FindControl("phEmpty");
                phEmpty.Visible = true;
            }

        }
    }
}