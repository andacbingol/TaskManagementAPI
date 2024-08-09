using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.User.UpdatePassword;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommandRequest>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(u=>u.Password)
            .NotEmpty();

        RuleFor(u => u.PasswordNew)
            .NotEmpty()
                .WithMessage("Password cannot be Empty")
            .NotEqual(c => c.Password)
                .WithMessage("Your new password cannot be the same as your current password!")
            .Length(6, 18)
                .WithMessage("Password must be between 6 and 18 characters")
            .Matches(".*[0-9]")
                .WithMessage("Password has to contain a digit!")
            .Must(IsContainsUppercase)
                .WithMessage("Password has to contain an Uppercase letter")
            .Must(IsContainsLowercase)
                .WithMessage("Password has to contain an Lowercase letter");

        RuleFor(u => u.PasswordNewConfirm)
            .Equal(c => c.PasswordNew)
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
