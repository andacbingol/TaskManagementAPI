using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Role.DeleteRole;
public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommandRequest>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(d => d.Id)
            .NotEmpty();
    }
}
