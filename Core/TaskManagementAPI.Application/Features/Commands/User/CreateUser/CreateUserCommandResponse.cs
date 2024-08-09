using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementAPI.Application.Features.Commands.User.CreateUser;

public class CreateUserCommandResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; }
}
