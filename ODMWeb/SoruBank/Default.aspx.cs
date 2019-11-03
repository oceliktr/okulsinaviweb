using System;
using System.Web;
using System.Web.UI.WebControls;
using DAL;

namespace SoruBank
{
    public partial class SpVeriDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                    Response.Redirect("Giris.aspx");
              
        }
    }
}