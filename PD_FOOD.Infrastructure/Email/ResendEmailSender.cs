using Microsoft.Extensions.Options;
using PD_FOOD.Domain.Dtos;
using PD_FOOD.Domain.Interfaces;
using PD_FOOD.Infrastructure.Configurations;
using PD_FOOD.Infrastructure.Email.Base;
using Resend;


namespace PD_FOOD.Infrastructure.Email
{
    public class ResendEmailSender : EmailSenderBase, IEmailSender
    {
        private readonly ResendClient _resendClient;

        public ResendEmailSender(
            IOptions<EmailSettingsConfiguration> settings,
            IOptionsSnapshot<ResendClientOptions> resendOptions,
            HttpClient httpClient)
            : base(settings.Value)
        {
            resendOptions.Value.ApiToken = settings.Value.ApiKey;
            _resendClient = new ResendClient(resendOptions, httpClient);
        }

        public override async Task<bool> SendEmailAsync(List<SendEmailDto> emails)
        {
            var template = ReplaceVariables(GetTemplateEmail());

            var addressList = new EmailAddressList();
            foreach (var r in emails)
            {
                addressList.Add(new EmailAddress
                {
                    Email = r.Email,
                    DisplayName = r.UserName
                });
            }

            var email = new EmailMessage
            {
                From = _emailSettings.DefaultFromAddress,
                To = addressList,
                Subject = "Negative Balance Alert",
                HtmlBody = template
            };

            var result = await _resendClient.EmailSendAsync(email);
            return result != null && result.Success;
        }

    }


}
