using Craftify.Application.Authentication.Commands.Register;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;
using Craftify.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

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
            _db.SaveChanges();
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

        public string GenerateOTP(string email)
        {
            // Generate a random OTP
            string otp = GenerateRandomOTP();

            // Store the OTP along with the email address (and expiry date if needed)
            // You can decide whether to store OTP in the database or not
            DateTime expiry = DateTime.UtcNow.AddMinutes(1);
            _db.Authentications.Add(new()
            {
                Email = email,
                ExpireAt = expiry,
                OTP = otp
            });
            _db.SaveChanges();

            return otp;
        }
        public bool IsOTPValid(string email, string otp)
        {
            // Find the stored OTP in the database based on the email address
            // You can decide whether to store OTP in the database or not

            // For example, if you're storing OTP in the database
            var storedOTP = _db.Authentications.SingleOrDefault(o => o.Email == email);

            if (storedOTP != null)
            {
                // Check if the provided OTP matches the stored OTP
                if (otp == storedOTP.OTP)
                {
                    // Optionally, check for expiry if needed
                    if (storedOTP.ExpireAt > DateTime.UtcNow)
                    {
                        // OTP is valid
                        // Optionally, remove the OTP from the database if it's single-use
                        _db.Authentications.Remove(storedOTP);
                        _db.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    // Incorrect OTP
                    return false;
                }
            }

            // OTP is not valid
            return false;
        }

        private static string GenerateRandomOTP()
        {
            const int otpLength = 6; // Length of OTP
            const string digits = "0123456789"; // Characters to choose from
            var random = new Random();
            var otp = new string(Enumerable.Repeat(digits, otpLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return otp;
        }
        private static string GenerateRandomToken()
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
