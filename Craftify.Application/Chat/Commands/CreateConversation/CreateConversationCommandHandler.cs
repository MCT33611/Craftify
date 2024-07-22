using MediatR;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Chat.Common;
using Craftify.Domain.Entities;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;

namespace Craftify.Application.Chat.Commands.CreateConversation
{
    public class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, ConversationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateConversationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ConversationResult> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
        {
            var conversation = await _unitOfWork.Chat.CreateConversationAsync(request.UserId1, request.UserId2);
            await _unitOfWork.Save();

            return new ConversationResult
            {
                Id = conversation.Id,
                RoomId = conversation.RoomId,
                PeerOneId = conversation.PeerOneId,
                PeerTwoId = conversation.PeerTwoId,
                IsBlocked = conversation.IsBlocked,
                LastActivityTimestamp = conversation.LastActivityTimestamp
            };
        }
    }
}