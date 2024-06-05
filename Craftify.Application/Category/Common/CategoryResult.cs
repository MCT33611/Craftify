using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Category.Common
{
    public record CategoryResult(
        Guid Id,

        string CategoryName,

        string? Picture,

        decimal? MinmumPrice,

        decimal? MaximumPrice
        );
}
