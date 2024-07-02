using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Craftify.Application.Authentication.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Constants;
using Craftify.Application.Common.Interfaces.Service;

namespace Craftify.Application.Authentication.Commands.Register
{
    public class ConfirmEmailCommandHandler(
        IJwtTokenGenerator _jwtTokenGenerator,
        IUnitOfWork _unitOfWork,
        IDateTimeProvider _date
        ) : 
        IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (_unitOfWork.User.GetUserByEmail(command.Email) is not null)
            {
                return  Errors.User.DuplicateEmail;
            }

            User user = new()
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PasswordHash = _unitOfWork.User.HashPassword(command.Password),
                Role = AppConstants.Role_Customer,
                JoinDate = _date.UtcNow
            };

            string accessToken = _jwtTokenGenerator.GenerateToken(user,null);
            string refreshToken = _unitOfWork.User.GenerateRefreshToken(user.Email);

            _unitOfWork.User.Add(user);
            await _unitOfWork.Save();
            return new AuthenticationResult(
                user,
                accessToken,
                refreshToken
                );
        }

    }
}
