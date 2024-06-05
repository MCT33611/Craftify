using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Contracts.Service
{
    public record ServiceRequest(
    Guid ProviderId,
    string Title,
    string Description,
    Guid CategoryId,
    decimal? Price,
    bool Availability,
    string[] NewPicUrls,
    string ZipCode);
}
