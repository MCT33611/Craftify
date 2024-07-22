using Microsoft.AspNetCore.SignalR;
using MediatR;
using Craftify.Application.Chat.Commands.SendMessage;
using Craftify.Application.Chat.Queries.GetMessageById;
using Craftify.Application.Chat.Commands.MarkConversationAsRead;
using System.Security.Claims;
using Craftify.Application.Chat.Common.Dtos;

namespace Craftify.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ISender _mediator;

        public ChatHub(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessage(SendMessageRequest request)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                throw new HubException("User is not authenticated or user ID is invalid");
            }

            request.FromId = parsedUserId;

            var command = new SendMessageCommand
            {
                ConversationId = request.ConversationId,
                FromId = request.FromId,
                ToId = request.ToId,
                Content = request.Content,
                Type = request.Type,
                Media = request.Media?.Select(m => new MessageMediaDto
                {
                    FileName = m.FileName,
                    ContentType = m.ContentType,
                    FileSize = m.FileSize,
                    StoragePath = m.StoragePath,
                    Type = m.Type
                }).ToList()
            };

            var result = await _mediator.Send(command);

            await Clients.User(request.ToId.ToString()).SendAsync("ReceiveMessage", result);
            await Clients.Caller.SendAsync("MessageSent", result);
        }

        public async Task MarkConversationAsRead(Guid conversationId)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                throw new HubException("User is not authenticated or user ID is invalid");
            }

            var command = new MarkConversationAsReadCommand
            {
                ConversationId = conversationId,
                UserId = parsedUserId
            };

            var result = await _mediator.Send(command);

            if (result)
            {
                await Clients.Caller.SendAsync("ConversationMarkedAsRead", conversationId);
            }
        }

        public async Task JoinConversation(string conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
        }

        public async Task LeaveConversation(string conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
        }

        public async Task SendTypingNotification(Guid conversationId)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Clients.Group(conversationId.ToString()).SendAsync("UserTyping", userId);
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}