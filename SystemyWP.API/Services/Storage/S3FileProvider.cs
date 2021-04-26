using System.IO;
using System.Threading.Tasks;

namespace SystemyWP.API.Services.Storage
{
    class S3FileProvider : IFileProvider
    {
        private readonly IS3Client _s3Client;

        public S3FileProvider(IS3Client s3Client)
        {
            _s3Client = s3Client;
        }
        
        public Task<string> SaveProfileImageAsync(Stream fileStream)
        {
            var fileName = SystemyWpConstants.Files.GenerateProfileFileName();
            return _s3Client.SaveFile(fileName, "image/jpg", fileStream);
        }

        public Task<string> DeleteProfileImageAsync(string filePath)
        {
            return _s3Client.DeleteFile(filePath);
        }
    }
}