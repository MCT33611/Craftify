namespace Craftify.Domain.Entities
{
    public class Conversation
    {

        public Guid Id { get; set; }
        public string RoomId { get; set; } = null!;
        public Guid PeerOneId { get; set; }
        public User PeerOne { get; set; }
        public Guid PeerTwoId { get; set; }
        public User PeerTwo { get; set; }
        public bool IsBlocked { get; set; } 
        public Guid? BlockerId { get; set; }
        public DateTime LastActivityTimestamp { get; set; } 
        public ICollection<Message> Messages { get; set; } 
    }
}
