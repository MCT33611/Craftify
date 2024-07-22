using Craftify.Domain.Enums;

namespace Craftify.Application.Chat.Common
{
    public class MessageResult
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public string Content { get; set; }
        public MessageType Type { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public List<MessageMediaResult> Media { get; set; }
    }

    
}