using FluentValidation;

namespace TaskManagementAPI.Application.Features.Queries.Role.GetRoleById;
public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQueryRequest>
{
    public GetRoleByIdQueryValidator()
    {
        RuleFor(g => g.Id)
            .NotEmpty();
    }
}
