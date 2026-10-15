using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;
using JobFinder.BLL.DTOs.Auth;

namespace JobFinder.BLL.Validators;

public class LoginDTOValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}

