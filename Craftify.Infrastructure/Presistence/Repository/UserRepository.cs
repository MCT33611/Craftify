using CloudinaryDotNet.Actions;
using Craftify.Application.Authentication.Commands.Register;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Constants;
using Craftify.Domain.Entities;
using Craftify.Infrastructure.Persistence.Repository;
using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Craftify.Infrastructure.Presistence.Repositories
{
    public class UserRepository(
        CraftifyDbContext db,
        IPasswordHasher<object> _passwordHasher
        ) :Repository<User>(db), IUserRepository
    {


        private readonly CraftifyDbContext _db = db;

        public void Update( User user)
        {
            _db.Users.Update( user );
        }

        public void Subscribe(Subscription subscription,Worker worker)
        {
            _db.Workers.Add(worker);
            _db.Subscriptions.Add(subscription);
        }

        public bool ChangeUserRole(User user,string role = AppConstants.Role_Customer)
        {
            if (role != AppConstants.Role_Customer && role != AppConstants.Role_Worker) return false;
            if(_db.Users.FirstOrDefault(u => u.Id == user.Id) == null) return false;
            user.Role = role;
            _db.Users.Update(user);
            return true;
        }

        public User? GetUserByEmail(string email)
        {
            var user = _db.Users.FirstOrDefault(user => user.Email == email);
            return user;
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


        public string GeneratePasswordResetToken(string email)
        {
            // Generate a random token
            string token = GenerateRandomToken();

            // Set the expiry date to be, for example, 24 hours from now
            DateTime expiry = DateTime.UtcNow.AddHours(24);

            // Store the token along with the email address and expiry date
            _db.Authentications.Add(new()
            {
                Email = email,
                PasswordResetTokenExpireAt = expiry,
                PasswordResetToken = token,
                AuthType = Domain.Enums.AuthType.PasswordResetting
            });
            return token;
        }
        public bool IsPasswordResetTokenValid(string email, string token)
        {
            // Find the authentication record in the database based on the email and token
            var authentication = _db.Authentications
                .SingleOrDefault(a => a.Email == email && a.PasswordResetToken == token);

            // If authentication record is found
            if (authentication != null)
            {
                // Check if the token has not expired
                if (authentication.OTPExpireAt > DateTime.UtcNow)
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
            DateTime expiry = DateTime.Now.AddMinutes(5);
            _db.Authentications.Add(new()
            {
                Email = email,
                OTPExpireAt = expiry,
                OTP = otp,
                AuthType = Domain.Enums.AuthType.AuthEmailConfrimation

            });

            return otp;
        }
        public bool IsOTPValid(string email, string otp)
        {
            var storedOTP = _db.Authentications.SingleOrDefault(o => o.Email == email);

            if (storedOTP != null)
            {
                if (otp == storedOTP.OTP)
                {
                    _db.Authentications.Remove(storedOTP);


                    if (storedOTP.OTPExpireAt > DateTime.Now)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public string GenerateRefreshToken(string email)
        {
            string token = GenerateRandomToken();

            DateTime expiry = DateTime.Now.AddMonths(1);

            _db.Authentications.Add(new()
            {
                Email = email,
                RefreshTokenExpiryDate = expiry,
                RefreshToken = token,
                AuthType = Domain.Enums.AuthType.RefreshTokenManagement
            });
            return token;
        }
        public bool IsRefreshTokenValid(string email, string token)
        {
            var authentication = _db.Authentications
                .SingleOrDefault(a => a.Email == email && a.RefreshToken == token);

            if (authentication != null)
            {
                if (authentication.RefreshTokenExpiryDate > DateTime.Now)
                {
                    _db.Authentications.Remove(authentication);
                    return true;
                }
                else
                {
                    _db.Authentications.Remove(authentication);
                }
            }

            // Token is not valid
            return false;
        }



        public void Detach(User user)
        {
            var entry = _db.Entry(user);
            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }
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
        private static string GenerateRandomOTP()
        {
            const int otpLength = 4; // Length of OTP
            const string digits = "0123456789"; // Characters to choose from
            var random = new Random();
            var otp = new string(Enumerable.Repeat(digits, otpLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return otp;
        }



    }
}
