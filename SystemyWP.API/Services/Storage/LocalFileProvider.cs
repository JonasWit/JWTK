using System.IO;
using System.Threading.Tasks;
using Systemywp.Api.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Systemywp.Api.Services.Storage
{
    class LocalFileProvider : IFileProvider
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly FileSettings _settings;

        public LocalFileProvider(
            IWebHostEnvironment webHostEnvironment,
            IOptionsMonitor<FileSettings> fileSettingsMonitor)
        {
            _webHostEnvironment = webHostEnvironment;
            _settings = fileSettingsMonitor.CurrentValue;
        }
        
        public async Task<string> SaveProfileImageAsync(Stream fileStream)
        {
            var fileName = SystemyWpConstants.Files.GenerateProfileFileName();
            await SaveFile(fileStream, fileName);
            return $"{_settings.ImageUrl}/{fileName}";
        }

        public Task<string> DeleteProfileImageAsync(string filePath)
        {
            throw new System.NotImplementedException();
        }

        private async Task SaveFile(Stream fileStream, string fileName)
        {
            var savePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
            await using var stream = File.Create(savePath);
            await fileStream.CopyToAsync(stream);
        }
    }
}