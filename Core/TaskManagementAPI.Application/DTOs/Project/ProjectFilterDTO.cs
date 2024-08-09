using TaskManagementAPI.Application.RequestParameters;

namespace TaskManagementAPI.Application.DTOs;

public class ProjectFilterDTO
{
    public string? Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Pagination? Pagination { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
}
