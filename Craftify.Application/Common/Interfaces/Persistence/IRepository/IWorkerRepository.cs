using Craftify.Domain.Entities;

namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        void Update(Worker Worker);
    }
}
