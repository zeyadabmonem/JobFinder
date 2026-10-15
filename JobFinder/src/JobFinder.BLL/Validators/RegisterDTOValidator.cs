
using FluentValidation;
using JobFinder.BLL.DTOs.Auth;

namespace JobFinder.BLL.Validators;

public class RegisterDTOValidator
    : AbstractValidator<RegisterRequestDTO>
{
    public RegisterDTOValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.")
            .Matches("[A-Z]")
            .WithMessage("Password must contain an uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("Password must contain a lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("Password must contain a digit.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain a special character.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Password confirmation is required.")
            .Equal(x => x.Password)
            .WithMessage("Password and confirmation do not match.");

        RuleFor(x => x.CompanyName)
            .MaximumLength(200)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyName));
    }
}

