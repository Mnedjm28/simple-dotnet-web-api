using FluentValidation;
using SimpleDotNetWebApiApp.Application.Commands.Item;

namespace SimpleDotNetWebApiApp.Application.Validation.Item
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(150).WithMessage("Lenght must be less or equal than 150 characters");

            RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.")
                .GreaterThan(0).WithMessage("Invalid CategoryId.");
        }
    }
}
