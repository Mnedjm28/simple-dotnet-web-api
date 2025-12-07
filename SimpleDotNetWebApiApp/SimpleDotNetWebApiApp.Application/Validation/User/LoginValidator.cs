using FluentValidation;
using SimpleDotNetWebApiApp.Application.Dtos.User;

namespace SimpleDotNetWebApiApp.Application.Validation.Item
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
        }
    }
}
