using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Plan.Common
{
    public record PlanResult(
        Guid Id,

        string Title,

        string? Description,

        decimal? Price,
        
        int Duration

        );
}
