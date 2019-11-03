using System;

namespace ODM
{
    public partial class AdminUyarilar : System.Web.UI.UserControl
    {
        public bool PanelUyariKirmizi
        {
            get { return pnlUyariKirmizi.Visible; }
            set { pnlUyariKirmizi.Visible = value; }
        }
        public bool PanelUyariTuruncu
        {
            get { return pnlUyariTuruncu.Visible; }
            set { pnlUyariTuruncu.Visible = value; }
        }
        public bool PanelIslemTamam
        {
            get { return pnlOnay.Visible; }
            set { pnlOnay.Visible = value; }
        }
        public bool PanelBilgilendirme
        {
            get { return pnlBilgilendirme.Visible; }
            set { pnlBilgilendirme.Visible = value; }
        }
        public string LiteralUyariKirmizi
        {
            get { return ltrUyariKirmizi.Text; }
            set { ltrUyariKirmizi.Text = value; }
        }
        public string LiteralUyariTuruncu
        {
            get { return ltrUyariTuruncu.Text; }
            set { ltrUyariTuruncu.Text = value; }
        }
        public string LiteralIslemTamam
        {
            get { return ltrOnay.Text; }
            set { ltrOnay.Text = value; }
        }
        public string LiteralBilgilendirme
        {
            get { return ltrBilgilendirme.Text; }
            set { ltrBilgilendirme.Text = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
   
    }
}