using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;
using Craftify.Domain.Common.Errors;
using Craftify.Domain.Entities;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Domain.Constants;

namespace Craftify.Application.Profile.Commands.InitSubscribeProfile
{
    public class InitSubscribeProfileCommandHandler(
        IUnitOfWork _unitOfWork,
        IRazorpayService _razorpayService
        ) : IRequestHandler<InitSubscribeProfileCommand, ErrorOr<string>>
    {
        public async Task<ErrorOr<string>> Handle(InitSubscribeProfileCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var user =  _unitOfWork.User.GetUserById(request.WorkerId);
            if (user == null)
            {
                return Error.NotFound("user not fond");
            }

            var plan =  _unitOfWork.Plan.Get(p => p.Id == request.PlanId);
            if (plan == null)
            {
                return Error.NotFound("Plan not fond");
            }

            var orderId = await _razorpayService.CreateOrderIdAsync(plan.Price);
            return orderId;


        }
    }
}
