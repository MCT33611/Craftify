using Craftify.Domain.Entities;

namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface IChatRepository : IRepository<Conversation>
    {
        void UpdateConversation(Conversation conversation);
        void UpdateMessage(Message message);
        void AddMessage(Message message);
        Conversation GetOrCreateRoom(Guid senderId, Guid receiverId);
    }
}
