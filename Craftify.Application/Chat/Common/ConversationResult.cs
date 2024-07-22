using Craftify.Domain.Entities;

namespace Craftify.Application.Chat.Common
{
    public class ConversationResult
    {
        public Guid Id { get; set; }
        public string RoomId { get; set; } = null!;
        public Guid PeerOneId { get; set; }
        public User PeerOne { get; set; } = null!;
        public Guid PeerTwoId { get; set; }
        public User PeerTwo { get; set; } = null!;
        public bool IsBlocked { get; set; }
        public Guid? BlockerId { get; set; }
        public DateTime LastActivityTimestamp { get; set; }
    }
}