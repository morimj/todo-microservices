using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Application.Services.UserServices;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await userService.GetAllUsersAsync());
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            return Ok(await userService.GetUserByIdAsync(id));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            try
            {
                await userService.DeleteUserAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            try
            {
                await userService.UpdateUserAsync(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Model!");

            try
            {
                await userService.AddUserAsync(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created();
        }
    }
}
