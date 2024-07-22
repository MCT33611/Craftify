using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using MediatR;

namespace Craftify.Application.Chat.Queries.IsUserBlocked
{
    public class IsUserBlockedQueryHandler : IRequestHandler<IsUserBlockedQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public IsUserBlockedQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(IsUserBlockedQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Chat.IsUserBlockedAsync(request.UserId1, request.UserId2);
        }
    }
}