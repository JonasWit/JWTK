using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using SystemyWP.API.Constants;
using SystemyWP.API.Utilities;

namespace SystemyWP.API.Services.Email
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

        public Task<Response> SendClientInvite(string email, string link)
        {
            SendGridMessage msg = MailHelper.CreateSingleEmail(
                new EmailAddress(AppConstants.Emails.System),
                new EmailAddress(email),
                "systemywp.pl - Zaproszenie"
                , "",
                EmailTemplates.InvitationEmailBody(link)
            );

            return _client.SendEmailAsync(msg);
        }

        public Task<Response> SendEmailAsync(string email, string subject, string content)
        {
            SendGridMessage msg = MailHelper.CreateSingleEmail(
                new EmailAddress(AppConstants.Emails.System),
                new EmailAddress(email),
                subject
                , "",
                content
            );

            return _client.SendEmailAsync(msg);
        }
    }
}
