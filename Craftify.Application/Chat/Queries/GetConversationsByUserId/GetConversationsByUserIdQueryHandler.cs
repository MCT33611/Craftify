using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Chat.Common;
using MediatR;
using MapsterMapper;

namespace Craftify.Application.Chat.Queries.GetConversationsByUserId
{
    public class GetConversationsByUserIdQueryHandler : IRequestHandler<GetConversationsByUserIdQuery, List<ConversationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetConversationsByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ConversationResult>> Handle(GetConversationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var conversations = await _unitOfWork.Chat.GetConversationsByUserIdAsync(request.UserId);
            var result =  _mapper.Map<List<ConversationResult>>(conversations);
            return result;
        }
    }
}