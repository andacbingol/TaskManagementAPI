using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Auth.PasswordReset;

public class PasswordResetCommandValidator : AbstractValidator<PasswordResetCommandRequest>
{
    public PasswordResetCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
