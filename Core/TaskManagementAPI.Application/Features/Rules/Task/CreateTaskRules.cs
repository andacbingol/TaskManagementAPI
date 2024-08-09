using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

namespace TaskManagementAPI.Application.Features.Rules.Task;
public class CreateTaskRules : BaseTaskRules
{
    private readonly ITaskService _taskService;
    public CreateTaskRules(IUserService userService, IProjectService projectService, ITaskService taskService)
        : base(userService, projectService,
            "You are not authorized to create task for an another user.",
            "You are not authorized to create a task for this project.")
    {
        _taskService = taskService;
    }

    public async System.Threading.Tasks.Task TaskTitleAlreadyExistForThisProject(Guid id, string title, Guid projectId)
    {
        var exist = await _taskService.IsTitleAlreadyExistForProjectAsync(id, title, projectId);
        if (exist)
            throw new ResourceConflictException("A task with the same title is already exist for this project.");
    }

    public async System.Threading.Tasks.Task TaskIdAlreadyExistFor(Guid id)
    {
        var exist = await _taskService.HasAnyAsync(new TaskDTO() { Id = id});
        if (exist)
            throw new ResourceConflictException("A task with the same id is already exist.");
    }
}
