using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using MediatR;

namespace Craftify.Application.Chat.Queries.GetUnreadConversationsCount
{
    public class GetUnreadConversationsCountQueryHandler : IRequestHandler<GetUnreadConversationsCountQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUnreadConversationsCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetUnreadConversationsCountQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Chat.GetUnreadConversationsCountAsync(request.UserId);
        }
    }
}