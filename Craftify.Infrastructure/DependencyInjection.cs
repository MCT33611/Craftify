using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Infrastructure.Authentication;
using Craftify.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Craftify.Infrastructure.Presistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Infrastructure.Presistence.Repository;
using Microsoft.AspNetCore.Identity;
using Craftify.Domain.Constants;
using Microsoft.AspNetCore.Identity.UI.Services;
namespace Craftify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager _config
            )
        {
            services.AddAuth(_config);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddDbContext<CraftifyDbContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmailSender, EmailSender>();

            services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();

            services.AddScoped<ICloudinaryService, CloudinaryService>(provider =>
            {
                // Retrieve Cloudinary configuration from appsettings.json
                var cloudinarySettings = _config.GetSection(CloudinarySettings.SectionName).Get<CloudinarySettings>();
                return new CloudinaryService(
                    cloudinarySettings!.CloudName,
                    cloudinarySettings!.ApiKey,
                    cloudinarySettings!.ApiSecret
                    );
            });
            return services;
        }

        public static IServiceCollection AddAuth(
           this IServiceCollection services,
           ConfigurationManager _config
           )
        {
            var jwtSettings = new JwtSettings();
            _config.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            var googleSettings = new GoogleSettings();
            _config.Bind(GoogleSettings.SectionName, googleSettings);
            services.AddSingleton(Options.Create(googleSettings));
            services.AddSingleton<IGooglePayloadGenerator, GooglePayloadGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret!))
                    };
                });
            services.AddAuthorizationBuilder()
                .AddPolicy(AppConstants.Role_Admin, policy =>
                {
                    policy.RequireRole(AppConstants.Role_Admin);
                })
                .AddPolicy(AppConstants.Role_Customer, policy =>
                {
                    policy.RequireRole(AppConstants.Role_Customer);
                })
                .AddPolicy(AppConstants.Role_Admin, policy =>
                {
                    policy.RequireRole(AppConstants.Role_Customer);
                });
            return services;
        }

    }
}
