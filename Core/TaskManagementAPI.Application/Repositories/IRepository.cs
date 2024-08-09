using Microsoft.EntityFrameworkCore;

using TaskManagementAPI.Domain.Entities.Common;

namespace TaskManagementAPI.Application.Repositories;

public interface IRepository<IEntity> where IEntity : class, new()
{
    public DbSet<IEntity> Table { get; }
}
