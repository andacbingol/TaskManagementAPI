using TaskManagementAPI.Domain.Entities.Common;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Domain.Entities;

public class Project : IEntity
{
    public Project()
    {
        Tasks = new HashSet<Task>();
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Guid UserId { get; set; }
    public virtual AppUser AppUser { get; set; }
    public virtual ICollection<Task> Tasks { get; set; }
}
