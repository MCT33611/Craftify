using Craftify.Application.Services.Authentication;
using Craftify.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Craftify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService _authenticationService) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResponse = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
                );
            return Ok(authResponse);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResponse = _authenticationService.Login(
                request.Email,
                request.Passwrod
                );
            return Ok(authResponse);
        }
    }
}
