using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Commands.DeleteService
{
    public class DeleteServiceCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<DeleteServiceCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if(_unitOfWork.Service.Get(s => s.Id == command.Id) is not Domain.Entities.Service service)
            {
                return Errors.User.InvaildCredetial;
            }
            _unitOfWork.Service.Remove(service);
            _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
