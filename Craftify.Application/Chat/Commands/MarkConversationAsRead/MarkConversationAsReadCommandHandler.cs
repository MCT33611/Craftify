using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using MediatR;

namespace Craftify.Application.Chat.Commands.MarkConversationAsRead
{
    public class MarkConversationAsReadCommandHandler : IRequestHandler<MarkConversationAsReadCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkConversationAsReadCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(MarkConversationAsReadCommand request, CancellationToken cancellationToken)
        {
            var conversation = await _unitOfWork.Chat.GetConversationByIdAsync(request.ConversationId);
            if (conversation == null)
            {
                throw new Exception("Conversation not found");
            }

            // Check if the user is part of the conversation
            if (conversation.PeerOneId != request.UserId && conversation.PeerTwoId != request.UserId)
            {
                throw new UnauthorizedAccessException("User is not part of this conversation");
            }

            var success = await _unitOfWork.Chat.MarkConversationAsReadAsync(request.ConversationId, request.UserId);
            if (!success)
            {
                throw new Exception("Failed to mark conversation as read");
            }

            await _unitOfWork.Save();

            return true;
        }
    }
}