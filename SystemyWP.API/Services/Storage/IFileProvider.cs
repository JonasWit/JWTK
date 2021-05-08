using System.IO;
using System.Threading.Tasks;

namespace Systemywp.Api.Services.Storage
{
    public interface IFileProvider
    {
        public Task<string> SaveProfileImageAsync(Stream fileStream);
        public Task<string> DeleteProfileImageAsync(string filePath);
    }
}