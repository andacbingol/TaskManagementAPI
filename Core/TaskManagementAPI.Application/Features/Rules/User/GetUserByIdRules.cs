using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Rules.User;

public class GetUserByIdRules : BaseUserRules
{
    public GetUserByIdRules(IUserService userService) : base(userService, "Access denied. You do not have the required permissions to view this user' information")
    {
    }
}
