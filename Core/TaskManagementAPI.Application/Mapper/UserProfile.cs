using AutoMapper;
using TaskManagementAPI.Application.DTOs.User;
using TaskManagementAPI.Application.Features.Commands.User.CreateUser;
using TaskManagementAPI.Application.Features.Commands.User.UpdatePassword;
using TaskManagementAPI.Application.Features.Queries.User.GetUserById;
using TaskManagementAPI.Application.Features.Queries.User.GetUsers;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Application.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserCommandRequest, CreateUserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        CreateMap<CreateUserDTO, AppUser>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => false));

        CreateMap<AppUser, CreateUserResultDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

        CreateMap<CreateUserResultDTO, CreateUserCommandResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

        CreateMap<UpdatePasswordCommandRequest, UpdatePasswordDTO>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.PasswordNew, opt => opt.MapFrom(src => src.PasswordNew))
            .ForMember(dest => dest.PasswordNewConfirm, opt => opt.MapFrom(src => src.PasswordNewConfirm));

        CreateMap<GetUsersQueryRequest, UserFilterDTO>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
            .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.LastLoginDate))
            .ForMember(dest => dest.Pagination, opt => opt.MapFrom(src => src.Pagination))
            .ForMember(dest => dest.SortBy, opt => opt.MapFrom(src => src.SortBy))
            .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.SortOrder));

        CreateMap<UserDTO, GetUsersQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
            .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.LastLoginDate));

        CreateMap<AppUser, UserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.AppUserRoles.Select(ur => ur.AppRole.Name).ToList()))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
            .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.LastLoginDate));

        CreateMap<UserDTO, GetUserByIdQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
            .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.LastLoginDate));

        CreateMap<UserDTO, LimitedUserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<LimitedUserDTO, GetUserByIdQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}
