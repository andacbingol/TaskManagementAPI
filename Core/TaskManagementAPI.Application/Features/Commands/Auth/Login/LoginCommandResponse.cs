﻿namespace TaskManagementAPI.Application.Features.Commands.Auth.Login;

public class LoginCommandResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}
