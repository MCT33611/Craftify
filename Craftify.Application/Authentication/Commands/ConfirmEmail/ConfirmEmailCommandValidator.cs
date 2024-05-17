
using FluentValidation;


namespace Craftify.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.OTP).NotEmpty();
        }
    }

}
