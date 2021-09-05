using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Portal;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Forms.LegalApp.Support;

namespace SystemyWP.API.Controllers.Portal
{
    [Route("/api/portal/contact")]
    public class ContactController : ApiController
    {
        public ContactController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestContact(
            [FromBody] ContactForm contactForm,
            [FromServices] EmailClient emailClient)
        {
            try
            {
                await emailClient.SendEmailAsync(
                    SystemyWpConstants.Emails.ContactAddress,
                    "Contact Request",
                    $"Name: {contactForm.Name}, Email: {contactForm.Email}, Phone: {contactForm.Phone}");
                
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        [HttpPost("lapp/support-request")]
        [Authorize(SystemyWpConstants.Policies.User)]
        public async Task<IActionResult> LegaAppSupportRequest(
            [FromBody] LegalAppSupportRequestForm form,
            [FromServices] EmailClient emailClient)
        {
            try
            {
                await emailClient.SendEmailAsync(
                    SystemyWpConstants.Emails.SupportAddress,
                    $"Support Request - Legal App |{Username}|{UserEmail}| {form.Subject}",
                    form.Body);
                
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
    }
}