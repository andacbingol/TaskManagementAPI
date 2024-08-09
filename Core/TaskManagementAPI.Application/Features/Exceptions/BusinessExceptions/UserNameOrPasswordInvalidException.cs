using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class UserNameOrPasswordInvalidException : BusinessBaseException
{
    public UserNameOrPasswordInvalidException() : base("Your UserName or password is incorrect!") { }
}
