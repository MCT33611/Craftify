using Craftify.Application.Plan.Common;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Plan.Queries.GetAllPlan
{
    public record GetAllPlanQuery() : IRequest<ErrorOr<IEnumerable<PlanResult>>>;
}
