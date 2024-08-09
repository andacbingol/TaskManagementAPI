using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = TaskManagementAPI.Domain.Entities.Task;

namespace TaskManagementAPI.Persistence.EntityConfigurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Project)
               .WithMany(p => p.Tasks)
               .HasForeignKey(t => t.ProjectId);

        builder.Property(t => t.Title)
               .HasMaxLength(25);

        builder.Property(t => t.Description)
               .HasMaxLength(150);
    }
}
