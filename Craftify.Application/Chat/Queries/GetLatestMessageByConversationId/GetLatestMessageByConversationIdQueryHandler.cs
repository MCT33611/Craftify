using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Chat.Common;
using MediatR;
using MapsterMapper;

namespace Craftify.Application.Chat.Queries.GetLatestMessageByConversationId
{
    public class GetLatestMessageByConversationIdQueryHandler : IRequestHandler<GetLatestMessageByConversationIdQuery, MessageResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLatestMessageByConversationIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MessageResult> Handle(GetLatestMessageByConversationIdQuery request, CancellationToken cancellationToken)
        {
            var latestMessage = await _unitOfWork.Chat.GetLatestMessageByConversationIdAsync(request.ConversationId);

            if (latestMessage == null)
            {
                return null;
            }

            return _mapper.Map<MessageResult>(latestMessage);
        }
    }
}