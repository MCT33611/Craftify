using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;
using Craftify.Application.Authentication.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using Craftify.Domain.Common.Errors;
using Craftify.Domain.Constants;
using Microsoft.Extensions.Options;

namespace Craftify.Application.Authentication.Commands.Register
{
    public class LoginWithGoogleCommandHandler(
        IJwtTokenGenerator _jwtTokenGenerator,
        IGooglePayloadGenerator _googlePayloadGenerator,
        IUnitOfWork _unitOfWork
        ) :
        IRequestHandler<LoginWithGoogleCommand, ErrorOr<AuthenticationResult>>
    {

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginWithGoogleCommand command, CancellationToken cancellationToken)
        {
            try
            {

                Payload payload = await _googlePayloadGenerator.GeneratePayload(command.Credential);

                User user = new()
                {
                    Email = payload.Email,
                    FirstName = payload.Name,
                    ProfilePicture = payload.Picture,
                    Role = AppConstants.Role_Customer,
                    EmailConfirmed = true,
                    PasswordHash = Guid.NewGuid().ToString()
                };

                User? existingUser = _unitOfWork.User.GetUserByEmail(user.Email);
                if (existingUser != null)
                {
                    Worker worker = _unitOfWork.Worker.Get(w => w.UserId == existingUser.Id);
                    user.Id = existingUser.Id;
                    user.Role = existingUser.Role;
                    return new AuthenticationResult(
                        user,
                        _jwtTokenGenerator.GenerateToken(user, worker?.Id)
                        );
                }

                _unitOfWork.User.Add(user);
                _unitOfWork.Save();

                return new AuthenticationResult(
                    user,
                    _jwtTokenGenerator.GenerateToken(user,null)
                    );
            }
            catch (Exception)
            {
                return Errors.User.InvaildCredetial;
            }
        }
    }

}