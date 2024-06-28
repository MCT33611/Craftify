using Craftify.Domain.Entities;

namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface IPlanRepository : IRepository<Domain.Entities.Plan>
    {
        void Update(Domain.Entities.Plan plan);
    }
}
