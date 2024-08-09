using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementAPI.Application.Repositories;
using TaskManagementAPI.Domain.Entities;
using TaskManagementAPI.Persistence.Context;

namespace TaskManagementAPI.Persistence.Repositories;

public class ProjectWriteRepository : WriteRepository<Project>, IProjectWriteRepository
{
    public ProjectWriteRepository(TaskManagementAPIDbContext context) : base(context)
    {
    }
}
