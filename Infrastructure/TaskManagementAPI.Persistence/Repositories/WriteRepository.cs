using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Application.Repositories;
using TaskManagementAPI.Domain.Entities.Common;
using TaskManagementAPI.Persistence.Context;

namespace TaskManagementAPI.Persistence.Repositories;

public class WriteRepository<IEntity> : IWriteRepository<IEntity> where IEntity : class, new()
{
    private readonly TaskManagementAPIDbContext _context;

    public WriteRepository(TaskManagementAPIDbContext context)
    {
        _context = context;
    }
    public DbSet<IEntity> Table => _context.Set<IEntity>();
    public async Task<bool> AddAsync(IEntity entity)
    {
        var result = await Table.AddAsync(entity);
        return result.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<IEntity> entities)
    {
        await Table.AddRangeAsync(entities);
        return true;
    }

    public bool Remove(IEntity entity)
    {
        var result = Table.Remove(entity);
        return result.State == EntityState.Deleted;
    }

    public async Task<bool> RemoveByIdAsync(Guid Id)
    {
        IEntity? entity = await Table.FindAsync(Id);
        if (entity != null)
        {
            return Remove(entity);
        }
        return false;
    }

    public bool RemoveRange(List<IEntity> entities)
    {
        Table.RemoveRange(entities);
        return true;
    }
    public bool Update(IEntity entity)
    {
        var result = Table.Update(entity);
        return result.State == EntityState.Modified;
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
