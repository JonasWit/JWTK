// ReSharper disable once CheckNamespace

using System;
using SystemyWP.API;
using SystemyWP.API.Services.Storage;
using SystemyWP.API.Settings;
using Microsoft.Extensions.Configuration;
using SystemyWP.API.Constants;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterService
    {
        public static IServiceCollection AddFileServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var settingsSection = configuration.GetSection(nameof(FileSettings));
            var settings = settingsSection.Get<FileSettings>();
            services.Configure<FileSettings>(settingsSection);
            
            services.AddSingleton<TemporaryFileStorage>();
            if (settings.Provider.Equals(AppConstants.Files.Providers.Local))
            {
                services.AddSingleton<IFileProvider, LocalFileProvider>();
            }
            else if (settings.Provider.Equals(AppConstants.Files.Providers.S3))
            {
                services.Configure<S3Settings>(configuration.GetSection(nameof(S3Settings)));
                services.AddSingleton<IFileProvider, S3FileProvider>();
                services.AddSingleton<IS3Client, LinodeS3Client>();
            }
            else
            {
                throw new Exception($"Invalid file manager provider: {settings.Provider}");
            }

            return services;
        }
    }
}