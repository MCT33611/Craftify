using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;
using Craftify.Application.Authentication.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using Craftify.Domain.Common.Errors;

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
                    EmailConfirmed = true
                };
                string token = _jwtTokenGenerator.GenerateToken(user);

                User? existingUser = _unitOfWork.User.GetUserByEmail(user.Email);
                if (existingUser != null)
                {
                    _unitOfWork.User.Update(user);
                    return new AuthenticationResult(
                        user,
                        token
                        );
                }

                _unitOfWork.User.Add(user);
                return new AuthenticationResult(
                    user,
                    token
                    );
            }
            catch (Exception)
            {
                return Errors.User.InvaildCredetial;
            }
        }
    }

}