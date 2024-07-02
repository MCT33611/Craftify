using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;
using Craftify.Application.Authentication.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using Craftify.Domain.Common.Errors;
using Craftify.Domain.Constants;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Craftify.Application.Authentication.Commands.Refresh
{
    public class RefreshCommandHandler(
        IJwtTokenGenerator _jwtTokenGenerator,
        IUnitOfWork _unitOfWork
        ) :
        IRequestHandler<RefreshCommand, ErrorOr<AuthenticationResult>>
    {

        public async Task<ErrorOr<AuthenticationResult>> Handle(RefreshCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (!_unitOfWork.User.IsRefreshTokenValid(command.Email,command.RefreshToken))
                {
                    return Error.Custom(404,"404","Token Exprired");
                }
                User user = _unitOfWork.User.Get(u => u.Email == command.Email);
                if (user == null) return Error.NotFound("User not found");

                string newAccessToken = _jwtTokenGenerator.GenerateToken(user,null);

                string newRefreshToken = _unitOfWork.User.GenerateRefreshToken(command.Email);

                await _unitOfWork.Save();

                return new AuthenticationResult(
                    user,
                    newAccessToken,
                    newRefreshToken
                    );
            }
            catch (Exception)
            {
                return Errors.User.InvaildCredetial;
            }
        }
    }

}