using System;
using System.Linq;
using System.Reflection;
using SystemyWP.API.CustomAttributes;
using SystemyWP.API.Localization;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Services.Logging;
using SystemyWP.API.Settings;
using SystemyWP.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SystemyWP.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private const string NuxtJsApp = "NuxtJsApp";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("Default")));

            AddIdentity(services);

            services.AddControllers()
                .AddFluentValidation(x =>
                    x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));

            services.AddRazorPages();
            
            services.Configure<SendGridOptions>(_configuration.GetSection(nameof(SendGridOptions)));
            services.Configure<CorsSettings>(_configuration.GetSection(nameof(CorsSettings)));

            AddMarkedServices(services);

            services.AddScoped<EmailClient>();
            services.AddFileServices(_configuration);

            services.AddCors(options => options.AddPolicy(NuxtJsApp, build => build
                .AllowAnyHeader()
                .WithOrigins(_configuration.GetValue("CorsSettings:PortalUrl", ""))
                .AllowAnyMethod()
                .AllowCredentials()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                var serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
                loggerFactory.AddProvider(new AppLoggerProvider(
                    serviceProvider.GetRequiredService<AppDbContext>()));
                app.UseExceptionHandler("/Error");
            }

            app.UseCookiePolicy(
                new CookiePolicyOptions
                {
                    Secure = CookieSecurePolicy.Always,
                    MinimumSameSitePolicy = SameSiteMode.Strict
                });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(NuxtJsApp);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }

        private void AddMarkedServices(IServiceCollection services)
        {
            var transientServiceType = typeof(TransientService);
            var scopedServiceType = typeof(ScopedService);
            var singletonServiceType = typeof(SingletonService);

            var appDefinedTransientTypes = transientServiceType.Assembly.DefinedTypes.ToList();
            var appDefinedScopedTypes = scopedServiceType.Assembly.DefinedTypes.ToList();
            var appDefinedSingletonTypes = singletonServiceType.Assembly.DefinedTypes.ToList();

            var transientServices = appDefinedTransientTypes
                .Where(typeInfo => typeInfo.GetTypeInfo().GetCustomAttribute<TransientService>() != null);
            var scopedServices = appDefinedScopedTypes
                .Where(typeInfo => typeInfo.GetTypeInfo().GetCustomAttribute<ScopedService>() != null);
            var singletonServices = appDefinedSingletonTypes
                .Where(typeInfo => typeInfo.GetTypeInfo().GetCustomAttribute<SingletonService>() != null);

            foreach (var service in transientServices)
                services.AddTransient(service);
            foreach (var service in scopedServices)
                services.AddScoped(service);
            foreach (var service in singletonServices)
                services.AddSingleton(service);
        }

        private void AddIdentity(IServiceCollection services)
        {
            services.AddDbContext<ApiIdentityDbContext>(config =>
                config.UseNpgsql(_configuration.GetConnectionString("Default")));

            services.AddDataProtection()
                .SetApplicationName("Systemywp")
                .UseCryptographicAlgorithms(
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA512
                    })
                .PersistKeysToDbContext<ApiIdentityDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.User.AllowedUserNameCharacters = "ąęźżćłśĄĘĆŚŹŻŚŁabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                    options.User.RequireUniqueEmail = true;

                    if (_env.IsDevelopment())
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequiredLength = 4;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                        options.Lockout.MaxFailedAccessAttempts = 2;
                        options.Lockout.AllowedForNewUsers = true;
                    }
                    else
                    {
                        options.Password.RequireDigit = true;
                        options.Password.RequiredLength = 16;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                        options.Lockout.MaxFailedAccessAttempts = 3;
                        options.Lockout.AllowedForNewUsers = true;
                    }
                })
                .AddErrorDescriber<PolishIdentityErrorDescriber>()
                .AddEntityFrameworkStores<ApiIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(48));

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(10);
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/Login";
                config.LogoutPath = "/api/auth/logout";
                config.Cookie.Domain = _configuration["CookieDomain"];
                config.Cookie.Name = "systemywp_id";
                config.ExpireTimeSpan = TimeSpan.FromHours(5);
                config.Cookie.SameSite = SameSiteMode.Strict;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(SystemyWpConstants.Policies.User, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(SystemyWpConstants.Claims.Role,
                        SystemyWpConstants.Roles.User,
                        SystemyWpConstants.Roles.PortalAdmin,
                        SystemyWpConstants.Roles.UserAdmin));

                options.AddPolicy(SystemyWpConstants.Policies.UserAdmin, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(SystemyWpConstants.Claims.Role,
                        SystemyWpConstants.Roles.PortalAdmin,
                        SystemyWpConstants.Roles.UserAdmin));

                options.AddPolicy(SystemyWpConstants.Policies.PortalAdmin, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(SystemyWpConstants.Claims.Role,
                        SystemyWpConstants.Roles.PortalAdmin));

                options.AddPolicy(SystemyWpConstants.Policies.LegalAppAccess, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(SystemyWpConstants.Claims.AppAccess));
            });
        }
    }
}