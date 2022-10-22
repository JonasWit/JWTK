using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using MasterService.API.Constants;

namespace MasterService.API.Services.Email
{
    public class EmailClient
    {
        private readonly IOptionsMonitor<EmailClientOptions> _optionsMonitor;
        private readonly SendGridClient _client;

        public EmailClient(IOptionsMonitor<EmailClientOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
            _client = new SendGridClient(_optionsMonitor.CurrentValue.ApiKey);
        }

        public Task<Response> SendPasswordResetAsync(string recipient, string url)
        {
            SendGridMessage msg = MailHelper.CreateSingleTemplateEmail(
                new EmailAddress(AppConstants.Emails.System),
                new EmailAddress(recipient),
                AppConstants.EmailTemplates.ResetPassword,
                new { url }
            );

            return _client.SendEmailAsync(msg);
        }

        public Task<Response> SendEmailConfirmationAsync(string recipient, string url)
        {
            SendGridMessage msg = MailHelper.CreateSingleTemplateEmail(
                new EmailAddress(AppConstants.Emails.System),
                new EmailAddress(recipient),
                AppConstants.EmailTemplates.ConfirmEmail,
                new { url }
            );

            return _client.SendEmailAsync(msg);
        }
    }
}
