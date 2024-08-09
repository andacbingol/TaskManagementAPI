using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementAPI.Application.Repositories;
using TaskManagementAPI.Persistence.Context;

namespace TaskManagementAPI.Persistence.Repositories;

public class TaskReadRepository : ReadRepository<Domain.Entities.Task>, ITaskReadRepository
{
    public TaskReadRepository(TaskManagementAPIDbContext context) : base(context)
    {
    }
}
