using Craftify.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Authentication.Commands.Register
{
    public record LoginWithGoogleCommand(
            string Credential
        ) :IRequest<ErrorOr<AuthenticationResult>>;
}
