using PD_FOOD.Domain.Dtos;
using PD_FOOD.Domain.Interfaces;
using PD_FOOD.Infrastructure.Configurations;
using System.Reflection;


namespace PD_FOOD.Infrastructure.Email.Base
{
    public abstract class EmailSenderBase : IEmailSender
    {
        protected readonly EmailSettingsConfiguration _emailSettings;

        public EmailSenderBase(EmailSettingsConfiguration emailSettings)
        {
            _emailSettings = emailSettings;
        }

        protected string GetTemplateEmail()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resources = assembly.GetManifestResourceNames();
            var targetResource = Array.Find(resources, s => s.EndsWith("template_email.html"));
            Stream resourceStream = assembly.GetManifestResourceStream(targetResource ?? "")!;
            StreamReader reader = new StreamReader(resourceStream);
            return reader.ReadToEnd();
        }

        protected string ReplaceVariables(string templateEmail)
        {
            return templateEmail.Replace("%negativeValue%", "500");
        }

        public abstract Task<bool> SendEmailAsync(List<SendEmailDto> emails);
    }

}
