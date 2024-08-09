using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Application.Features.Rules.User;

public class UpdatePasswordRules : BaseRules
{
    private readonly IUserService _userService;

    public UpdatePasswordRules(IUserService userService)
    {
        _userService = userService;
    }

    public virtual async System.Threading.Tasks.Task PasswordIncorrectAsync(Guid id, string password)
    {
        AppUser? appUser = await _userService.FindByIdAsync(id);
        if (appUser is not null)
            if (!await _userService.CheckPasswordAsync(appUser, password))
                throw new UpdatePasswordInvalidPasswordException("Current password is incorrect");
    }
    public virtual void PasswordConfirmIncorrect(string passwordNew, string passwordNewConfirm)
    {
        if (!passwordNew.Equals(passwordNewConfirm))
            throw new UpdatePasswordInvalidPasswordConfirmException("New password and confirmation do not match");
    }
}
