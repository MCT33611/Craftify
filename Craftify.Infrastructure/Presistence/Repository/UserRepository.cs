using Craftify.Application.Authentication.Commands.Register;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;
using Craftify.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Identity;

namespace Craftify.Infrastructure.Presistence.Repositories
{
    public class UserRepository(
        CraftifyDbContext _db,
        IPasswordHasher<object> _passwordHasher
        ) :Repository<User>(_db), IUserRepository
    {

        public void Update( User user)
        {
            _db.Users.Add( user );
        }

        public User? GetUserByEmail(string email)
        {
            return _db.Users.SingleOrDefault(user => user.Email == email);
        }

        public User? GetUserById(Guid Id)
        {
            return _db.Users.SingleOrDefault(user => user.Id == Id);
        }

        public string HashPassword(string providedPassword)
        {
            // Hash the password
            return _passwordHasher.HashPassword(null!, providedPassword);
             
        }

        public bool VerifyPassword(string PasswordHash, string providedPassword)
        {
            // Verify the provided password against the hashed password
            var result = _passwordHasher.VerifyHashedPassword(null!,PasswordHash, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        public string GenerateResetToken(string email)
        {
            // Generate a random token
            string token = GenerateRandomToken();

            // Set the expiry date to be, for example, 24 hours from now
            DateTime expiry = DateTime.UtcNow.AddHours(24);

            // Store the token along with the email address and expiry date
            _db.Authentications.Add(new()
            {
                Email = email,
                ExpireAt = expiry,
                ResetToken = token
            });

            return token;
        }

        // Check if the token is valid for the given email address
        public bool IsTokenValid(string email, string token)
        {
            // Find the authentication record in the database based on the email and token
            var authentication = _db.Authentications
                .SingleOrDefault(a => a.Email == email && a.ResetToken == token);

            // If authentication record is found
            if (authentication != null)
            {
                // Check if the token has not expired
                if (authentication.ExpireAt > DateTime.UtcNow)
                {
                    // Token has used, remove it from the database
                    _db.Authentications.Remove(authentication);
                    // Token is valid
                    return true;
                }
                else
                {
                    // Token has expired, remove it from the database
                    _db.Authentications.Remove(authentication);
                }
                _db.SaveChanges();
            }

            // Token is not valid
            return false;
        }

        private string GenerateRandomToken()
        {
            const int tokenLength = 32;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var token = new string(Enumerable.Repeat(chars, tokenLength)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return token;
        }
    }
}
