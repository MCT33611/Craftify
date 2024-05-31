using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;
using Craftify.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Profile.Commands.DeleteProfile
{
    public class DeleteProfileCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<DeleteProfileCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(DeleteProfileCommand command, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.User.GetUserById( command.Id );
            if (user == null)
                return Errors.User.InvaildCredetial;

            _unitOfWork.User.Remove(user);
            _unitOfWork.Save();
            await Task.CompletedTask;
            return Unit.Value;
        }
    }
}
