using Microsoft.VisualBasic;
using ODM.CKYazdirDb.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ODM.CKYazdirDb.Library;
using ODM.CKYazdirDb.Model;

namespace ODM.CKYazdirDb
{
    public partial class FormAna : Form
    {
        public FormAna()
        {
            InitializeComponent();
        }
        private void BtnKutuk_Click(object sender, EventArgs e)
        {
            FormKutukIslemleri frm = new FormKutukIslemleri();
            frm.ShowDialog();
        }

        private void BtnAyarlar_Click(object sender, EventArgs e)
        {
            FormAyarlar frm = new FormAyarlar();
            frm.ShowDialog();
        }

        private void BtnCkOlustur_Click(object sender, EventArgs e)
        {
            FormCkOlustur frm = new FormCkOlustur();
            frm.ShowDialog();
        }



        private void FormAna_Load(object sender, EventArgs e)
        {
            //background nesnesi sağlıklı çalışması için gerekli. 
            CheckForIllegalCrossThreadCalls = false;
        }

        private void BtnTextOlustur_Click(object sender, EventArgs e)
        {
            FormTxtOlustur frm = new FormTxtOlustur();
            frm.ShowDialog();
        }

        private void BtnCevapYukle_Click(object sender, EventArgs e)
        {
            FormCevaplariYukle frm = new FormCevaplariYukle();
            frm.ShowDialog();
        }

        private void BtnKazanimlar_Click(object sender, EventArgs e)
        {
            FormKazanimlar frm = new FormKazanimlar();
            frm.ShowDialog();
        }

        private void BtnBrans_Click(object sender, EventArgs e)
        {
            FormBranslar frm = new FormBranslar();
            frm.ShowDialog();
        }

        private void btnDegerlenirmeKarne_Click(object sender, EventArgs e)
        {
            FormSonDegerlendirme frm = new FormSonDegerlendirme();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("D:/a.pdf", FileMode.Create));
            document.Open();
            Paragraph p = new Paragraph("Test");

            ColumnText.ShowTextAligned(writer.DirectContent,
                Element.ALIGN_CENTER, new Phrase("single line"), 200, 800, 0);


          
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(@"C:\Users\osman\Desktop\ÜDS2\UDS7.png");

            jpg.ScaleToFit(document.PageSize.Width, document.PageSize.Height); //fotoğrafın boyutu
            jpg.SetAbsolutePosition(0, 0); //başlangıç pozisyonu
            jpg.SpacingBefore = 0f; //Görüntüden önce boşluk boyutu
            jpg.SpacingAfter = 0f; //Görüntüden sonra boşluk boyutu
            jpg.Alignment = Element.ALIGN_LEFT;

           // ndurx.Value= Convert.ToDecimal(document.PageSize.Width);
          //  ndury.Value= Convert.ToDecimal(document.PageSize.Height);

            float llx = Convert.ToSingle(ndllx.Value); //112
            float lly = Convert.ToSingle(ndlly.Value); //665
            float urx = Convert.ToSingle(ndurx.Value);//550
            float ury = Convert.ToSingle(ndury.Value);//0
            float leading = Convert.ToSingle(ndleading.Value);//15

    

            ColumnText ct = new ColumnText(writer.DirectContent);
            Phrase myText = new Phrase(textBox1.Text, new Font(iTextSharp.text.Font.FontFamily.HELVETICA, Convert.ToSingle(ndFontSize.Value)));

            ct.SetSimpleColumn(myText,llx , lly, urx, ury, leading, Element.ALIGN_LEFT);
            ct.Go();

            document.Add(p);
            document.Add(jpg);
            //
            //document.NewPage();
            ////
            //document.Add(p);
            //document.Add(jpg);

            document.Close();
            System.Diagnostics.Process.Start("D:/a.pdf");
        }
    }
}
