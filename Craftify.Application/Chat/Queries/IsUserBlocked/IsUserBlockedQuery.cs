using MediatR;

namespace Craftify.Application.Chat.Queries.IsUserBlocked
{
    public record IsUserBlockedQuery(Guid UserId1, Guid UserId2) : IRequest<bool>;
}