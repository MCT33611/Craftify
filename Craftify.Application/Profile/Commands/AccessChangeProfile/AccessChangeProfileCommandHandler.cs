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
using Craftify.Application.Common.Interfaces.Service;

namespace Craftify.Application.Profile.Commands.AccessChangeProfile
{
    public class AccessChangeProfileCommandHandler(
        IUnitOfWork _unitOfWrok
        ) : IRequestHandler<AccessChangeProfileCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(AccessChangeProfileCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var user = _unitOfWrok.User.GetUserById(request.Id);
            if (user == null)
                return Errors.User.InvaildCredetial;

            user.Blocked = !user.Blocked;

            _unitOfWrok.User.Update(user);
            await _unitOfWrok.Save();
            return Unit.Value;

        }
    }
}
