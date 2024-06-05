using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<CreateCategoryCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            Domain.Entities.Category category = new()
            {
                Id = Guid.NewGuid(),
                CategoryName = request.CategoryName,
                Picture = request.Picture,
                MaximumPrice = request.MaximumPrice,
                MinmumPrice = request.MinmumPrice,
            };
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            return category.Id;
        }
    }
}
