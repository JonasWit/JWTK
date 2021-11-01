﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Logging;
using SystemyWP.API.Services.Storage;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Controllers.Users
{
    [Route("api/users")]
    [Authorize]
    public class UserController : ApiController
    {
        public UserController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("my-access-logs")]
        public async Task<IActionResult> GetAccessLogs(int cursor, int take)
        {
            try
            {
                var result =  _context.PortalLogs
                    .Where(pl => pl.UserId.Equals(UserId) && pl.LogType == LogType.Access)
                    .OrderByDescending(x => x.Created)
                    .Skip(cursor)
                    .Take(take)
                    .Select(PortalLogRecordProjections.StandardProjection)
                    .ToList();

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        [HttpDelete("my-access-logs/{logId}")]
        public async Task<IActionResult> DeleteAccessLog(long logId)
        {
            try
            {
                var result = _context.PortalLogs
                    .Where(pl => pl.UserId.Equals(UserId) && pl.LogType == LogType.Access)
                    .FirstOrDefault(pl => pl.Id == logId);

                if (result is not null)
                {
                    _context.Remove(result);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        [HttpGet("me")]
        public async Task<IActionResult> GetProfile([FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await _context.Users
                    .Include(x => x.LegalAccessKey)
                    .Include(x => x.MedicalAccessKey)
                    .Include(x => x.RestaurantAccessKey)
                    .FirstOrDefaultAsync(x => x.Id.Equals(UserId));
                
                if (user is not null)
                {
                    user.LastLogin = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    return Ok(UserProjections
                        .UserProfileProjection(Role, LegalAppAllowed, MedicalAppAllowed, RestaurantAppAllowed)
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
                    .UserProfileProjection(Role, LegalAppAllowed, MedicalAppAllowed, RestaurantAppAllowed)
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
            try
            {
                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(UserId));
                if (userProfile is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);
                
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
            try
            {
                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(UserId));
                if (userProfile is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);
                
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
                return Ok("Image deleted");
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        [HttpDelete("me/image")]
        public async Task<IActionResult> DeleteProfileImage([FromServices] IFileProvider fileManager)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(UserId));
                if (user is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                if (!string.IsNullOrEmpty(user.Image)) await fileManager.DeleteFileAsync(user.Image);
                user.Image = string.Empty;

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