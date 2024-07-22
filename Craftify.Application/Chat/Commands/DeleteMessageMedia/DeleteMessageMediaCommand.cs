using MediatR;

namespace Craftify.Application.Chat.Commands.DeleteMessageMedia
{
    public class DeleteMessageMediaCommand : IRequest<bool>
    {
        public Guid MediaId { get; set; }
        public Guid UserId { get; set; }  // To verify if the user has permission to delete
    }
}