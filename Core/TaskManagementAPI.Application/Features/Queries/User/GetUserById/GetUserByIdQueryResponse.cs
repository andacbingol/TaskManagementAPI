using TaskManagementAPI.Application.DTOs.User;

namespace TaskManagementAPI.Application.Features.Queries.User.GetUserById;

public class GetUserByIdQueryResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
}
