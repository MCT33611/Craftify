using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Commands.CreateService
{
    public record CreateServiceCommand(
        string Title,
        string Description,
        Guid   CategoryId,
        decimal? Price,
        bool Availability,
        string[] NewPicUrls,
        string ZipCode) : IRequest<ErrorOr<Guid>>;
}
