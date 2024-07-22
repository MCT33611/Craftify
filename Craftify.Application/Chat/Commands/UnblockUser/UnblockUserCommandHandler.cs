using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using MediatR;

namespace Craftify.Application.Chat.Commands.UnblockUser
{
    public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnblockUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Chat.UnblockUserAsync(request.UnblockerId, request.UnblockedId);
        }
    }
}