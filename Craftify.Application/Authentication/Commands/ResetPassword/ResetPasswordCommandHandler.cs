using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Craftify.Application.Authentication.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace Craftify.Application.Authentication.Commands.ResetPasswordCommand
{
    public class ResetPasswordCommandHandler(
        IUnitOfWork _unitOfWork
        ) : 
        IRequestHandler<ResetPasswordCommand, ErrorOr<bool>>
    {

        public async Task<ErrorOr<bool>> Handle(ResetPasswordCommand querry, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.User.GetUserByEmail(querry.Email);

            if (user == null)
                return false; 

            if (!_unitOfWork.User.IsPasswordResetTokenValid(querry.Email, querry.Token))


            user.PasswordHash = _unitOfWork.User.HashPassword(querry.NewPassword);
            _unitOfWork.User.Update(user);
            await _unitOfWork.Save();

            await Task.CompletedTask;
            return true;
        }
    }
}
