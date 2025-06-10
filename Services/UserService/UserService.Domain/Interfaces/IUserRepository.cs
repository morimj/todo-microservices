using SharedKernel.Filters;
using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(UserFilter? userFilter = null);
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
        void Update(User user);
    }
}
