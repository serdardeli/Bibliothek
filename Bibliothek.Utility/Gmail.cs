using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.Utility
{
    public class Gmail
    {
        public static void SendMail(string body)
        {
            var fromAddress = new MailAddress("mail@SerdarDeli.com", "İletişim Formu");
            var toAddress = new MailAddress("SerdarDeli@gmail.com");

            //var fromAddress = new MailAddress("SerdarDeli@gmail.com", "İletişim Formu");

            const string subject = "İletişim Formu";
            using (var smtp = new SmtpClient
            {
                Host = "mail.SerdarDeli.com",

                //Host = "smtp.gmail.com",

                Port = 25, //SerdarDeli.com

                /*Port = 587,*/ //gmail

                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "[Like@No@Other]4")      
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}
