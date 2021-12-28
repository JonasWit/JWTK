using System;
using System.Threading.Tasks;
using AutoMapper;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Portal;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Services.Logging;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Enums;

namespace SystemyWP.API.Controllers.Portal
{
    [Route("/api/[controller]")]
    public class ContactController : ApiControllerBase
    {
        public ContactController(PortalLogger portalLogger, AppDbContext context, IMapper mapper) : base(portalLogger, context, mapper)
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
                await NewLog(LogType.Exception, e);
                return ServerError;
            }
        }
        
        // [HttpPost("lapp/support-request")]
        // [Authorize(SystemyWpConstants.Policies.User)]
        // public async Task<IActionResult> LegaAppSupportRequest(
        //     [FromServices] EmailClient emailClient)
        // {
        //     try
        //     {
        //         // await emailClient.SendEmailAsync(
        //         //     SystemyWpConstants.Emails.SupportAddress,
        //         //     $"Support Request - Legal App |{UserEmail}| {form.Subject}",
        //         //     form.Body);
        //         
        //         return Ok();
        //     }
        //     catch (Exception e)
        //     {
        //         await NewLog(LogType.Exception, e);
        //         return ServerError;
        //     }
        // }


    }
}