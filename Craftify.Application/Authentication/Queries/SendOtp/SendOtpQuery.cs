using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Authentication.Queries.SendOtp
{
    public record SendOtpQuery(string Email) : IRequest<ErrorOr<bool>>;
}
