using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Domain.Entities.Common;

namespace TaskManagementAPI.Domain.Entities.Identity;

public class AppUserRole : IdentityUserRole<Guid>, IEntity
{
    public virtual AppUser AppUser { get; set; }
    public virtual AppRole AppRole { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
