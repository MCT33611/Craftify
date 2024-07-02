using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Authentication.Common;
using Craftify.Domain.Entities;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;

namespace Craftify.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler(
        IJwtTokenGenerator _jwtTokenGenerator,
        IUnitOfWork _unitOfWork
        ) :
        IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            //1. validate user does exist
            if (_unitOfWork.User.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            //2. Validate the password is correct

            if (!_unitOfWork.User.VerifyPassword(user.PasswordHash,query.Password))
            {
                return Errors.Authentication.InvalidCredentials;
            }
            //3. Create JWT token
            Worker? worker = _unitOfWork.Worker.Get(w => w.UserId == user.Id);

            string accessToken = _jwtTokenGenerator.GenerateToken(user, worker?.Id);
            string refreshToken = _unitOfWork.User.GenerateRefreshToken(user.Email);

            await _unitOfWork.Save();
            return new AuthenticationResult(
                user,
                accessToken,
                refreshToken
                );
        }
    }
}
