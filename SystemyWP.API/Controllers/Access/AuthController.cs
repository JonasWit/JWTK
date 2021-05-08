﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Systemywp.Api.Controllers.BaseClases;
using Systemywp.Api.Services.Logging;
using Systemywp.Data;
using Systemywp.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Systemywp.Api.Controllers.Access
{
    [Route("api/auth")]
    [Authorize]
    public class AuthController : ApiController
    {
        public AuthController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout(
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IWebHostEnvironment env)
        {
            await signInManager.SignOutAsync();
            return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "/");
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IWebHostEnvironment env,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                await signInManager.SignOutAsync();
                var user = await userManager.FindByIdAsync(UserId);
                await userManager.UpdateSecurityStampAsync(user);

                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(UserId));

                if (userProfile is not null) _context.Remove(userProfile);

                await _context.SaveChangesAsync();
                return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "/");
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "/");
            }
        }
    }
}