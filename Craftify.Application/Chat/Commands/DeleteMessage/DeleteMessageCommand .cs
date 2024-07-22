using MediatR;

namespace Craftify.Application.Chat.Commands.DeleteMessage
{
    public class DeleteMessageCommand : IRequest<bool>
    {
        public Guid MessageId { get; set; }
        public Guid UserId { get; set; } 
    }
}