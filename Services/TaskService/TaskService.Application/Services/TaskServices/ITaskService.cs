using SharedKernel.Filters;
using TaskService.Application.DTOs;

namespace TaskService.Application.Services.TaskServices
{
    public interface ITaskService
    {
        Task<List<TodoTaskDto>?> GetAllTasksAsync(TodoTaskFilter? todoTaskFilter = null);
        Task<TodoTaskDto?> GetTaskByIdAsync(Guid id);
        Task<Guid> AddTaskAsync(TodoTaskDto taskDto);
        Task UpdateTaskAsync(TodoTaskDto taskDto);
        Task DeleteTaskAsync(Guid id);
    }
}
