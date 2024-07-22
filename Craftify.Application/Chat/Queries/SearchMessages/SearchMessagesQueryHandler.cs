using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Chat.Common;
using MediatR;
using MapsterMapper;

namespace Craftify.Application.Chat.Queries.SearchMessages
{
    public class SearchMessagesQueryHandler : IRequestHandler<SearchMessagesQuery, List<MessageResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchMessagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MessageResult>> Handle(SearchMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _unitOfWork.Chat.SearchMessagesAsync(request.ConversationId, request.SearchTerm);
            return _mapper.Map<List<MessageResult>>(messages);
        }
    }
}