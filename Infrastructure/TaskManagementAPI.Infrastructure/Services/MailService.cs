using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using TaskManagementAPI.Application.Abstractions.Services;

namespace TaskManagementAPI.Infrastructure.Services;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
    }
    private async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
    {

        MailMessage mail = new();
        mail.IsBodyHtml = isBodyHtml;
        foreach (string to in tos)
            mail.To.Add(to);
        mail.Subject = subject;
        mail.Body = body;
        mail.From = new(_configuration["Mail:UserName"], _configuration["Mail:DisplayName"], System.Text.Encoding.UTF8);
        SmtpClient smtp = new();
        smtp.Credentials = new NetworkCredential(_configuration["Mail:UserName"], _configuration["Mail:Password"]);
        smtp.Port = Int32.Parse(_configuration["Mail:Port"]);
        smtp.EnableSsl = true;
        smtp.Host = _configuration["Mail:Host"];
        await smtp.SendMailAsync(mail);
    }
    public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
    {
        string mail = $"Hello,<br>Your password can be reset by clicking the link below.<br>If you did not request a new password, please ignore this email<br><strong><a target=\"_blank\" href=\"" +
            $"{_configuration["ApiUrl"]}" +
            $"/auth/confirm-password-reset" +
            $"?Id=" +
            $"{userId}" +
            $"&" +
            $"ResetToken" +
            $"=" +
            $"{resetToken}" +
            $"\">Click to reset your password...</a></strong><br><br><span style=\"font-size:12px;\"><br><br><br>Task Management";

        await SendMailAsync(to, "Password Reset Request for Task Management", mail);
    }

    public async Task SendEmailConfirmMailAsync(string to, string userId, string emailConfirmToken)
    {
        string mail = $"Hello,<br>Click the below link to confirm your email.<br>The link will expire in 7 days<br><strong><a target=\"_blank\" href=\"" +
            $"{_configuration["ApiUrl"]}" +
            $"/auth/confirmEmail" +
            $"?Id=" +
            $"{userId}" +
            $"&" +
            $"ConfirmEmailToken" +
            $"=" +
            $"{emailConfirmToken}" +
            $"\">Click to confirm your email...</a></strong><br><br><span style=\"font-size:12px;\"><br><br><br>Task Management";

        await SendMailAsync(to, "Email Confirm Request for Task Management", mail);
    }
}
