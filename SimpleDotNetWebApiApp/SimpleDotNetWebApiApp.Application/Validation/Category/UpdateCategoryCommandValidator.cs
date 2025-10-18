using FluentValidation;
using SimpleDotNetWebApiApp.Application.Commands.Category;

namespace SimpleDotNetWebApiApp.Application.Validation.Category
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(150).WithMessage("Lenght must be less or equal than 150 characters");

            RuleFor(x => x.Note)
            .MaximumLength(500).WithMessage("Note must be less than 500 character.");
        }
    }
}
