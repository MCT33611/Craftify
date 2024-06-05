using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<UpdateCategoryCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var category = _unitOfWork.Category.Get(c => c.Id == request.Id);
            if (category != null)
            {
                category.CategoryName = request.CategoryName ?? category.CategoryName;
                category.Picture = request.Picture ?? category.Picture;
                category.MinmumPrice = request.MinmumPrice ?? category.MinmumPrice;
                category.MaximumPrice = request.MaximumPrice ?? category.MaximumPrice;

                _unitOfWork.Category.Update(category); 
                _unitOfWork.Save(); 
            }
            else
            {
                return Errors.User.InvaildCredetial;
            }
            return Unit.Value;
        }
    }
}
