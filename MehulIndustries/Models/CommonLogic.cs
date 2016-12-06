using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MehulIndustries.Models
{
    public class CommonLogic
    {
        public static bool SendMail(string to, string body, string subject)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]),
                    Password = Convert.ToString(ConfigurationManager.AppSettings["MailPassword"])
                };
                smtp.Credentials = credential;
                smtp.Host = Convert.ToString(ConfigurationManager.AppSettings["MailHost"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]);
                smtp.Timeout = 200000;
                try
                {
                    smtp.Send(message);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}