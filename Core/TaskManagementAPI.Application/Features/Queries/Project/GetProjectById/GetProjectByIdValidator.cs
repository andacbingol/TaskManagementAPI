using FluentValidation;

namespace TaskManagementAPI.Application.Features.Queries.Project.GetProjectById;

public class GetProjectByIdValidator : AbstractValidator<GetProjectByIdQueryRequest>
{
    public GetProjectByIdValidator()
    {
        RuleFor(g => g.Id)
            .NotEmpty();

        RuleFor(g => g.UserId)
            .NotEmpty();
    }
}
