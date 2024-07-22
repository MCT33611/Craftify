using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using MediatR;

namespace Craftify.Application.Chat.Commands.DeleteMessage
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMessageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.Chat.GetMessageByIdAsync(request.MessageId);
            if (message == null)
            {
                throw new Exception("Message not found");
            }

            // Check if the user has permission to delete the message
            if (message.FromId != request.UserId)
            {
                throw new UnauthorizedAccessException("You don't have permission to delete this message");
            }

            var success = await _unitOfWork.Chat.DeleteMessageAsync(request.MessageId);
            if (!success)
            {
                throw new Exception("Failed to delete message");
            }

            await _unitOfWork.Save();

            return true;
        }
    }
}