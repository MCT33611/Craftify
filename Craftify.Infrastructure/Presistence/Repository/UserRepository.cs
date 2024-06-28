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
        CraftifyDbContext _db,
        IPasswordHasher<object> _passwordHasher
        ) :Repository<User>(_db), IUserRepository
    {




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
            DateTime expiry = DateTime.Now.AddMinutes(5);
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
            var storedOTP = _db.Authentications.SingleOrDefault(o => o.Email == email);

            if (storedOTP != null)
            {
                if (otp == storedOTP.OTP)
                {
                    _db.Authentications.Remove(storedOTP);
                    _db.SaveChanges();


                    if (storedOTP.ExpireAt > DateTime.Now)
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



        public async Task<Domain.Entities.Authentication?> GetByTokenAsync(string token)
        {
            return await _db.Authentications
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task AddAsync(Domain.Entities.Authentication refreshToken)
        {
            await _db.Authentications.AddAsync(refreshToken);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Authentication refreshToken)
        {
            _db.Authentications.Update(refreshToken);
            await _db.SaveChangesAsync();
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
