using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class LGSSoruBank_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Request.Cookies["uyeCookie"] == null) Response.Redirect("~/ODM/Giris.aspx");

        //string uyeAdiCookies = Request.Cookies["uyeCookie"]["Oturum"].ToString();

        //if (uyeAdiCookies == "Acik")
        //{
            Response.Redirect("~/LGSSoruBank/Giris.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/ODM/Giris.aspx");
        //}



    }


}