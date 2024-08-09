using TaskManagementAPI.Domain.Entities.Common;

namespace TaskManagementAPI.Application.Repositories;

public interface IWriteRepository<IEntity> : IRepository<IEntity> where IEntity : class, new()
{
    Task<bool> AddAsync(IEntity entity);
    Task<bool> AddRangeAsync(List<IEntity> entities);
    Task<bool> RemoveByIdAsync(Guid Id);
    bool Remove(IEntity entity);
    bool RemoveRange(List<IEntity> entities);
    bool Update(IEntity entity);
    Task<int> SaveAsync();
}
