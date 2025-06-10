using System.ComponentModel.DataAnnotations;

namespace TaskService.Application.DTOs
{
    public class TodoTaskDto : BaseDto
    {
        [MaxLength(150)]
        public string TaskTitle { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }
    }
}
