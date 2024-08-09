using Craftify.Domain.Enums;

namespace Craftify.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; } = null!;
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; } 
        public MessageType Type { get; set; } 
        public ICollection<MessageMedia> Media { get; set; } 
    }
}