using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.DTOs.Task;

namespace TaskManagementAPI.Application.Abstractions.Services;

public interface ITaskService
{
    Task<List<TaskDTO>> GetProjectTasksAsync(TaskFilterDTO taskFilterDTO, Guid projectId);
    Task<TaskDTO?> GetProjectTaskById(Guid id, Guid projectId);
    Task<bool> CreateTaskAsync(CreateTaskDTO createTaskDTO);
    Task<bool> UpdateTaskAsync(UpdateTaskDTO updateTaskDTO);
    Task<bool> RemoveTaskByIdAsync(Guid id);
    Task<bool> IsTitleAlreadyExistForProjectAsync(Guid id, string title, Guid projectId);
    Task<bool> HasAnyAsync(TaskDTO taskDTO);
}
