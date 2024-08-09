using TaskManagementAPI.Application.RequestParameters;
using TaskManagementAPI.Domain.Entities.Common;

namespace TaskManagementAPI.Persistence.Extensions;

public static class QueryExtentions
{
    public static IQueryable<TEntity> ApplyPagination<TEntity>(this IQueryable<TEntity> query, Pagination? pagination) where TEntity : class, IEntity, new()
    {
        if (pagination is null)
        {
            return query;
        }
        if (pagination.Page <= 0)
        {
            return query.Take(0);
        }
        return query.Skip((pagination.Page - 1) * pagination.Size)
            .Take(pagination.Size);
    }
}
