﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Logging;
using SystemyWP.API.Services.Storage;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace SystemyWP.API.Controllers.Access
{
    [Route("api/users")]
    [Authorize]
    public class UserController : ApiController
    {
        public UserController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            try
            {
                await _portalLogger.Log(LogType.Access, HttpContext.Request.Path.Value, UserId, UserEmail, "Logging in");
                
                var user = await _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefaultAsync(x => x.Id.Equals(UserId));

                if (user is not null)
                {
                    user.LastLogin = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                    return Ok(UserProjections
                        .LegalAppUserProjection(Role, LegalAppAllowed)
                        .Compile()
                        .Invoke(user));
                }

                var newUser = new User
                {
                    Username = Username,
                    Email = UserEmail,
                    Id = UserId,
                    LastLogin = DateTime.UtcNow,
                    CreatedBy = UserEmail
                };

                _context.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(UserProjections
                    .LegalAppUserProjection(Role, LegalAppAllowed)
                    .Compile()
                    .Invoke(newUser));
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPut("personal-data/update")]
        public async Task<IActionResult> UpdatePersonalData([FromBody] UserPersonalDataForm form)
        {
            var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(UserId));
            if (userProfile is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

            try
            {
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
                userProfile.Krs = form.Krs;
                userProfile.Nip = form.Nip;
                userProfile.Regon = form.Regon;

                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpDelete("personal-data/clear/{userId}")]
        public async Task<IActionResult> DeletePersonalData(string userId)
        {
            var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(UserId));
            if (userProfile is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

            try
            {
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
                userProfile.Krs = null;
                userProfile.Nip = null;
                userProfile.Regon = null;

                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPut("me/image")]
        public async Task<IActionResult> UpdateProfileImage(IFormFile image, [FromServices] IFileProvider fileManager)
        {
            try
            {
                if (image is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(UserId));
                if (user is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                if (!string.IsNullOrEmpty(user.Image)) await fileManager.DeleteFileAsync(user.Image);

                await using (var stream = new MemoryStream())
                using (var imageProcessor = await Image.LoadAsync(image.OpenReadStream()))
                {
                    imageProcessor.Mutate(x => x.Resize(120, 120));
                    var processImage = imageProcessor.SaveAsync(stream, new JpegEncoder());
                    var saveImage = fileManager.SaveProfileImageAsync(stream);
                    await Task.WhenAll(processImage, saveImage);
                    user.Image = await saveImage;
                }

                await _context.SaveChangesAsync();
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