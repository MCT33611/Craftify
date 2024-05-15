using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Authentication.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(string Email) : IRequest<ErrorOr<string>>;
}
