using MediatR;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Chat.Common;
using Craftify.Domain.Entities;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;

namespace Craftify.Application.Chat.Commands.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, MessageResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SendMessageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                ConversationId = request.ConversationId,
                FromId = request.FromId,
                ToId = request.ToId,
                Content = request.Content,
                Type = request.Type,
                Timestamp = DateTime.UtcNow,
                IsRead = false
            };

            if (request.Media != null && request.Media.Any())
            {
                message.Media = request.Media.Select(m => new MessageMedia
                {
                    Id = Guid.NewGuid(),
                    FileName = m.FileName,
                    ContentType = m.ContentType,
                    FileSize = m.FileSize,
                    StoragePath = m.StoragePath,
                    Type = m.Type
                }).ToList();
            }

            await _unitOfWork.Chat.CreateMessageAsync(message);
            await _unitOfWork.Save();

            return new MessageResult
            {
                Id = message.Id,
                ConversationId = message.ConversationId,
                FromId = message.FromId,
                ToId = message.ToId,
                Content = message.Content,
                Type = message.Type,
                Timestamp = message.Timestamp,
                IsRead = message.IsRead,
                Media = message.Media?.Select(m => new MessageMediaResult
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