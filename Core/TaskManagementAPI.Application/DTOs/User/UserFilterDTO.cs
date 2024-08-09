using TaskManagementAPI.Application.RequestParameters;

namespace TaskManagementAPI.Application.DTOs.User;

public class UserFilterDTO
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public List<string>? Roles { get; set; }
    public Pagination? Pagination { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
}
