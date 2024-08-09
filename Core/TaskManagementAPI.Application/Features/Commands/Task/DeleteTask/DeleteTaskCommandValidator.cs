using FluentValidation;

namespace TaskManagementAPI.Application.Features.Commands.Task.DeleteTask;
public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommandRequest>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(d => d.Id)
            .NotEmpty();

        RuleFor(d => d.ProjectId)
            .NotEmpty();

        RuleFor(d => d.UserId)
            .NotEmpty();
    }
}
