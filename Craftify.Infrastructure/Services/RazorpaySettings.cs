using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Infrastructure.Services
{
    public class RazorpaySettings
    {
        public const string SectionName = "RazorpaySettings";
        public string KeyId { get; set; } = null!;
        public string KeySecret { get; set; } = null!;
    }
}
