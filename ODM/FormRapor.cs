using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace ODM
{
    public partial class FormRapor : Form
    {
        public FormRapor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            KazanimKarneRapor objRpt = new KazanimKarneRapor();

           KazanimKarneDB veriDb = new KazanimKarneDB();


            // Setting data source of our report object
            objRpt.SetDataSource(veriDb.KayitlariGetir());
            
           TextObject root = (TextObject)objRpt.ReportDefinition.ReportObjects["Text1"];
            root.Text = @"ERZURUM İL MİLLİ EĞİTİM MÜDÜRLÜĞÜ\n
            Ölçme Değerlendirme Merkezi\n
            28.12.2017 - İZLEME ARAŞTIRMASI TÜRKÇE DERSİ\n
                HAKKI PINAR ORTAOKULU 5 - A ŞUBE KARNESİ";

            // Binding the crystalReportViewer with our report object. 
            crystalReportViewer1.ReportSource = objRpt;
        }
    }
}
