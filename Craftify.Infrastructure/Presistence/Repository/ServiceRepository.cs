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

        public void UploadPicatures(string[] picturesUrls, Guid serviceId)
        {
            var oldPictures = _db.ServicePictures.Where(pic => pic.ServiceId == serviceId).ToArray();
            _db.ServicePictures.RemoveRange(oldPictures);
            var newPictures = new List<ServicePictures>();

            foreach (var url in picturesUrls)
            {
                ServicePictures newPic = new()
                {
                    ServiceId = serviceId,
                    PictureUrl = url
                };

                newPictures.Add(newPic);
            }

            _db.ServicePictures.AddRange(newPictures);

        }
    }
}
