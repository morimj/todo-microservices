using System.ComponentModel.DataAnnotations;

namespace UserService.Application.DTOs
{
    public class UserDto : BaseDto
    {
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(40)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string? Mobile { get; set; }

        [MaxLength(40)]
        public string? Email { get; set; }
    }
}
