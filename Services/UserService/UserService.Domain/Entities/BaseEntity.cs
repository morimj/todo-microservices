using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; internal set; }
        public DateTime CreatedAt { get; internal set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; internal set; }

        public void SetCreatedAt(DateTime dateTime)
        {
            CreatedAt = dateTime;
        }

        public void SetUpdatedAt(DateTime dateTime)
        {
            UpdatedAt = dateTime;
        }
    }
}
