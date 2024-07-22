using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Chat.Common;
using MediatR;
using MapsterMapper;

namespace Craftify.Application.Chat.Queries.GetMediaByType
{
    public class GetMediaByTypeQueryHandler : IRequestHandler<GetMediaByTypeQuery, List<MessageMediaResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMediaByTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MessageMediaResult>> Handle(GetMediaByTypeQuery request, CancellationToken cancellationToken)
        {
            var mediaList = await _unitOfWork.Chat.GetMediaByTypeAsync(request.ConversationId, request.MediaType);
            return _mapper.Map<List<MessageMediaResult>>(mediaList);
        }
    }
}