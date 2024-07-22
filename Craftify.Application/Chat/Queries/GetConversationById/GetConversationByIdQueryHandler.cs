using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using MediatR;
using Craftify.Domain.Entities;
using MapsterMapper;
using Craftify.Application.Chat.Common;
using Craftify.Domain.Common.Errors;
using Org.BouncyCastle.Crypto;

namespace Craftify.Application.Chat.Queries.GetConversationById
{
    public class GetConversationByIdQueryHandler : IRequestHandler<GetConversationByIdQuery, ConversationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetConversationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ConversationResult> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
        {
            var conversation = await _unitOfWork.Chat.GetConversationByIdAsync(request.ConversationId);

            if (conversation == null)
            {
                throw new NotFoundException("Conversation not found.");
            }

            return _mapper.Map<ConversationResult>(conversation);
        }
    }
}