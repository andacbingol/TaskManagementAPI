using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementAPI.Application.DTOs.Authentication;

namespace TaskManagementAPI.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthenticationService
    {
        Task<TokenDTO> LoginAsync(LoginUserDTO loginUserDTO);
        Task<TokenDTO> RefreshTokenLoginAsync(RefreshTokenLoginUserDTO refreshTokenLoginUserDTO);
        Task GenerateEmailConfirmationTokenAsync(string email);
        Task ConfirmEmailAsync(ConfirmEmailTokenDTO confirmEmailTokenDTO);
        Task GeneratePasswordResetTokenAsync(string email);
        Task PasswordResetAsync(PasswordResetDTO passwordResetDTO);
    }
}
