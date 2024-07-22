using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Chat.Common;
using MediatR;
using MapsterMapper;
using Craftify.Domain.Common.Errors;

namespace Craftify.Application.Chat.Queries.GetMessageById
{
    public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, MessageResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMessageByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MessageResult> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.Chat.GetMessageByIdAsync(request.MessageId);

            if (message == null)
            {
                throw new NotFoundException("Message not found.");
            }

            return _mapper.Map<MessageResult>(message);
        }
    }
}