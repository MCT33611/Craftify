﻿using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;
using Craftify.Domain.Common.Errors;

namespace Craftify.Application.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<ForgotPasswordCommand, ErrorOr<string>>
    {
        public async Task<ErrorOr<string>> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.User.GetUserByEmail(command.Email);
            if (user == null)
            {
                return Errors.User.InvaildCredetial;
            }

            if (!user.EmailConfirmed)
            {
                return Errors.User.EmailNotConfirmed;
            }

            var resetToken = _unitOfWork.User.GenerateResetToken(command.Email);
            await Task.CompletedTask;
            return resetToken;
        }
    }
}