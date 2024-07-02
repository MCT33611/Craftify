using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Common.Errors;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Plan.Commands.DeletePlan
{
    public class DeletePlanCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<DeletePlanCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(DeletePlanCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (_unitOfWork.Plan.Get(p => p.Id == command.Id) is not Domain.Entities.Plan plan)
            {
                return Errors.User.InvaildCredetial;
            }
            _unitOfWork.Plan.Remove(plan);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
