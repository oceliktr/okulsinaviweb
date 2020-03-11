using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;


    public class Eposta
    {
        string _konu;
        string _mesaj;
        MailAddressCollection _to;
        MailAddressCollection _cc;
        MailAddressCollection _bcc;

        private MailAddressCollection To
        {
            get { return _to; }
            set { _to = value; }
        }

        private MailAddressCollection Bcc
        {
            get { return _bcc; }
            set { _bcc = value; }
        }

        private MailAddressCollection Cc
        {
            get { return _cc; }
            set { _cc = value; }
        }

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

        public string Mesaj
        {
            get { return _mesaj; }
            set { _mesaj = value; }
        }

        public string Konu
        {
            get { return _konu; }
            set { _konu = value; }
        }

        public void Gonder(string serverAdresi,int portNo,string kullaniciMail,string mailSifresi,string epostaGonderenAdres, string epostaGonderenIsmi)
        {
            try
            {
                MailMessage msg = new MailMessage
                {
                    BodyEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true,
                    Subject = Konu,
                    Body = Mesaj,
                    From = new MailAddress(epostaGonderenAdres, epostaGonderenIsmi)
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

                var smtp = new SmtpClient(serverAdresi, portNo);
                var nc = new NetworkCredential(kullaniciMail, mailSifresi);
                smtp.Credentials = (ICredentialsByHost)nc.GetCredential(serverAdresi, portNo, "Basic");
                // AuthTypes: "Basic", "NTLM", "Digest", "Kerberos", "Negotiate"

                smtp.Send(msg);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
