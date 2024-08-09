using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagementAPI.Application.Repositories;
using TaskManagementAPI.Domain.Entities.Common;
using TaskManagementAPI.Persistence.Context;

namespace TaskManagementAPI.Persistence.Repositories;

public class ReadRepository<IEntity> : IReadRepository<IEntity> where IEntity : class, new()
{
    private readonly TaskManagementAPIDbContext _context;

    public ReadRepository(TaskManagementAPIDbContext context)
    {
        _context = context;
    }

    public DbSet<IEntity> Table => _context.Set<IEntity>();

    public IQueryable<IEntity> GetAll(Expression<Func<IEntity, bool>>? filter = null, bool tracking = false)
    {
        IQueryable<IEntity> query;

        if (filter == null)
            query = Table.AsQueryable();
        else
            query = Table.Where(filter);

        if (!tracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }

    public async Task<IEntity?> GetByIdAsync(Guid Id, bool tracking = false)
    {
        var query = Table;
        if (!tracking)
        {
            query.AsNoTracking();
        }
        return await query.FindAsync(Id);
    }

    public async Task<IEntity?> GetSingleAsync(Expression<Func<IEntity, bool>> filter, bool tracking = false)
    {
        var query = Table.AsQueryable();
        if (!tracking)
        {
            query = query.AsNoTracking();
        }
        return await query.SingleOrDefaultAsync(filter);
    }

    public async Task<bool> HasAnyAsync(Expression<Func<IEntity, bool>> filter)
    {
        var query = Table.AsQueryable().AsNoTracking();
        return await query.AnyAsync(filter);
    }

    public Task<bool> HasAnyAsync()
    {
        return Table.AsQueryable().AsNoTracking().AnyAsync();
    }
}
