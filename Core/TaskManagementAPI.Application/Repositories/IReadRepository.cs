using System.Linq.Expressions;
using TaskManagementAPI.Domain.Entities.Common;

namespace TaskManagementAPI.Application.Repositories;

public interface IReadRepository<IEntity> : IRepository<IEntity> where IEntity : class, new()
{
    IQueryable<IEntity> GetAll(Expression<Func<IEntity, bool>>? filter = null, bool tracking = false);
    Task<IEntity?> GetByIdAsync(Guid Id, bool tracking = false);
    Task<IEntity?> GetSingleAsync(Expression<Func<IEntity, bool>> filter, bool tracking = false);
    Task<bool> HasAnyAsync(Expression<Func<IEntity, bool>> filter);
    Task<bool> HasAnyAsync();
}
