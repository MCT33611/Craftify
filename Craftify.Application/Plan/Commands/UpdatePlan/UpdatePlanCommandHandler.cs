using Craftify.Application.Plan.Commands.UpdatePlan;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using Craftify.Application.Common.Interfaces.Service;

namespace Craftify.Application.Plan.Commands.UpdatePlan
{
    public class UpdatePlanCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<UpdatePlanCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var Plan = _unitOfWork.Plan.Get(c => c.Id == request.Id);
            if (Plan != null)
            {
                Plan.Title = request.Title ?? Plan.Title;
                Plan.Description = request.Description ?? Plan.Description;
                Plan.Price = request.Price ?? Plan.Price;
                Plan.Duration = request.Duration;

                _unitOfWork.Plan.Update(Plan);
                _unitOfWork.Save();
            }
            else
            {
                return Errors.User.InvaildCredetial;
            }
            return Unit.Value;
        }
    }
}
