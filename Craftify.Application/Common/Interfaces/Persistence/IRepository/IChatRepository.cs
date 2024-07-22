using Craftify.Domain.Entities;
using Craftify.Domain.Enums;

namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface IChatRepository
    {
        // Existing methods
        Task<Conversation> CreateConversationAsync(Guid userId1, Guid userId2);
        Task<Conversation> GetConversationByIdAsync(Guid conversationId);
        Task<Conversation> GetConversationByRoomIdAsync(string roomId);
        Task<IEnumerable<Conversation>> GetConversationsByUserIdAsync(Guid userId);
        Task<Conversation> GetConversationByUserIdsAsync(Guid userId1, Guid userId2);
        Task<bool> UpdateConversationAsync(Conversation conversation);
        Task<bool> DeleteConversationAsync(Guid conversationId);

        Task<Message> CreateMessageAsync(Message message);
        Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId);
        Task<Message> GetMessageByIdAsync(Guid messageId);
        Task<(IEnumerable<Message> Messages, int TotalCount)> GetPaginatedMessagesByConversationIdAsync(Guid conversationId, int page, int pageSize);
        Task<bool> UpdateMessageAsync(Message message);
        Task<bool> DeleteMessageAsync(Guid messageId);

        Task<MessageMedia> CreateMessageMediaAsync(MessageMedia media);
        Task<IEnumerable<MessageMedia>> GetMediaByMessageIdAsync(Guid messageId);
        Task<MessageMedia?> GetMessageMediaByIdAsync(Guid mediaId);
        Task<bool> MarkConversationAsReadAsync(Guid conversationId, Guid userId);
        Task<int> GetUnreadConversationsCountAsync(Guid userId);
        Task<Message> GetLatestMessageByConversationIdAsync(Guid conversationId);
        Task<int> GetUnreadMessagesCountAsync(Guid conversationId, Guid userId);
        Task<(IEnumerable<Message> Messages, int TotalCount)> GetPaginatedMessagesAsync(Guid conversationId, int page, int pageSize);
        Task<bool> DeleteMessageMediaAsync(Guid mediaId);
        Task<List<MessageMedia>> GetMediaByTypeAsync(Guid conversationId, MediaType mediaType);
        Task<bool> BlockUserAsync(Guid blockerId, Guid blockedId);
        Task<bool> UnblockUserAsync(Guid unblockerId, Guid unblockedId);
        Task<bool> IsUserBlockedAsync(Guid userId1, Guid userId2);
        Task<List<Message>> SearchMessagesAsync(Guid conversationId, string searchTerm);
        Task<List<Conversation>> SearchConversationsAsync(Guid userId, string searchTerm);
    }
}