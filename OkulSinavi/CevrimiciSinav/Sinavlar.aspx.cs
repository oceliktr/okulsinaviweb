using System;

public partial class Sinav_Sinavlar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Ogrenci"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            TestOgrenci ogrenci = (TestOgrenci)Session["Ogrenci"];
            ltrAdiSoyadi.Text = ogrenci.Adi + " " + ogrenci.Soyadi;

            TestDonemDb dnmDb = new TestDonemDb();
            int donem = dnmDb.AktifDonem().Id;


            if (ogrenci.OpaqId == "252525")
            {
                rptSinavlarDemo.DataSource = CacheHelper.Sinavlar();
                rptSinavlarDemo.DataBind();
                rptSinavlar.Visible = false;
            }
            else
            {  //mükerrer oturumu kontrol için
                if (CacheHelper.KullaniciGirisKontrol(ogrenci.OpaqId) != ogrenci.GirisKey)
                {
                   Response.Redirect("Default.aspx");
                }
                rptSinavlar.DataSource = CacheHelper.AktifSinavlar(ogrenci.Sinifi);
                rptSinavlar.DataBind();
                rptSinavlarDemo.Visible = false;
            }
        }
    }
}