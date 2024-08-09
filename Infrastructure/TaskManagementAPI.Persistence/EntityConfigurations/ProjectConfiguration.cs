using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementAPI.Domain.Entities;

namespace TaskManagementAPI.Persistence.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.AppUser)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId);

        builder.Property(p => p.Name)
                .HasMaxLength(25);

        builder.Property(p => p.Description)
                .HasMaxLength(150);

        builder.ToTable(t => t.HasCheckConstraint("CK_Project_EndDate_After_StartDate", "\"EndDate\" >= \"StartDate\""));
    }
}
