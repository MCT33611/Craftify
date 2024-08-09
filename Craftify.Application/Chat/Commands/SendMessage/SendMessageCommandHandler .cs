using MediatR;
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
                ConversationId = request.ConversationId,
                FromId = request.FromId,
                ToId = request.ToId,
                Content = request.Content,
                Type = request.Type,
                Timestamp = DateTime.UtcNow
            };

            if (request.Media != null && request.Media.Any())
            {
                message.Media = request.Media.Select(m => new MessageMedia
                {
                    FileName = m.FileName,
                    ContentType = m.ContentType,
                    FileSize = m.FileSize,
                    CdnUrl = m.CdnUrl,
                    Type = m.Type
                }).ToList();
            }

            await _unitOfWork.Chat.CreateMessageAsync(message);
            await _unitOfWork.Save();

            return new MessageResult
            {
                Id = message.Id,
                Content = message.Content,
                Timestamp = message.Timestamp,
                FromId = message.FromId,
                ToId = message.ToId,
                Type = message.Type,
                Media = message.Media?.Select(m => new MessageMediaResult
                {
                    Id = m.Id,
                    FileName = m.FileName,
                    ContentType = m.ContentType,
                    FileSize = m.FileSize,
                    CdnUrl = m.CdnUrl,
                    Type = m.Type
                }).ToList()
            };
        }
    }
}