using Craftify.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Craftify.Domain.Entities
{
    public class Message
    {
        // Existing properties
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; } = null!;
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public bool IsDeleted { get; set; }

        // New properties
        public bool IsRead { get; set; } // To track read status
        public MessageType Type { get; set; } // To differentiate between text, media, or mixed messages
        public ICollection<MessageMedia> Media { get; set; } // Navigation property for media attachments
    }
}