using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Rules.Project;

public class GetProjectByIdRules : BaseProjectRules
{
    public GetProjectByIdRules(IUserService userService) : base(userService, "Access denied. You do not have the required permissions to view this user' project.")
    {
    }
}
