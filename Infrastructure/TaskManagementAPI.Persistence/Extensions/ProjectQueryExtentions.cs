using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Domain.Entities;

namespace TaskManagementAPI.Persistence.Extensions;

public static class ProjectQueryExtentions
{
    public static IQueryable<Project> ApplyFilters(this IQueryable<Project> query, ProjectFilterDTO filter)
    {
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        if (filter.StartDate.HasValue)
        {
            query = query.Where(p => p.StartDate >= filter.StartDate);
        }
        if (filter.EndDate.HasValue)
        {
            query = query.Where(p => p.EndDate <= filter.EndDate);
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
    public static IQueryable<Project> ApplySorts(this IQueryable<Project> query, ProjectFilterDTO filter)
    {
        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            query = filter.SortBy.ToLower() switch
            {
                "name" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "startdate" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(p => p.StartDate) : query.OrderBy(p => p.StartDate),
                "enddate" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(p => p.EndDate) : query.OrderBy(p => p.EndDate),
                "createddate" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(p => p.CreatedDate) : query.OrderBy(p => p.CreatedDate),
                "updateddate" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(p => p.UpdatedDate) : query.OrderBy(p => p.UpdatedDate),
                _ => query
            };
        }
        return query;
    }
}
