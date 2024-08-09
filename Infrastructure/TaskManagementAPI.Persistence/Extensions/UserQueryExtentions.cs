using TaskManagementAPI.Application.DTOs.User;
using TaskManagementAPI.Application.RequestParameters;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Persistence.Extensions;

public static class UserQueryExtentions
{
    public static IQueryable<AppUser> ApplyFilter(this IQueryable<AppUser> query, UserFilterDTO filter)
    {
        if (!string.IsNullOrEmpty(filter.Username))
        {
            query = query.Where(u => u.UserName.ToLower().Contains(filter.Username.ToLower()));
        }
        if (!string.IsNullOrEmpty(filter.Email))
        {
            query = query.Where(u => u.Email == filter.Email);
        }
        //if (!string.IsNullOrEmpty(filter.Roles))
        //{
        //    query = query.Select(u => u.AppUserRoles.Select(ur => ur.AppRole))
        //}
        if (filter.CreatedDate.HasValue)
        {
            query = query.Where(u => u.CreatedDate >= filter.CreatedDate);
        }
        if (filter.UpdatedDate.HasValue)
        {
            query = query.Where(u => u.UpdatedDate >= filter.UpdatedDate);
        }
        if (filter.LastLoginDate.HasValue)
        {
            query = query.Where(u => u.LastLoginDate >= filter.LastLoginDate);
        }
        return query;
    }

    public static IQueryable<AppUser> ApplySorts(this IQueryable<AppUser> query, UserFilterDTO filter)
    {
        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            query = filter.SortBy.ToLower() switch
            {
                "username" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.UserName) : query.OrderBy(u => u.UserName),
                "email" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email),
                "createddate" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.CreatedDate) : query.OrderBy(u => u.CreatedDate),
                "updateddate" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.UpdatedDate) : query.OrderBy(u => u.UpdatedDate),
                "lastlogindate" => filter.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.LastLoginDate) : query.OrderBy(u => u.LastLoginDate),
                _ => query
            };
        }
        return query;
    }
}
