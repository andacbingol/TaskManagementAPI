using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class UpdatePasswordFailedUserNotFoundException : BusinessBaseException
{
    public UpdatePasswordFailedUserNotFoundException() : base("Update password failed! AppUser not found!")
    {
    }
}
