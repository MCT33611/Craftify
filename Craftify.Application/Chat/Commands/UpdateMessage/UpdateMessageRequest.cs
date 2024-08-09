using Craftify.Domain.Enums;

namespace Craftify.Application.Chat.Commands.UpdateMessage
{
    public class UpdateMessageRequest
    {
        public Guid MessageId { get; set; }
        public string NewContent { get; set; }
        public MessageType Type { get; set; }
    }
}