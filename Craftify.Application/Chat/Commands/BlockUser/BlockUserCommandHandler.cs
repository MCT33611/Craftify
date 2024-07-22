using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using MediatR;

namespace Craftify.Application.Chat.Commands.BlockUser
{
    public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlockUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var conversation = await _unitOfWork.Chat.GetConversationByUserIdsAsync(request.BlockerId, request.BlockedId);
            if (conversation == null)
            {
                throw new Exception("Conversation not found");
            }

            var success = await _unitOfWork.Chat.BlockUserAsync(request.BlockerId, request.BlockedId);
            if (!success)
            {
                throw new Exception("Failed to block user");
            }

            await _unitOfWork.Save();

            return true;
        }
    }
}