using AutoMapper;
using TaskService.Application.DTOs;
using TaskService.Domain.Entities;

namespace TaskService.Application.Mappings
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TodoTask, TodoTaskDto>();
        }
    }
}
