using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Craftify.Domain.Entities
{
    public class Message
    {

        public Guid Id { get; set; }

        [ForeignKey(nameof(Room))]
        public Guid RoomId { get; set; }

        [AllowNull]
        public Conversation Room { get; set; }

        public DateTime TimeSpan { get; set; }

        public string Content { get; set; } = null!;

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }
    }
}
