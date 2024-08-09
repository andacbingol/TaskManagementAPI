using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Rules.Task;
public class GetTasksRules : BaseTaskRules
{
    public GetTasksRules(IUserService userService, IProjectService projectService) 
        : base(userService, projectService, "You are not authorized to view tasks that another user project owns.", "You are not authorized to view tasks for this project")
    {
    }
}
