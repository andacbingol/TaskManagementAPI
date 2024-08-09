using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Task.UpdateTask;
public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommandRequest>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty();

        RuleFor(u => u.UserId)
            .NotEmpty();

        RuleFor(u => u.ProjectId)
            .NotEmpty();
    }
}

public class UpdateTaskCommandBodyValidator : AbstractValidator<UpdateTaskCommandRequestBody>
{
    public UpdateTaskCommandBodyValidator()
    {
        RuleFor(u => u.Title)
            .NotEmpty()
            .MaximumLength(25)
            .MinimumLength(2);

        RuleFor(u => u.Description)
            .MaximumLength(150);

        RuleFor(u => u.Priority)
            .IsInEnum();

        RuleFor(u => u.Status)
            .IsInEnum();

        RuleFor(u => u.DueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow);
    }
}
