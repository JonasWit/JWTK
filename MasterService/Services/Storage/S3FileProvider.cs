using System.IO;
using System.Threading.Tasks;
using MasterService.API.Constants;

namespace MasterService.API.Services.Storage
{
    class S3FileProvider : IFileProvider
    {
        private readonly IS3Client _s3Client;

        public S3FileProvider(IS3Client s3Client)
        {
            _s3Client = s3Client;
        }

        public Task<string> SavePortalPublicationImageAsync(Stream fileStream)
        {
            var fileName = AppConstants.Files.GeneratePortalPublicationFileName();
            return _s3Client.SaveFile(fileName, "image/jpg", fileStream);
        }

        public Task<string> SaveProfileImageAsync(Stream fileStream)
        {
            var fileName = AppConstants.Files.GenerateProfileFileName();
            return _s3Client.SaveFile(fileName, "image/jpg", fileStream);
        }

        public Task<string> DeleteFileAsync(string filePath) => 
            _s3Client.DeleteFile(filePath);
    }
}