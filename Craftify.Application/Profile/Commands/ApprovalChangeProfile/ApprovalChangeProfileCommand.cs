using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Commands.ApprovalChangeProfile;

public record ApprovalChangeProfileCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
