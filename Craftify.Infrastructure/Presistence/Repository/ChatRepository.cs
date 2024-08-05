using Craftify.Application.Chat.Common;
using Craftify.Application.Chat.Queries.GetMediaByType;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Craftify.Domain.Enums;
using Craftify.Infrastructure.Presistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Craftify.Infrastructure.Persistence.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly CraftifyDbContext _context;

        public object Error { get; private set; }

        public ChatRepository(CraftifyDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation> CreateConversationAsync(Guid userId1, Guid userId2)
        {
            string roomId = GetRoomId(userId1, userId2);
            var conversation = await _context.Conversations.FirstOrDefaultAsync(c => c.RoomId == roomId);

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    Id = Guid.NewGuid(),
                    RoomId = roomId,
                    PeerOneId = userId1,
                    PeerTwoId = userId2,
                    LastActivityTimestamp = DateTime.UtcNow
                };

                await _context.Conversations.AddAsync(conversation);
                await _context.SaveChangesAsync();
            }

            return conversation;
        }

        public async Task<Conversation> GetConversationByIdAsync(Guid conversationId)
        {
            return await _context.Conversations
                .Include(c => c.PeerOne)
                .Include(c => c.PeerTwo)
                .FirstOrDefaultAsync(c => c.Id == conversationId);
        }

        public async Task<Conversation> GetConversationByRoomIdAsync(string roomId)
        {
            return await _context.Conversations
                .Include(c => c.PeerOne)
                .Include(c => c.PeerTwo)
                .FirstOrDefaultAsync(c => c.RoomId == roomId);
        }

        public async Task<IEnumerable<Conversation>> GetConversationsByUserIdAsync(Guid userId)
        {
            return await _context.Conversations
                .Where(c => c.PeerOneId == userId || c.PeerTwoId == userId)
                .Include(c => c.PeerOne)
                .Include(c => c.PeerTwo)
                .OrderByDescending(c => c.LastActivityTimestamp)
                .ToListAsync();
        }

        public async Task<Conversation> GetConversationByUserIdsAsync(Guid userId1, Guid userId2)
        {
            string roomId = GetRoomId(userId1, userId2);
            return await _context.Conversations.FirstOrDefaultAsync(c => c.RoomId == roomId);
        }

        public async Task<bool> UpdateConversationAsync(Conversation conversation)
        {
            conversation.LastActivityTimestamp = DateTime.UtcNow;
            _context.Conversations.Update(conversation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteConversationAsync(Guid conversationId)
        {
            var conversation = await _context.Conversations.FindAsync(conversationId);
            if (conversation != null)
            {
                _context.Conversations.Remove(conversation);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            message.Timestamp = DateTime.UtcNow;
            await _context.Messages.AddAsync(message);

            var conversation = await _context.Conversations.FindAsync(message.ConversationId);
            if (conversation != null)
            {
                conversation.LastActivityTimestamp = message.Timestamp;
                _context.Conversations.Update(conversation);
            }

            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId)
        {
            return await _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.Timestamp)
                .Include(m => m.Media)
                .ToListAsync();
        }

        public async Task<Message> GetMessageByIdAsync(Guid messageId)
        {
            return await _context.Messages
                .Include(m => m.Media)
                .FirstOrDefaultAsync(m => m.Id == messageId);
        }

        public async Task<(IEnumerable<Message> Messages, int TotalCount)> GetPaginatedMessagesByConversationIdAsync(Guid conversationId, int page, int pageSize)
        {
            var query = _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.Timestamp);

            var totalCount = await query.CountAsync();

            var messages = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(m => m.Media)
                .ToListAsync();

            return (messages, totalCount);
        }

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            if ((DateTime.UtcNow - message.Timestamp).TotalMinutes <= 10)
            {
                _context.Messages.Update(message);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteMessageAsync(Guid messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null && (DateTime.UtcNow - message.Timestamp).TotalMinutes <= 10)
            {
                message.Content = string.Empty;
                message.IsDeleted = true;
                _context.Messages.Update(message);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> MarkConversationAsReadAsync(Guid conversationId, Guid userId)
        {
            var messages = await _context.Messages
                .Where(m => m.ConversationId == conversationId && m.ToId == userId && !m.IsRead)
                .ToListAsync();

            foreach (var message in messages)
            {
                message.IsRead = true;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetUnreadConversationsCountAsync(Guid userId)
        {
            return await _context.Conversations
                .Where(c => (c.PeerOneId == userId || c.PeerTwoId == userId) &&
                       c.Messages.Any(m => m.ToId == userId && !m.IsRead))
                .CountAsync();
        }

        public async Task<Message> GetLatestMessageByConversationIdAsync(Guid conversationId)
        {
            return await _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.Timestamp)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetUnreadMessagesCountAsync(Guid conversationId, Guid userId)
        {
            return await _context.Messages
                .CountAsync(m => m.ConversationId == conversationId && m.ToId == userId && !m.IsRead);
        }

        public async Task<(IEnumerable<Message> Messages, int TotalCount)> GetPaginatedMessagesAsync(Guid conversationId, int page, int pageSize)
        {
            var query = _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.Timestamp);

            var totalCount = await query.CountAsync();

            var messages = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(m => m.Media)
                .ToListAsync();

            return (messages, totalCount);
        }

        public async Task<MessageMedia> CreateMessageMediaAsync(MessageMedia media)
        {
            await _context.MessageMedia.AddAsync(media);
            await _context.SaveChangesAsync();
            return media;
        }

        public async Task<IEnumerable<MessageMedia>> GetMediaByMessageIdAsync(Guid messageId)
        {
            return await _context.MessageMedia
                .Where(mm => mm.MessageId == messageId)
                .ToListAsync();
        }

        public async Task<MessageMedia?> GetMessageMediaByIdAsync(Guid mediaId)
        {
            return await _context.MessageMedia
                .Include(mm => mm.Message)
                .FirstOrDefaultAsync(mm => mm.Id == mediaId);
        }

        public async Task<bool> DeleteMessageMediaAsync(Guid mediaId)
        {
            var media = await _context.MessageMedia.FindAsync(mediaId);
            if (media != null)
            {
                _context.MessageMedia.Remove(media);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<MessageMedia>> GetMediaByTypeAsync(Guid conversationId, MediaType mediaType)
        {
            return await _context.MessageMedia
                .Where(mm => mm.Message.ConversationId == conversationId && mm.Type == mediaType)
                .Include(mm => mm.Message)
                .ToListAsync();
        }

        public async Task<bool> BlockUserAsync(Guid blockerId, Guid blockedId)
        {
            var conversation = await GetConversationByUserIdsAsync(blockerId, blockedId);
            if (conversation != null)
            {
                conversation.IsBlocked = true;
                conversation.BlockerId = blockerId;
                return await UpdateConversationAsync(conversation);
            }
            return false;
        }

        public async Task<bool> UnblockUserAsync(Guid unblockerId, Guid unblockedId)
        {
            var conversation = await GetConversationByUserIdsAsync(unblockerId, unblockedId);
            if (conversation != null)
            {
                conversation.IsBlocked = false;
                return await UpdateConversationAsync(conversation);
            }
            return false;
        }

        public async Task<bool> IsUserBlockedAsync(Guid userId1, Guid userId2)
        {
            var conversation = await GetConversationByUserIdsAsync(userId1, userId2);
            return conversation?.IsBlocked ?? false;
        }

        public async Task<List<Message>> SearchMessagesAsync(Guid conversationId, string searchTerm)
        {
            return await _context.Messages
                .Where(m => m.ConversationId == conversationId && m.Content.Contains(searchTerm))
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task<List<Conversation>> SearchConversationsAsync(Guid userId, string searchTerm)
        {
            return await _context.Conversations
                .Where(c => (c.PeerOneId == userId || c.PeerTwoId == userId) &&
                            (c.PeerOne.FirstName.Contains(searchTerm) ||
                             c.PeerOne.LastName.Contains(searchTerm) ||
                             c.PeerTwo.FirstName.Contains(searchTerm) ||
                             c.PeerTwo.LastName.Contains(searchTerm) ||
                             c.Messages.Any(m => m.Content.Contains(searchTerm))))
                .Include(c => c.PeerOne)
                .Include(c => c.PeerTwo)
                .Include(c => c.Messages.OrderByDescending(m => m.Timestamp).Take(1))
                .ToListAsync();
        }

        private string GetRoomId(Guid userId1, Guid userId2)
        {
            return userId1.CompareTo(userId2) < 0
                ? $"{userId1}-{userId2}"
                : $"{userId2}-{userId1}";
        }

    }
}