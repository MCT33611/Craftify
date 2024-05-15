using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; } = null!;
        public string? StreetAdderss { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        public DateTime JoinDate { get; set; }

        public string? ProfilePicture { get; set; }

        public string Role { get; set; } = null!;


    }
}
