using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Dtos.General;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Repositories.General;
using SystemyWP.API.Services.Auth;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Controllers.Users
{
    [Route("api/[controller]")]
    public class AuthController : ApiControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(
            IUserRepository userRepository,
            PortalLogger portalLogger, 
            AppDbContext context, 
            IMapper mapper) : base(portalLogger, context, mapper)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserCredentialsForm userCredentialsForm)
        {
            var emailAddressExists = _context.Users.FirstOrDefault(u => u.Email == userCredentialsForm.Email);
            if (emailAddressExists is not null) return NotFound();

            var newUser = new User()
            {
                Id = new Guid().ToString(),
                Password = Encryptor.Encrypt(userCredentialsForm.Password),
                Email = userCredentialsForm.Email
            };

            _userRepository.CreateUser(newUser);
            await _userRepository.SaveChanges();
            return Ok();
        }
       


 
    }
}