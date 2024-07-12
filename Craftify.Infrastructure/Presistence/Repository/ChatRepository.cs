using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Craftify.Infrastructure.Persistence.Repository;

namespace Craftify.Infrastructure.Presistence.Repository
{
    public class ChatRepository(
        CraftifyDbContext _db
        ) : Repository<Conversation>(_db), IChatRepository
    {

        public void UpdateConversation(Conversation conversation)
        {
            _db.Conversations.Update(conversation);
        }

        public void UpdateMessage(Message message)
        {
            _db.Messages.Update(message);
        }
        public void AddMessage(Message message)
        {
            _db.Messages.Add(message);
        }

        public Conversation GetOrCreateRoom(Guid senderId, Guid receiverId)
        {

            var compositeId = CombineGuids(senderId,receiverId);

            var existingConversation = _db.Conversations.FirstOrDefault(c =>
                (c.PeerOneId == senderId && c.PeerTwoId == receiverId) ||
                (c.PeerOneId == receiverId && c.PeerTwoId == senderId));

            if (existingConversation != null)
            {
                return existingConversation;
            }

            var newConversation = new Conversation
            {
                Id = compositeId,
                PeerOneId = senderId,
                PeerTwoId = receiverId
            };

            _db.Conversations.Add(newConversation);

            return newConversation;
        }


        private static Guid CombineGuids(Guid guid1, Guid guid2)
        {
            byte[] bytes1 = guid1.ToByteArray();
            byte[] bytes2 = guid2.ToByteArray();
            byte[] newBytes = new byte[16];

            for (int i = 0; i < 16; i++)
            {
                newBytes[i] = (byte)(bytes1[i] ^ bytes2[i]);
            }

            return new Guid(newBytes);
        }
    }
}
