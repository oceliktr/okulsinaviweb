using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class CS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        pnlPerson.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        StyleSheet styles = new StyleSheet();
      
        styles.LoadTagStyle("#helvetica", "height", "500px");
        styles.LoadTagStyle("#helvetica", "font-weight", "bold");
        styles.LoadTagStyle("#helvetica", "font-family", "Cambria");
        styles.LoadTagStyle("helvetica", "font-size", "8px");
        styles.LoadTagStyle("#helvetica", "background-color", "Blue");
        styles.LoadTagStyle("#helvetica", "color", "White");
        styles.LoadTagStyle("#helvetica", "padding-left", "5px");


        HTMLWorker htmlparser = new HTMLWorker(pdfDoc, null, styles);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();

    }
}