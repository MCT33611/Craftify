using Craftify.Domain.Entities;

namespace Craftify.Application.Common.Interfaces.Persistence
{
    public interface IServiceRepository : IRepository<Domain.Entities.Service>
    {
        void Update(Domain.Entities.Service user);
        void UploadPicatures(string[] picturesUrls, Guid serviceId);
    }
}
