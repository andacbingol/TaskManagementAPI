using FluentValidation;

namespace TaskManagementAPI.Application.Features.Queries.Project.GetProjects;

public class GetProjectQueryValidator : AbstractValidator<GetProjectsQueryRequest>
{
    public GetProjectQueryValidator()
    {
        RuleFor(g => g.UserId)
            .NotEmpty();
    }
}
