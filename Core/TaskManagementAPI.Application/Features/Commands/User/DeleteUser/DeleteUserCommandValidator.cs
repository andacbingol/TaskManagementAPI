using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.User.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommandRequest>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(d => d.Id)
            .NotEmpty();
    }
}
