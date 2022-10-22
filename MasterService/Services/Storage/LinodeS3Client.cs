using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

namespace MasterService.API.Services.Storage
{
    class LinodeS3Client : IS3Client
    {
        private readonly S3Settings _settings;

        public LinodeS3Client(IOptionsMonitor<S3Settings> optionsMonitor)
        {
            _settings = optionsMonitor.CurrentValue;
        }

        public async Task<string> SaveFile(string fileName, string mime, Stream fileStream)
        {
            using var client = Client;
            var request = new PutObjectRequest
            {
                BucketName = _settings.Bucket,
                Key = $"{_settings.Root}/{fileName}",
                ContentType = mime,
                InputStream = fileStream,
                CannedACL = S3CannedACL.PublicRead
            };

            await client.PutObjectAsync(request);
            return ObjectUrl(fileName);
        }

        public async Task<string> DeleteFile(string fileUrl)
        {
            using var client = Client;

            var request = new DeleteObjectRequest
            {
                BucketName = _settings.Bucket,
                Key = ObjectRootUrl(Path.GetFileName(fileUrl)),
            };
            var result = await client.DeleteObjectAsync(request);
            return result.RequestCharged;
        }

        private string ObjectUrl(string fileName) =>
            $"{_settings.ServiceUrl}/{_settings.Bucket}/{_settings.Root}/{fileName}";

        private string ObjectRootUrl(string fileName) =>
            $"{_settings.Root}/{fileName}";

        private AmazonS3Client Client => new(
            _settings.AccessKey, _settings.SecretKey,
            new AmazonS3Config
            {
                ServiceURL = _settings.ServiceUrl,
            });
    }
}