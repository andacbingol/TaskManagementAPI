using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Persistence.EntityConfigurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.RefreshToken)
               .IsRequired(false);

        builder.Property(u => u.RefreshTokenEndDate)
               .IsRequired(false);
    }
}
