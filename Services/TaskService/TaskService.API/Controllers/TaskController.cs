using Microsoft.AspNetCore.Mvc;
using TaskService.Application.DTOs;
using TaskService.Application.Publishers;
using TaskService.Application.Services.TaskServices;

namespace TaskService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ITaskService taskService, ITaskCreatedPublisher taskCreatedPublisher) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await taskService.GetAllTasksAsync());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddTask(TodoTaskDto todoTaskDto)
        {
            todoTaskDto.UserId = Guid.NewGuid();

            try
            {
                var taskId = await taskService.AddTaskAsync(todoTaskDto);
                await taskCreatedPublisher.PublishAsync(taskId, todoTaskDto.TaskTitle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            return Ok(await taskService.GetTaskByIdAsync(id));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteTaskById(Guid id)
        {
            try
            {
                await taskService.DeleteTaskAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateTask(TodoTaskDto taskDto)
        {
            try
            {
                await taskService.UpdateTaskAsync(taskDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
