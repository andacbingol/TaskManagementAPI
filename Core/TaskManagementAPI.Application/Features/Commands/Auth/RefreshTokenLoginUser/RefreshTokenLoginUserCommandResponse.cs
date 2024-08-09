namespace TaskManagementAPI.Application.Features.Commands.Auth.RefreshTokenLoginUser;

public class RefreshTokenLoginUserCommandResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}
