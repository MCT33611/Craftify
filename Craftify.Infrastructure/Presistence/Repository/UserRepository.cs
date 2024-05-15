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
    }
}
