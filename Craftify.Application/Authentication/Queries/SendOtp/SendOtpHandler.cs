using Craftify.Application.Authentication.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Craftify.Application.Authentication.Queries.SendOtp
{
    public class SendOtpHandler(
        IUnitOfWork _unitOfWork,
        IEmailSender _emailSender
        ) :
        IRequestHandler<SendOtpQuery, ErrorOr<bool>>
    {
        public async Task<ErrorOr<bool>> Handle(SendOtpQuery query, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.User.GetUserByEmail(query.Email);
            if (user != null)
            {
                var otpCode = _unitOfWork.User.GenerateOTP(query.Email);
                await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Your OTP is {otpCode}.");
                return true;
            }
            return false;
        }
    }
}
