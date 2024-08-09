using AutoMapper;
using TaskManagementAPI.Application.DTOs.Authentication;
using TaskManagementAPI.Application.DTOs.Authentication.Facebook;
using TaskManagementAPI.Application.DTOs.Authentication.Google;
using TaskManagementAPI.Application.Features.Commands.Auth.ConfirmEmail;
using TaskManagementAPI.Application.Features.Commands.Auth.ConfirmPasswordReset;
using TaskManagementAPI.Application.Features.Commands.Auth.FacebookLoginUser;
using TaskManagementAPI.Application.Features.Commands.Auth.GoogleLoginUser;
using TaskManagementAPI.Application.Features.Commands.Auth.Login;
using TaskManagementAPI.Application.Features.Commands.Auth.RefreshTokenLoginUser;

namespace TaskManagementAPI.Application.Mapper;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<LoginCommandRequest, LoginUserDTO>()
            .ForMember(dest => dest.UsernameOrEmail, opt => opt.MapFrom(src => src.UsernameOrEmail))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        CreateMap<TokenDTO, LoginCommandResponse>()
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken))
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken))
            .ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => src.Expiration));

        CreateMap<RefreshTokenLoginUserCommandRequest, RefreshTokenLoginUserDTO>()
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken))
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken));

        CreateMap<TokenDTO, RefreshTokenLoginUserCommandResponse>()
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken))
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken))
            .ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => src.Expiration));

        CreateMap<GoogleLoginUserCommandRequest, GoogleLoginUserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IdToken, opt => opt.MapFrom(src => src.IdToken))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoUrl))
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider));

        CreateMap<FacebookLoginUserCommandRequest, FacebookLoginUserDTO>()
            .ForMember(dest => dest.AuthToken, opt => opt.MapFrom(src => src.AuthToken));

        CreateMap<ConfirmEmailCommandRequest, ConfirmEmailTokenDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ConfirmEmailToken, opt => opt.MapFrom(src => src.ConfirmEmailToken));

        CreateMap<ConfirmPasswordResetCommandRequest, PasswordResetDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ResetToken, opt => opt.MapFrom(src => src.ResetToken))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.PasswordConfirm, opt => opt.MapFrom(src => src.PasswordConfirm));
    }
}
