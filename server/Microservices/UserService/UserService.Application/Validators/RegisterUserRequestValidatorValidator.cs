using FluentValidation;
using UserService.Application.DTO;

namespace UserService.Application.Validators;

public sealed class RegisterUserRequestValidator: AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}