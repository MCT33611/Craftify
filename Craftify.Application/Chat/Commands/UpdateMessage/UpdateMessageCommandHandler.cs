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
                IsRead = message.IsRead
            };
        }
    }
}