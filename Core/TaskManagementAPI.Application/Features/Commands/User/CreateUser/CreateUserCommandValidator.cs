using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.User.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
                .WithMessage("UserName cannot be Empty")
            .Length(2, 18)
                .WithMessage("UserName must be between 2 and 18 characters");

        RuleFor(u => u.Password)
            .NotEmpty()
                .WithMessage("Password cannot be Empty")
            .Length(6, 18)
                .WithMessage("Password must be between 6 and 18 characters")
            .Matches(".*[0-9]")
                .WithMessage("Password has to contain a digit!")
            .Must(IsContainsUppercase)
                .WithMessage("Password has to contain an Uppercase letter")
            .Must(IsContainsLowercase)
                .WithMessage("Password has to contain an Lowercase letter");

        RuleFor(u => u.PasswordConfirm)
            .Equal(u => u.Password)
                .WithMessage("Confirm Password must match Password");

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress();

    }
    private bool IsContainsUppercase(string password)
    {
        return password.Any(char.IsUpper);
    }
    private bool IsContainsLowercase(string password)
    {
        return password.Any(char.IsLower);
    }
}
