using TaskManagementAPI.Domain.Entities.Common;
using TaskManagementAPI.Domain.Enums;

namespace TaskManagementAPI.Domain.Entities;

public class Task : IEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; }
}
