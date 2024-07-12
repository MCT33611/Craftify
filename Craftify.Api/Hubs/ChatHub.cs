using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Craftify.Api.Hubs
{
        public class ChatHub : Hub
        {
            private readonly IUnitOfWork _unitOfWork;

            public ChatHub(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task SendMessage(string receiverId, string message)
            {
                try
                {
                    var senderId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (string.IsNullOrEmpty(senderId))
                    {
                        throw new HubException("User not authenticated");
                    }

                    var conversation = _unitOfWork.Chat.GetOrCreateRoom(Guid.Parse(senderId), Guid.Parse(receiverId));

                    var newMessage = new Message
                    {
                        Id = Guid.NewGuid(),
                        RoomId = conversation.Id,
                        SenderId = Guid.Parse(senderId),
                        ReceiverId = Guid.Parse(receiverId),
                        Content = message,
                        TimeSpan = DateTime.UtcNow
                    };

                    _unitOfWork.Chat.AddMessage(newMessage);
                    await _unitOfWork.Save();

/*                    Console.WriteLine($"Sending message from {senderId} to {receiverId}: {message}");
                    await Clients.Group(receiverId).SendAsync("ReceiveMessage", senderId, message);
                    Console.WriteLine($"Message sent to {receiverId}");*/
                    await Clients.Group(conversation.Id.ToString()).SendAsync("ReceiveMessage", senderId, message);
                }
                catch (Exception ex)
                {
                    await Clients.Caller.SendAsync("ErrorOccurred", "An error occurred while sending the message.");
                    throw;
                }

            }

            public async Task JoinRoom(string roomId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                await Clients.Group(roomId).SendAsync("UserJoined", Context.UserIdentifier);
            }

            public async Task LeaveRoom(string roomId)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
                await Clients.Group(roomId).SendAsync("UserLeft", Context.UserIdentifier);
            }

            public async Task Typing(string roomId)
            {
                await Clients.Group(roomId).SendAsync("UserTyping", Context.UserIdentifier);
            }

            public override async Task OnConnectedAsync()
            {
                await Clients.All.SendAsync("UserOnline", Context.UserIdentifier);
                await base.OnConnectedAsync();
            }

            public override async Task OnDisconnectedAsync(Exception exception)
            {
                await Clients.All.SendAsync("UserOffline", Context.UserIdentifier);
                await base.OnDisconnectedAsync(exception);
            }
        }

}
