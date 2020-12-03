using OkulSinavi;
using System;
using System.Web.UI.WebControls;

public partial class Sinav_MasterPage : System.Web.UI.MasterPage
{
    public void UyariKirmizi(string uyariMesaji, PlaceHolder phUyari)
    {
        AdminUyarilar uyari = (AdminUyarilar)LoadControl("~/Kutuphaneler/Uyarilar.ascx");
        uyari.PanelUyariKirmizi = true;
        uyari.LiteralUyariKirmizi = uyariMesaji;
        phUyari.Controls.Add(uyari);
    }

    public void UyariTuruncu(string uyariMesaji, PlaceHolder phUyari)
    {
        AdminUyarilar uyari = (AdminUyarilar)LoadControl("~/Kutuphaneler/Uyarilar.ascx");
        uyari.PanelUyariTuruncu = true;
        uyari.LiteralUyariTuruncu = uyariMesaji;
        phUyari.Controls.Add(uyari);
    }

    public void UyariBilgilendirme(string uyariMesaji, PlaceHolder phUyari)
    {
        AdminUyarilar uyari = (AdminUyarilar)LoadControl("~/Kutuphaneler/Uyarilar.ascx");
        uyari.PanelBilgilendirme = true;
        uyari.LiteralBilgilendirme = uyariMesaji;
        phUyari.Controls.Add(uyari);
    }

    public void UyariIslemTamam(string uyariMesaji, PlaceHolder phUyari)
    {
        AdminUyarilar uyari = (AdminUyarilar)LoadControl("~/Kutuphaneler/Uyarilar.ascx");
        uyari.PanelIslemTamam = true;
        uyari.LiteralIslemTamam = uyariMesaji;
        phUyari.Controls.Add(uyari);
    }

    public int UyeId()
    {
        int uyeId = 0;
        if (Request.Cookies["uyeCookie"] == null) return uyeId;
        string uyeAdiCookies = Request.Cookies["uyeCookie"]["Oturum"];

        if (uyeAdiCookies == "Acik")
            uyeId = Request.Cookies["uyeCookie"]["UyeId"].ToInt32();
        return uyeId;
    }

    public string Yetki()
    {
        string yetkiId = "";
        if (Request.Cookies["uyeCookie"] == null) return yetkiId;
        string uyeAdiCookies = Request.Cookies["uyeCookie"]["Oturum"];

        if (uyeAdiCookies == "Acik")
            yetkiId = Request.Cookies["uyeCookie"]["Yetki"];
        return yetkiId;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        GenelIslemler.SayfaBaslikBilgisi("", "", Master);

        if (IsPostBack) return;

        //try
        //{
        //    if (Request.Cookies["uyeCookie"] != null)
        //    {
        //        string uyeAdiCookies = Request.Cookies["uyeCookie"]["Oturum"];

        //        if (uyeAdiCookies == "Acik")
        //        {
        //            int uyeId = Request.Cookies["uyeCookie"]["UyeId"].ToInt32();
        //            Session["UyeId"] = uyeId.ToString();
        //            KullanicilarDb veriDb = new KullanicilarDb();
        //            bool kayit = veriDb.KayitKontrol(uyeId);

        //            if (kayit == false)
        //                Response.Redirect("~/Cikis.aspx");
        //        }
        //        else
        //            Response.Redirect("~/Cikis.aspx");
        //    }
        //    else
        //        Response.Redirect("~/Cikis.aspx");
        //}
        //catch (Exception)
        //{
        //    Response.Redirect("~/Cikis.aspx");
        //}

        //try
        //{
        //    if (Yetki().Contains("Root"))
        //    {
        //        liKullanicilar.Visible = true;
        //        liAyarlar.Visible = true;
        //    }
        //    else
        //    {
        //        liKullanicilar.Visible = false;
        //        liAyarlar.Visible = false;
        //    }
        //}
        //catch (Exception)
        //{
        //    Response.Redirect("~/Cikis.aspx");
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}