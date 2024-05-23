using Craftify.Application.Authentication.Queries.Login;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Authentication.Queries.SendOtp
{
    public class SendOtpQueryValidator : AbstractValidator<SendOtpQuery>
    {
        public SendOtpQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }

    }
}
