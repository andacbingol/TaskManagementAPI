using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class FacebookLoginAuthenticationFailedException : BusinessBaseException
{
    public FacebookLoginAuthenticationFailedException() : base("Facebook login failed!")
    {
    }
}
