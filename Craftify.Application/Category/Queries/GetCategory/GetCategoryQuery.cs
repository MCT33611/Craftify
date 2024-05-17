using Craftify.Application.Category.Common;
using Craftify.Application.Service.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Category.Queries.GetCategory
{
    public record GetCategoryQuery(
        Guid? Id
        ) : IRequest<ErrorOr<CategoryResult>>;
}
