using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Principal;
using TaskManagementAPI.Domain.Entities;
using TaskManagementAPI.Domain.Entities.Common;
using TaskManagementAPI.Domain.Entities.Identity;
using TaskManagementAPI.Persistence.Extensions;

namespace TaskManagementAPI.Persistence.Context;

public class TaskManagementAPIDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public TaskManagementAPIDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Domain.Entities.Task> Tasks { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyUtcDateTimeConverter();
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker.Entries<IEntity>();
        foreach (var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}