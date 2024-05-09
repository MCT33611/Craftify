using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Craftify.Infrastructure.Authentication
{
    public class JwtTokenGenerator(
        IDateTimeProvider _dateTimeProvider,
        IOptions<JwtSettings> jwtOptions
        ) : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;


        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret!));
            var siginingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: siginingCredentials);
                            
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
