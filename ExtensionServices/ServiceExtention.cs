using CodePulse.API.Models.DTO;
using CodePulse.API.Models.DTO.Configurations;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using CodePulse.API.Services;
using CodePulse.API.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System;

namespace CodePulse.API.ExtensionServices
{
    public static class ServiceExtention
    {
        public static void ServiceConfigure(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IEmailSender, EmailSender>();
        }
        
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void SerilogConfigure(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                           .WriteTo.Console()
                           .WriteTo.File("logs/MyLog.log", rollingInterval: RollingInterval.Day)
                           .MinimumLevel.Debug()
                           .CreateLogger();
        }

        // configuration for the password
        public static void IdentityConfigure(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                
                // User Lockout

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 3;
            }
            );
        }
       
        public static void IOptionConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfiguration>(options => configuration.GetSection("EmailConfiguration").Bind(options));
        }
    }
}
