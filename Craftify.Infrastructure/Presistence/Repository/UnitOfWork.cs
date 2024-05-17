using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Craftify.Infrastructure.Presistence.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Infrastructure.Presistence.Repository
{
    public class UnitOfWork(
        CraftifyDbContext _db,
        IPasswordHasher<object> _passwordHasher
        ) : IUnitOfWork
    {

        public IUserRepository User { get; } = new UserRepository(_db,_passwordHasher);

        public IServiceRepository Service => new ServiceRepository(_db);

        public ICategoryRepository Category => new CategoryRepository(_db);

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
