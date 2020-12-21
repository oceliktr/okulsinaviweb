using System;
using System.Web.UI.WebControls;

public partial class OkulSinavi_CevrimiciSinavYonetim_OgrenciSinavDetayRoot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OturumIslemleri oturum = new OturumIslemleri();
            KullanicilarInfo kInfo = oturum.OturumKontrol();

            if (!kInfo.Yetki.Contains("Root") && !kInfo.Yetki.Contains("Admin") && !kInfo.Yetki.Contains("Ogretmen"))
            {
                Response.Redirect("Default.aspx");
            }

            if (Request.QueryString["OpaqId"] != null)
            {
                
                    string opaqId = Request.QueryString["OpaqId"];

                    TestKutukDb testKutukDb= new TestKutukDb();
                    TestKutukInfo kutukInfo = testKutukDb.KayitBilgiGetir(opaqId);

                    if (kInfo.KurumKodu.ToInt32()!=kutukInfo.KurumKodu&& !kInfo.Yetki.Contains("Root"))
                    {
                        Response.Redirect("Default.aspx");
                    }

                    ltrOgrenciAdiSoyadi.Text = kutukInfo.Adi.IlkHarfleriBuyut() + " " + kutukInfo.Soyadi.IlkHarfleriBuyut();

                    
                    TestSinavlarDb veriDb = new TestSinavlarDb();
                    ddlSinavlar.DataSource = veriDb.KayitlariGetir(kutukInfo.Sinifi,kutukInfo.KurumKodu.ToString());
                    ddlSinavlar.DataTextField = "SinavAdi";
                    ddlSinavlar.DataValueField = "Id";
                    ddlSinavlar.DataBind();
                    ddlSinavlar.Items.Insert(0, new ListItem("Sınav Seçiniz", ""));

                    ddlSinavlar.Attributes.Add("onchange", "Sinav()");
                
            }
        }
    }
}