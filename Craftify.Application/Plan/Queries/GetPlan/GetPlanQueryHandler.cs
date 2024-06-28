using Craftify.Application.Plan.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Plan.Queries.GetPlan
{
    public class GetPlanQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetPlanQuery, ErrorOr<PlanResult>>
    {

        public async Task<ErrorOr<PlanResult>> Handle(GetPlanQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var Plan = _unitOfWork.Plan.Get(s => s.Id == query.Id);
            return new PlanResult(
                Plan.Id,
                Plan.Title,
                Plan.Description,
                Plan.Price,
                Plan.Duration
                );
        }
    }
}

