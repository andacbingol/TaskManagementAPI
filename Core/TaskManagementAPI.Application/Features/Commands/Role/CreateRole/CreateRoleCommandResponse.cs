using TaskManagementAPI.Application.DTOs.Role;

namespace TaskManagementAPI.Application.Features.Commands.Role.CreateRole;
public class CreateRoleCommandResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
