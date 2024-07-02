using Craftify.Domain.Constants;
using Craftify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetUserByEmail(string email);
        User? GetUserById(Guid Id);
        bool VerifyPassword(string PasswordHash, string providedPassword);
        string HashPassword(string providedPassword);
        bool IsPasswordResetTokenValid(string email, string token);
        string GeneratePasswordResetToken(string email);
        void Update(User user);
        string GenerateOTP(string email);
        bool IsOTPValid(string email, string otp);
        void Detach(User user);
        public bool ChangeUserRole(User user, string role = AppConstants.Role_Customer);
        public void Subscribe(Subscription subscription, Worker worker);

        public string GenerateRefreshToken(string email);
        public bool IsRefreshTokenValid(string email, string token);
    }
}
