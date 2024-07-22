using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Chat.Common;
using MediatR;
using MapsterMapper;
using Craftify.Application.Common.Models;

namespace Craftify.Application.Chat.Queries.GetMessagesByConversationId
{
    public class GetMessagesByConversationIdQueryHandler : IRequestHandler<GetMessagesByConversationIdQuery, MessageListResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMessagesByConversationIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MessageListResult> Handle(GetMessagesByConversationIdQuery request, CancellationToken cancellationToken)
        {
            var (messages, totalCount) = await _unitOfWork.Chat.GetPaginatedMessagesByConversationIdAsync(
                request.ConversationId,
                request.Page,
                request.PageSize
            );

            var messageResults = _mapper.Map<IEnumerable<MessageResult>>(messages);

            var paginationMetadata = new PaginationMetadata
            {
                TotalCount = totalCount,
                PageSize = request.PageSize,
                CurrentPage = request.Page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };

            return new MessageListResult
            {
                Messages = messageResults,
                Metadata = paginationMetadata
            };
        }
    }
}