using Craftify.Application.Authentication.Commands.Register;
using Craftify.Application.Authentication.Common;
using Craftify.Application.Authentication.Queries.Login;
using Craftify.Contracts.Authentication;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Craftify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(
        ISender _mediator,
        IMapper _mapper
        ) : ApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResult>(authResult)),
                errors => Problem(errors)
                );
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var authResult = await _mediator.Send(query);
            if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
            }
            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResult>(authResult)),
                errors => Problem(errors)
                );
        }

    }
}
