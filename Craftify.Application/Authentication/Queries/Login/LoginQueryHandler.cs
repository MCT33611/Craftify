﻿using Craftify.Application.Authentication.Commands.Register;
using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Authentication.Common;
using Craftify.Domain.Entities;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler(
        IJwtTokenGenerator _jwtTokenGenerator,
        IUserRepository _userRepository
        ) :
        IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            //1. validate user does exist
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            //2. Validate the password is correct

            if (user.Password != query.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }
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