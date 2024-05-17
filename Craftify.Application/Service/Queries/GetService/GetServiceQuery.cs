using Craftify.Application.Service.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Queries.GetService
{
    public record GetServiceQuery(
        Guid? Id
        ) : IRequest<ErrorOr<ServiceResult>>;
}
