using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Common
{
    public record ServiceResult(
        Guid Id,
        Guid ProviderId,
        string Title,
        string Description,
        Guid CategoryId,
        decimal? Price,
        bool Availability,
        string ZipCode);
}
