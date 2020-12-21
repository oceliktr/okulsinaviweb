using System;

namespace OkulSinavi
{
    public partial class AdminAdminMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();

            
            if (kInfo.Yetki.Contains("Admin"))
            {
                liKullanicilar.Visible = true;
                liOkulOgrListesi.Visible = true;
                liSinavYonetim.Visible = true;
                liSinavlar.Visible = true;
                liDemo.Visible = true;
            }
            else if (kInfo.Yetki.Contains("Ogretmen"))
            {
                liOkulOgrListesi.Visible = true;
                liSinavlar.Visible = true;
                liDemo.Visible = true;
            }
        }
    }
}