using System.Linq.Expressions;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Domain.Entities;

namespace TaskManagementAPI.Application.Abstractions.Services;

public interface IProjectService
{
    Task<List<ProjectDTO>> GetProjectsAsync(ProjectFilterDTO projectFilterDTO);
    Task<List<ProjectDTO>> GetUserProjectsAsync(ProjectFilterDTO filter, Guid userId);
    Task<ProjectDTO?> GetProjectByIdAsync(Guid id);
    Task<ProjectDTO?> GetUserProjectByIdAsync(Guid id, Guid userId);
    Task<bool> HasAnyAsync(ProjectDTO projectDTO);
    Task<bool> CreateProjectAsync(CreateProjectDTO createProjectDTO);
    Task<bool> RemoveProjectByIdAsync(Guid id);
    Task<bool> UpdateProjectAsync(UpdateProjectDTO updateProjectDTO);
    Task<bool> IsProjectNameExistForUserAsync(Guid id, string name, Guid userId);
    Task<bool> IsUserIsProjectOwnerAsync(Guid id, Guid userId);
}
