using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Infrastructure.Authentication
{
    public class GoogleSettings
    {
        public const string SectionName = "GoogleSettings";

        public string? GoogleClientId { get; set; }

        public string? Secret { get; set; }

        public string? AppKey { get; set; }
    }
}
