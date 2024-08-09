namespace TaskManagementAPI.Application.Abstractions.Services;

public interface IMailService
{
    Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
    Task SendEmailConfirmMailAsync(string to, string userId, string emailConfirmToken);
}
