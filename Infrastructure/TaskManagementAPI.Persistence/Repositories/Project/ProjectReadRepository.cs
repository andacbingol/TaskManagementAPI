using TaskManagementAPI.Application.Repositories;
using TaskManagementAPI.Domain.Entities;
using TaskManagementAPI.Persistence.Context;

namespace TaskManagementAPI.Persistence.Repositories;

public class ProjectReadRepository : ReadRepository<Project>, IProjectReadRepository
{
    public ProjectReadRepository(TaskManagementAPIDbContext context) : base(context)
    {
    }
}
