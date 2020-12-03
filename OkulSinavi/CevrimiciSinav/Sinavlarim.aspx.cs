using System;

public partial class Sinav_Sinavlarim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Ogrenci"] == null)
            {
                Server.Transfer("Default.aspx");
            }

            TestOgrenci ogrenci = (TestOgrenci)Session["Ogrenci"];
            //mükerrer oturumu kontrol için
            if (CacheHelper.KullaniciGirisKontrol(ogrenci.OpaqId) != ogrenci.GirisKey)
            {
                Response.Redirect("Default.aspx");
            }
            TestOgrPuanDb sinavlariDb = new TestOgrPuanDb();
            rptSinavlar.DataSource = sinavlariDb.KayitlariGetir(ogrenci.OpaqId);
            rptSinavlar.DataBind();
        }
    }
}