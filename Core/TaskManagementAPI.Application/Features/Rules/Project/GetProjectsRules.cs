using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Domain.Constants;

namespace TaskManagementAPI.Application.Features.Rules.Project;

public class GetProjectsRules : BaseProjectRules
{
    public GetProjectsRules(IUserService userService) : base(userService, "Access denied. You do not have the required permissions to view this user' projects.")
    {
    }
}
