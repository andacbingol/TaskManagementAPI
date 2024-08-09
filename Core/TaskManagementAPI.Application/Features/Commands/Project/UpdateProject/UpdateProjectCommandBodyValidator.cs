using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Project.UpdateProject;

public class UpdateProjectCommandBodyValidator : AbstractValidator<UpdateProjectCommandRequestBody>
{
    public UpdateProjectCommandBodyValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(25)
            .MinimumLength(2);

        RuleFor(p => p.Description)
            .MaximumLength(150);

        RuleFor(p => p.EndDate)
            .GreaterThanOrEqualTo(p => p.StartDate);
    }
}
public class UpdateProjectCommandRouteValidator : AbstractValidator<UpdateProjectCommandRequest>
{
    public UpdateProjectCommandRouteValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();

        RuleFor(p => p.UserId)
            .NotEmpty();
    }
}
