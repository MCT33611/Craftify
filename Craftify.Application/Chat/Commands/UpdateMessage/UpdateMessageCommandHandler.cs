using Craftify.Application.Chat.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using MediatR;

namespace Craftify.Application.Chat.Commands.UpdateMessage
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, MessageResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMessageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.Chat.GetMessageByIdAsync(request.MessageId);
            if (message == null)
            {
                throw new Exception("Message not found");
            }

            message.Content = request.NewContent;
            message.Type = request.Type;

            // Update media if provided
            if (request.UpdatedMedia != null && request.UpdatedMedia.Any())
            {
                // Remove existing media
                await _unitOfWork.Chat.DeleteMessageMediaAsync(message.Id);

                // Add new media
                foreach (var mediaDto in request.UpdatedMedia)
                {
                    var media = new MessageMedia
                    {
                        MessageId = message.Id,
                        FileName = mediaDto.FileName,
                        ContentType = mediaDto.ContentType,
                        FileSize = mediaDto.FileSize,
                        StoragePath = mediaDto.StoragePath,
                        Type = mediaDto.Type
                    };
                    await _unitOfWork.Chat.CreateMessageMediaAsync(media);
                }
            }

            var success = await _unitOfWork.Chat.UpdateMessageAsync(message);
            if (!success)
            {
                throw new Exception("Failed to update message");
            }

            await _unitOfWork.Save();

            return new MessageResult
            {
                Id = message.Id,
                ConversationId = message.ConversationId,
                Content = message.Content,
                Timestamp = message.Timestamp,
                FromId = message.FromId,
                ToId = message.ToId,
                Type = message.Type,
                IsRead = message.IsRead,
                Media = message.Media.Select(m => new MessageMediaResult
                {
                    Id = m.Id,
                    FileName = m.FileName,
                    ContentType = m.ContentType,
                    FileSize = m.FileSize,
                    StoragePath = m.StoragePath,
                    Type = m.Type
                }).ToList()
            };
        }
    }
}