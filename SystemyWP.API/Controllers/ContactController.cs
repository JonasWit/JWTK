using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Data;
using SystemyWP.API.Forms.Conact;
using SystemyWP.API.Services.Email;

namespace SystemyWP.API.Controllers
{
    [Route("[controller]")]
    public class ContactController : ApiControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactController(
            AppDbContext context, 
            IMapper mapper)
        {
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
                return ServerError;
            }
        }
    }
}