using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Infrastructure.Presistence
{
    public class UserRepository : IUserRepository
    {
        public static readonly List<User> Users = [];
        public void Add(User user)
        {
            Users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return Users.SingleOrDefault(user => user.Email == email);
        }
    }
}
