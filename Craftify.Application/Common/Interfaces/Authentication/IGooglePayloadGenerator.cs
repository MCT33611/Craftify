using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Craftify.Application.Common.Interfaces.Authentication
{
    public interface IGooglePayloadGenerator
    {
        Task<Payload> GeneratePayload(string Credential);
    }
}
