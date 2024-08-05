using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using MediatR;
using Craftify.Domain.Entities;
using MapsterMapper;
using Craftify.Application.Chat.Common;
using Craftify.Domain.Common.Errors;
using Org.BouncyCastle.Crypto;

namespace Craftify.Application.Chat.Queries.GetConversationByRoomId
{
    public class GetConversationByRoomIdQueryHandler : IRequestHandler<GetConversationByRoomIdQuery, ConversationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetConversationByRoomIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ConversationResult> Handle(GetConversationByRoomIdQuery request, CancellationToken cancellationToken)
        {
            var conversation = await _unitOfWork.Chat.GetConversationByRoomIdAsync(request.RoomId);

            if (conversation == null)
            {
                throw new NotFoundException("Conversation not found.");
            }

            return _mapper.Map<ConversationResult>(conversation);
        }
    }
}