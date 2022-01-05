using System;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Services.Logging;
using SystemyWP.API.Settings;
using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Middleware;
using SystemyWP.API.Repositories.General;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SystemyWP.API.Data;
using SystemyWP.API.Services.Auth;

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
            services.AddOptions();
            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(_configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(_configuration.GetSection("IpRateLimitPolicies"));
            services.AddInMemoryRateLimiting();

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(_configuration.GetConnectionString("Default")));
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            AddIdentity(services);

            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));
            
            services.Configure<SendGridOptions>(_configuration.GetSection(nameof(SendGridOptions)));
            services.Configure<CorsSettings>(_configuration.GetSection(nameof(CorsSettings)));
            services.Configure<AuthSettings>(_configuration.GetSection(nameof(AuthSettings)));
            
            services.AddScoped<EmailClient>();
            services.AddScoped<PortalLogger>();
            services.AddTransient<Encryptor>();
            services.AddTransient<JwtManager>();
            
            services.AddFileServices(_configuration);

            services.AddCors(options => options.AddPolicy(NuxtJsApp, build => build
                .AllowAnyHeader()
                .WithOrigins(_configuration.GetValue("CorsSettings:PortalUrl", ""))
                .AllowAnyMethod()
                .AllowCredentials()));

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
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

            app.UseCustomResponseHeaders();
            app.UseCors(NuxtJsApp);
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIpRateLimiting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            
            DBManager.PrepareDatabase(app);
        }
        
        private void AddIdentity(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.RequireHttpsMetadata = true;
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue("AuthSettings:SecretKey", ""))),
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                };
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy(SystemyWpConstants.Policies.User, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(SystemyWpConstants.Claims.Role,
                        SystemyWpConstants.Roles.User,
                        SystemyWpConstants.Roles.Admin));

                options.AddPolicy(SystemyWpConstants.Policies.Admin, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(SystemyWpConstants.Claims.Role,
                        SystemyWpConstants.Roles.Admin));
            });
        }
    }
}