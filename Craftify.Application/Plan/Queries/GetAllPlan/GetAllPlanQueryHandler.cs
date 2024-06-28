using Craftify.Application.Plan.Common;
using Craftify.Application.Plan.Queries.GetAllPlan;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Plan.Queries.GetAllPlan
{
    public class GetAllPlanQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetAllPlanQuery, ErrorOr<IEnumerable<PlanResult>>>
    {

        public async Task<ErrorOr<IEnumerable<PlanResult>>> Handle(GetAllPlanQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            try
            {
                var plans = _unitOfWork.Plan.GetAll().ToList();
                var PlanResults = plans.Select(Plan => new PlanResult
                (
                    Plan.Id,
                    Plan.Title,
                    Plan.Description,
                    Plan.Price,
                    Plan.Duration
                ));

                return PlanResults.ToList();
            }
            catch (Exception)
            {
                return Error.NotFound();
            }
        }
    }
}

