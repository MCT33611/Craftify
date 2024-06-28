using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Plan.Commands.CreatePlan
{
    public class CreatePlanCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<CreatePlanCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            Domain.Entities.Plan plan = new()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Price  = request.Price,
                Duration = request.Duration,
            };
            _unitOfWork.Plan.Add(plan);
            _unitOfWork.Save();
            return plan.Id;
        }
    }
}
