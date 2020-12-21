using System;

namespace OkulSinavi
{
    public partial class admin_AyarlarRoot : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!Master.Yetki().Contains("Admin"))
                    Response.Redirect("/Yonetim/Giris.aspx");
                try
                {
                    AyarlarDb veriDb = new AyarlarDb();
                    AyarlarInfo info = veriDb.KayitBilgiGetir(1);
                    txtSiteAdi.Text = info.SiteAdi;

                    txtEpostaServer.Text = info.EpostaServer;
                    txtEpostaGonderenAdres.Text = info.EpostaGonderenAdres;
                    //txtEpostaReply.Text = info.EpostaReply;
                    txtEpostaPass.Text = info.EpostaPass;
                    txtEpostaGonderenIsmi.Text = info.EpostaGonderenIsmi;
                    txtEpostaSSL.Text = info.EpostaSsl.ToString();
                    txtEpostaPort.Text = info.EpostaPort.ToString();
                    txtEpostaSiteAdres.Text = info.EpostaSiteAdres;
                    txtEpostaAliciAdres.Text = info.EpostaAliciAdres;

                    txtAdres.Text = info.SiteAdres;
               
                }
                catch (Exception ex)
                {
                    string uyariMesaji = "Hata : " + ex.Message;
                    Master.UyariKirmizi(uyariMesaji, phUyari);
                }

            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {

            try
            {
                AyarlarDb veriDb = new AyarlarDb();
                AyarlarInfo info = new AyarlarInfo
                {
                    SiteAdi = txtSiteAdi.Text,
                    SiteAdres = txtAdres.Text,
                    EpostaServer = txtEpostaServer.Text,
                    EpostaGonderenAdres = txtEpostaGonderenAdres.Text,
                    EpostaReply = "",
                    EpostaPass = txtEpostaPass.Text,
                    EpostaGonderenIsmi = txtEpostaGonderenIsmi.Text,
                    EpostaSsl = txtEpostaSSL.Text.ToInt32(),
                    EpostaPort = txtEpostaPort.Text.ToInt32(),
                    EpostaSiteAdres = txtEpostaSiteAdres.Text,
                    EpostaAliciAdres = txtEpostaAliciAdres.Text,
                    Id = 1
                };

                veriDb.KayitGuncelle(info);
                Master.UyariIslemTamam("Site ayarları kaydedildi.", phUyari);
            }
            catch (Exception ex)
            {
                string uyariMesaji = "Hata : " + ex.Message;
                Master.UyariKirmizi(uyariMesaji, phUyari);
            }
        }
   
        protected void btnMailTest_Click(object sender, EventArgs e)
        {
            try
            {
                string epostaAliciAdres = txtEpostaAliciAdres.Text;
                string epostaGonderenIsmi = txtEpostaGonderenIsmi.Text;
                string epostaGonderenAdres = txtEpostaGonderenAdres.Text;
                string epostaServer = txtEpostaServer.Text;
                string epostaSiteAdres = txtEpostaSiteAdres.Text;
                string epostaSiteEmailSifre = txtEpostaPass.Text;
                int epostaPort = txtEpostaPort.Text.ToInt32();

                Eposta myEposta = new Eposta
                {
                    Konu = string.Format("{0} | Mail Test", epostaGonderenIsmi),
                    Mesaj = "<b>Test maili gönderildi</b>"
                };
                myEposta.AddTo(epostaAliciAdres);
                myEposta.Gonder(epostaServer, epostaPort, epostaSiteAdres, epostaSiteEmailSifre, epostaGonderenAdres, epostaGonderenIsmi);

                Master.UyariIslemTamam("Mail gönderildi", phUyari);

            }
            catch (Exception ex)
            {
                Master.UyariKirmizi("Hata : " + ex.Message, phUyari);
            }
        }

    }
}