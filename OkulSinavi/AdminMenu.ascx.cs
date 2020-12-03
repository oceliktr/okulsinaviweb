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
                liBaranslar.Visible = true;
                liKurumlar.Visible = true;
                liKullanicilar.Visible = true;
                liSinavYonetim.Visible = true;
            }
            if (kInfo.Yetki.Contains("Root"))
            {
                liBaranslar.Visible = true;
                liKurumlar.Visible = true;
                liKullanicilar.Visible = true;
                liSinavYonetim.Visible = true;
            }
        }
    }
}