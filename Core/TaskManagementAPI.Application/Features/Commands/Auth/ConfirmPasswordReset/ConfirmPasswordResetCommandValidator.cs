using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Auth.ConfirmPasswordReset;

public class ConfirmPasswordResetCommandValidator : AbstractValidator<ConfirmPasswordResetCommandRequest>
{
    public ConfirmPasswordResetCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
        
        RuleFor(c => c.ResetToken)
            .NotEmpty();

        RuleFor(c => c.Password)
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

        RuleFor(c => c.PasswordConfirm)
            .Equal(c => c.Password)
                .WithMessage("Confirm Password must match Password");
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
