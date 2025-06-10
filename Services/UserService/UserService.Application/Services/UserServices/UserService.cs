using AutoMapper;
using SharedKernel.Filters;
using UserService.Application.DTOs;
using UserService.Domain.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Services.UserServices
{
    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
    {
        public async Task AddUserAsync(UserDto userDto)
        {
            var user = new User(userDto.FirstName, userDto.LastName, userDto.Mobile, userDto.Email);

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await userRepository.DeleteAsync(id);
            await userRepository.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsersAsync(UserFilter? userFilter = null)
        {
            return (await userRepository.GetAllAsync(userFilter))
                .Select(mapper.Map<UserDto>).ToList();
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);

            return mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = await userRepository.GetByIdAsync(userDto.Id);

            if (user == null)
                return;

            user.SetEmail(userDto.Email);
            user.SetFirstName(userDto.FirstName);
            user.SetLastName(userDto.LastName);
            user.SetMobile(userDto.Mobile);
            user.SetUpdatedAt(DateTime.UtcNow);

            userRepository.Update(user);
            await userRepository.SaveChangesAsync();
        }
    }
}
