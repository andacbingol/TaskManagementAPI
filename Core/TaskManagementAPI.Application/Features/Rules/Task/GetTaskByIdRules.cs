using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Rules.Task;
public class GetTaskByIdRules : BaseTaskRules
{
    public GetTaskByIdRules(IUserService userService, IProjectService projectService) 
        : base(userService, projectService, "You are not authorized to see a task that another user owns.", "You are not authorized to see a task for this project")
    {
    }
}
