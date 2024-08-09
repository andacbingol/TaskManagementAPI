using FluentValidation;
using TaskManagementAPI.Application.Features.Commands.Auth.ConfirmPasswordReset;

namespace TaskManagementAPI.Application.Features.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommandRequest>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();

        RuleFor(c => c.ConfirmEmailToken)
            .NotEmpty();
    }
}
