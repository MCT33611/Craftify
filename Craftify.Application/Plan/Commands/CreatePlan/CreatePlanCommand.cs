using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Plan.Commands.CreatePlan
{
    public record CreatePlanCommand(
        Guid Id,

        string Title,

        string? Description,

        decimal? Price,
        int Duration
        ) : IRequest<ErrorOr<Guid>>;
}
