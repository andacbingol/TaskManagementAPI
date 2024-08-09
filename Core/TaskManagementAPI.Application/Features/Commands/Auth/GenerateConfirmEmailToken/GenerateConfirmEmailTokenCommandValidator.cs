using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Auth.GenerateConfirmEmailToken;

public class GenerateConfirmEmailTokenCommandValidator : AbstractValidator<GenerateConfirmEmailTokenCommandRequest>
{
    public GenerateConfirmEmailTokenCommandValidator()
    {
        RuleFor(g => g.Email)
            .NotEmpty();
    }
}
