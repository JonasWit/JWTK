﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions;
using SystemyWP.API.Forms;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.API.Services.Storage;
using SystemyWP.Data;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UserController : ApiController
    {
        public UserController(PortalLogger portalLogger) : base(portalLogger)
        {
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe(
            [FromServices] AppDbContext context)
        {
            try
            {
                var testExc = new Exception("TEST exception");
                testExc.SetStackTrace(new StackTrace());
                await _portalLogger.Log(LogType.Access, HttpContext.Request.Path.Value, UserId, UserEmail, "Loggin in");
                await _portalLogger.Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, "Test Exception",testExc );
                
                var userId = UserId;
                if (string.IsNullOrEmpty(userId)) return BadRequest();

                var user = await context.Users
                    .FirstOrDefaultAsync(x => x.Id.Equals(UserId));

                if (user is not null)
                {
                    user.LastLogin = DateTime.UtcNow;
                    user.AccessKey = context.AccessKeys
                        .Include(x => x.Users)
                        .FirstOrDefault(x => x.Users.Any(y => y.Id.Equals(user.Id)));

                    await context.SaveChangesAsync();
                    return Ok(UserProjections
                        .UserProjection(Username, Role, LegalAppAllowed)
                        .Compile()
                        .Invoke(user));
                }

                var newUser = new User
                {
                    Id = UserId,
                    LastLogin = DateTime.UtcNow
                };

                context.Add(newUser);
                await context.SaveChangesAsync();
                
                return Ok(UserProjections
                    .UserProjection(Username, Role, LegalAppAllowed)
                    .Compile()
                    .Invoke(newUser));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpPut("personal-data/update")]
        public async Task<IActionResult> UpdatePersonalData(
            [FromServices] AppDbContext context,
            [FromBody] UserPersonalDataForm form)
        {
            var userProfile = context.Users.FirstOrDefault(x => x.Id.Equals(UserId));
            if (userProfile is null)
            {
                return BadRequest("Nie znaleziono profilu użytkownika. Skontaktuj się z administratorem!");
            }

            userProfile.Address = form.Address;
            userProfile.City = form.City;
            userProfile.Country = form.Country;
            userProfile.Name = form.Name;
            userProfile.Surname = form.Surname;
            userProfile.Vivodership = form.Vivodership;
            userProfile.AddressCorrespondence = form.AddressCorrespondence;
            userProfile.PhoneNumber = form.PhoneNumber;
            userProfile.PostCode = form.PostCode;
            userProfile.CompanyFullName = form.CompanyFullName;
            userProfile.KRS = form.KRS;
            userProfile.NIP = form.NIP;
            userProfile.REGON = form.REGON;

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("personal-data/clear/{userId}")]
        public async Task<IActionResult> DeletePersonalData(string userId,
            [FromServices] AppDbContext context)
        {
            var userProfile = context.Users.FirstOrDefault(x => x.Id.Equals(UserId));
            if (userProfile is null)
            {
                return BadRequest("Nie znaleziono profilu użytkownika. Skontaktuj się z administratorem!");
            }

            userProfile.Address = null;
            userProfile.City = null;
            userProfile.Country = null;
            userProfile.Name = null;
            userProfile.Surname = null;
            userProfile.Vivodership = null;
            userProfile.AddressCorrespondence = null;
            userProfile.PhoneNumber = null;
            userProfile.PostCode = null;
            userProfile.CompanyFullName = null;
            userProfile.KRS = null;
            userProfile.NIP = null;
            userProfile.REGON = null;

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("me/image")]
        public async Task<IActionResult> UpdateProfileImage(
            [FromServices] AppDbContext context,
            IFormFile image,
            [FromServices] IFileProvider fileManager)
        {
            if (image is null) return BadRequest();

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id.Equals(UserId));
            if (user is null) return NoContent();

            if (!string.IsNullOrEmpty(user.Image))
            {
                await fileManager.DeleteProfileImageAsync(user.Image);
            }

            await using (var stream = new MemoryStream())
            using (var imageProcessor = await Image.LoadAsync(image.OpenReadStream()))
            {
                imageProcessor.Mutate(x => x.Resize(120, 120));
                var processImage = imageProcessor.SaveAsync(stream, new JpegEncoder());
                var saveImage = fileManager.SaveProfileImageAsync(stream);
                await Task.WhenAll(processImage, saveImage);
                user.Image = await saveImage;
            }

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("personal-data/delete-account")]
        public IActionResult DeleteAccount()
        {



            return Ok();
        }
    }
}