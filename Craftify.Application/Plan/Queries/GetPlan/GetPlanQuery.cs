using Craftify.Application.Plan.Common;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Plan.Queries.GetPlan
{
    public record GetPlanQuery(
        Guid Id
        ) : IRequest<ErrorOr<PlanResult>>;
}
