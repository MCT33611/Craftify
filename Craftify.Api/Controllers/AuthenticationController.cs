using Craftify.Application.Authentication.Commands.ConfirmEmail;
using Craftify.Application.Authentication.Commands.ForgotPassword;
using Craftify.Application.Authentication.Commands.Register;
using Craftify.Application.Authentication.Commands.ResetPasswordCommand;
using Craftify.Application.Authentication.Common;
using Craftify.Application.Authentication.Queries.Login;
using Craftify.Application.Authentication.Queries.SendOtp;
using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Contracts.Authentication;
using Craftify.Domain.Common.Errors;
using Craftify.Infrastructure.Authentication;
using ErrorOr;
using Google.Apis.Auth.OAuth2.Requests;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Craftify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(
        ISender _mediator,
        IMapper _mapper, 
        IUnitOfWork _unitOfWork,
        IJwtTokenGenerator _jwtTokenGenerator,
        IOptions<JwtSettings> jwtOptions
        ) : ApiController
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors)
                );
        }

        [HttpPut("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
        {
            try
            {
                var command = new ConfirmEmailCommand(request.Email, request.OTP);
                ErrorOr<bool> result = await _mediator.Send(command);

                if (!result.IsError)
                {
                    return Ok(new { EmailConfirmation = true });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,$"An error occurred : {result.FirstError}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
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

        [HttpPost("LoginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] string credential)
        {
            var command = new LoginWithGoogleCommand(credential);
            var result = await _mediator.Send(command);

            return result.Match(
                Ok,
                error => StatusCode(StatusCodes.Status500InternalServerError,error)
            );
        }

        [HttpPost("ForgotPassword/{Email}")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            var command = new ForgotPasswordCommand(Email);
            var result = await _mediator.Send(command);

            return result.Match<IActionResult>(
                success => Ok(new { ResetToken = success }),
                error => NotFound(Errors.User.InvaildCredetial));
        }

        

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            if (model == null)
            {
                return BadRequest("Email, token, and new password are required for password reset.");
            }

            var command = _mapper.Map<ResetPasswordCommand>(model);
            var result = await _mediator.Send(command);

            if (result.Value)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to reset password.");
            }
        }

        [HttpPost("SendOtp/{email}")]
        public async Task<IActionResult> SendOtp(string email)
        {
            var command = new SendOtpQuery(email);
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(
                success => Ok(new { OtpSent = success }),
                error => NotFound(Errors.User.InvaildCredetial));
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var refreshToken = await _unitOfWork.User.GetByTokenAsync(request.RefreshToken);

            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.ExpiryDate <= DateTime.UtcNow)
            {
                return Unauthorized();
            }

            var user = _unitOfWork.User.GetUserById(refreshToken.UserId);

            if (user == null)
            {
                return Unauthorized();
            }

            var newJwtToken = _jwtTokenGenerator.GenerateToken(user, null);
            var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            refreshToken.Token = newRefreshToken;
            refreshToken.ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);
            refreshToken.IsRevoked = false;

            await _unitOfWork.User.UpdateAsync(refreshToken);

            return Ok(new AuthenticationResponse
            (
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                newJwtToken,
                newRefreshToken
            ));
        }


    }
}
