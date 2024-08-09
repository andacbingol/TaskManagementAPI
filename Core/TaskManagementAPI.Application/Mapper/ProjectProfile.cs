using AutoMapper;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Commands.Project.CreateProject;
using TaskManagementAPI.Application.Features.Commands.Project.UpdateProject;
using TaskManagementAPI.Application.Features.Queries.Project.GetProjectById;
using TaskManagementAPI.Application.Features.Queries.Project.GetProjects;
using TaskManagementAPI.Domain.Entities;

namespace TaskManagementAPI.Application.Mapper;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

        CreateMap<CreateProjectDTO, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

        CreateMap<UpdateProjectDTO, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

        CreateMap<GetProjectsQueryRequest, ProjectFilterDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
            .ForMember(dest => dest.Pagination, opt => opt.MapFrom(src => src.Pagination))
            .ForMember(dest => dest.SortBy, opt => opt.MapFrom(src => src.SortBy))
            .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.SortOrder));

        CreateMap<ProjectDTO, GetProjectsQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

        CreateMap<ProjectDTO, GetProjectByIdQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

        CreateMap<CreateProjectCommandRequest, CreateProjectDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CreateProjectCommandRequestBody.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CreateProjectCommandRequestBody.Description))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.CreateProjectCommandRequestBody.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.CreateProjectCommandRequestBody.EndDate))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<UpdateProjectCommandRequest, UpdateProjectDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UpdateProjectCommandRequestBody.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.UpdateProjectCommandRequestBody.Description))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.UpdateProjectCommandRequestBody.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.UpdateProjectCommandRequestBody.EndDate));

    }
}
