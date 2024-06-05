using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Service.Common;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Queries.GetAllService
{
    public class GetAllServiceQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetAllProfileQuery, ErrorOr<IEnumerable<ServiceResult>>>
    {

        public async Task<ErrorOr<IEnumerable<ServiceResult>>> Handle(GetAllProfileQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            try
            {
                var services = _unitOfWork.Service.GetAll().ToList();
                var serviceResults = services.Select(service => new ServiceResult
                (
                    // Map Service entity properties to ServiceResult properties
                    service.Id,
                    service.ProviderId,
                    service.Title,
                    service.Description,
                    service.CategoryId,
                    service.Price,
                    service.Availability,
                    service.ZipCode
                ));

                return serviceResults.ToList();
            }
            catch (Exception)
            {
                // Return an ErrorOr with the exception
                return Error.NotFound();
            }
        }
    }
}

