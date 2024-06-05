using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Category.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(
        Guid Id,

        string CategoryName,
        string Picture,

        Decimal? MinmumPrice,

        Decimal? MaximumPrice
        ) : IRequest<ErrorOr<Unit>>;
}
