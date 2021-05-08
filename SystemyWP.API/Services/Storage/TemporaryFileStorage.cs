﻿using System.IO;
using Systemywp.Api.Settings;
using Microsoft.Extensions.Options;

namespace Systemywp.Api.Services.Storage
{
    public class TemporaryFileStorage
    {
        private readonly FileSettings _settings;

        public TemporaryFileStorage(IOptionsMonitor<FileSettings> optionsMonitor)
        {
            _settings = optionsMonitor.CurrentValue;
        }

        public bool TemporaryFileExists(string fileName)
        {
            var path = GetSavePath(fileName);
            return File.Exists(path);
        }

        public void DeleteTemporaryFile(string fileName)
        {
            var path = GetSavePath(fileName);
            if (File.Exists(path)) File.Delete(path);
        }

        public string GetSavePath(string fileName)
        {
            return Path.Combine(_settings.WorkingDirectory, fileName);
        }
    }
}