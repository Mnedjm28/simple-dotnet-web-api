using FluentValidation;
using SimpleDotNetWebApiApp.Application.Commands.Item;

namespace SimpleDotNetWebApiApp.Application.Validation.Item
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator()
        {
            RuleFor(x => x.Item.Name)
                .NotEmpty().WithMessage("Name is reauired.")
                .MaximumLength(150).WithMessage("Lenght must be less or equal than 150 characters");

            RuleFor(x => x.Item.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Item.CategoryId)
                .NotEmpty().WithMessage("CategoryId is reauired.")
                .GreaterThan(0).WithMessage("Invalid CategoryId.");
        }
    }
}
