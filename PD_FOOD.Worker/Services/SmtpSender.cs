using PD_FOOD.Domain.Intercface;
using System.Net.Mail;

namespace PD_FOOD.Worker.Services
{
    public class SmtpSender : ISmtpSender
    {
        public Task SendMailAsync(MailMessage message)
        {
            using var client = new SmtpClient("localhost");
            return client.SendMailAsync(message);
        }
    }
}
