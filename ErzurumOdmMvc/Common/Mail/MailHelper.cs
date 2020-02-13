using ErzurumOdmMvc.Entities;
using System;
using System.Net;
using System.Net.Mail;

namespace ErzurumOdmMvc.Common.Mail
{
    public class MailHelper
    {
        private MailAddressCollection To { get; set; }

        private MailAddressCollection Bcc { get; set; }

        private MailAddressCollection Cc { get; set; }

        public void AddTo(string eposta)
        {
            if (To == null)
                To = new MailAddressCollection();
            To.Add(eposta);
        }

        public void AddBcc(string eposta)
        {
            if (Bcc == null)
                Bcc = new MailAddressCollection();
            Bcc.Add(eposta);
        }

        public void AddCc(string eposta)
        {
            if (Cc == null)
                Cc = new MailAddressCollection();
            Cc.Add(eposta);
        }

        public string Mesaj { get; set; }

        public string Konu { get; set; }

        public MailResult Gonder(string konu, string mesaj, SiteAyar ayrInfo)
        {
            MailResult result = new MailResult { Sonuc = false };
            try
            {
                MailMessage msg = new MailMessage
                {
                    BodyEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true,
                    Subject = konu,
                    Body = mesaj,
                    From = new MailAddress(ayrInfo.MailAdresi, ayrInfo.MailGonderenIsim)
                };

                //msg.ReplyTo = new MailAddress(strReplyTo);

                if (To != null)
                    foreach (var mail in To)
                    {
                        msg.To.Add(mail);
                    }

                if (Cc != null)
                    foreach (var mail in Cc)
                    {
                        msg.CC.Add(mail);
                    }

                if (Bcc != null)
                    foreach (var mail in Bcc)
                    {
                        msg.Bcc.Add(mail);
                    }

                var smtp = new SmtpClient(ayrInfo.MailServer, ayrInfo.MailPort);
                var nc = new NetworkCredential(ayrInfo.MailAdresi, ayrInfo.MailSifresi);
                smtp.Credentials = (ICredentialsByHost)nc.GetCredential(ayrInfo.MailServer, ayrInfo.MailPort, "Basic");
                // AuthTypes: "Basic", "NTLM", "Digest", "Kerberos", "Negotiate"
                smtp.EnableSsl = true;
                smtp.Send(msg);

                result.Mesaj = "E-posta başarıyla gönderildi.";
                result.Sonuc = true;
            }
            catch (Exception ex)
            {
                result.Mesaj = ex.Message+" "+ex.InnerException.Message;
            }

            return result;
        }
    }
}