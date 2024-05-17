using Craftify.Application.Category.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Service.Common;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Category.Queries.GetCategory
{
    public class GetCategoryQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetCategoryQuery, ErrorOr<CategoryResult>>
    {

        public async Task<ErrorOr<CategoryResult>> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (query.Id == null)
            {
                return Errors.User.InvaildCredetial;
            }

            var category = _unitOfWork.Category.Get(s => s.Id == query.Id);
            return new CategoryResult(
                category.Id,
                category.CategoryName,
                category.MinmumPrice,
                category.MaximumPrice
                );
        }
    }
}

