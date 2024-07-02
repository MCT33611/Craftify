using Craftify.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Authentication.Commands.Refresh
{
    public record RefreshCommand(
            string Email,
            string RefreshToken,
            string AccessToken
        ) : IRequest<ErrorOr<AuthenticationResult>>;
}
