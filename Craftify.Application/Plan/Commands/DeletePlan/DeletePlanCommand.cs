using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Plan.Commands.DeletePlan
{
    public record DeletePlanCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
