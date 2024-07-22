using MediatR;
using Craftify.Application.Chat.Common;

namespace Craftify.Application.Chat.Commands.CreateConversation
{
    public class CreateConversationCommand : IRequest<ConversationResult>
    {
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }
    }
}