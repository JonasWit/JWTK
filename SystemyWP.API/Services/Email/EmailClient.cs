using System.Threading.Tasks;
using SystemyWP.API.Utilities;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SystemyWP.API.Services.Email
{
    public class EmailClient
    {
        private readonly IOptionsMonitor<SendGridOptions> _optionsMonitor;
        private readonly SendGridClient _client;

        public EmailClient(IOptionsMonitor<SendGridOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
            _client = new SendGridClient(_optionsMonitor.CurrentValue.ApiKey);
        }

        public Task<Response> SendClientInvite(string email, string link)
        {
            var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(_optionsMonitor.CurrentValue.From),
                new EmailAddress(email),
                "systemywp.pl - Zaproszenie"
                , "",
                EmailTemplates.InvitationEmailBody(link)
            );

            return _client.SendEmailAsync(msg);
        }
        
        public Task<Response> SendEmailAsync(string email, string subject, string content)
        {
            var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(_optionsMonitor.CurrentValue.From),
                new EmailAddress(email),
                subject
                , "",
                content
            );

            return _client.SendEmailAsync(msg);
        }
    }
}