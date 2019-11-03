using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnketAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cikis"] != null)
            {
                if (Request.QueryString["cikis"].ToString()=="ok")
                {
                    Session["Admin"] = null;
                }
            }
            
            if (Session["Admin"] != null)
            {
                phLogin.Visible = false;
                anketFormu.Visible = true;
            }
            else
            {
                phLogin.Visible = true;
                anketFormu.Visible = false;
            }

            AnketDB anket = new AnketDB();
            rptOkullar.DataSource = anket.TamamlamayanOkullar();
            rptOkullar.DataBind();
        }
    }

    private int KayitNo()
    {
        int id = 0;
        if (Request.QueryString["Id"] != null)
        {
            if (Request.QueryString["Id"].IsInteger())
                id = Request.QueryString["Id"].ToInt32();
        }

        return id;
    }

    
    protected void btnGiris_OnClick(object sender, EventArgs e)
    {
        string kurumKodu = txtKurumKodu.Text.ToTemizMetin();
        if (!string.IsNullOrEmpty(kurumKodu))
        {
           
            if (kurumKodu == "650200")
            {
                phLogin.Visible = false;
                anketFormu.Visible = true;
                Session["Admin"] = "Admin";

                AnketDB anket = new AnketDB();
                rptOkullar.DataSource = anket.TamamlamayanOkullar();
                rptOkullar.DataBind();
            }
            else
            {
                phGirisError.Visible = true;
            }
        }
    }

    protected void UploadDataTableToExcel(DataTable veriler, string dosyaAdi)
    {
        string attachment = "attachment; filename=" + dosyaAdi;
        Response.ClearContent();
        Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-9");
        Response.Charset = "ISO-8859-9";
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";

        string tablo = string.Empty;
        foreach (DataColumn sutun in veriler.Columns)
        {
            Response.Write(tablo + sutun.ColumnName);
            tablo = "\t";
        }
        Response.Write("\n");
        foreach (DataRow satir in veriler.Rows)
        {
            tablo = "";
            for (int j = 0; j < veriler.Columns.Count; j++)
            {
                Response.Write(tablo + satir[j].ToString().Replace(Environment.NewLine, ""));
                tablo = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    protected void btnExceleAktar_OnClick(object sender, EventArgs e)
    {

        AnketDB anket = new AnketDB();

        DataTable tablo = anket.KayitlariGetir();
        UploadDataTableToExcel(tablo, "Abide 4 Anket.xls");
    }

    protected void rptOkullar_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            string kurumKodu = KayitNo().ToString();
            AnketDB anket = new AnketDB();
            rptList.DataSource = anket.KayitlariGetir(kurumKodu);
            rptList.DataBind();
        }
    }

}