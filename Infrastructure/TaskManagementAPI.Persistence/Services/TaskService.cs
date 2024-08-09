using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.DTOs.Task;
using TaskManagementAPI.Application.Repositories;
using TaskManagementAPI.Domain.Entities;
using TaskManagementAPI.Persistence.Extensions;

namespace TaskManagementAPI.Persistence.Services;

public class TaskService : ITaskService
{
    private readonly ITaskReadRepository _taskReadRepository;
    private readonly ITaskWriteRepository _taskWriteRepository;
    private readonly IMapper _mapper;

    public TaskService(ITaskReadRepository taskReadRepository, IMapper mapper, ITaskWriteRepository taskWriteRepository)
    {
        _taskReadRepository = taskReadRepository;
        _mapper = mapper;
        _taskWriteRepository = taskWriteRepository;
    }
    public async Task<List<TaskDTO>> GetProjectTasksAsync(TaskFilterDTO taskFilterDTO, Guid projectId)
    {
        return await _taskReadRepository.GetAll(t => t.ProjectId == projectId)
            .ApplyFilters(taskFilterDTO)
            .ApplySorts(taskFilterDTO)
            .ApplyPagination(taskFilterDTO.Pagination)
            .Select(t => _mapper.Map<TaskDTO>(t))
            .ToListAsync();
    }

    public async Task<TaskDTO?> GetProjectTaskById(Guid id, Guid projectId)
    {
        Domain.Entities.Task? task = await _taskReadRepository.GetSingleAsync(t => (t.ProjectId == projectId) && (t.Id == id));

        //return task is null ? null : _mapper.Map<TaskDTO>(task);
        return _mapper.Map<TaskDTO?>(task);
    }
    public async Task<bool> CreateTaskAsync(CreateTaskDTO createTaskDTO)
    {
        Domain.Entities.Task task = _mapper.Map<Domain.Entities.Task>(createTaskDTO);
        if (await _taskWriteRepository.AddAsync(task))
        {
            await _taskWriteRepository.SaveAsync();
            return true;
        }
        return false;
    }
    public async Task<bool> UpdateTaskAsync(UpdateTaskDTO updateTaskDTO)
    {
        Domain.Entities.Task? task = await _taskReadRepository.GetByIdAsync(updateTaskDTO.Id, tracking: true);

        if (task is null)
            return false;

        if (!CheckIfThereIsChange(task, updateTaskDTO))
            return true;

        _mapper.Map(updateTaskDTO, task);

        int result = await _taskWriteRepository.SaveAsync();

        if(result > 0)
            return true;

        return false;
    }
    public async Task<bool> RemoveTaskByIdAsync(Guid id)
    {
        bool result = await _taskWriteRepository.RemoveByIdAsync(id);

        if(result)
            await _taskWriteRepository.SaveAsync();

        return result;
    }
    public async Task<bool> IsTitleAlreadyExistForProjectAsync(Guid id, string title, Guid projectId)
    {
        return await _taskReadRepository
            .GetAll(filter: t => t.ProjectId == projectId)
            .AnyAsync(t => t.Title.Equals(title) && t.Id != id);
    }

    public async Task<bool> HasAnyAsync(TaskDTO taskDTO)
    {
        return await _taskReadRepository.HasAnyAsync(t => t.Id == taskDTO.Id);
    }

    private bool CheckIfThereIsChange(Domain.Entities.Task task, UpdateTaskDTO updateTaskDTO)
    {
        return !(updateTaskDTO.Title.Equals(task.Title)
            && updateTaskDTO.Description.Equals(task.Description)
            && updateTaskDTO.Priority.Equals(task.Priority)
            && updateTaskDTO.Status.Equals(task.Status)
            && updateTaskDTO.DueDate.Equals(task.DueDate));
    }
}
