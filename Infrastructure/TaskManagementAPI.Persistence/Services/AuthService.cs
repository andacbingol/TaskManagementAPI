using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.Authentication;
using TaskManagementAPI.Application.DTOs.Authentication.Facebook;
using TaskManagementAPI.Application.DTOs.Authentication.Google;
using TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;
using TaskManagementAPI.Application.Helper;
using TaskManagementAPI.Domain.Constants;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly FacebookLoginSettings _facebookLoginSettings;
    private readonly GoogleLoginSettings _googleLoginSettings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMailService _mailService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IConfiguration _configuration;

    public AuthService(SignInManager<AppUser> signInManager, ITokenService tokenService, IOptions<FacebookLoginSettings> facebookOptions, IOptions<GoogleLoginSettings> googleOptions, IHttpClientFactory httpClientFactory, IMailService mailService, IUserService userService, IConfiguration configuration, IRoleService roleService)
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
        _facebookLoginSettings = facebookOptions.Value;
        _googleLoginSettings = googleOptions.Value;
        _httpClientFactory = httpClientFactory;
        _mailService = mailService;
        _userService = userService;
        _configuration = configuration;
        _roleService = roleService;
    }

    private async Task<TokenDTO> CreateUserExternalAsync(AppUser? appUser, string email, string name, UserLoginInfo info)
    {
        if (appUser is null) // First time External login attempt. User creation
        {
            appUser = await _userService.FindByEmailAsync(email);
            if (appUser is null)
            {
                appUser = new()
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    UserName = email
                };
                IdentityResult result = await _userService.CreateAsync(appUser);
                if (!result.Succeeded)
                {
                    string errorMessage = $"External authentication AppUser creation failed for '{appUser.Email}'";
                    throw new ExternalAuthenticationFailedException(errorMessage,result);
                }

            }
        }
        string defaultRole = Roles.AppUser;

        if (!await _roleService.RoleExistAsync(defaultRole))
            await _roleService.CreateRoleAsync(defaultRole);

        await _userService.AddUserToRoleAsync(appUser, defaultRole);

        await _userService.AddLoginAsync(appUser, info); //AspNetUserLogins
        IList<string> roles = new List<string>() { defaultRole };
        TokenDTO tokenDTO = await _tokenService.CreateAccessTokenAsync(appUser, roles);
        await UpdateRefreshTokenAsync(tokenDTO.RefreshToken, appUser, tokenDTO.Expiration);
        return tokenDTO;
    }
    public async Task<TokenDTO> FacebookLoginAsync(FacebookLoginUserDTO facebookLoginUserDTO)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();

        string accessTokenResponse = await httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_facebookLoginSettings.Client_ID}&client_secret={_facebookLoginSettings.Client_Secret}&grant_type=client_credentials");

        FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

        string userAccessTokenValidation = await httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={facebookLoginUserDTO.AuthToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

        FacebookUserAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

        if (validation?.Data.IsValid != false)
        {
            string userInfoResponse = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={facebookLoginUserDTO.AuthToken}");

            FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);
            if (userInfo is not null)
            {
                UserLoginInfo info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                AppUser user = await _userService.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info);
            }
        }
        throw new FacebookLoginAuthenticationFailedException();
    }
    public async Task<TokenDTO> GoogleLoginAsync(GoogleLoginUserDTO googleLoginUserDTO)
    {
        GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _googleLoginSettings.Client_ID }
        };
        GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginUserDTO.IdToken, settings);
        UserLoginInfo info = new UserLoginInfo(googleLoginUserDTO.Provider, payload.Subject, googleLoginUserDTO.Provider);
        AppUser? appUser = await _userService.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        return await CreateUserExternalAsync(appUser,payload.Email, payload.Name, info);
    }

    public async Task<TokenDTO> LoginAsync(LoginUserDTO loginUserDTO)
    {
        AppUser? appUser = await _userService.FindByNameAsync(loginUserDTO.UsernameOrEmail) ?? await _userService.FindByEmailAsync(loginUserDTO.UsernameOrEmail);
        bool passwordCheck = false;

        if (appUser is not null)
            passwordCheck = await _userService.CheckPasswordAsync(appUser, loginUserDTO.Password);

        if (appUser is null || !passwordCheck)
            throw new UserNameOrPasswordInvalidException();

        IList<string> roles = await _userService.GetUserRolesAsync(appUser.Email);
        SignInResult result = await _signInManager.PasswordSignInAsync(appUser, loginUserDTO.Password,false,true);

        if (!result.Succeeded) // Authentication failed!
        {
            string errorMessage = result.ToString();
            throw new LoginUserAuthenticationFailedException($"User login failed: {errorMessage}");
        }

        TokenDTO tokenDTO = await _tokenService.CreateAccessTokenAsync(appUser, roles);
        await UpdateRefreshTokenAsync(tokenDTO.RefreshToken, appUser, tokenDTO.Expiration);
        appUser.LastLoginDate = DateTime.UtcNow;
        await _userService.UpdateUserAsync(appUser);
        await _userService.UpdateSecurityStampAsync(appUser);

        return tokenDTO;
        
    }

    public async Task<TokenDTO> RefreshTokenLoginAsync(RefreshTokenLoginUserDTO refreshTokenLoginUserDTO)
    {
        ClaimsPrincipal claimsPrincipal = _tokenService.GetPrincipalFromExpiredToken(refreshTokenLoginUserDTO.AccessToken);

        string email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

        AppUser? appUser = await _userService.FindByEmailAsync(email);

        if (appUser is null)
            throw new UserNotFoundException("RefreshToken login failed! AppUser not found!");
        
        if (appUser.RefreshTokenEndDate <= DateTime.UtcNow) //Refresh Token expired
            throw new RefreshTokenExpiredException();


        IList<string> roles = await _userService.GetUserRolesAsync(appUser.Email);

        TokenDTO tokenDTO = await _tokenService.CreateAccessTokenAsync(appUser, roles);

        await UpdateRefreshTokenAsync(tokenDTO.RefreshToken, appUser, tokenDTO.Expiration);

        return tokenDTO;
    }
    private async Task UpdateRefreshTokenAsync(string refreshToken, AppUser appUser, DateTime accessTokenDate)
    {
        if (appUser is null)
        {
            throw new UserNotFoundException("Update RefreshToken failed! AppUser not found!");
        }
        int refreshTokenExpirationInMinutes;
        _ = int.TryParse(_configuration["Token:RefreshTokenExpirationInMinutes"], out refreshTokenExpirationInMinutes);
        appUser.RefreshToken = refreshToken;
        appUser.RefreshTokenEndDate = accessTokenDate.AddMinutes(refreshTokenExpirationInMinutes);
        await _userService.UpdateUserAsync(appUser);

        await _userService.UpdateSecurityStampAsync(appUser);
    }
    public async Task GenerateEmailConfirmationTokenAsync(string email)
    {
        AppUser? appUser = await _userService.FindByEmailAsync(email);

        if (appUser is null)
            throw new UserNotFoundException("Generate Email Confirmation Token failed! AppUser not found!");

        string emailConfirmToken = await _userService.GenerateEmailConfirmationTokenAsync(appUser);
        emailConfirmToken = emailConfirmToken.UrlEncode();

        await _mailService.SendEmailConfirmMailAsync(email, appUser.Id.ToString(), emailConfirmToken);
    }
    public async Task ConfirmEmailAsync(ConfirmEmailTokenDTO confirmEmailTokenDTO)
    {
        AppUser? appUser = await _userService.FindByIdAsync(confirmEmailTokenDTO.Id);

        if (appUser is null)
            throw new UserNotFoundException("Email confirmation failed! AppUser not found!");

        string confirmEmailToken = confirmEmailTokenDTO.ConfirmEmailToken.UrlDecode();
        IdentityResult result = await _userService.ConfirmEmailAsync(appUser, confirmEmailToken);

        if (!result.Succeeded)
        {
            string errorMessage = $"Email confirmation failed for '{appUser.Email}'";
            throw new ConfirmEmailFailedException(errorMessage, result);
        }
        await _userService.UpdateSecurityStampAsync(appUser);
    }

    public async Task GeneratePasswordResetTokenAsync(string email)
    {
        AppUser? appUser = await _userService.FindByEmailAsync(email);

        if (appUser is null)
            throw new UserNotFoundException("Generate Password Reset failed! AppUser not found!");

        string resetPasswordToken = await _userService.GeneratePasswordResetTokenAsync(appUser);
        resetPasswordToken = resetPasswordToken.UrlEncode();
        await _mailService.SendPasswordResetMailAsync(email, appUser.Id.ToString(), resetPasswordToken);
    }

    public async Task PasswordResetAsync(PasswordResetDTO passwordResetDTO)
    {
        AppUser? appUser = await _userService.FindByIdAsync(passwordResetDTO.Id);

        if (appUser is null)
            throw new UserNotFoundException("Password reset failed! AppUser not found!");

        string resetToken = passwordResetDTO.ResetToken.UrlDecode();
        IdentityResult result = await _userService.ResetPasswordAsync(appUser, resetToken, passwordResetDTO.Password);

        if (!result.Succeeded)
        {
            string errorMessage = $"Password Reset failed for '{appUser.UserName}'";
            throw new PasswordResetFailedException(errorMessage, result);
        }

        await _userService.UpdateSecurityStampAsync(appUser);
    }
}
