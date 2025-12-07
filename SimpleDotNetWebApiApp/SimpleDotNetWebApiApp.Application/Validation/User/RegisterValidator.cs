using FluentValidation;
using SimpleDotNetWebApiApp.Application.Dtos.User;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Application.Validation.Item
{
    public class RegisterValidator : AbstractValidator<UserDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(30).WithMessage("Lenght must be less or equal than 30 characters")
                .MinimumLength(2).WithMessage("Lenght must be greater or equal than 2 characters");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .IsInEnum().WithMessage("Invalid role.");
        }
    }
}
