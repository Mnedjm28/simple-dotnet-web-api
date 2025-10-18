using FluentValidation;
using SimpleDotNetWebApiApp.Application.Commands.Category;

namespace SimpleDotNetWebApiApp.Application.Validation.Item
{
    public partial class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(150).WithMessage("Lenght must be less or equal than 150 characters")
                .MinimumLength(2).WithMessage("Lenght must be greater or equal than 2 characters");

            RuleFor(x => x.Note)
            .MaximumLength(500).WithMessage("Note must be less than 500 character.");
        }
    }
}
