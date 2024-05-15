using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Infrastructure.Authentication;
using Craftify.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Infrastructure.Presistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Craftify.Infrastructure.Presistence.Repositories;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Infrastructure.Presistence.Repository;
using Microsoft.AspNetCore.Identity;
using Craftify.Domain.Entities;
using Craftify.Domain.Constants;
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

            services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AppConstants.Role_Admin, policy =>
                {
                    policy.RequireRole(AppConstants.Role_Admin);
                });

                options.AddPolicy(AppConstants.Role_Customer, policy =>
                {
                    policy.RequireRole(AppConstants.Role_Customer);
                });

                options.AddPolicy(AppConstants.Role_Admin, policy =>
                {
                    policy.RequireRole(AppConstants.Role_Customer);
                });

            });
            return services;
        }

    }
}
