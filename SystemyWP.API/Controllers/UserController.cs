using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Storage;
using SystemyWP.Data;
using SystemyWP.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace SystemyWP.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = UserId;
            if (string.IsNullOrEmpty(userId)) return BadRequest();

            var user = await _appDbContext.Users
                .Where(x => x.Id.Equals(userId))
                .Select(UserProjections.UserProjection(Role, DataAccessKey, AllowedApps))
                .FirstOrDefaultAsync();

            if (user is not null) return Ok(user);

            var newUser = new User
            {
                Id = UserId,
                Username = Username,
            };

            _appDbContext.Add(newUser);
            await _appDbContext.SaveChangesAsync();

            return Ok(UserProjections.UserProjection(Role, DataAccessKey, AllowedApps).Compile().Invoke(newUser));
        }

        [HttpPut("me/image")]
        public async Task<IActionResult> UpdateProfileImage(
            IFormFile image,
            [FromServices] IFileProvider fileManager)
        {
            if (image is null) return BadRequest();

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(UserId));
            if (user is null) return NoContent();

            await using (var stream = new MemoryStream())
            // using (var imageProcessor = await Image.LoadAsync(image.OpenReadStream()))
            // {
            //     imageProcessor.Mutate(x => x.Resize(48, 48));
            //     var processImage = imageProcessor.SaveAsync(stream, new JpegEncoder());
            //     var saveImage = fileManager.SaveProfileImageAsync(stream);
            //     await Task.WhenAll(processImage, saveImage);
            //     user.Image = await saveImage;
            // }

            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}