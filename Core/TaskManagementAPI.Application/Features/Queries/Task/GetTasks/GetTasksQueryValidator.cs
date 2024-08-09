using FluentValidation;

namespace TaskManagementAPI.Application.Features.Queries.Task.GetTasks;
public class GetTasksQueryValidator : AbstractValidator<GetTasksQueryRequest>
{
    public GetTasksQueryValidator()
    {
        RuleFor(r => r.UserId)
            .NotEmpty();

        RuleFor(r => r.ProjectId)
            .NotEmpty();

        RuleFor(r => r.Priority)
            .IsInEnum();

        RuleFor(r => r.Status)
            .IsInEnum();
    }
}
