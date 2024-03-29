﻿using System.IO;
using System.Threading.Tasks;

namespace SystemyWP.API.Services.Storage
{
    public interface IS3Client
    {
        Task<string> SaveFile(string fileName, string mime, Stream fileStream);
        Task<string> DeleteFile(string fileUrl);
    }
}