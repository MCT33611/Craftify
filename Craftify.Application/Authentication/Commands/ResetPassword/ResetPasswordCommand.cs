using ErrorOr;
using MediatR;

namespace Craftify.Application.Authentication.Commands.ResetPasswordCommand
{
    public record ResetPasswordCommand(
        string Email,
        string Token,
        string NewPassword
        ):IRequest<ErrorOr<bool>>;
}
