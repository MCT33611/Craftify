using Craftify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetUserByEmail(string email);
        User? GetUserById(Guid Id);
        bool VerifyPassword(string PasswordHash, string providedPassword);
        string HashPassword(string providedPassword);
        void Update(User user);
    }
}
