using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Craftify.Application.Authentication.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Constants;

namespace Craftify.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler(
        IUnitOfWork _unitOfWork
        ) : 
        IRequestHandler<ConfirmEmailCommand, ErrorOr<bool>>
    {

        public async Task<ErrorOr<bool>> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
        {
            if (_unitOfWork.User.GetUserByEmail(command.Email) is not User user)
            {
                return  Errors.User.DuplicateEmail;
            }
            if (!_unitOfWork.User.IsOTPValid(command.Email, command.OTP))
            {
                return Errors.Authentication.OtpTimeOut;
            }

            user.EmailConfirmed = true;

            _unitOfWork.User.Update(user);
            await _unitOfWork.Save();

            await Task.CompletedTask;

            return true;
        }

    }
}
