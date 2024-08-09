using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementAPI.Application.DTOs.Authentication;
using TaskManagementAPI.Application.DTOs.Authentication.Facebook;
using TaskManagementAPI.Application.DTOs.Authentication.Google;

namespace TaskManagementAPI.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthenticationService
    {
        Task<TokenDTO> GoogleLoginAsync(GoogleLoginUserDTO googleLoginUserDTO);
        Task<TokenDTO> FacebookLoginAsync(FacebookLoginUserDTO facebookLoginUserDTO);
    }
}
