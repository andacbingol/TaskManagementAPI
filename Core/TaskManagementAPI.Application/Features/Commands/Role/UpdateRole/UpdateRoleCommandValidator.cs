using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Role.UpdateRole;
public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommandRequest>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty();
    }
}

public class UpdateRoleCommandBodyValidator : AbstractValidator<UpdateRoleCommandRequestBody>
{
    public UpdateRoleCommandBodyValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(255);
    }
}
