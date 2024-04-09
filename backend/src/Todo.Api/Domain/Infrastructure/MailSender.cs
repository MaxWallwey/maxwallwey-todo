using System.Net;
using System.Net.Mail;

namespace Todo.Api.Domain.Infrastructure;

public class MailSender : IMailSender
{
    public void SendEmail(string recipientAddress, string subject, string body)
    { 
        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("max.wallwey@gmail.com", "kguvlqwzxxkdfglm");
            using (var message = new MailMessage(
                       from: new MailAddress("max.wallwey@gmail.com", "TODO"),
                       to: new MailAddress(recipientAddress)
                   ))
            {
                message.Subject = subject;
                message.Body = body;
                
                smtpClient.Send(message);
            }
        }
    }
}