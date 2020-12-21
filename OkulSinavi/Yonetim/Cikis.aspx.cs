using System;
using System.Web;

namespace OkulSinavi
{
    public partial class Cikis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            var uyeCookie = new HttpCookie("uyeCookie")
            {
                Expires = GenelIslemler.YerelTarih().AddDays(-1)
            };
            Response.Cookies.Add(uyeCookie);
            Response.Redirect("/Yonetim/Default.aspx");
        }
    }
}