using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Chat.Common;
using MediatR;
using MapsterMapper;

namespace Craftify.Application.Chat.Queries.GetPaginatedMessages
{
    public class GetPaginatedMessagesQueryHandler : IRequestHandler<GetPaginatedMessagesQuery, PaginatedMessageResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPaginatedMessagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedMessageResult> Handle(GetPaginatedMessagesQuery request, CancellationToken cancellationToken)
        {
            var (messages, totalCount) = await _unitOfWork.Chat.GetPaginatedMessagesAsync(
                request.ConversationId,
                request.Page,
                request.PageSize
            );

            var messageResults = _mapper.Map<List<MessageResult>>(messages);

            return new PaginatedMessageResult
            {
                Messages = messageResults,
                TotalCount = totalCount,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}