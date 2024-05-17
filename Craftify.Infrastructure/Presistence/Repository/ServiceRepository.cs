using Craftify.Application.Authentication.Commands.Register;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Craftify.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace Craftify.Infrastructure.Presistence.Repositories
{
    public class ServiceRepository(
        CraftifyDbContext _db
        ) :Repository<Service>(_db), IServiceRepository
    {


        public void Update(Service user)
        {
            _db.Services.Update(user);
        }
    }
}
