using Craftify.Application.Service.Commands.CreateService;
using FluentValidation;


namespace Craftify.Application.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CategoryName).NotEmpty();
            RuleFor(x => x.MaximumPrice).NotEmpty();
            RuleFor(x => x.MinmumPrice).NotEmpty();
        }
    }

}
