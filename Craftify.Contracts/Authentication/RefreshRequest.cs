using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Contracts.Authentication
{
    public record RefreshRequest(
        string Email,
        string RefreshToken,
        string AccessToken
    );
}
