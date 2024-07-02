using Craftify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Domain.Entities
{
    public class Authentication
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Email { get; set; }
        public AuthType AuthType { get; set; } 

        public DateTime OTPExpireAt { get; set; } = DateTime.Now;
        public string? OTP { get; set; }

        public string? PasswordResetToken { get; set; }
        public DateTime PasswordResetTokenExpireAt { get; set; } = DateTime.Now;

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryDate { get; set; }
        public bool IsRevoked { get; set; }

    }
}
