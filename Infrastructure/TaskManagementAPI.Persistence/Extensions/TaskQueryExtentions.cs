using TaskManagementAPI.Application.DTOs;
using Task = TaskManagementAPI.Domain.Entities.Task;
namespace TaskManagementAPI.Persistence.Extensions;
public static class TaskQueryExtentions
{
    public static IQueryable<Task> ApplyFilters(this IQueryable<Task> query, TaskFilterDTO filter)
    {
        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(t => t.Title.ToLower().Contains(filter.Title.ToLower()));
        }
        if (filter.Priority.HasValue)
        {
            query = query.Where(t => t.Priority == filter.Priority);
        }
        if (filter.Status.HasValue)
        {
            query = query.Where(t => t.Status == filter.Status);
        }
        if (filter.DueDate.HasValue)
        {
            query = query.Where(p => p.DueDate <= filter.DueDate);
        }
        if (filter.CreatedDate.HasValue)
        {
            query = query.Where(p => p.CreatedDate >= filter.CreatedDate);
        }
        if (filter.UpdatedDate.HasValue)
        {
            query = query.Where(p => p.UpdatedDate >= filter.UpdatedDate);
        }
        return query;
    }

    public static IQueryable<Task> ApplySorts(this IQueryable<Task> query, TaskFilterDTO filter)
    {
        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            query = filter.SortBy.ToLower() switch
            {
                "title" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title),
                "priority" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(t => t.Priority) : query.OrderBy(t => t.Priority),
                "status" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(t => t.Status) : query.OrderBy(t => t.Status),
                "duedate" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(t => t.DueDate) : query.OrderBy(t => t.DueDate),
                "createddate" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(t => t.CreatedDate) : query.OrderBy(t => t.CreatedDate),
                "updateddate" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(t => t.UpdatedDate) : query.OrderBy(t => t.UpdatedDate),
                _ => query
            };
        }
        return query;
    }
}
