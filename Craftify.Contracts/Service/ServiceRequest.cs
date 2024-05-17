using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Contracts.Service
{
    public record ServiceRequest(
    string Title,
    string Description,
    string Category,
    decimal? Price,
    bool Availability,
    string ZipCode);
}
