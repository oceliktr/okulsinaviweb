using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Okul_SinaviYonetim_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TestDonemDb dnmDb = new TestDonemDb();
            ddlDonemler.DataSource = dnmDb.KayitlariGetir();
            ddlDonemler.DataValueField = "Id";
            ddlDonemler.DataTextField = "Donem";
            ddlDonemler.DataBind();
            ddlDonemler.Items.Insert(0, new ListItem("Dönem Seçiniz", ""));

            TestDonemInfo donemInfo = TestSeciliDonem.SeciliDonem();

            ddlDonemler.SelectedValue = donemInfo.Id.ToString();
            ltrAktifDonem.Text = donemInfo.Donem;

        }
    }

    protected void ddlDonemler_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string donem = ddlDonemler.SelectedValue;
        TestDonemDb dnmDb = new TestDonemDb();
        if (donem == "")
        {
            donem = dnmDb.AktifDonem().Id.ToString();
        }

        HttpCookie uyeCookie = new HttpCookie("csDonem");
        uyeCookie["SeciliDonem"] = donem;
        uyeCookie.Expires = GenelIslemler.YerelTarih().AddDays(1);
        Response.Cookies.Add(uyeCookie);

        ltrAktifDonem.Text = dnmDb.KayitBilgiGetir(donem.ToInt32()).Donem;

    }
}