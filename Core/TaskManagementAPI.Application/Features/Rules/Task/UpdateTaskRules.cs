using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

namespace TaskManagementAPI.Application.Features.Rules.Task;
public class UpdateTaskRules : BaseTaskRules
{
    private readonly ITaskService _taskService;
    public UpdateTaskRules(IUserService userService, IProjectService projectService, ITaskService taskService)
        : base(userService, projectService,
            "You are not authorized to update task for an another user.",
            "You are not authorized to update a task for this project.")
    {
        _taskService = taskService;
    }

    public async System.Threading.Tasks.Task CheckTaskTitleAlreadyExistForProject(Guid id, string title, Guid projectId)
    {
        var exist = await _taskService.IsTitleAlreadyExistForProjectAsync(id, title, projectId);
        if (exist)
            throw new ResourceConflictException("A task with the same name already exists for this project.");
    }  
}
