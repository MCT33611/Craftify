using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Craftify.Infrastructure.Authentication
{
    public class JwtTokenGenerator(
        IDateTimeProvider _dateTimeProvider,
        IOptions<JwtSettings> jwtOptions
        ) : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;


        public string GenerateToken(User user,Guid?workerId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret!));
            var siginingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new (JwtRegisteredClaimNames.GivenName,user.FirstName),
                new (JwtRegisteredClaimNames.FamilyName,user.LastName ?? ""),
                new (JwtRegisteredClaimNames.Email,user.Email),
                new (ClaimTypes.Role,user.Role),
                new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            if (workerId.HasValue && workerId != null)
            {
                claims.Add(new Claim("WorkerId", workerId.ToString()!));
            }

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                //expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                expires: _dateTimeProvider.UtcNow.AddSeconds(3),
                claims: claims,
                signingCredentials: siginingCredentials);
                            
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


    }
}
