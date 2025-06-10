using System.ComponentModel.DataAnnotations;

namespace TaskService.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        public void SetCreatedAt(DateTime dateTime)
        {
            CreatedAt = dateTime;
        }

        public void SetUpdatedAt(DateTime dateTime)
        {
            UpdatedAt = dateTime;
        }

        public void SetId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.");

            Id = id;
        }
    }
}
