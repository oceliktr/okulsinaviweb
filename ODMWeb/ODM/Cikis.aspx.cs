using System;
using System.Web;

namespace ODM
{
    public partial class Cikis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            var uyeCookie = new HttpCookie("uyeCookie")
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Add(uyeCookie);
            Response.Redirect("Default.aspx");
        }
    }
}