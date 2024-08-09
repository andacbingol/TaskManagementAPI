using TaskManagementAPI.Application.Repositories;
using TaskManagementAPI.Persistence.Context;

namespace TaskManagementAPI.Persistence.Repositories;

public class TaskWriteRepository : WriteRepository<Domain.Entities.Task>, ITaskWriteRepository
{
    public TaskWriteRepository(TaskManagementAPIDbContext context) : base(context)
    {
    }
}
