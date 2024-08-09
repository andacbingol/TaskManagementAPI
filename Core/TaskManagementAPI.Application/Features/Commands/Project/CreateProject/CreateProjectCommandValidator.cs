using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Project.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommandRequest>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(p => p.CreateProjectCommandRequestBody.Name)
            .NotEmpty()
            .MaximumLength(25)
            .MinimumLength(2);

        RuleFor(p => p.CreateProjectCommandRequestBody.Description)
            .MaximumLength(150);

        RuleFor(p => p.CreateProjectCommandRequestBody.EndDate)
            .GreaterThanOrEqualTo(p => p.CreateProjectCommandRequestBody.StartDate);

        RuleFor(p => p.UserId)
            .NotEmpty();
    }
}
