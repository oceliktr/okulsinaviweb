using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc.Common.Mail
{
    /// <summary>
    /// Mail gönderme işlemlerinin yönetildiği yer.
    /// </summary>
    public class MailManager
    {
        public enum AktivasyonTurleri
        {
            YeniSifreTalepBaglantisiGonder=0,
            EpostaOnaylamaMailiGonder=1
        }

        public MailResult TestMaili(string eposta, SiteAyar info)
        {

            string konu = "Test e-Postası";
            string mesaj =
                $@"<table style='border-collapse: collapse;border: 1px solid #C0C0C0;' cellpadding=3 cellspacing=3 width=617>
                     <tr><td><p>Test e-postası gönderildi.</p>
                     </td></tr></table>";

            MailHelper myEposta = new MailHelper();
            myEposta.AddTo(eposta);
            return myEposta.Gonder(konu, mesaj, info);
        }
        public MailResult SendMailtoAdmin(string msg, string stackTrace, string url, SiteAyar info)
        {
           
            string konu = "Hata oluştu";
            string mailMesaj = $@"<table style='border-collapse: collapse;border: 1px solid #C0C0C0;' cellpadding=3 cellspacing=3>
                                <tr><td>
                                <p>İyi günler;</p><p><font face=Verdana size=2>ÖDM web adresinde bir hata oluştu.<br><br><b>Hata Mesajı:</b> {msg} <br><br><b>Hata Sayfası:</b> {url} <br><br><b>Stack Trace:</b> {stackTrace.Replace("konum", "<br>konum").Replace(" at", "<br>")}<br><br></font></p>
                             </td></tr></table>";
            MailHelper myEposta = new MailHelper();
            myEposta.AddTo(info.SiteMail);
            return myEposta.Gonder(konu, mailMesaj, info);
        }
   }

}
