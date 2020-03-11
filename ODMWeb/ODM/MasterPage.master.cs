using System;
using System.Web;
using System.Web.UI.WebControls;

namespace ODM
{
    public partial class AdminMasterPage : System.Web.UI.MasterPage
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
            GenelIslemler.SayfaBaslikBilgisi("","",Master);

            string sitil = "skin-purple";
            string kutu = "";
            string sidebar = "";
            ltrSitil.Text = string.Format("<link rel=\"stylesheet\" href=\"/Content/dist/css/skins/{0}.css\" />", sitil);
            string body = string.Format("{0} {1} {2}  sidebar-mini", sitil, kutu, sidebar);
            myBody.Attributes.Add("class", body); //sabit genişlik

            if (IsPostBack) return;
         
                try
                {
                    if (Request.Cookies["uyeCookie"] != null)
                    {
                        string uyeAdiCookies = Request.Cookies["uyeCookie"]["Oturum"];

                        if (uyeAdiCookies == "Acik")
                        {
                            int uyeId = Request.Cookies["uyeCookie"]["UyeId"].ToInt32();
                            Session["UyeId"] = uyeId.ToString();
                            KullanicilarDb veriDb = new KullanicilarDb();
                            bool kayit = veriDb.KayitKontrol(uyeId);

                            if (kayit == false)
                                Response.Redirect("Cikis.aspx");
                        }
                        else
                            Response.Redirect("Cikis.aspx");
                    }
                    else
                        Response.Redirect("Cikis.aspx");
                }
                catch (Exception)
                {
                    Response.Redirect("Cikis.aspx");
                }
            

            try
            {
                if (Yetki().Contains("Root"))
                {
                    liKullanicilar.Visible = true;
                    liAyarlar.Visible = true;
                }
                else
                {
                    liKullanicilar.Visible = false;
                    liAyarlar.Visible = false;
                }
            }
            catch (Exception)
            {
                
               Response.Redirect("Cikis.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string avatar = "/Content/images/avatar.png";
            if (IsPostBack) return;
            HttpCookie httpCookie = Request.Cookies["uyeCookie"];
            if (httpCookie == null) return;
            int uyeId = httpCookie["UyeId"].ToInt32();

            KullanicilarDb kDb = new KullanicilarDb();
            KullanicilarInfo kInfo = kDb.KayitBilgiGetir(uyeId);
            ltrAdiSoyadi.Text = kInfo.AdiSoyadi;
            ltrKullaniciAdi.Text = kInfo.AdiSoyadi;
            
            imgAvatar.ImageUrl = avatar;
            imgAvatarMini.ImageUrl = avatar;
            ltrOncekiGiris.Text = kInfo.OncekiGiris.TarihYaz();
            ltrGirisSayisi.Text = kInfo.GirisSayisi + " defa";

            ltrAdiSoyadiSol.Text = kInfo.AdiSoyadi;
            imgAvatarSol.ImageUrl = avatar;
        }
    }
}
