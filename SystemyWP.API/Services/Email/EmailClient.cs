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
                "Systemywp.pl - Zaproszenie"
                , "",
                EmailTemplates.InvitationEmailBody(link)
            );

            return _client.SendEmailAsync(msg);
        }
        
        public Task<Response> SendEmailAsync(string email, string title, string content)
        {
            var htmlContent = content;
            var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(_optionsMonitor.CurrentValue.From),
                new EmailAddress(email),
                title
                , "",
                htmlContent
            );

            return _client.SendEmailAsync(msg);
        }
    }
}