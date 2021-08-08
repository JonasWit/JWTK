using System;
using System.IO;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Portal;
using SystemyWP.API.Services.Logging;
using SystemyWP.API.Services.Storage;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace SystemyWP.API.Controllers.Portal
{
    [Route("/api/portal-news")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class NewsController : ApiController
    {
        public NewsController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddNews(
            [FromBody] PublicationForm publicationForm,
            IFormFile image, 
            [FromServices] IFileProvider fileManager)
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
                    imageProcessor.Mutate(x => x.Resize(480, 480));
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