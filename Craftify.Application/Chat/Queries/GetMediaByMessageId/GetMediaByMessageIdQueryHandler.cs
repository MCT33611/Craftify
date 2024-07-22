using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Chat.Common;
using MediatR;
using MapsterMapper;

namespace Craftify.Application.Chat.Queries.GetMediaByMessageId
{
    public class GetMediaByMessageIdQueryHandler : IRequestHandler<GetMediaByMessageIdQuery, List<MessageMediaResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMediaByMessageIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MessageMediaResult>> Handle(GetMediaByMessageIdQuery request, CancellationToken cancellationToken)
        {
            var mediaList = await _unitOfWork.Chat.GetMediaByMessageIdAsync(request.MessageId);
            return _mapper.Map<List<MessageMediaResult>>(mediaList);
        }
    }
}