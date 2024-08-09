using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Persistence.EntityConfigurations;

public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.HasOne(ur => ur.AppUser)
            .WithMany(u => u.AppUserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder.HasOne(ur => ur.AppRole)
            .WithMany(r => r.AppUserRoles)
            .HasForeignKey(ur => ur.RoleId);
    }
}
