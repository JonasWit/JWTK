using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models.UsersManagement;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Repositories.General;
using SystemyWP.API.Services.Auth;
using SystemyWP.API.Services.Logging;

namespace SystemyWP.API.Controllers.Users
{
    [Route("api/[controller]")]
    public class AuthController : ApiControllerBase
    {
        private readonly Encryptor _encryptor;
        private readonly IUserRepository _userRepository;

        public AuthController(
            Encryptor encryptor,
            IUserRepository userRepository,
            PortalLogger portalLogger, 
            AppDbContext context, 
            IMapper mapper) : base(portalLogger, context, mapper)
        {
            _encryptor = encryptor;
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
                Password = _encryptor.Encrypt(userCredentialsForm.Password),
                Email = userCredentialsForm.Email
            };

            _userRepository.CreateUser(newUser);
            await _userRepository.SaveChanges();
            return Ok();
        }
       


 
    }
}