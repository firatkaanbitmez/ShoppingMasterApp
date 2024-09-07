using FluentValidation;
using ShoppingMasterApp.Application.CQRS.Commands.Order;

namespace ShoppingMasterApp.Application.Validations
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Category name is required.");
        }
    }
}
