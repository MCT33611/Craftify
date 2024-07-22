using Craftify.Application.Chat.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using MediatR;

namespace Craftify.Application.Chat.Commands.AddMessageMedia
{
    public class AddMessageMediaCommandHandler : IRequestHandler<AddMessageMediaCommand, MessageMediaResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddMessageMediaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageMediaResult> Handle(AddMessageMediaCommand request, CancellationToken cancellationToken)
        {
            var messageMedia = new MessageMedia
            {
                MessageId = request.MessageId,
                FileName = request.FileName,
                ContentType = request.ContentType,
                FileSize = request.FileSize,
                StoragePath = request.StoragePath,
                Type = request.Type
            };

            var createdMedia = await _unitOfWork.Chat.CreateMessageMediaAsync(messageMedia);

            return new MessageMediaResult
            {
                Id = createdMedia.Id,
                MessageId = createdMedia.MessageId,
                FileName = createdMedia.FileName,
                ContentType = createdMedia.ContentType,
                FileSize = createdMedia.FileSize,
                StoragePath = createdMedia.StoragePath,
                Type = createdMedia.Type
            };
        }
    }
}