using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Category.Commands.CreateCategory
{
    public record CreateCategoryCommand(
        Guid Id,

        string CategoryName ,

        string Picture,


        Decimal? MinmumPrice ,

        Decimal? MaximumPrice 

        ) : IRequest<ErrorOr<Guid>>;
}
