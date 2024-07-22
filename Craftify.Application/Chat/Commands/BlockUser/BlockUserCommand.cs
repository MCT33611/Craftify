using MediatR;

namespace Craftify.Application.Chat.Commands.BlockUser
{
    public class BlockUserCommand : IRequest<bool>
    {
        public Guid BlockerId { get; set; }
        public Guid BlockedId { get; set; }
    }
}