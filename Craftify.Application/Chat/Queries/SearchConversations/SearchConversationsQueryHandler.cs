using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Chat.Common;
using MediatR;
using MapsterMapper;

namespace Craftify.Application.Chat.Queries.SearchConversations
{
    public class SearchConversationsQueryHandler : IRequestHandler<SearchConversationsQuery, List<ConversationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchConversationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ConversationResult>> Handle(SearchConversationsQuery request, CancellationToken cancellationToken)
        {
            var conversations = await _unitOfWork.Chat.SearchConversationsAsync(request.UserId, request.SearchTerm);
            return _mapper.Map<List<ConversationResult>>(conversations);
        }
    }
}