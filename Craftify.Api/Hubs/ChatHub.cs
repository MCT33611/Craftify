using Microsoft.AspNetCore.SignalR;
using MediatR;
using Craftify.Application.Chat.Commands.SendMessage;
using Craftify.Application.Chat.Queries.GetMessageById;
using Craftify.Application.Chat.Commands.MarkConversationAsRead;
using System.Security.Claims;
using MapsterMapper;
using Craftify.Application.Chat.Commands.UpdateMessage;
using Craftify.Application.Chat.Commands.DeleteMessage;
using Microsoft.AspNetCore.Authorization;

namespace Craftify.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public ChatHub(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
            else
            {
                await Clients.Caller.SendAsync("Error", "User Not Authenticated");
            }
            await base.OnConnectedAsync();
        }

        public async Task JoinConversation(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
        }

        public async Task SendMessage(SendMessageRequest request)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                throw new HubException("User is not authenticated or user ID is invalid");
            }

            request.FromId = parsedUserId;
            var command = _mapper.Map<SendMessageCommand>(request);

            var result = await _mediator.Send(command);

            string room = GetRoomId(request.FromId,request.ToId);

            await Clients.Group(room).SendAsync("MessageReceived", result);
        }

        public async Task UpdateMessage(Guid messageId, UpdateMessageRequest request)
        {
            var command = _mapper.Map<UpdateMessageCommand>(request);
            command.MessageId = messageId;
            var result = await _mediator.Send(command);
            await Clients.Group(result.ConversationId.ToString()).SendAsync("MessageUpdated", result);
        }

        
        public async Task<bool> DeleteMessage(Guid messageId)
        {
            try
            {
                var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
                {
                    throw new HubException("User is not authenticated or user ID is invalid");
                }

                var command = new DeleteMessageCommand
                {
                    MessageId = messageId,
                    UserId = parsedUserId
                };

                var result = await _mediator.Send(command);
                if (result)
                {
                    var message = await _mediator.Send(new GetMessageByIdQuery { MessageId = messageId });
                    await Clients.Group(message.ConversationId.ToString()).SendAsync("MessageDeleted", messageId);
                }
                return result;
            }
            catch (UnauthorizedAccessException)
            {
                throw new HubException("Unauthorized access");
            }
            catch (InvalidOperationException)
            {
                throw new HubException("Invalid user ID");
            }
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

        public async Task SendTypingNotification(string room)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Clients.Group(room).SendAsync("UserTyping", userId);
            }
        }

        public async Task LeaveConversation(string room)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
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


        private string GetRoomId(Guid userId1, Guid userId2)
        {
            return userId1.CompareTo(userId2) < 0
                ? $"{userId1}-{userId2}"
                : $"{userId2}-{userId1}";
        }
    }
}