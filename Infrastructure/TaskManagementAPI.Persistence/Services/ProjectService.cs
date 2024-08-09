using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Repositories;
using TaskManagementAPI.Domain.Entities;
using TaskManagementAPI.Persistence.Extensions;

namespace TaskManagementAPI.Persistence.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectReadRepository _projectReadRepository;
    private readonly IProjectWriteRepository _projectWriteRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectReadRepository projectReadRepository, IProjectWriteRepository projectWriteRepository, IMapper mapper)
    {
        _projectReadRepository = projectReadRepository;
        _projectWriteRepository = projectWriteRepository;
        _mapper = mapper;
    }
    public async Task<List<ProjectDTO>> GetProjectsAsync(ProjectFilterDTO filter)
    {
        var query = _projectReadRepository.GetAll();
        return await FilterProjectsAsync(query, filter);
    }
    public async Task<List<ProjectDTO>> GetUserProjectsAsync(ProjectFilterDTO filter, Guid userId)
    {

        var query = _projectReadRepository.GetAll(p => p.UserId == userId);
        return await FilterProjectsAsync(query, filter);
    }
    private async Task<List<ProjectDTO>> FilterProjectsAsync(IQueryable<Project> query, ProjectFilterDTO filter)
    {
        return await query.ApplyFilters(filter)
            .ApplySorts(filter)
            .ApplyPagination(filter.Pagination)
                .Select(p => _mapper.Map<ProjectDTO>(p))
                    .ToListAsync();
    }

    public async Task<ProjectDTO?> GetUserProjectByIdAsync(Guid id, Guid userId)
    {
        Project? project = await _projectReadRepository.GetSingleAsync(p => (p.UserId == userId) && (p.Id == id));

        return _mapper.Map<ProjectDTO?>(project);
    }
    public async Task<ProjectDTO?> GetProjectByIdAsync(Guid id)
    {
        Project? project = await _projectReadRepository.GetByIdAsync(id);

        return project is null ? null : _mapper.Map<ProjectDTO>(project);
    }

    public async Task<bool> HasAnyAsync(ProjectDTO projectDTO)
    {
        return await _projectReadRepository.HasAnyAsync(p => p.Id == projectDTO.Id);
    }
    public async Task<bool> CreateProjectAsync(CreateProjectDTO createProjectDTO)
    {
        if (await _projectWriteRepository.AddAsync(_mapper.Map<Project>(createProjectDTO)))
        {
            await _projectWriteRepository.SaveAsync();
            return true;
        }
        return false;
    }
    public async Task<bool> RemoveProjectByIdAsync(Guid id)
    {
        bool result = await _projectWriteRepository.RemoveByIdAsync(id);
        if (result)
            await _projectWriteRepository.SaveAsync();
        return result;
    }

    public async Task<bool> UpdateProjectAsync(UpdateProjectDTO updateProjectDTO)
    {
        Project? project = await _projectReadRepository.GetByIdAsync(updateProjectDTO.Id, tracking: true);
        if (project is not null)
        {
            if (!CheckIfThereIsChange(project, updateProjectDTO))
                return true;

            _mapper.Map(updateProjectDTO, project);
            int result = await _projectWriteRepository.SaveAsync();
            if (result > 0)
                return true;
            return false;
        }
        return false;
    }
    public async Task<bool> IsProjectNameExistForUserAsync(Guid id, string name, Guid userId)
    {
        return await _projectReadRepository
            .GetAll(filter: p => p.UserId == userId)
            .AnyAsync(p => p.Name == name && p.Id != id);
    }

    public async Task<bool> IsUserIsProjectOwnerAsync(Guid id, Guid userId)
    {
        var project = await _projectReadRepository.GetSingleAsync(p => p.Id == id);
        return project?.UserId == userId;
    }
    private bool CheckIfThereIsChange(Project project, UpdateProjectDTO updateProjectDTO)
    {
        return !(updateProjectDTO.Name.Equals(project.Name)
            && updateProjectDTO.Description.Equals(project.Description)
            && updateProjectDTO.StartDate.Equals(project.StartDate)
            && updateProjectDTO.EndDate.Equals(project.EndDate));
    }
}
