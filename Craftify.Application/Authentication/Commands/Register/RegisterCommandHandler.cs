﻿using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Craftify.Application.Authentication.Common;

namespace Craftify.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler(
        IJwtTokenGenerator _jwtTokenGenerator,
        IUserRepository _userRepository
        ) : 
        IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return  Errors.User.DuplicateEmail;
            }
            //2. Create user (generat unique ID) & Presist to DB
            User user = new()
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Password = command.Password
            };
            _userRepository.Add(user);
            //3. Create JWT token
            string token = _jwtTokenGenerator.GenerateToken(user);
            await Task.CompletedTask;

            return new AuthenticationResult(
                user,
                token
                );
        }

    }
}