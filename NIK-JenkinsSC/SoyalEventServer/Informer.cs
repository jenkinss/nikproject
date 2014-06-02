using System.Net;
using System.Net.Mail;

namespace SoyalEventServer
{
    public class Informer
    {
        public static void SendMail(string subject, string body)
        {
            var fromAddress = new MailAddress("informer.bistech@gmail.com", "BIS Informator");
            var toAddressA = new MailAddress("nedbal.attila@gmail.com", "Attila");
            var toAddressP = new MailAddress("bundash.p@gmail.com", "Péter");

            const string fromPassword = "Ati_741852963";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddressA)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            using (var message = new MailMessage(fromAddress, toAddressP)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
