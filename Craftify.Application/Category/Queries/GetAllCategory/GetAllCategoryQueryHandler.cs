using Craftify.Application.Category.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Category.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetAllCategoryQuery, ErrorOr<IEnumerable<CategoryResult>>>
    {

        public async Task<ErrorOr<IEnumerable<CategoryResult>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            try
            {
                var categories = _unitOfWork.Category.GetAll().ToList();
                var categoryResults = categories.Select(category => new CategoryResult
                (
                    category.Id,
                    category.CategoryName,
                    category.Picture,
                    category.MaximumPrice,
                    category.MinmumPrice
                ));

                return categoryResults.ToList();
            }
            catch (Exception)
            {
                return Error.NotFound();
            }
        }
    }
}

