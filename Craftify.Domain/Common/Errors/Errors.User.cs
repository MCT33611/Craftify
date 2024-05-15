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
        public static class User
        {
            public static Error DuplicateEmail => Error.Conflict(
                code:"User.DuplicateEmail",
                description:"Email is already in use");

            public static Error InvaildCredetial => Error.NotFound(
                code: "User.InvaildCredetial",
                description: "given credetial is not valid");

            public static Error EmailNotConfirmed => Error.NotFound(
                code: "User.EmailNotConfirmed",
                description: "given email is not confirmed");
        }
    }
}
