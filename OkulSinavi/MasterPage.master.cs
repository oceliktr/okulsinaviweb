using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string filename = Path.GetFileName(Request.Path);
            if (filename == "default.aspx")
            {
                liAnasayfa.Attributes.Add("class", "active");
            }
            else if (filename == "Iletisim.aspx")
            {

                liIletisim.Attributes.Add("class", "active");

            }
            else if(filename == "Hakkimizda.aspx"|| filename == "Nedir.aspx" || filename == "Mesafeli-Satis-Sozlesmesi-Ornegi.aspx" || filename == "Gizlilik-Sozlesmesi.aspx")
            {

                liKurumsal.Attributes.Add("class", "active");
            }
        }
    }
}
