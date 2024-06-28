using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;
using Craftify.Domain.Common.Errors;
using Craftify.Domain.Entities;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Domain.Constants;

namespace Craftify.Application.Profile.Commands.SubscribeProfile
{
    public class SubscribeProfileCommandHandler(
        IUnitOfWork _unitOfWrok,
        IDateTimeProvider _dateTime,
        IRazorpayService razorpay
        ) : IRequestHandler<SubscribeProfileCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(SubscribeProfileCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;


            var paymentIdValidation = razorpay.ValidatePaymentIdAsync(request.PaymentId);

            if(!paymentIdValidation.Result) return Errors.User.InvaildCredetial;

            // Retrieve user from repository
            var user = _unitOfWrok.User.GetUserById(request.UserId);

            if (user == null)return Errors.User.InvaildCredetial;

            var plan = _unitOfWrok.Plan.Get(p => p.Id == request.PlanId);

            if (plan == null) return Errors.User.InvaildCredetial;


            Guid WorkerId = Guid.NewGuid();
            Subscription subscription = new()
            {
                Id = Guid.NewGuid(),
                WorkerId =WorkerId,
                PlanId = request.PlanId,
                PaymentId = request.PaymentId,
                ExpireAt = _dateTime.UtcNow.AddMonths(plan.Duration)
            };

            Worker worker = new()
            {
                Id = WorkerId,
                UserId = request.UserId,
                ServiceTitle = request.ServiceTitle!,
                LogoUrl = request.LogoUrl,
                Description = request.Description,
                CertificationUrl = request.CertificationUrl,
                Skills = request.Skills,
                PerHourPrice = request.PerHourPrice,
            };

            // Update user in repository
            _unitOfWrok.User.Subscribe(subscription,worker);
            _unitOfWrok.User.ChangeUserRole(user,AppConstants.Role_Worker);
            _unitOfWrok.Save();
            return Unit.Value;

        }
    }
}
