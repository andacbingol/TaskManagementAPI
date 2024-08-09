using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

namespace TaskManagementAPI.Application.Features.Rules.User;

public class CreateUserRules : BaseRules
{
    private readonly IUserService _userService;

    public CreateUserRules(IUserService userService)
    {
        _userService = userService;
    }
    public async System.Threading.Tasks.Task EmailAlreadyExist(string email)
    {
        if (await _userService.CheckIfEmailExistAsync(email))
            throw new ResourceConflictException("A user with this email already exists.");
    }
    public async System.Threading.Tasks.Task UserNameAlreadyExist(string userName)
    {
        if (await _userService.CheckIfUserNameExistAsync(userName))
            throw new ResourceConflictException("A user with this username already exists.");
    }
}
