using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Commands.UpdateService
{
    public record UpdateServiceCommand(
        Guid Id,
        string Title,
        string Description,
        Guid CategoryId,
        decimal? Price,
        bool Availability,
        string ZipCode
        ): IRequest<ErrorOr<Unit>>;
}
