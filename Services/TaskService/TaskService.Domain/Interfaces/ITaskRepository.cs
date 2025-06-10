using SharedKernel.Filters;
using TaskService.Domain.Entities;

namespace TaskService.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TodoTask>?> GetAllAsync(TodoTaskFilter? todoTaskFilter = null);
        Task<TodoTask?> GetByIdAsync(Guid id);
        Task<TodoTask?> AddAsync(TodoTask task);
        Task UpdateAsync(TodoTask task);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
