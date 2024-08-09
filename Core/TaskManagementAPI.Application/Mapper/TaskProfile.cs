using AutoMapper;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.DTOs.Task;
using TaskManagementAPI.Application.Features.Commands.Task.CreateTask;
using TaskManagementAPI.Application.Features.Commands.Task.UpdateTask;
using TaskManagementAPI.Application.Features.Queries.Task.GetTaskById;
using TaskManagementAPI.Application.Features.Queries.Task.GetTasks;
using TaskManagementAPI.Domain.Enums;
using Task = TaskManagementAPI.Domain.Entities.Task;

namespace TaskManagementAPI.Application.Mapper;
public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));

        CreateMap<GetTasksQueryRequest, TaskFilterDTO>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
            .ForMember(dest => dest.SortBy, opt => opt.MapFrom(src => src.SortBy))
            .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.SortOrder));

        CreateMap<TaskDTO, GetTasksQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));

        CreateMap<TaskDTO, GetTaskByIdQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate));

        CreateMap<CreateTaskCommandRequest, CreateTaskDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.CreateTaskCommandRequestBody.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CreateTaskCommandRequestBody.Description))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.CreateTaskCommandRequestBody.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Status.ToDo))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.CreateTaskCommandRequestBody.DueDate));

        CreateMap<UpdateTaskCommandRequest, UpdateTaskDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.UpdateTaskCommandRequestBody.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.UpdateTaskCommandRequestBody.Description))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.UpdateTaskCommandRequestBody.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.UpdateTaskCommandRequestBody.Status))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.UpdateTaskCommandRequestBody.DueDate));

        CreateMap<CreateTaskDTO, Task>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));

        CreateMap<UpdateTaskDTO, Task>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate));
    }
}
