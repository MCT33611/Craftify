using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Domain.Entities;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Craftify.Infrastructure.Authentication
{
    public class GooglePayloadGenerator(
        IOptions<GoogleSettings> googleOptions
        ) : IGooglePayloadGenerator
    {
        private readonly GoogleSettings _googleSettings = googleOptions.Value;


        public async Task<Payload> GeneratePayload( string Credential)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = [_googleSettings.GoogleClientId]
            };
            Payload payload = await ValidateAsync(Credential, settings);
            return payload;

        }
    }
}
