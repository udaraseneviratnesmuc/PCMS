using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Web.Configuration;

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
            smtpClient.Host = WebConfigurationManager.AppSettings["smtpHost"];
            smtpClient.EnableSsl = true;

            NetworkCredential NetworkCred = new NetworkCredential(WebConfigurationManager.AppSettings["smtpUsername"], WebConfigurationManager.AppSettings["smtpPassword"]);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = NetworkCred;
            smtpClient.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["smtpPort"]);
            smtpClient.Send(mailMsg);
        }
    }
}