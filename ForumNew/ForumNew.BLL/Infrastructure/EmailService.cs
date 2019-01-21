using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ForumNew.BLL.Infrastructure
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string from = "ForumTest123456789@gmail.com";
            string pass = "Q1w2e3r4t5y6";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(from, pass);
            client.EnableSsl = true;

            var mail = new MailMessage(from, message.Destination, message.Subject, message.Body);
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }
    }
}
