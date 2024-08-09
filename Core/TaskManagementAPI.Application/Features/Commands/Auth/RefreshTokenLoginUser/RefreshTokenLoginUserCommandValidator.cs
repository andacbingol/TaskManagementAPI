using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Auth.RefreshTokenLoginUser;

public class RefreshTokenLoginUserCommandValidator : AbstractValidator<RefreshTokenLoginUserCommandRequest>
{
    public RefreshTokenLoginUserCommandValidator()
    {
        RuleFor(r => r.AccessToken)
            .NotEmpty();

        RuleFor(r => r.RefreshToken)
            .NotEmpty();
    }
}
