using MediatR;

namespace Craftify.Application.Chat.Commands.UnblockUser
{
    public class UnblockUserCommand : IRequest<bool>
    {
        public Guid UnblockerId { get; set; }
        public Guid UnblockedId { get; set; }
    }
}