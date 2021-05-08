using System;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Portal;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Mvc;

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
                await _portalLogger
                    .Log(LogType.PersonalData, HttpContext.Request.Path.Value, UserId, UserEmail,
                        $"Contact requested by {contactForm.Name} - {contactForm.Email}");

                await emailClient.SendEmailAsync(
                    contactForm.Email,
                    "Contact Request",
                    $"Name: {contactForm.Name}, Email: {contactForm.Email}, Phone: {contactForm.Phone}");
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.PersonalData, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                Console.WriteLine(e);
            }

            return Ok();
        }
    }
}