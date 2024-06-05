using Craftify.Application.Service.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Queries.GetAllService
{
    public record GetAllProfileQuery() : IRequest<ErrorOr<IEnumerable<ServiceResult>>>;
}
