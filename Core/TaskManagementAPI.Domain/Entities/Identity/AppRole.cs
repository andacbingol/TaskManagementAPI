using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Domain.Entities.Common;

namespace TaskManagementAPI.Domain.Entities.Identity;

public class AppRole : IdentityRole<Guid>, IEntity
{
    public AppRole()
    {
        AppUserRoles = new HashSet<AppUserRole>();
    }

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
}
