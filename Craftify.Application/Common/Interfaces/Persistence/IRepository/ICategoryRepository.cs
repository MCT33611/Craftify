using Craftify.Domain.Entities;

namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface ICategoryRepository : IRepository<Domain.Entities.Category>
    {
        void Update(Domain.Entities.Category category);
    }
}
