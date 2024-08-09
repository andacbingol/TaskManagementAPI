using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Domain.Constants;

namespace TaskManagementAPI.Application.Features.Rules.Project;

public class DeleteProjectRules : BaseProjectRules
{
    public DeleteProjectRules(IUserService userService) : base(userService, "You do not have permission to delete this project.")
    {
    }
}
