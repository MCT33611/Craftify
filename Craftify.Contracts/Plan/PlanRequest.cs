using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Contracts.Plan
{
    public record PlanRequest(
    Guid Id,

    string Title,

    string? Description,

    decimal? Price,

    int Duration
    );
}
