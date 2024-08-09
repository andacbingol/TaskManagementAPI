using FluentValidation;
using TaskManagementAPI.Domain.Enums;

namespace TaskManagementAPI.Application.Features.Commands.Task.CreateTask;
public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommandRequest>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(p => p.UserId)
            .NotEmpty();

        RuleFor(p => p.ProjectId)
            .NotEmpty();
    }
}

public class CreateTaskCommandBodyValidator : AbstractValidator<CreateTaskCommandRequestBody>
{
    public CreateTaskCommandBodyValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty()
            .MaximumLength(25)
            .MinimumLength(2);

        RuleFor(p => p.Description)
            .MaximumLength(150);

        RuleFor(p => p.Priority)
            .IsInEnum();

        RuleFor(p => p.DueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow);
    }
}
