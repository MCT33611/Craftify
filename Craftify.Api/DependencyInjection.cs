using Craftify.Api.Filters;
using Craftify.Api.Mapping;
using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Infrastructure.Authentication;
using Microsoft.OpenApi.Models;

namespace Craftify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                });

            });
            services.AddCors(options =>
            {


                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddMappings();
            return services;
        }
    }
}
