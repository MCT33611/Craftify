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

                string htmlTemplate = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }
                        .container {
                            background-color: #ffffff;
                            margin: 50px auto;
                            padding: 20px;
                            border-radius: 10px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                            max-width: 600px;
                        }
                        .header {
                            background-color: #007bff;
                            color: #ffffff;
                            padding: 10px;
                            text-align: center;
                            border-radius: 10px 10px 0 0;
                        }
                        .content {
                            padding: 20px;
                            text-align: center;
                        }
                        .otp {
                            font-size: 24px;
                            font-weight: bold;
                            color: #007bff;
                            margin: 20px 0;
                        }
                        .footer {
                            text-align: center;
                            padding: 10px;
                            font-size: 12px;
                            color: #666666;
                        }
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Confirm Your Email</h1>
                        </div>
                        <div class='content'>
                            <p>Dear User,</p>
                            <p>Please use the following One Time Password (OTP) to complete your email verification process.</p>
                            <div class='otp'>{{OTP}}</div>
                            <p>This OTP is valid for the next 10 minutes.</p>
                            <p>If you did not request this, please ignore this email.</p>
                        </div>
                        <div class='footer'>
                            &copy; 2024 Craftify. All rights reserved.
                        </div>
                    </div>
                </body>
                </html>";

                string messageBody = htmlTemplate.Replace("{{OTP}}", otpCode);
                await _emailSender.SendEmailAsync(user.Email, "Confirm your email", messageBody);
                return true;
            }
            return false;
        }
    }
}
