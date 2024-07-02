using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Contracts.Authentication
{
    public class GoogleCredentialRequest
    {
        public string IdToken { get; set; } = null!;
    }
}
