using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Basvuru_Kayit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["p"] != null)
            {
                if (Request.QueryString["p"] == "1")
                {
                    ltrBasvuruAciklama.Text =
                        "Okulunuz (kurumkodu@meb.k12.tr) e-posta adresine bir bağlantı göndereceğiz. Başvuru işleminizin tamamlanması için bu bağlantıya tıklamanız gerekmektedir.";
                }
                else if (Request.QueryString["p"] == "2")
                {
                    ltrBasvuruAciklama.Text = "Standart Paket başvurunuz ödeme işlemi tamamlandıktan sonra aktif hale gelecektir.";
                }
                else if (Request.QueryString["p"] == "3")
                {
                    ltrBasvuruAciklama.Text = "Premium Paket başvurunuz ödeme işlemi tamamlandıktan sonra aktif hale gelecektir.";
                }
                else
                {
                    Response.Redirect("Basvuru.aspx");
                }
            }
        }
    }
}