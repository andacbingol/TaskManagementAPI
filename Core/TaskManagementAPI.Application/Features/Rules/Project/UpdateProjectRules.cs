using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

namespace TaskManagementAPI.Application.Features.Rules.Project;

public class UpdateProjectRules : BaseProjectRules
{
    private readonly IProjectService _projectService;
    public UpdateProjectRules(IUserService userService, IProjectService projectService)
        : base(userService, "You are not authorized to update this project.")
    {
        _projectService = projectService;
    }
    public async System.Threading.Tasks.Task ProjectNameAlreadyExistForAccount(Guid id, string name, Guid userId)
    {
        var exist = await _projectService.IsProjectNameExistForUserAsync(id, name, userId);
        if (exist)
            throw new ResourceConflictException("A project with the same name already exists for this user.");
    }
}
    

