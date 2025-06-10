using SharedKernel.Filters;
using UserService.Application.DTOs;

namespace UserService.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync(UserFilter? userFilter = null);
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task AddUserAsync(UserDto userDto);
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(Guid id);
    }
}
