using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Rules.Task;
public class DeleteTaskRules : BaseTaskRules
{
    public DeleteTaskRules(IUserService userService, IProjectService projectService) : base(userService, projectService, "You do not have permission to delete this task.", "You do not have permission to delete this task.")
    {
    }
}
