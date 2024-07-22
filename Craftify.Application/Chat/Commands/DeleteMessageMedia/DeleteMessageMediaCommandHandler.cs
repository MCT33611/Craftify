using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Common.Errors;
using MediatR;

namespace Craftify.Application.Chat.Commands.DeleteMessageMedia
{
    public class DeleteMessageMediaCommandHandler : IRequestHandler<DeleteMessageMediaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMessageMediaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteMessageMediaCommand request, CancellationToken cancellationToken)
        {
            var media = await _unitOfWork.Chat.GetMessageMediaByIdAsync(request.MediaId);
            if (media == null)
            {
                throw new NotFoundException("Message media not found.");
            }

            var message = await _unitOfWork.Chat.GetMessageByIdAsync(media.MessageId);
            if (message == null || message.FromId != request.UserId)
            {
                throw new UnauthorizedAccessException("You don't have permission to delete this media.");
            }

            return await _unitOfWork.Chat.DeleteMessageMediaAsync(request.MediaId);
        }
    }
}