using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Auth.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginCommandRequest>
{
    public LoginUserCommandValidator()
    {
        RuleFor(l => l.UsernameOrEmail)
            .NotEmpty();

        RuleFor(l => l.Password)
            .NotEmpty();
    }
}
