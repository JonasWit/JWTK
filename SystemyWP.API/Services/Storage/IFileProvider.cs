using System.IO;
using System.Threading.Tasks;

namespace SystemyWP.API.Services.Storage
{
    public interface IFileProvider
    {
        public Task<string> SaveProfileImageAsync(Stream fileStream);
    }
}