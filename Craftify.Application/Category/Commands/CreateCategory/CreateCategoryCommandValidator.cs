using Craftify.Application.Service.Commands.CreateService;
using FluentValidation;


namespace Craftify.Application.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty();
        }
    }

}
