using AutoMapper;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.DTOs.Role;
using TaskManagementAPI.Application.Features.Commands.Role.CreateRole;
using TaskManagementAPI.Application.Features.Commands.Role.UpdateRole;
using TaskManagementAPI.Application.Features.Queries.Role.GetRoleById;
using TaskManagementAPI.Application.Features.Queries.Role.GetRoles;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Application.Mapper;
public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<AppRole, RoleDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<RoleDTO, GetRolesQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<RoleDTO, GetRoleByIdQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<CreateRoleCommandRequest, CreateRoleDTO>()
            .ForMember(dest => dest.Id, opt => Guid.NewGuid())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<CreateRoleDTO, AppRole>()
            .ForMember(dest => dest.Id, opt => Guid.NewGuid())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<CreateRoleCommandRequest, CreateRoleDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<UpdateRoleCommandRequest, UpdateRoleDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UpdateRoleCommandRequestBody.Name));

        CreateMap<UpdateRoleDTO, AppRole>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
