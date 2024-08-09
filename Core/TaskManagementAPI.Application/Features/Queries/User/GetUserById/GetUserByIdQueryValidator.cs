using FluentValidation;

namespace TaskManagementAPI.Application.Features.Queries.User.GetUserById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQueryRequest>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(g => g.Id)
            .NotEmpty();
    }
}
