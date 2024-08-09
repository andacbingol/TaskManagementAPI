using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementAPI.Domain.Entities;

namespace TaskManagementAPI.Application.Repositories;

public interface IProjectWriteRepository : IWriteRepository<Project>
{
}
