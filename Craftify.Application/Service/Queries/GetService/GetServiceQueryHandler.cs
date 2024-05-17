using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Service.Common;
using Craftify.Domain.Common.Errors;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Queries.GetService
{
    public class GetServiceQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetServiceQuery, ErrorOr<ServiceResult>>
    {

        public async Task<ErrorOr<ServiceResult>> Handle(GetServiceQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (query.Id == null)
            {
                return Errors.User.InvaildCredetial;
            }

            var service = _unitOfWork.Service.Get(s => s.Id == query.Id);
            return new ServiceResult(
                service.Id,
                service.ProviderId,
                service.Title,
                service.Description,
                service.CategoryId,
                service.Price,
                service.Availability,
                service.ZipCode
                );
        }
    }
}

