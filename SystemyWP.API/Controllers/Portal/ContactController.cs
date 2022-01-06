using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Services.Logging;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Enums;
using SystemyWP.API.Forms.Conact;

namespace SystemyWP.API.Controllers.Portal
{
    [Route("/api/[controller]")]
    public class ContactController : ApiControllerBase
    {
        private readonly PortalLogger _portalLogger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactController(
            PortalLogger portalLogger, 
            AppDbContext context, 
            IMapper mapper)
        {
            _portalLogger = portalLogger;
            _context = context;
            _mapper = mapper;
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
                await _portalLogger.Log(HttpContext.Request.Path.Value, UserId, LogType.Exception, e);
                return ServerError;
            }
        }
    }
}