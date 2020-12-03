using System;

public partial class Okul_SinaviYonetim_UstMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        OturumIslemleri oturum = new OturumIslemleri();
        KullanicilarInfo kInfo = oturum.OturumKontrol();

        if (kInfo.Yetki.Contains("Root"))
        {
            liTestler.Visible = true;
            liSinavYonetim.Visible = true;
            liSinavRapor.Visible = true;
            liOgrenciler.Visible = true;
            liDemo.Visible = true;
        }
        if (kInfo.Yetki.Contains("Admin"))
        {
            liSinavYonetim.Visible = true;
            liSinavRapor.Visible = true;
            liTestler.Visible = true;
            liOgrenciler.Visible = true;
            liDemo.Visible = true;
        }
        else if (kInfo.Yetki.Contains("OkulYetkilisi"))
        {
            liOkulOgrListesi.Visible = true;
            liTestler.Visible = true;
        }
        else if (kInfo.Yetki.Contains("IlceMEMYetkilisi"))
        {
            liTestler.Visible = true;
        }
        else if (kInfo.Yetki.Contains("LgsIlKomisyonu"))
        {
           liTestler.Visible = true;
        }
    }
}