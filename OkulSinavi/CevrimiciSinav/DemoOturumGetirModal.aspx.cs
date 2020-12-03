using System;

public partial class CevrimiciSinav_DemoOturumGetirModal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TestOgrenci ogrenci = (TestOgrenci)Session["Ogrenci"];
            if (ogrenci.OpaqId != "252525")
                Response.Redirect("Default.asp");

            if (Request.QueryString["SinavId"] != null)
            {
                TestOturumlarDb oturumDb = new TestOturumlarDb();
                int sinavId = Request.QueryString["SinavId"].ToInt32();

                rptOturumlar.DataSource = oturumDb.KayitlariGetir(sinavId);
                rptOturumlar.DataBind();
            }

        }
    }

}