using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Domain.Entities.Common;

namespace TaskManagementAPI.Domain.Entities.Identity;

public class AppUser : IdentityUser<Guid>, IEntity
{
    public AppUser()
    {
        Projects = new HashSet<Project>();
        AppUserRoles = new HashSet<AppUserRole>();
    }
    public DateTime? LastLoginDate { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public virtual ICollection<Project> Projects { get; set; }
    public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
}
