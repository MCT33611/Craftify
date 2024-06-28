using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Craftify.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Infrastructure.Presistence.Repository
{
    public class PlanRepository(
        CraftifyDbContext _db
        ) : Repository<Plan>(_db), IPlanRepository
    {
        public void Update(Plan plan)
        {
           _db.Plans.Update(plan);
        }
    }
}
