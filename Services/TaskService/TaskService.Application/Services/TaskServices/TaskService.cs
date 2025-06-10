using SharedKernel.Filters;
using TaskService.Application.DTOs;
using TaskService.Domain.Interfaces;
using TaskService.Domain.Entities;
using AutoMapper;

namespace TaskService.Application.Services.TaskServices
{
    public class TaskService(ITaskRepository taskRepository, IMapper mapper) : ITaskService
    {
        public async Task<Guid> AddTaskAsync(TodoTaskDto taskDto)
        {
            var task = await taskRepository.AddAsync(new TodoTask(taskDto.TaskTitle, taskDto.UserId));

            return task!.Id;
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            await taskRepository.DeleteAsync(id);
        }

        public async Task<List<TodoTaskDto>?> GetAllTasksAsync(TodoTaskFilter? todoTaskFilter = null)
        {
            var tasks = await taskRepository.GetAllAsync(todoTaskFilter);

            return tasks?.Select(mapper.Map<TodoTaskDto>).ToList();
        }

        public async Task<TodoTaskDto?> GetTaskByIdAsync(Guid id)
        {
            return mapper.Map<TodoTaskDto?>(await taskRepository.GetByIdAsync(id));
        }

        public async Task UpdateTaskAsync(TodoTaskDto taskDto)
        {
            var task = await taskRepository.GetByIdAsync(taskDto.Id);

            if (task == null) return;

            task.SetUpdatedAt(DateTime.UtcNow);
            task.SetTitle(taskDto.TaskTitle);
            task.SetIsCompleted(taskDto.IsCompleted);

            await taskRepository.UpdateAsync(task);
        }
    }
}
