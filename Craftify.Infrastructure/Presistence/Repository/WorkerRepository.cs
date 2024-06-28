using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Craftify.Infrastructure.Persistence.Repository;

namespace Craftify.Infrastructure.Presistence.Repository
{
    public class WorkerRepository(
        CraftifyDbContext _db
        ) : Repository<Worker>(_db), IWorkerRepository
    {
        public void Update(Worker Worker)
        {
           _db.Workers.Update(Worker);
        }
    }
}
