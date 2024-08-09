using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

namespace TaskManagementAPI.Application.Features.Rules.Project;

public class CreateProjectRules : BaseProjectRules
{
    private readonly IProjectService _projectService;

    public CreateProjectRules(IUserService userService, IProjectService projectService) 
        : base(userService, "You are not authorized to create a new project for an another user.")
    {
        _projectService = projectService;
    }

    public async System.Threading.Tasks.Task ProjectNameAlreadyExistForAccount(Guid id, string name, Guid userId)
    {
        var exist = await _projectService.IsProjectNameExistForUserAsync(id, name, userId);
        if (exist)
            throw new ResourceConflictException("A project with the same name is already exists for this user.");
    }

    public async System.Threading.Tasks.Task ProjectIdAlreadyExist(Guid id)
    {
        var exist = await _projectService.HasAnyAsync(new ProjectDTO() { Id = id});
        if (exist)
            throw new ResourceConflictException("A project with the same id is already exists.");
    }
}
