using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using MediatR;

namespace Craftify.Application.Chat.Queries.GetUnreadMessagesCount
{
    public class GetUnreadMessagesCountQueryHandler : IRequestHandler<GetUnreadMessagesCountQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUnreadMessagesCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetUnreadMessagesCountQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Chat.GetUnreadMessagesCountAsync(request.ConversationId, request.UserId);
        }
    }
}