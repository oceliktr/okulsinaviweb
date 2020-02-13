using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ODM_LgsRaporOkulGrafik : System.Web.UI.Page
{
    public string Yetki()
    {
        string yetkiId = "";
        if (Request.Cookies["uyeCookie"] == null) return yetkiId;
        string uyeAdiCookies = Request.Cookies["uyeCookie"]["Oturum"];

        if (uyeAdiCookies == "Acik")
            yetkiId = Request.Cookies["uyeCookie"]["Yetki"];
        return yetkiId;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Yetki().Contains("Root") && !Yetki().Contains("Admin") && !Yetki().Contains("IlceMEMYetkilisi") && !Yetki().Contains("OkulYetkilisi") && !Yetki().Contains("LgsIlKomisyonu"))
                Response.Redirect("Giris.aspx");
          

            int kurumKodu = 0;
            if (Request.QueryString["kurumkodu"] != null)
                kurumKodu = Request.QueryString["kurumkodu"].ToInt32();
            int sinif = 0;
            if (Request.QueryString["sinif"] != null)
                sinif = Request.QueryString["sinif"].ToInt32();

            CkKarneKutukDB kutukDb = new CkKarneKutukDB();
            CkKarneKutukInfo kutukInfo = kutukDb.KayitBilgiGetir(sinif, kurumKodu);

            ltrIlceOkulAdi.Text = string.Format("{0} - {1} ({2}. Sınıf)",kutukInfo.IlceAdi,kutukInfo.KurumAdi,sinif);

            CkKarneSonuclariDB karneSonuclariDb= new CkKarneSonuclariDB();
            List<CkKarneSonuclariInfo> ksList = karneSonuclariDb.KayitlariDizeGetir(kurumKodu, sinif);

            List<OkulGrafikViewModel> sonucList = new List<OkulGrafikViewModel>();
            foreach (var snv in ksList.DistinctBy(p => p.SinavId))
            {

              sonucList.Add(new OkulGrafikViewModel(snv.SinavId,snv.BransId, snv.Sinif, snv.Sube, snv.Dogru, snv.Yanlis, snv.Bos, snv.SinavAdi, snv.BransAdi, 0,0,0,0));  
            }
        }
    }
    
}