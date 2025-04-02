using System.Net.Mail;


namespace PD_FOOD.Domain.Intercface
{
    public interface ISmtpSender
    {
        Task SendMailAsync(MailMessage message);
    }
}
