using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Commands.AccessChangeProfile
{
    public record AccessChangeProfileCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
