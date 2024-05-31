using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
                code: "Authentication.InvalidCredentials",
                description:"Invalied Credentionals");

            public static Error OtpTimeOut => Error.Validation(
                code: "Authentication.Otp Time out",
                description: "otp time out");
        }
    }
}
