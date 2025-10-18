using FluentValidation;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Validation.Item
{
    public class ItemValidator : AbstractValidator<ItemDto>
    {
        public ItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(150).WithMessage("Lenght must be less or equal than 150 characters")
                .MinimumLength(2).WithMessage("Lenght must be greater or equal than 2 characters");

            RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.")
                .GreaterThan(0).WithMessage("Invalid CategoryId.");
        }
    }
}
