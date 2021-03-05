using System;
using SystemyWP.API.Localization;
using SystemyWP.API.Services.Email;
using SystemyWP.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SystemyWP.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private const string NuxtJsApp = "NuxtJsApp";

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Dev"));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(_configuration.GetConnectionString("Default")));
            }

            AddIdentity(services);

            services.AddControllers()
                .AddFluentValidation(x =>
                    x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));

            services.AddRazorPages();
            services.Configure<SendGridOptions>(_configuration.GetSection(nameof(SendGridOptions)));

            services.AddScoped<EmailClient>();
            services.AddFileServices(_configuration);   
            services.AddCors(options => options.AddPolicy(NuxtJsApp, build => build
                    .AllowAnyHeader()
                    .WithOrigins("https://localhost:3000", "https://portal.systemywp.pl")
                    .AllowAnyMethod()
                    .AllowCredentials()));          
 
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (_env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(NuxtJsApp);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
        
        private void AddIdentity(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddDbContext<ApiIdentityDbContext>(config =>
                    config.UseInMemoryDatabase("DevIdentity"));
            }
            else
            {
                services.AddDbContext<ApiIdentityDbContext>(config =>
                    config.UseNpgsql(_configuration.GetConnectionString("Default")));
            }
            
            // services.AddDataProtection()
            //     .SetApplicationName("SystemyWspomaganiaPracy")
            //     .PersistKeysToDbContext<ApiIdentityDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;

                    if (_env.IsDevelopment())
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequiredLength = 4;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                        options.Lockout.MaxFailedAccessAttempts = 3;
                        options.Lockout.AllowedForNewUsers = true;
                    }
                    else
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequiredLength = 8;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                        options.Lockout.MaxFailedAccessAttempts = 3;
                        options.Lockout.AllowedForNewUsers = true;
                    }
                })
                .AddErrorDescriber<PolishIdentityErrorDescriber>()
                .AddEntityFrameworkStores<ApiIdentityDbContext>()
                .AddDefaultTokenProviders();
            
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
                config.ExpireTimeSpan = TimeSpan.FromHours(2);      
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(SystemyWPConstants.Policies.Client, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(SystemyWPConstants.Claims.Role,
                        SystemyWPConstants.Roles.Client, 
                        SystemyWPConstants.Roles.PortalAdmin, 
                        SystemyWPConstants.Roles.ClientAdmin));
                
                options.AddPolicy(SystemyWPConstants.Policies.ClientAdmin, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(SystemyWPConstants.Claims.Role,
                        SystemyWPConstants.Roles.PortalAdmin, 
                        SystemyWPConstants.Roles.ClientAdmin));

                options.AddPolicy(SystemyWPConstants.Policies.PortalAdmin, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(SystemyWPConstants.Claims.Role,
                        SystemyWPConstants.Roles.PortalAdmin));

                options.AddPolicy(SystemyWPConstants.Policies.LegalAppAccess, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(SystemyWPConstants.Claims.LegalAppAccess));
            });
        }
    }
}