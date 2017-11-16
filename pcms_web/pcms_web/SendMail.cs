using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace pcms_web
{
    public class SendMail
    {
        public void sendViaGmail(string fromAddr, string toAddr, string subject, string body) 
        {
            MailMessage mailMsg = new MailMessage(fromAddr, toAddr);

            mailMsg.Subject = subject;
            mailMsg.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;

            NetworkCredential NetworkCred = new NetworkCredential("udara.seneviratne@hsenidmobile.com", "lifeisshort123");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = NetworkCred;
            smtpClient.Port = 587;
            smtpClient.Send(mailMsg);
        }
    }
}