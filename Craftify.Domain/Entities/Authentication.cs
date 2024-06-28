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
        public DateTime ExpireAt { get; set; } = DateTime.Now;
        public string? Email { get; set; }
        public string? OTP { get; set; }
        public string? ResetToken { get; set; }
        public string? Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }

    }
}
