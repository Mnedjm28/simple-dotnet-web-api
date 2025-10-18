using FluentValidation;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Validation.Item
{
    public class UpdateItemValidator : AbstractValidator<UpdateItemDto>
    {
        public UpdateItemValidator()
        {
            Include(new ItemValidator());

            RuleFor(x => x.IgnoreImage)
            .NotNull().WithMessage("IgnoreImage must be specified.");
        }
    }
}
