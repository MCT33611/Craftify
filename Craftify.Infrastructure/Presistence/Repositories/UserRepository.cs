using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;

namespace Craftify.Infrastructure.Presistence.Repositories
{
    public class UserRepository(CraftifyDbContext _db) : IUserRepository
    {
        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges(); 
        }

        public User? GetUserByEmail(string email)
        {
            return _db.Users.SingleOrDefault(user => user.Email == email);
        }
    }
}
