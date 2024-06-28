using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Plan.Commands.UpdatePlan
{
    public record UpdatePlanCommand(
        Guid Id,

        string Title,

        string? Description,

        decimal? Price,

        int Duration

        ) : IRequest<ErrorOr<Unit>>;
}
