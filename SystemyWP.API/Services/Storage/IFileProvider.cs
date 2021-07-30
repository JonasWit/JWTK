using System.IO;
using System.Threading.Tasks;

namespace SystemyWP.API.Services.Storage
{
    public interface IFileProvider
    {
        public Task<string> SavePortalPublicationImageAsync(Stream fileStream);
        public Task<string> SaveProfileImageAsync(Stream fileStream);
        public Task<string> DeleteFileAsync(string filePath);
    }
}